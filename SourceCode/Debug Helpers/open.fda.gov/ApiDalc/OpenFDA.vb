#Region " Imports "

Imports System.Security.Policy
Imports ApiDalc.DataObjects
Imports ApiDalc.Enumerations
Imports Newtonsoft.Json.Linq ' OpenSource
Imports System.Text.RegularExpressions

#End Region

''' <summary>
''' Open FDA
''' </summary>
''' <remarks></remarks>
Public Class OpenFda

#Region " Public Properties "

    Public Shared Property HostUrl As String = "https://api.fda.gov/"

#End Region

#Region " Member Variables "

    Private _search As String
    Private _count As String
    Private _limit As Integer = 0
    Private _meta As JObject
    Private _results As Object 'JObject
    Private _keyWords As New HashSet(Of String)
    Private _endPointType As OpenFdaApiEndPoints
    Private _restClient As IRestClient

#End Region

#Region " Constructors "

    Public Sub New()
        _restClient = New RestClient
    End Sub

    Public Sub New(restClient As IRestClient)
        _restClient = restClient
    End Sub

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Builds the url string for the endpoint, using the 
    ''' </summary>
    ''' <param name="endPointType"></param>
    ''' <param name="limit"></param>
    ''' <param name="ongoingOnly"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BuildUrl(ByVal endPointType As OpenFdaApiEndPoints, Optional ByVal limit As Integer = 0, Optional ByVal ongoingOnly As Boolean = True) As String

        Dim uri As Uri
        Dim sb As New System.Text.StringBuilder
        Dim hostUrl As String = GetOpenFdaEndPoint(endPointType)

        sb.Append(hostUrl)

        If Not String.IsNullOrEmpty(_search) Then

            sb.Append("&search=")
            sb.Append(_search)

            If ongoingOnly Then

                If Not String.IsNullOrEmpty(_search) Then
                    sb.Append("+AND")
                End If

                sb.Append("+status=ongoing")

            End If

        End If

        If Not String.IsNullOrEmpty(_count) Then

            sb.Append("&count=")
            sb.Append(_count)

        Else

            ' NOTE: if count is passed then Limit does not do anything
            ' so if Count then Limit is ignored/does nothing
            _limit = limit

            If _limit < 0 Then
                _limit = 0
            ElseIf _limit > 100 Then
                _limit = 100
            End If

            If _limit > 1 Then

                sb.Append("&limit=")
                sb.Append(_limit)

            End If

        End If

        uri = New Uri(sb.ToString)

        Return uri.ToString

    End Function

    Public Function ExecuteExact(url As String) As String

        Dim result As String = String.Empty

        result = Execute(url)

        Return result

    End Function

    Public Function Execute(ByVal url As String) As String

        Dim result As String = _restClient.Execute(url)

        _meta = New JObject()

        If Not String.IsNullOrEmpty(result) Then

            Dim jo As JObject = JObject.Parse(result)

            _meta = CType(jo.GetValue("meta"), JObject) ' If the property doesn't exist, it returns null 
            _results = jo("results")    ' If the property doesn't exist, it returns null 

        End If

        Return result

    End Function

    Public Sub AddCountField(ByVal field As String)
        _count = field
    End Sub

    Public Function GetDeviceEventsByDescriptionCount(ByVal keyword As String) As Integer

        'Dim tmpAdverseDrugEventtList As List(Of AdverseDrugEvent)
        Dim endPointType As OpenFdaApiEndPoints = OpenFdaApiEndPoints.DeviceEvent
        Dim dataSetSize As Integer = 0
        Dim yearCheck As Integer = -1

        'Limit first query to a 1 year window
        Dim beginDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1))
        Dim endDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck))

        ResetSearch()
        AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, New List(Of String)({keyword}), FilterCompairType.And)
        AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)
        'Dim limit As String = AddResultLimit(100)

        Dim apiUrl As String = BuildUrl(endPointType)
        Dim searchResults As String = Execute(apiUrl)
        dataSetSize = GetMetaResults().Total()

        Do

            ' If there was not data in the 1 yr window the get all results.
            ' Check date range window for results year 2 thur year 5.
            If dataSetSize = 0 Then

                yearCheck -= 1
                endDate = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck))

                ResetSearch()
                AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, New List(Of String)({keyword}), FilterCompairType.And)
                AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)

                apiUrl = BuildUrl(endPointType)

                searchResults = Execute(apiUrl)
                'OpenFdaApiHits += 1

                dataSetSize = GetMetaResults().Total()

            End If

        Loop Until dataSetSize > 0 OrElse Math.Abs(yearCheck) >= 5

        'Search w/o Date range filter
        If String.IsNullOrEmpty(searchResults) Then

            ResetSearch()
            AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, New List(Of String)({keyword}), FilterCompairType.And)

            apiUrl = BuildUrl(endPointType)

            searchResults = Execute(apiUrl)

        End If

        If Not String.IsNullOrEmpty(searchResults) Then
            dataSetSize = GetMetaResults().Total()
        End If

        Return dataSetSize

    End Function

    Public Function GetDeviceEventByDescription(ByVal keyword As String) As List(Of SearchResultDrugEvent)

        Dim deviceEventList As New List(Of AdverseDeviceEvent)
        Dim endPointType As OpenFdaApiEndPoints = OpenFdaApiEndPoints.DeviceEvent
        Dim dataSetSize As Integer = 0
        Dim yearCheck As Integer = -1

        'Limit first query to a 1 year window
        Dim beginDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1))
        Dim endDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck))

        ResetSearch()
        AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, New List(Of String)({keyword}), FilterCompairType.And)
        AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)
        Dim limit As String = AddResultLimit(100)

        Dim apiUrl As String = BuildUrl(endPointType)
        Dim searchResults As String = Execute(apiUrl & limit)
        'OpenFdaApiHits += 1

        dataSetSize = GetMetaResults().Total()

        Do

            ' If there was not data in the 1 yr window the get all results.
            ' Check date range window for results year 2 thur year 5.
            If dataSetSize = 0 Then

                yearCheck -= 1
                endDate = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck))

                ResetSearch()
                AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, New List(Of String)({keyword}), FilterCompairType.And)
                AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)

                apiUrl = BuildUrl(endPointType)

                searchResults = Execute(apiUrl & limit)
                'OpenFdaApiHits += 1

                dataSetSize = GetMetaResults().Total()

            End If

        Loop Until dataSetSize > 0 OrElse Math.Abs(yearCheck) >= 5

        'Search w/o Date range filter
        If String.IsNullOrEmpty(searchResults) Then

            ResetSearch()
            AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, New List(Of String)({keyword}), FilterCompairType.And)

            apiUrl = BuildUrl(endPointType)

            searchResults = Execute(apiUrl & limit)
            'OpenFdaApiHits += 1
            dataSetSize = GetMetaResults().Total()

        End If

        If Not String.IsNullOrEmpty(searchResults) Then
            dataSetSize = GetMetaResults().Total()
        End If

        ' LIMIT the number of page request to a MAX of 5
        Dim pageLimit As Integer = CInt(Decimal.Ceiling(CDec(dataSetSize / 100)))
        If pageLimit > 5 Then
            pageLimit = 5
        End If

        Dim skipValue As Integer = 0
        If dataSetSize > 0 Then

            Do
                pageLimit -= 1

                If Not String.IsNullOrEmpty(searchResults) Then

                    Dim result As List(Of AdverseDeviceEvent) = AdverseDeviceEvent.CnvJsonDataToList(searchResults)
                    deviceEventList.AddRange(result)

                End If

                If pageLimit > 0 Then

                    skipValue += 100
                    'Dim newApiUrl As String = apiUrl.Replace("&limit=100", String.Format("&limit=100&skip={0}", skipValue))
                    limit = String.Format("&limit=100&skip={0}", skipValue)
                    searchResults = Execute(apiUrl & limit)
                    ' OpenFdaApiHits += 1

                End If

            Loop Until pageLimit = 0

        End If

        'Dim deviceEventList As List(Of AdverseDeviceEvent) = AdverseDeviceEvent.CnvJsonDataToList(searchResults)
        'AdverseDeviceEvent.CnvJsonDataToList(results)

        'Dim tmpAdverseDeviceEventList As List(Of AdverseDeviceEvent)
        Dim tmpSearchResultDeviceEvent As List(Of SearchResultDrugEvent) = AdverseDeviceEvent.CnvDeviceEventsToResultDrugEvents(deviceEventList)

        'Dim sortedResult = (From el In tmpSearchResultDeviceEvent Select el Order By el.ReportDate Descending).ToList()

        Return tmpSearchResultDeviceEvent

    End Function

    Public Function GetDrugEventsByDrugNameCount(ByVal drugName As String) As Integer

        'Dim tmpAdverseDrugEventtList As List(Of AdverseDrugEvent)
        Dim endPointType As OpenFdaApiEndPoints = OpenFdaApiEndPoints.DrugEvent
        Dim dataSetSize As Integer = 0
        Dim yearCheck As Integer = -1

        Dim apiUrl As String
        Dim searchResults As String

        'Limit first query to a 1 year window
        Dim beginDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1))
        Dim endDate As String = String.Empty

        ResetSearch()
        AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}), FilterCompairType.And)
        AddCountField("receivedate")

        apiUrl = BuildUrl(endPointType)
        searchResults = Execute(apiUrl)

        If Not String.IsNullOrEmpty(searchResults) Then

            Dim jo As JObject = JObject.Parse(searchResults)

            Dim maxEventDate As Date = (From el In jo("results") Select ConvertDateStringToDate(el("time").ToString, "yyyyMMdd")).Max()

            If Not maxEventDate = Nothing Then
                endDate = String.Format("{0:yyyyMMdd}", maxEventDate.AddYears(yearCheck))
            End If

        End If

        If String.IsNullOrEmpty(endDate) Then
            Return 0
        End If

        ResetSearch()
        AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}), FilterCompairType.And)
        AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)
        'Dim limit As String = AddResultLimit(100)

        apiUrl = BuildUrl(endPointType)
        searchResults = Execute(apiUrl)
        dataSetSize = GetMetaResults().Total()

        Do

            ' If there was not data in the 1 yr window the get all results.
            ' Check date range window for results year 2 thur year 5.
            If dataSetSize = 0 Then

                yearCheck -= 1
                endDate = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck))

                ResetSearch()
                AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}), FilterCompairType.And)
                AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)

                apiUrl = BuildUrl(endPointType)

                searchResults = Execute(apiUrl)
                'OpenFdaApiHits += 1

                dataSetSize = GetMetaResults().Total()

            End If

        Loop Until dataSetSize > 0 OrElse Math.Abs(yearCheck) >= 5

        'Search w/o Date range filter
        If String.IsNullOrEmpty(searchResults) Then

            ResetSearch()
            AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}), FilterCompairType.And)

            apiUrl = BuildUrl(endPointType)

            searchResults = Execute(apiUrl)

        End If

        If Not String.IsNullOrEmpty(searchResults) Then
            dataSetSize = GetMetaResults().Total()
        End If

        Return dataSetSize

    End Function

    Public Function GetDrugEventsByDrugName(ByVal drugName As String) As List(Of SearchResultDrugEvent)

        Dim drugEventList As New List(Of AdverseDrugEvent)
        Dim tmpSearchResultDrugEvent As New List(Of SearchResultDrugEvent)
        Dim endPointType As OpenFdaApiEndPoints = OpenFdaApiEndPoints.DrugEvent
        Dim dataSetSize As Integer = 0
        Dim yearCheck As Integer = -1

        Dim apiUrl As String
        Dim searchResults As String
        Dim limit As String

        'Limit first query to a 1 year window
        Dim beginDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1))
        Dim endDate As String = String.Empty

        ResetSearch()
        AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}), FilterCompairType.And)
        AddCountField("receivedate")

        apiUrl = BuildUrl(endPointType)
        searchResults = Execute(apiUrl)

        If Not String.IsNullOrEmpty(searchResults) Then

            Dim jo As JObject = JObject.Parse(searchResults)

            Dim maxEventDate As Date = (From el In jo("results") Select ConvertDateStringToDate(el("time").ToString, "yyyyMMdd")).Max()

            If Not maxEventDate = Nothing Then
                endDate = String.Format("{0:yyyyMMdd}", maxEventDate.AddYears(yearCheck))
            End If

        End If

        If String.IsNullOrEmpty(endDate) Then
            Return tmpSearchResultDrugEvent
        End If

        ResetSearch()
        AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}), FilterCompairType.And)
        AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)

        limit = AddResultLimit(100)

        apiUrl = BuildUrl(endPointType)
        searchResults = Execute(apiUrl & limit)
        'OpenFdaApiHits += 1

        dataSetSize = GetMetaResults().Total()

        Do

            ' If there was not data in the 1 yr window the get all results.
            ' Check date range window for results year 2 thur year 5.
            If dataSetSize = 0 Then

                yearCheck -= 1
                endDate = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck))

                ResetSearch()
                AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}), FilterCompairType.And)
                AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)

                apiUrl = BuildUrl(endPointType)

                searchResults = Execute(apiUrl & limit)
                'OpenFdaApiHits += 1

                dataSetSize = GetMetaResults().Total()

            End If

        Loop Until dataSetSize > 0 OrElse Math.Abs(yearCheck) >= 5

        'Search w/o Date range filter
        If String.IsNullOrEmpty(searchResults) Then

            ResetSearch()
            AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}), FilterCompairType.And)

            apiUrl = BuildUrl(endPointType)

            searchResults = Execute(apiUrl & limit)
            'OpenFdaApiHits += 1
            dataSetSize = GetMetaResults().Total()

        End If

        If Not String.IsNullOrEmpty(searchResults) Then
            dataSetSize = GetMetaResults().Total()
        End If

        ' LIMIT the number of page request to a MAX of 5
        Dim pageLimit As Integer = CInt(Decimal.Ceiling(CDec(dataSetSize / 100)))
        If pageLimit > 5 Then
            pageLimit = 5
        End If

        Dim skipValue As Integer = 0
        If dataSetSize > 0 Then

            Do
                pageLimit -= 1

                If Not String.IsNullOrEmpty(searchResults) Then

                    Dim result As List(Of AdverseDrugEvent) = AdverseDrugEvent.CnvJsonDataToList(searchResults)
                    drugEventList.AddRange(result)

                End If

                If pageLimit > 0 Then

                    skipValue += 100
                    'Dim newApiUrl As String = apiUrl.Replace("&limit=100", String.Format("&limit=100&skip={0}", skipValue))
                    limit = String.Format("&limit=100&skip={0}", skipValue)
                    searchResults = Execute(apiUrl & limit)
                    ' OpenFdaApiHits += 1

                End If

            Loop Until pageLimit = 0

        End If

        tmpSearchResultDrugEvent = SearchResultDrugEvent.ConvertJsonData(drugEventList)

        'Dim sortedResult = (From el In tmpSearchResultDrugEvent Select el Order By el.ReportDate Descending).ToList()

        Return tmpSearchResultDrugEvent

    End Function

    Public Sub SearchOnFieldByValue(searchField As String, searchFieldValue As String)

        searchFieldValue = RemoveSpecialCharactersFromKeyword(searchFieldValue)
        searchFieldValue = searchFieldValue.Replace(" ", "+")

        If searchFieldValue.Contains("+") Then
            searchFieldValue = """" & searchFieldValue & """"
        End If

        _search = String.Format("{0}:{1}", searchField, searchFieldValue)

    End Sub

    Public Sub SearchFieldExists(searchField As String)
        SearchOnFieldByValue("_exists_", searchField)
    End Sub

    Public Sub AddSearchFilter(endPointType As OpenFdaApiEndPoints, endpointField As String, keyWord As String, operationCompairType As FilterCompairType)

        keyWord = RemoveSpecialCharactersFromKeyword(keyWord)
        keyWord = keyWord.Replace(" ", "+")

        If keyWord.Contains("+") Then
            keyWord = """" & keyWord & """"
        End If

        Dim param As String = String.Format("{0}:({1})", endpointField, keyWord)

        If Not String.IsNullOrEmpty(_search) Then

            If operationCompairType = FilterCompairType.Or Then
                _search += "+"
            Else
                _search += "+AND+"
            End If

        End If

        _search += param

    End Sub

    Public Function AddSearchFilter(ByVal endpointType As OpenFdaApiEndPoints, ByVal type As FdaFilterTypes, ByVal filters As List(Of String), Optional ByVal operationCompairType As FilterCompairType = FilterCompairType.Or) As String

        ' Add Filter to KeyWord List
        Dim keyword As String = String.Empty
        Dim keywordToRemove As String() = {"null", "all"}

        For indx As Integer = 0 To filters.Count - 1

            filters(indx) = RemoveSpecialCharactersFromKeyword(filters(indx))

            If keywordToRemove.Contains(filters(indx).ToLower) Then
                filters(indx) = String.Empty
            End If

        Next

        Dim tmpFilters As List(Of String) = (From el In filters Where el.Length > 0 Select el).ToList()

        If Not tmpFilters.Count = filters.Count Then

            filters.Clear()
            filters.AddRange(tmpFilters)

        End If

        If filters.Count = 0 Then
            Return String.Empty
        End If

        For Each itm In filters
            keyword += itm.ToLower & ","
        Next

        If Not String.IsNullOrEmpty(keyword) Then
            keyword = keyword.Substring(0, keyword.Length - 1)
        End If

        If Not _keyWords.Contains(keyword) Then
            _keyWords.Add(keyword)
        End If

        Dim param As String = String.Empty

        Dim tmp As String = String.Empty

        Select Case type

            Case FdaFilterTypes.Date

                If filters.Count = 1 Then

                    'Dim tmpDate As DateTime = ConvertDateStringToDate(filters(0), "yyyyMMdd")
                    Dim tmpDate As DateTime = DateTime.ParseExact(filters(0), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

                    tmp = String.Format("{0:yyyyMMdd}", tmpDate) 'tmpDate.ToString("yyyyMMdd")

                Else

                    Dim minDate As Nullable(Of DateTime) = Nothing
                    Dim maxDate As Nullable(Of DateTime) = Nothing

                    For Each itm In filters

                        'Dim itmDate As DateTime = ConvertDateStringToDate(itm, "yyyyMMdd")
                        Dim itmDate As DateTime = DateTime.ParseExact(itm, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

                        If itmDate < minDate Or minDate Is Nothing Then
                            minDate = itmDate
                        End If

                        If itmDate > maxDate Or maxDate Is Nothing Then
                            maxDate = itmDate
                        End If

                    Next

                    Dim dtMin As String = String.Format("{0:yyyyMMdd}", minDate) 'minDate.ToString("yyyyMMdd")
                    Dim dtMax As String = String.Format("{0:yyyyMMdd}", maxDate) ' maxDate.ToString("yyyyMMdd")

                    tmp = String.Format("[{0}+TO+{1}]", dtMin, dtMax)

                End If

            Case Else

                Dim tmpItm As String

                For Each itm In filters

                    tmpItm = itm.Replace(" ", "+")

                    If tmpItm.Contains("+") Then
                        tmpItm = """" & tmpItm & """"
                    End If

                    tmp += tmpItm & "+"

                Next

                If Not String.IsNullOrEmpty(tmp) Then
                    tmp = tmp.Substring(0, tmp.Length - 1)
                End If

        End Select

        Select Case endpointType

            Case OpenFdaApiEndPoints.DrugEvent

                Select Case type

                    Case FdaFilterTypes.Date
                        param = "("
                        param += "receivedate:" & tmp & ""
                        param += ")"

                    Case FdaFilterTypes.DrugEventDrugName

                        param = "(patient.drug.openfda.substance_name:" & tmp
                        param += "+"
                        param += "patient.drug.openfda.brand_name:" & tmp
                        param += "+"
                        param += "patient.drug.openfda.generic_name:" & tmp
                        param += "+"
                        param += "patient.drug.medicinalproduct:" & tmp & ")"

                End Select

            Case OpenFdaApiEndPoints.DeviceRecall, OpenFdaApiEndPoints.DrugRecall, OpenFdaApiEndPoints.FoodRecall

                Select Case type

                    Case FdaFilterTypes.Region

                        Dim tmpEnum As Enumerations.States = CType([Enum].Parse(GetType(Enumerations.States), tmp), Enumerations.States)
                        param = "(state:(" & tmp & ")"
                        param += "+"
                        'param += "distribution_pattern:(Nationwide+" & tmp & "))" ' TODO:  Need the State NAME + GetEnumDescription(tmpEnum)
                        param += String.Format("distribution_pattern:(Nationwide+{0}+{1}))", tmp, GetEnumDescription(tmpEnum)) ' TODO:  Need the State NAME + GetEnumDescription(tmpEnum)

                    Case FdaFilterTypes.RecallReason

                        Dim keywordList As String() = tmp.Replace("""", String.Empty).Split(CChar("+"))

                        param = "(("
                        For Each itm In keywordList
                            param += String.Format("reason_for_recall:{0}+AND+", itm)
                        Next
                        'Remove the Ending +AND+
                        param = param.Substring(0, param.Length - 5)
                        param += ")+("
                        For Each itm In keywordList
                            param += String.Format("product_description:{0}+AND+", itm)
                        Next
                        'Remove the Ending +AND+
                        param = param.Substring(0, param.Length - 5)
                        param += "))"

                    Case FdaFilterTypes.Date
                        param = "("
                        param += "report_date:" & tmp & ""
                        param += "+"
                        param += "recall_initiation_date:" & tmp & ""
                        param += ")"

                End Select

            Case OpenFdaApiEndPoints.DeviceEvent

                Select Case type

                    Case FdaFilterTypes.Date
                        param = "("
                        param += "date_of_event:" & tmp & ""
                        param += ")"

                    Case FdaFilterTypes.DeviceEventDescription
                        param = "(device.brand_name:" & tmp
                        param += "+"
                        param += "device.generic_name:" & tmp
                        param += "+"
                        param += "mdr_text.text:" & tmp & ")"

                End Select

            Case OpenFdaApiEndPoints.DrugLabel
                'TBD

            Case Else
                ' do nothing

        End Select

        If Not String.IsNullOrEmpty(param) Then

            If Not String.IsNullOrEmpty(_search) Then

                If operationCompairType = FilterCompairType.Or Then
                    _search += "+"
                Else
                    _search += "+AND+"
                End If

            End If

            _search += param

        End If

        Return param

    End Function

#End Region

#Region " Friend Methods "

    Friend Function GetOpenFdaEndPoint(endpoint As OpenFdaApiEndPoints) As String

        _endPointType = endpoint

        Dim result As String = String.Empty
        Dim endPT As String = GetEnumDefaultValue(endpoint)

        result = AddForwardSlash(HostUrl) & endPT & ".json?"

        Return result

    End Function

    Friend Function GetMetaResults() As MetaResults

        Dim metaData As New MetaResults

        If _meta IsNot Nothing Then
            metaData = GetMetaResults(_meta)
        End If

        Return metaData

    End Function

    Friend Function GetMetaResults(ByVal searchResults As String) As MetaResults

        Dim metaData As New MetaResults

        If Not String.IsNullOrEmpty(searchResults) Then

            Dim jo As JObject = JObject.Parse(searchResults)
            Dim meta As JObject = CType(jo.GetValue("meta"), JObject)

            metaData = GetMetaResults(meta)

        End If

        Return metaData

    End Function

    Friend Function GetMetaResults(ByVal meta As JObject) As MetaResults

        Dim metaData As New MetaResults

        If meta("results") IsNot Nothing Then

            With metaData

                .Limit = CInt(meta("results")("limit"))
                .Skip = CInt(meta("results")("skip"))
                .Total = CInt(meta("results")("total"))

            End With

        End If

        Return metaData

    End Function

    Friend Sub ResetSearch()

        _search = String.Empty
        '_limit = String.Empty
        _count = String.Empty

    End Sub

    ''' <summary>
    ''' Gets States list
    ''' </summary>
    ''' <returns>List of StateData</returns>
    ''' <remarks></remarks>
    Friend Function GetStates() As List(Of StateData)

        Dim result As New List(Of StateData)
        Dim stateArray As Array
        Dim tmpEnumValue As States

        stateArray = System.Enum.GetValues(GetType(States))

        For Each itm In stateArray

            tmpEnumValue = DirectCast([Enum].Parse(GetType(States), CStr(itm)), States)

            Dim state As New StateData With {.name = GetEnumDescription(tmpEnumValue), .abbreviation = tmpEnumValue.ToString}

            result.Add(state)

        Next

        Return result

    End Function

    Friend Function AddResultLimit(ByVal limit As Integer) As String

        Dim parm As String = String.Empty

        Select Case limit

            Case Is <= 0
                limit = 0

            Case Is > 100
                limit = 100

            Case Else
                'limit = limit

        End Select

        parm = String.Format("&limit={0}", limit)

        Return parm

    End Function

#End Region

#Region " Private Methods "

    Private Function RemoveSpecialCharactersFromKeyword(ByVal keyword As String) As String

        Dim tmpitm As String = Regex.Replace(keyword, "\s+", " ")
        tmpitm = Regex.Replace(tmpitm, "[\^\[\-\$\{\*\(\\\+\)\|\?\<\>!@#%&;]", " ")
        keyword = Regex.Replace(tmpitm, " {2,}", " ")

        Return keyword

    End Function

#End Region

End Class