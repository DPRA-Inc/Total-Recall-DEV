﻿#Region " Imports "

Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

''' <summary>
''' Open FDA
''' </summary>
''' <remarks></remarks>
Public Class OpenFDA

#Region " Public Properties "

    Public Shared Property HostURL As String = "https://api.fda.gov/"

#End Region

#Region " Private Members "

    Private _search As String
    Private _count As String
    Private _limit As Integer = 0

    Private _resultSet As String
    Private _meta As JObject
    Private _results As Object 'JObject

    Private _keyWords As New HashSet(Of String)

    Private _endPointType As OpenFDAApiEndPoints

#End Region

#Region " Public Methods "

    Public Function GetOpenFDAEndPoint(endpoint As OpenFDAApiEndPoints) As String

        _endPointType = endpoint

        Dim result As String = String.Empty

        Dim endPT As String = GetEnumDefault(endpoint)

        result = AddFS(HostURL) & endPT & ".json?"

        Return result

    End Function

    Public Function BuildUrl(ByVal endPointType As OpenFDAApiEndPoints, Optional ByVal limit As Integer = 0, Optional ByVal ongoingOnly As Boolean = True) As String

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

    Public Function ExecuteExact(url As String) As String

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

    Public Sub SearchOnFieldByValue(searchField As String, searchFieldValue As String)

        searchFieldValue = searchFieldValue.Replace(" ", "+")

        If searchFieldValue.Contains("+") Then
            searchFieldValue = """" & searchFieldValue & """"
        End If

        _search = String.Format("{0}:{1}", searchField, searchFieldValue)

    End Sub

    Public Sub SearchFieldExists(searchField As String)
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

    Public Function GetMetaResults() As MetaResults
        Dim metaData As New MetaResults

        If _meta IsNot Nothing Then

            If _meta("results") IsNot Nothing Then

                With metaData

                    .Limit = _meta("results")("limit")
                    .Skip = _meta("results")("skip")
                    .Total = _meta("results")("total")

                End With

            End If

        End If

        Return metaData

    End Function

#End Region

End Class