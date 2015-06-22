

#Region " Imports "

Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Public Class WrapperOpenFDA

    Private _fda As OpenFDA

    Public Function GetRecallsSummary(ByVal keyWordList As List(Of String)) As List(Of RecallSearchResultData)

        Dim results As List(Of RecallSearchResultData)

        'Dim results As New List(Of RecallSearchResultData)

        '_fda = New OpenFDA
        'Dim filterType As FDAFilterTypes = FDAFilterTypes.RecallReason
        'Dim maxresultsize As Integer = 0

        'Dim resultCount As Integer
        'Dim RecallResultList As List(Of ResultRecall)
        'For Each kwGroup In keyWordList

        '    Dim filterList As New List(Of String)
        '    Dim xx As String() = kwGroup.Split(",")
        '    For Each itm In xx
        '        filterList.Add(itm)
        '    Next


        '    ''Dim maxResultSize As Integer = 25
        '    'Dim endPointType As OpenFDAApiEndPoints
        '    'Dim apiUrl As String = String.Empty
        '    'Dim searchResults As String
        '    'Dim srMetaData As MetaResults

        '    RecallResultList = New List(Of ResultRecall)
        '    resultCount = ExecuteSearch(OpenFDAApiEndPoints.FoodRecall, filterType, filterList, maxresultsize, RecallResultList)
        '    'Dim recallData As RecallSearchResultData = ExecuteSearch(OpenFDAApiEndPoints.FoodRecall, filterType, filterList, maxresultsize)

        '    For Each itm As ResultRecall In RecallResultList

        '        itm.KeyWord = kwGroup
        '        'masterRecallResultList.Add(itm)
        '        Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
        '                                                           .Type = itm.product_type,
        '                                                           .Count = resultCount,
        '                                                           .Description_1 = itm.product_description,
        '                                                           .Description_2 = itm.reason_for_recall}

        '        recallData_AddPropertyInfo(recallData, itm)

        '        'If itm.distribution_pattern.ToLower.Contains("nationwide") Then
        '        '    recallData.isNationWide = True
        '        'End If

        '        'If Not String.IsNullOrEmpty(itm.state) Then
        '        '    recallData.Regions.Add(itm.state)
        '        'End If

        '        'Dim items As Array
        '        'items = System.Enum.GetValues(GetType(enumStates))
        '        ''Dim item As String
        '        'Dim tmpState As enumStates
        '        'For Each item In items

        '        '    tmpState = DirectCast([Enum].Parse(GetType(enumStates), item), enumStates)
        '        '    If itm.distribution_pattern.Contains(tmpState.ToString) OrElse
        '        '        itm.distribution_pattern.Contains(GetEnumDescription(tmpState)) OrElse
        '        '        recallData.isNationWide Then

        '        '        recallData.Regions.Add(tmpState.ToString)

        '        '    End If

        '        'Next

        '        results.Add(recallData)

        '    Next

        '    RecallResultList = New List(Of ResultRecall)
        '    resultCount = ExecuteSearch(OpenFDAApiEndPoints.DrugRecall, filterType, filterList, maxresultsize, RecallResultList)

        '    For Each itm As ResultRecall In RecallResultList

        '        itm.KeyWord = kwGroup
        '        'masterRecallResultList.Add(itm)
        '        Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
        '                                                           .Type = itm.product_type,
        '                                                           .Count = resultCount,
        '                                                           .Description_1 = itm.product_description,
        '                                                           .Description_2 = itm.reason_for_recall}

        '        recallData_AddPropertyInfo(recallData, itm)

        '        'If itm.distribution_pattern.ToLower.Contains("nationwide") Then
        '        '    recallData.isNationWide = True
        '        'End If

        '        'If Not String.IsNullOrEmpty(itm.state) Then
        '        '    recallData.Regions.Add(itm.state)
        '        'End If

        '        'Dim items As Array
        '        'items = System.Enum.GetValues(GetType(enumStates))
        '        ''Dim item As String
        '        'Dim tmpState As enumStates
        '        'For Each item In items

        '        '    tmpState = DirectCast([Enum].Parse(GetType(enumStates), item), enumStates)
        '        '    If itm.distribution_pattern.Contains(tmpState.ToString) OrElse
        '        '        itm.distribution_pattern.Contains(GetEnumDescription(tmpState)) OrElse
        '        '        recallData.isNationWide Then

        '        '        recallData.Regions.Add(tmpState.ToString)

        '        '    End If

        '        'Next

        '        results.Add(recallData)

        '    Next

        'Next

        results = GetRecallInfo(keyWordList, 0)

        Return results

    End Function

    Public Function GetRecallsDetail(ByVal keyWord As String) As List(Of RecallSearchResultData)

        Dim results As List(Of RecallSearchResultData)

        'Dim results As New List(Of RecallSearchResultData)

        '_fda = New OpenFDA
        'Dim filterType As FDAFilterTypes = FDAFilterTypes.RecallReason
        'Dim maxresultsize As Integer = 100

        'Dim resultCount As Integer
        'Dim RecallResultList As List(Of ResultRecall)
        'Dim keyWordList As New List(Of String)
        'keyWordList.Add(keyWord)

        'For Each kwGroup In keyWordList

        '    Dim filterList As New List(Of String)
        '    Dim xx As String() = kwGroup.Split(",")
        '    For Each itm In xx
        '        filterList.Add(itm)
        '    Next

        '    RecallResultList = New List(Of ResultRecall)
        '    resultCount = ExecuteSearch(OpenFDAApiEndPoints.FoodRecall, filterType, filterList, maxresultsize, RecallResultList)
        '    'Dim recallData As RecallSearchResultData = ExecuteSearch(OpenFDAApiEndPoints.FoodRecall, filterType, filterList, maxresultsize)

        '    For Each itm As ResultRecall In RecallResultList

        '        itm.KeyWord = kwGroup
        '        'masterRecallResultList.Add(itm)
        '        Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
        '                                                           .Type = itm.product_type,
        '                                                           .Count = resultCount,
        '                                                           .Description_1 = itm.product_description,
        '                                                           .Description_2 = itm.reason_for_recall}

        '        recallData_AddPropertyInfo(recallData, itm)


        '        results.Add(recallData)

        '    Next

        '    RecallResultList = New List(Of ResultRecall)
        '    resultCount = ExecuteSearch(OpenFDAApiEndPoints.DrugRecall, filterType, filterList, maxresultsize, RecallResultList)

        '    For Each itm As ResultRecall In RecallResultList

        '        itm.KeyWord = kwGroup
        '        'masterRecallResultList.Add(itm)
        '        Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
        '                                                           .Type = itm.product_type,
        '                                                           .Count = resultCount,
        '                                                           .Description_1 = itm.product_description,
        '                                                           .Description_2 = itm.reason_for_recall}

        '        recallData_AddPropertyInfo(recallData, itm)

        '        results.Add(recallData)

        '    Next

        'Next

        Dim keyWordList As New List(Of String)
        keyWordList.Add(keyWord)

        results = GetRecallInfo(keyWordList, 100)

        Return results

    End Function

    Private Function GetRecallInfo(ByVal keyWordList As List(Of String), ByVal maxresultsize As Integer) As List(Of RecallSearchResultData)

        Dim results As New List(Of RecallSearchResultData)

        _fda = New OpenFDA
        Dim filterType As FDAFilterTypes = FDAFilterTypes.RecallReason

        Dim resultCount As Integer
        Dim RecallResultList As List(Of ResultRecall)
        For Each kwGroup In keyWordList

            Dim filterList As New List(Of String)
            Dim xx As String() = kwGroup.Split(",")
            For Each itm In xx
                filterList.Add(itm)
            Next

            Dim endPointList As New List(Of OpenFDAApiEndPoints)({OpenFDAApiEndPoints.FoodRecall, OpenFDAApiEndPoints.DrugRecall})

            For Each endPoint In endPointList

                RecallResultList = New List(Of ResultRecall)
                resultCount = ExecuteSearch(endPoint, filterType, filterList, maxresultsize, RecallResultList)

                For Each itm As ResultRecall In RecallResultList

                    Dim itmClassification As enumClassification
                    Select Case itm.classification
                        Case "Class I"
                            itmClassification = enumClassification.Class_I
                        Case "Class II"
                            itmClassification = enumClassification.Class_II
                        Case "Class III"
                            itmClassification = enumClassification.Class_III
                    End Select

                    itm.KeyWord = kwGroup

                    Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
                                                                       .Type = itm.product_type,
                                                                       .Count = resultCount,
                                                                       .Classification = String.Format("{0}  -  {1}", itm.classification, GetEnumDescription(itmClassification)),
                                                                       .Description_1 = itm.product_description,
                                                                       .Description_2 = itm.reason_for_recall}

                    recallData_AddPropertyInfo(recallData, itm)

                    results.Add(recallData)

                Next

            Next

            'RecallResultList = New List(Of ResultRecall)
            'resultCount = ExecuteSearch(OpenFDAApiEndPoints.FoodRecall, filterType, filterList, maxresultsize, RecallResultList)

            'For Each itm As ResultRecall In RecallResultList

            '    itm.KeyWord = kwGroup
            '    Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
            '                                                       .Type = itm.product_type,
            '                                                       .Count = resultCount,
            '                                                       .Description_1 = itm.product_description,
            '                                                       .Description_2 = itm.reason_for_recall}

            '    recallData_AddPropertyInfo(recallData, itm)

            '    results.Add(recallData)

            'Next

            'RecallResultList = New List(Of ResultRecall)
            'resultCount = ExecuteSearch(OpenFDAApiEndPoints.DrugRecall, filterType, filterList, maxresultsize, RecallResultList)

            'For Each itm As ResultRecall In RecallResultList

            '    itm.KeyWord = kwGroup
            '    Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
            '                                                       .Type = itm.product_type,
            '                                                       .Count = resultCount,
            '                                                       .Description_1 = itm.product_description,
            '                                                       .Description_2 = itm.reason_for_recall}

            '    recallData_AddPropertyInfo(recallData, itm)

            '    results.Add(recallData)

            'Next

        Next

        Return results

    End Function

    Private Function ExecuteSearch(endPointType As OpenFDAApiEndPoints, filterType As FDAFilterTypes, filterList As List(Of String), ByVal maxresultsize As Integer, ByRef RecallResultList As List(Of ResultRecall)) As Integer

        'Dim fda As New OpenFDA
        Dim apiUrl As String = String.Empty
        Dim searchResults As String
        Dim srMetaData As MetaResults
        Dim tmpRecallResultList As New List(Of ResultRecall)

        _fda.AddSearchFilter(endPointType, filterType, filterList)
        apiUrl = _fda.buildUrl(endPointType, maxresultsize)
        searchResults = _fda.Execute(apiUrl)

        srMetaData = MetaResults.cnvJsonData(searchResults)
        If srMetaData.total > 0 Then

            tmpRecallResultList = ResultRecall.cnvJsonDataToList(searchResults)
            If tmpRecallResultList.Count > 0 Then

                For Each itm As ResultRecall In tmpRecallResultList
                    RecallResultList.Add(itm)
                Next

            End If

        End If

        'Return tmpRecallResultList.Count > 0
        Return srMetaData.total

    End Function

    Private Sub recallData_AddPropertyInfo(ByRef recallData As RecallSearchResultData, ByVal itm As ResultRecall)

        If itm.distribution_pattern.ToLower.Contains("nationwide") Then
            recallData.isNationWide = True
        End If

        If Not String.IsNullOrEmpty(itm.state) Then
            recallData.Regions.Add(itm.state)
        End If

        Dim items As Array
        items = System.Enum.GetValues(GetType(enumStates))
        'Dim item As String
        Dim tmpState As enumStates
        For Each item In items

            tmpState = DirectCast([Enum].Parse(GetType(enumStates), item), enumStates)
            If itm.distribution_pattern.Contains(tmpState.ToString) OrElse
                itm.distribution_pattern.Contains(GetEnumDescription(tmpState)) OrElse
                recallData.isNationWide Then

                recallData.Regions.Add(tmpState.ToString)

            End If

        Next

    End Sub

End Class


Public Class OpenFDA

    Public Shared HostURL As String = "https://api.fda.gov/"

    Private _search As String
    Private _count As String
    Private _limit As Integer = 0

    Private _resultSet As String
    Private _meta As JObject
    Private _results As Object 'JObject

    Private _keyWords As New HashSet(Of String)

    Private endPointType As OpenFDAApiEndPoints


    Public Function GetOpenFDAEndPoint(endpoint As OpenFDAApiEndPoints) As String

        endPointType = endpoint

        Dim result As String = String.Empty

        Dim endPT As String = GetEnumDefault(endpoint)
        result = AddFS(HostURL) & endPT & ".json?"

        Return result

    End Function

    Function buildUrl(ByVal endPointType As OpenFDAApiEndPoints, Optional ByVal limit As Integer = 0, Optional ByVal ongoingOnly As Boolean = True) As String

        Dim uri As Uri
        Dim sb As New System.Text.StringBuilder
        'Dim hostUrl As String = GetOpenFDAEndPoint(OpenFDAApiEndPoints.FoodRecall)
        Dim hostUrl As String = GetOpenFDAEndPoint(endPointType)

        'If Not String.IsNullOrEmpty(_search) Then
        '    _search = "&search=" & _search
        'End If

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

        '' NOTE: if count is passed then Limit does not do anything
        '' so if Count then Limit is ignored/does nothing
        '_limit = limit
        'If _limit < 0 Then
        '    _limit = 0
        'ElseIf _limit > 100 Then
        '    _limit = 100
        'End If

        'sb.Append("&limit=")
        'sb.Append(_limit)

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

        'uri = New Uri(res & _search & _limit)
        uri = New Uri(sb.ToString)

        Return uri.ToString

    End Function

    Function ExecuteExact(url As String) As String

        Dim result As String = String.Empty

        Dim webClient = New Net.WebClient()

        webClient.Headers.Clear()

        Try
            result = webClient.DownloadString(url)
            _resultSet = result

        Catch ex As Net.WebException
            Debug.Write(ex.Message)

            'ex.dump("Net.WebException")
            'ex.Message.dump("")
            ''ex.response.statusCode.dump()
            ''ex.response.statusDescription.dump()
            'webClient.dump()

        Catch ex As Exception
            Debug.Write(ex.Message)

        End Try


        If Not String.IsNullOrEmpty(result) Then

            Dim jo As JObject = JObject.Parse(result)
            'jo.dump()
            'jo.Children.Count.dump("children")
            'jo.GetValue("meddta").dump()
            'o.GetValue("meta").dump()
            _meta = jo.GetValue("meta")
            Try
                _results = jo.PropertyValues("results")
            Catch ex As Exception

            End Try
            'jo.Property("meta").dump("prop")


            'Dim tags As List(Of Newtonsoft.Json.Linq.JToken) = jo.SelectToken("meta").Children().ToList
            ' tags.Dump()
            ''for each i as object in jo.Properties("meta")
            ''	i.tostring.dump()
            ''next
            ''for each i in jo.PropertyValues("meta")
            ''	i.dump()
            ''next
            'jo("meta").dump("MetaData")
            'jo("meta")("results")("skip").dump("skip")
            'jo("meta")("results")("limit").dump("limit")
            'jo("meta")("results")("total").dump("total")

        End If

        Return result
    End Function

    Public Function Execute(ByVal url As String) As String

        Dim result As String = String.Empty
        _resultSet = String.Empty
        'Dim res As String = GetOpenFDAEndPoint(OpenFDAApiEndPoints.DrugEvent)
        Dim webClient = New Net.WebClient()

        webClient.Headers.Clear()

        Try
            result = webClient.DownloadString(url)
            _resultSet = result

        Catch ex As Net.WebException
            Debug.Write(ex.Message)

            'ex.dump("Net.WebException")
            'ex.Message.dump("")
            ''ex.response.statusCode.dump()
            ''ex.response.statusDescription.dump()
            'webClient.dump()

        Catch ex As Exception
            Debug.Write(ex.Message)

        End Try


        If Not String.IsNullOrEmpty(result) Then

            Dim jo As JObject = JObject.Parse(result)
            'jo.dump()
            'jo.Children.Count.dump("children")
            'jo.GetValue("meddta").dump()
            'o.GetValue("meta").dump()
            _meta = jo.GetValue("meta")
            Try
                '_results = jo.PropertyValues("results")
                _results = jo.GetValue("results")
            Catch ex As Exception

            End Try
            'jo.Property("meta").dump("prop")


            'Dim tags As List(Of Newtonsoft.Json.Linq.JToken) = jo.SelectToken("meta").Children().ToList
            ' tags.Dump()
            ''for each i as object in jo.Properties("meta")
            ''	i.tostring.dump()
            ''next
            ''for each i in jo.PropertyValues("meta")
            ''	i.dump()
            ''next
            'jo("meta").dump("MetaData")
            'jo("meta")("results")("skip").dump("skip")
            'jo("meta")("results")("limit").dump("limit")
            'jo("meta")("results")("total").dump("total")

        End If

        Return result

    End Function

#Region " Search "

    Public Sub ResetSearch()
        _search = String.Empty
    End Sub

    Sub SearchOnFieldByValue(searchField As String, searchFieldValue As String)

        searchFieldValue = searchFieldValue.Replace(" ", "+")
        If searchFieldValue.Contains("+") Then
            searchFieldValue = """" & searchFieldValue & """"
        End If

        _search = String.Format("{0}:{1}", searchField, searchFieldValue)

    End Sub

    Sub SearchFieldExists(searchField As String)
        SearchOnFieldByValue("_exists_", searchField)
    End Sub


    Public Function AddSearchFilter(ByVal endpointType As OpenFDAApiEndPoints, ByVal type As FDAFilterTypes, ByVal filters As List(Of String)) As String

        ' Add Filter to KeyWord List
        Dim keyword As String = String.Empty
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

        'Dim endpointType As OpenFDAApiEndPoints = OpenFDAApiEndPoints.FoodRecall

        ' desc     -- product_Description, Reason_For_Recall, Code_Info
        ' Location -- Distribution_Pattern & State & Country
        ' Status   -- Status (ongoing, completed, terminated)
        ' Date     -- Report_Date & recall_initation_date
        ' Severity -- Classification

        '??
        ' Recalling_Firm

        Dim tmp As String = String.Empty

        Select Case type
            Case FDAFilterTypes.Date


                If filters.Count = 1 Then

                    Dim tmpDate As DateTime = DateTime.ParseExact(filters(0), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                    tmp = String.Format("{0:yyyyMMdd}", tmpDate) 'tmpDate.ToString("yyyyMMdd")

                Else

                    Dim minDate As Nullable(Of DateTime) = Nothing
                    Dim maxDate As Nullable(Of DateTime) = Nothing

                    For Each itm In filters

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
            Case OpenFDAApiEndPoints.DeviceRecall, OpenFDAApiEndPoints.DrugRecall, OpenFDAApiEndPoints.FoodRecall

                Select Case type

                    Case FDAFilterTypes.Region
                        'param += "country:" & tmp
                        param += "state:(" & tmp & ")"
                        param += "+"
                        param += "distribution_pattern:(Nationwide+" & tmp & ")"

                        'TODO = Have a lookup to convert list of stateCodes to list of StateNames

                    Case FDAFilterTypes.RecallReason
                        param += "reason_for_recall:(" & tmp & ")"
                        param += "+"
                        param += "product_description:(" & tmp & ")"
                        'param += "code_info:" & tmp

                    Case FDAFilterTypes.Date
                        param += "("
                        param += "report_date:" & tmp & ""
                        param += "+"
                        param += "recall_initiation_date:" & tmp & ""
                        param += ")"


                End Select

            Case OpenFDAApiEndPoints.DeviceEvent, OpenFDAApiEndPoints.DeviceEvent
                'TBD

            Case OpenFDAApiEndPoints.DrugLabel
                'TBD

            Case Else
                ' do nothing

        End Select

        'param = param.Substring(0, param.Length - 1)

        If Not String.IsNullOrEmpty(param) Then

            If Not String.IsNullOrEmpty(_search) Then
                _search += "+"
            End If

            _search += param
            '_search += "(" & param & ")"

        End If

        Return param

    End Function

#End Region


#Region " Limits "

    Public Function AddResultLimit(ByVal limit As Integer) As String
        Dim parm As String = String.Empty

        Select Case limit
            Case Is <= 0
                limit = 0
            Case Is > 100
                limit = 100
            Case Else
                'limit = limit
        End Select

        parm = String.Format("&Limit={0}", limit)

        Return parm

    End Function

#End Region

#Region " Count "



#End Region

    Function getMetaResults() As MetaResults
        Dim metaData As New MetaResults

        If _meta IsNot Nothing Then

            If _meta("results") IsNot Nothing Then

                With metaData
                    .limit = _meta("results")("limit")
                    .skip = _meta("results")("skip")
                    .total = _meta("results")("total")
                End With

            End If

        End If

        Return metaData

    End Function


End Class

#Region " Enumerations "

Public Enum enumStates

    <System.ComponentModel.Description("Alabama")>
    AL
    <System.ComponentModel.Description("Alaska")>
    AK
    <System.ComponentModel.Description("Arizona")>
    AZ
    <System.ComponentModel.Description("Arkansas")>
    AR
    <System.ComponentModel.Description("California")>
    CA
    <System.ComponentModel.Description("Colorado")>
    CO
    <System.ComponentModel.Description("Connecticut")>
    CT
    <System.ComponentModel.Description("Delaware")>
    DE
    <System.ComponentModel.Description("Florida")>
    FL
    <System.ComponentModel.Description("Georgia")>
    GA
    <System.ComponentModel.Description("Hawaii")>
    HI
    <System.ComponentModel.Description("Idaho")>
    ID
    <System.ComponentModel.Description("Illinois")>
    IL
    <System.ComponentModel.Description("Indiana")>
    [IN]
    <System.ComponentModel.Description("Iowa")>
    IA
    <System.ComponentModel.Description("Kansas")>
    KS
    <System.ComponentModel.Description("Kentucky")>
    KY
    <System.ComponentModel.Description("Louisiana")>
    LA
    <System.ComponentModel.Description("Maine")>
    [ME]
    <System.ComponentModel.Description("Maryland")>
    MD
    <System.ComponentModel.Description("Massachusetts")>
    MA
    <System.ComponentModel.Description("Michigan")>
    MI
    <System.ComponentModel.Description("Minnesota")>
    MN
    <System.ComponentModel.Description("Mississippi")>
    MS
    <System.ComponentModel.Description("Missouri")>
    MO
    <System.ComponentModel.Description("Montana")>
    MT
    <System.ComponentModel.Description("Nebraska")>
    NE
    <System.ComponentModel.Description("Nevada")>
    NV
    <System.ComponentModel.Description("New Hampshire")>
    NH
    <System.ComponentModel.Description("New Jersey")>
    NJ
    <System.ComponentModel.Description("New Mexico")>
    NM
    <System.ComponentModel.Description("New York")>
    NY
    <System.ComponentModel.Description("North Carolina")>
    NC
    <System.ComponentModel.Description("North Dakota")>
    ND
    <System.ComponentModel.Description("Ohio")>
    OH
    <System.ComponentModel.Description("Oklahoma")>
    OK
    <System.ComponentModel.Description("Oregon")>
    [OR]
    <System.ComponentModel.Description("Pennsylvania")>
    PA
    <System.ComponentModel.Description("Rhode Island")>
    RI
    <System.ComponentModel.Description("South Carolina")>
    SC
    <System.ComponentModel.Description("South Dakota")>
    SD
    <System.ComponentModel.Description("Tennessee")>
    TN
    <System.ComponentModel.Description("Texas")>
    TX
    <System.ComponentModel.Description("Utah")>
    UT
    <System.ComponentModel.Description("Vermont")>
    VT
    <System.ComponentModel.Description("Virginia")>
    VA
    <System.ComponentModel.Description("Washington")>
    WA
    <System.ComponentModel.Description("West Virginia")>
    WV
    <System.ComponentModel.Description("Wisconsin")>
    WI
    <System.ComponentModel.Description("Wyoming")>
    WY

End Enum

Public Enum FDAFilterTypes

    RecallReason
    Classification
    Region
    [Date]

End Enum

Public Enum OpenFDAApiEndPoints

    '<System.ComponentModel.Description("drug/event")>
    '<System.ComponentModel.DisplayNameAttribute("drug/event")>
    <System.ComponentModel.Description("Drug Event")>
    <System.ComponentModel.DefaultValueAttribute("drug/event")>
    DrugEvent

    <System.ComponentModel.Description("Drug Label")>
    <System.ComponentModel.DefaultValueAttribute("drug/label")>
    DrugLabel

    <System.ComponentModel.Description("Drug Recall")>
    <System.ComponentModel.DefaultValueAttribute("drug/enforcement")>
    DrugRecall

    <System.ComponentModel.Description("Device Event")>
    <System.ComponentModel.DefaultValueAttribute("device/event")>
    DeviceEvent

    <System.ComponentModel.Description("Device Recall")>
    <System.ComponentModel.DefaultValueAttribute("device/enforcement")>
    DeviceRecall

    <System.ComponentModel.Description("Food Recall")>
    <System.ComponentModel.DefaultValueAttribute("food/enforcement")>
    FoodRecall

End Enum

'An encoded value for the category of individual submitting the report.
Public Enum enumPrimarySourceQualification

    Physician = 1
    Pharmacist = 2

    <System.ComponentModel.Description("Other Health Professional")>
    OtherHealthProfessional = 3

    Lawyer = 4

    <System.ComponentModel.Description("Consumer or non-health professional")>
    Consumer = 5

End Enum

'sender.sendertype
'string
'The name of the organization sending the report. Because FDA is providing these reports to you, it will always appear as 2.
'1 = Pharmaceutical Company
'2 = Regulatory Authority
'3 = Health Professional
'4 = Regional Pharmacovigilance Center
'5 = WHO Collaborating Center for International Drug Monitoring
'6 = Other


'receiver.receivertype
'string
'The name of the organization receiving the report.
'1 = Pharmaceutical Company
'2 = Regulatory Authority
'3 = Health Professional
'4 = Regional Pharmacovigilance Center
'5 = WHO Collaborating Center for International Drug Monitoring
'6 = Other


'patient.patientonsetageunit
'string
'The unit of measurement for the patient.patientonsetage field.
'800 = Decade
'801 = Year
'802 = Month
'803 = Week
'804 = Day
'805 = Hour

'The sex of the patient.
Public Enum enumPatientSex

    Unknown = 0
    Male = 1
    Female = 2

End Enum
'patient.patientsex
'string
'The sex of the patient.
'0 = Unknown
'1 = Male
'2 = Female


'Actions taken with the drug
Public Enum enumActionDrug

    <System.ComponentModel.Description("Drug Withdrawn")>
        DrugWithdrawn = 1

    <System.ComponentModel.Description("Dose Reduced")>
        DoseReduced = 2

    <System.ComponentModel.Description("Dose Increased")>
        DoseIncreased = 3

    <System.ComponentModel.Description("Dose not changed")>
        DoseNotChanged = 4

    <System.ComponentModel.Description("Unknown")>
        Unknown = 5

    <System.ComponentModel.Description("Not Applicable")>
        NotApplicable = 6

End Enum
'patient.drug.actiondrug
'string
'Actions taken with the drug
'1 = Drug withdrawn
'2 = Dose reduced
'3 = Dose increased
'4 = Dose not changed
'5 = Unknown
'6 = Not applicable

Public Enum enumClassification

    <System.ComponentModel.Description("Dangerous or defective products that predictably could cause serious health problems or death")>
    <System.ComponentModel.DefaultValue("Class I")>
    Class_I
    <System.ComponentModel.Description("Products that might cause a temporary health problem, or pose only a slight threat of a serious nature")>
    <System.ComponentModel.DefaultValue("Class II")>
    Class_II
    <System.ComponentModel.Description("Products that are unlikely to cause any adverse health reaction, but that violate FDA labeling or manufacturing laws")>
    <System.ComponentModel.DefaultValue("Class III")>
    Class_III

End Enum

'The unit for drugcumulativedosagenumb
Public Enum enumDrugCumulativeDosageUnit

    <System.ComponentModel.Description("kg kilogram(s)")>
        kilogram = 1

    <System.ComponentModel.Description("G gram(s)")>
        gram = 2

    <System.ComponentModel.Description("Mg milligram(s)")>
        milligram = 3

    <System.ComponentModel.Description("μg microgram(s)")>
        microgram = 4

End Enum

'patient.drug.drugcumulativedosageunit
'string
'The unit for drugcumulativedosagenumb
'001 = kg kilogram(s)
'002 = G gram(s)
'003 = Mg milligram(s)
'004 = μg microgram(s)


Public Enum enumDrugIntervalDosageDefinition

    Year = 801
    Month = 802
    Week = 803
    Day = 804
    Hour = 805
    Minute = 806
    <System.ComponentModel.Description("Trimester")>
    Trimester_807 = 807
    Cyclical = 810
    <System.ComponentModel.Description("Trimester")>
    Trimester_811 = 811
    <System.ComponentModel.Description("As Necessary")>
        AsNecessary = 812
    Total = 813

End Enum
'patient.drug.drugintervaldosagedefinition
'string
'The unit for the interval in patient.drug.drugintervaldosageunitnumb.
'801 = Year
'802 = Month
'803 = Week
'804 = Day
'805 = Hour
'806 = Minute
'807 = Trimester
'810 = Cyclical
'811 = Trimester
'812 = As Necessary
'813 = Total


'Whether the reaction occured on a readministration of the drug.
Public Enum enumDrugRecurreAdministration

    Yes = 1
    No = 2
    Unknown = 3

End Enum
'patient.drug.drugrecurreadministration
'string
'Whether the reaction occured on a readministration of the drug.
'1 = Yes
'2 = No
'3 = Unknown


'patient.drug.drugstructuredosageunit
'string
'The unit for drugstructuredosagenumb
'001 = kg kilogram(s)
'002 = G gram(s)
'003 = Mg milligram(s)
'004 = μg microgram(s)


'patient.drug.drugadministrationroute
'The drug’s route of administration.
'001 = Auricular (otic)
'002 = Buccal
'003 = Cutaneous
'004 = Dental
'005 = Endocervical
'006 = Endosinusial
'007 = Endotracheal
'008 = Epidural
'009 = Extra-amniotic
'010 = Hemodialysis
'011 = Intra corpus cavernosum
'012 = Intra-amniotic
'013 = Intra-arterial
'014 = Intra-articular
'015 = Intra-uterine
'016 = Intracardiac
'017 = Intracavernous
'018 = Intracerebral
'019 = Intracervical
'020 = Intracisternal
'021 = Intracorneal
'022 = Intracoronary
'023 = Intradermal
'024 = Intradiscal (intraspinal)
'025 = Intrahepatic
'026 = Intralesional
'027 = Intralymphatic
'028 = Intramedullar (bone marrow)
'029 = Intrameningeal
'030 = Intramuscular
'031 = Intraocular
'032 = Intrapericardial
'033 = Intraperitoneal
'034 = Intrapleural
'035 = Intrasynovial
'036 = Intratumor
'037 = Intrathecal
'038 = Intrathoracic
'039 = Intratracheal
'040 = Intravenous bolus
'041 = Intravenous drip
'042 = Intravenous (not otherwise specified)
'043 = Intravesical
'044 = Iontophoresis
'045 = Nasal
'046 = Occlusive dressing technique
'047 = Ophthalmic
'048 = Oral
'049 = Oropharingeal
'050 = Other
'051 = Parenteral
'052 = Periarticular
'053 = Perineural
'054 = Rectal
'055 = Respiratory (inhalation)
'056 = Retrobulbar
'057 = Sunconjunctival
'058 = Subcutaneous
'059 = Subdermal
'060 = Sublingual
'061 = Topical
'062 = Transdermal
'063 = Transmammary
'064 = Transplacental
'065 = Unknown
'066 = Urethral
'067 = Vaginal


'Reported role of the drug in the adverse event.
Public Enum enumDrugCharacterization

    <System.ComponentModel.Description("Suspect drug")>
        SuspectDrug = 1
    <System.ComponentModel.Description("Concomitant drug")>
        ConcomitantDrug = 2
    <System.ComponentModel.Description("Interacting drug")>
        InteractingDrug = 3

End Enum
'patient.drug.drugcharacterization
'Reported role of the drug in the adverse event.
'1 = Suspect drug
'2 = Concomitant drug
'3 = Interacting drug



Public Enum enumDrugTreatmentDurationUnit

    Year = 801
    Month = 802
    Week = 803
    Day = 804
    Hour = 805
    Minute = 806

End Enum
'patient.drug.drugtreatmentdurationunit
'The unit for patient.drug.drugtreatmentduration
'801 = Year
'802 = Month
'803 = Week
'804 = Day
'805 = Hour
'806 = Minute

Public Enum enumReactionOutcome

    <System.ComponentModel.Description("Recovered/resolved")>
    RecoveredResolved = 1
    <System.ComponentModel.Description("Recovering/resolving")>
    RecoveringResolving = 2
    <System.ComponentModel.Description("Not recovered/not resolved")>
    NotRecoveredNotResolved = 3
    <System.ComponentModel.Description("Recovered/resolved with sequelae")>
    RecoveredResolvedWithSequelae = 4
    <System.ComponentModel.Description("Fatal")>
    Fatal = 5
    <System.ComponentModel.Description("Unknown")>
    Unknown = 6

End Enum
'patient.reaction.reactionoutcome
'Outcome of the reaction or event at the time of last observation.
'1 = Recovered/resolved.
'2 = Recovering/resolving.
'3 = Not recovered/not resolved.
'4 = Recovered/resolved with sequelae.
'5 = Fatal.
'6 = Unknown.


#End Region

#Region " Data Objects "

Public Class RecallSearchResultData
    Public Property KeyWord As String
    Public Property Count As Integer
    Public Property Type As String
    Public Property Classification As String
    Public Property Description_1 As String
    Public Property Description_2 As String

    Public Property Regions As New HashSet(Of String)
    Public Property isNationWide As Boolean = False

End Class

Public Class MetaResults

    Public Property limit As Integer = 0
    Public Property skip As Integer = 0
    Public Property total As Integer = 0

    Public Shared Function cnvJsonData(jsondata As String) As MetaResults

        Dim result As MetaResults = Nothing
        If String.IsNullOrEmpty(jsondata) Then
            result = New MetaResults
        Else

            Dim jo As JObject = JObject.Parse(jsondata)
            result = cnvJsonData(jo)

        End If

        Return result

    End Function

    Public Shared Function cnvJsonData(jsondata As JObject) As MetaResults

        Dim metaData As New MetaResults
        Dim obj = jsondata.GetValue("meta")

        If obj("results") IsNot Nothing Then

            With metaData
                .limit = obj("results")("limit")
                .skip = obj("results")("skip")
                .total = obj("results")("total")
            End With

        End If

        Return metaData

    End Function

End Class



Public Class ResultRecall

    Public Property KeyWord As String = String.Empty

    Public Property city As String
    Public Property classification As String
    Public Property code_info As String
    Public Property country As String
    Public Property distribution_pattern As String
    Public Property event_id As String
    Public Property initial_firm_notification As String
    Public Property openfda As Object
    Public Property product_description As String
    Public Property product_quantity As String
    Public Property product_type As String
    Public Property reason_for_recall As String
    Public Property recall_initiation_date As String
    Public Property recall_number As String
    Public Property recalling_firm As String
    Public Property report_date As String
    Public Property state As String
    Public Property status As String
    Public Property voluntary_mandated As String

    Public Shared Function cnvJsonDataToList(jsondata As JObject) As List(Of ResultRecall)

        Dim result As New List(Of ResultRecall)
        For Each obj In jsondata.GetValue("results")
            Dim tmp As New ResultRecall

            tmp.city = obj("city")
            tmp.classification = obj("classification")
            tmp.code_info = obj("code_info")
            tmp.country = obj("country")
            tmp.distribution_pattern = obj("distribution_pattern")
            tmp.event_id = obj("event_id")
            tmp.initial_firm_notification = obj("initial_firm_notification")
            'tmp.openfda = zz("city")
            tmp.product_description = obj("product_description")
            tmp.product_quantity = obj("product_quantity")
            tmp.product_type = obj("product_type")
            tmp.reason_for_recall = obj("reason_for_recall")
            tmp.recall_initiation_date = obj("recall_initiation_date")
            tmp.recall_number = obj("recall_number")
            tmp.recalling_firm = obj("recalling_firm")
            tmp.report_date = obj("report_date")
            tmp.state = obj("state")
            tmp.status = obj("status")
            tmp.voluntary_mandated = obj("voluntary_mandated")

            result.Add(tmp)

        Next

        Return result

    End Function

    Public Shared Function cnvJsonDataToList(jsondata As String) As List(Of ResultRecall)

        Dim jo As JObject = JObject.Parse(jsondata)

        Return cnvJsonDataToList(jo)

    End Function

End Class


Public Class AdverseDrugEvent
    Public Property companynumb As String
    Public Property duplicate As String
    Public Property fulfillexpeditecriteria As String
    Public Property receivedateformat As String
    Public Property receiver As Object
    Public Property reportduplicate As Object
    Public Property seriousnesslifethreatening As String
    Public Property reporttype As String
    Public Property receiptdateformat As String
    Public Property receivedate As String
    Public Property sender As Object
    Public Property patient As PatientData
    Public Property receiptdate As String
    Public Property safetyreportversion As String
    Public Property safetyreportid As String
    Public Property primarysource As Object
    Public Property primarysourcecountry As String
    Public Property transmissiondate As String
    Public Property transmissiondateformat As String
    Public Property occurcountry As String


    Public Property serious As String
    '    serious
    'string
    '1 = The adverse event resulted in death, a life threatening condition, hospitalization, disability, congenital anomali, or other serious condition.
    '2 = The adverse event did not result in any of the above.

    Public Property seriousnessdeath As String ' boolean 1=True - death occured
    'seriousnessdeath
    'string
    'This value is 1 if the adverse event resulted in death, and absent otherwise.

    Public Property seriousnessother As String
    'seriousnessother
    'string
    'This value is 1 if the adverse event resulted in some other serious condition, and absent otherwise.

    Public Property seriousnesshospitalization As String
    'seriousnesshospitalization
    'string
    'This value is 1 if the adverse event resulted in a hospitalization, and absent otherwise.


    Public Property seriousnesscongenitalanomali As String
    'seriousnesscongenitalanomali
    'string
    'This value is 1 if the adverse event resulted in a congenital anomali, and absent otherwise.

    Public Property seriousnessdisabling As String
    'seriousnessdisabling
    'string
    'This value is 1 if the adverse event resulted in disability, and absent otherwise.
    Public Property seriousnseriousnesslifethreateningessother As String
    'seriousnesslifethreatening
    'string
    'This value is 1 if the adverse event resulted in a life threatening condition, and absent otherwise.



    Public Shared Function cnvJsonDataToList(jsondata As JObject) As List(Of AdverseDrugEvent)

        Dim result As New List(Of AdverseDrugEvent)
        For Each obj In jsondata.GetValue("results")
            Dim tmp As New AdverseDrugEvent

            tmp.companynumb = obj("companynumb")
            tmp.safetyreportid = obj("safetyreportid")
            tmp.fulfillexpeditecriteria = obj("fulfillexpeditecriteria")
            tmp.receivedateformat = obj("receivedateformat")
            tmp.receiptdateformat = obj("receiptdateformat")
            tmp.primarysource = obj("primarysource")
            tmp.receivedate = obj("receivedate")
            tmp.occurcountry = obj("occurcountry")


            tmp.serious = obj("serious")
            tmp.seriousnesscongenitalanomali = obj("seriousnesscongenitalanomali")
            tmp.seriousnessdeath = obj("seriousnessdeath")
            tmp.seriousnessdisabling = obj("seriousnessdisabling")
            tmp.seriousnesshospitalization = obj("seriousnesshospitalization")
            tmp.seriousnesslifethreatening = obj("seriousnesslifethreatening")
            tmp.seriousnessother = obj("seriousnessother")

            tmp.safetyreportid = obj("safetyreportid")
            tmp.safetyreportversion = obj("safetyreportversion")


            tmp.patient = PatientData.convertJsonDate(obj("patient"))

            result.Add(tmp)

        Next

        Return result

    End Function

    Public Shared Function cnvJsonDataToList(jsondata As String) As List(Of AdverseDrugEvent)

        Dim jo As JObject = JObject.Parse(jsondata)

        Return cnvJsonDataToList(jo)

    End Function

End Class

Public Class PatientData


    Public Property patientsex As enumPatientSex

    'The age of the patient when the event first occured
    Public Property patientonsetage As String

    'The unit of measurement for the patient.patientonsetage field
    Public Property patientonsetageunit As String

    'The weight of the patient expressed in kilograms.
    Public Property patientweight As String

    'If the patient died, this section contains information about the death.
    'Public Property patientdeath As List(Of String)
    'patient.patientdeath
    'patient.patientdeath.patientdeathdate
    'patient.patientdeath.patientdeathdateformat

    Public Property drug As List(Of DrugData)
    Public Property reaction As List(Of ReactionData)

    Shared Function convertJsonDate(jToken As JToken) As PatientData

        Dim data As New PatientData

        If IsJTokenValid(jToken) Then

            data.patientonsetage = jToken("patientonsetage")
            data.patientonsetageunit = jToken("patientonsetageunit")
            '        patient.patientonsetageunit()
            'string
            'The unit of measurement for the patient.patientonsetage field.
            '800 = Decade
            '801 = Year
            '802 = Month
            '803 = Week
            '804 = Day
            '805 = Hour


            Integer.TryParse(jToken("patientsex"), data.patientsex)

            'data.patientsex = jToken("patientsex")
            '        patient.patientsex()
            'string
            'The sex of the patient.
            '0 = Unknown
            '1 = Male
            '2 = Female

            data.patientweight = jToken("patientweight") ' KiloGrams
            'data.patientdeath = jToken("patientdeath")
            '        patient.patientdeath()
            '        List()
            'If the patient died, this section contains information about the death.
            '            patient.patientdeath.patientdeathdate()
            'string
            'Date that the patient died.
            '            patient.patientdeath.patientdeathdateformat()
            'string
            'Identifies the encoding format of the tient.patientdeath.patientdeathdate field. Always set to 102 (YYYYMMDD).

            data.drug = DrugData.convertJsonData(jToken("drug"))
            data.reaction = ReactionData.convertJsonData(jToken("reaction"))

        End If

        Return data

    End Function


End Class
Public Class DrugData

    Public Property ActionDrug As enumActionDrug
    Public Property DrugCumulativeDosageNumb As String
    Public Property DrugCumulativeDosageUnit As enumDrugCumulativeDosageUnit

    Public Property DrugTreatmentDuration As String
    Public Property DrugTreatmentDurationUnit As enumDrugTreatmentDurationUnit
    Public Property DrugIntervalDosageUnitNumb As String


    Public Property DrugIntervalDosageDefinition As enumDrugIntervalDosageDefinition

    Public Property DrugRecurreAdministration As enumdrugrecurreadministration
    Public Property DrugAdditional As String

    Public Property DrugAuthorizationNumb As String

    Public Property DrugDosageForm As String

    Public Property DrugCharacterization As enumDrugCharacterization
    Public Property Drugadministrationroute As String
    Public Property DrugDosageText As String
    Public Property DrugStartDate As String
    Public Property DrugEndDate As String
    Public Property DrugStartDateFormat As String
    Public Property DrugEndDateFormat As String
    'Public Property drugcharacterization As String
    'Public Property medicinalproduct As String

    Public Property DrugIndication As String
    Public Property MedicinalProduct As String

    Public Property OpenFDA As OpenFdaData

    Shared Function convertJsonData(jToken As JToken) As List(Of DrugData)

        Dim data As New List(Of DrugData)

        If IsJTokenValid(jToken) Then

            For Each drug In jToken

                Dim obj As New DrugData

                Integer.TryParse(drug("actiondrug"), obj.ActionDrug)
                Integer.TryParse(drug("drugrecurreadministration"), obj.DrugRecurreAdministration)

                obj.DrugIntervalDosageUnitNumb = drug("drugintervaldosageunitnumb")
                Integer.TryParse(drug("drugintervaldosagedefinition"), obj.DrugIntervalDosageDefinition)

                obj.DrugTreatmentDuration = drug("drugtreatmentduration")
                Integer.TryParse(drug("drugtreatmentdurationunit"), obj.DrugTreatmentDurationUnit)

                obj.DrugCumulativeDosageNumb = drug("drugcumulativedosagenumb")
                Integer.TryParse(drug("drugcumulativedosageunit"), obj.DrugCumulativeDosageUnit)


                Integer.TryParse(drug("drugcharacterization"), obj.DrugCharacterization)

                obj.DrugAdditional = drug("drugadditional")



                obj.DrugAuthorizationNumb = drug("drugauthorizationnumb")

                obj.DrugIndication = drug("drugindication")
                obj.MedicinalProduct = drug("medicinalproduct")
                obj.Drugadministrationroute = drug("drugadministrationroute")
                obj.DrugDosageText = drug("drugdosagetext")

                obj.DrugStartDate = drug("drugstartdate")
                obj.DrugStartDateFormat = drug("drugstartdateformat")
                obj.DrugEndDate = drug("drugenddate")
                obj.DrugEndDateFormat = drug("drugenddateformat")

                obj.DrugDosageForm = drug("drugdosageform")


                'obj.drugcharacterization = drug("drugcharacterization")
                'obj.medicinalproduct = drug("medicinalproduct")

                obj.OpenFDA = OpenFdaData.convertJsonData(drug("openfda"))

                data.Add(obj)

            Next

        End If
        Return data

    End Function

End Class

Public Class OpenFdaData

    Public Property application_number As New List(Of String)
    Public Property brand_name As New List(Of String)
    Public Property generic_name As New List(Of String)
    Public Property manufacturer_name As New List(Of String)
    Public Property nui As List(Of String)
    Public Property package_ndc As List(Of String)
    Public Property pharm_class_cs As List(Of String)
    Public Property pharm_class_epc As List(Of String)
    Public Property product_ndc As List(Of String)
    Public Property product_type As List(Of String)
    Public Property route As New List(Of String)
    Public Property rxcui As List(Of String)
    Public Property spl_id As List(Of String)
    Public Property spl_set_id As List(Of String)
    Public Property substance_name As List(Of String)
    Public Property unii As List(Of String)


    Shared Function convertJsonData(jToken As JToken) As OpenFdaData

        Dim data As New OpenFdaData

        If IsJTokenValid(jToken) Then

            'For Each itm In jToken("application_number")
            'Next
            For Each itm In jToken("brand_name")
                data.brand_name.Add(itm)
            Next
            For Each itm In jToken("generic_name")
                data.generic_name.Add(itm)
            Next
            For Each itm In jToken("manufacturer_name")
                data.manufacturer_name.Add(itm)
            Next
            For Each itm In jToken("route")
                data.route.Add(itm)
            Next

            'For Each reaction In jToken

            '    Dim obj As New ReactionData

            '    obj.ReactionMedDrapt = reaction("reactionmeddrapt")
            '    obj.ReactionMeddraversionPt = reaction("reactionmeddraversionpt")
            '    Integer.TryParse(reaction("reactionoutcome"), obj.ReactionOutcome)

            '    data.Add(obj)

            'Next
        End If

        Return data

    End Function


End Class

Public Class ReactionData

    Public Property ReactionMedDrapt As String
    Public Property ReactionMeddraversionPt As String
    Public Property ReactionOutcome As enumReactionOutcome

    Shared Function convertJsonData(jToken As JToken) As List(Of ReactionData)

        Dim data As New List(Of ReactionData)

        If IsJTokenValid(jToken) Then

            For Each reaction In jToken

                Dim obj As New ReactionData

                obj.ReactionMedDrapt = reaction("reactionmeddrapt")
                obj.ReactionMeddraversionPt = reaction("reactionmeddraversionpt")
                Integer.TryParse(reaction("reactionoutcome"), obj.ReactionOutcome)

                data.Add(obj)

            Next

        End If

        Return data

    End Function

End Class

#End Region