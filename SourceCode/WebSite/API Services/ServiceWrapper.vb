Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public NotInheritable Class ServiceWrapper

    Public Shared Sub ProcessRequest(context As HttpContext)

        If Not CheckSecurity() Then Return

        ' This Disables WebClient Caching...  Incase clients are using the Web Client 
        ' to do all the socket work, it caches responses.  We have to disable this
        ' in order to have it keep giving us updated data.
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        ' Get the SessionID            
        Dim requestBuffer As Byte() = GetRequestDetails(context.Request)

        Dim responseInfo As String = ""

        Try
            If context.Request.HttpMethod = "POST" Then
                responseInfo = HandlePOSTs(context, requestBuffer)
            Else
                Dim sInputbuffer As String = System.Text.Encoding.UTF8.GetString(requestBuffer)
                responseInfo = HandleGETs(context, sInputbuffer)
            End If

            If responseInfo IsNot Nothing Then
                Dim obj As Object = JsonConvert.DeserializeObject(responseInfo)
                Dim ob = TryCast(obj, JObject)

                If responseInfo.StartsWith("{""WebObjectType"":""TotalRecall.SiteExceptionInfo""") Then
                    Dim code = ob.GetValue("StatusCode")
                    context.Response.StatusCode = code.Value(Of Integer)()
                End If

                context.Response.Write(obj)

            End If

        Catch ex As Exception
            context.Response.StatusCode = CInt(System.Net.HttpStatusCode.OK)
            context.Response.Write("ERROR: " + ex.Message)
        End Try

    End Sub

    Private Shared Function GetRequestDetails(request As HttpRequest) As Byte()
        Dim inputbuffer As Byte() = Nothing

        Dim length As Integer = Convert.ToInt32(request.InputStream.Length)

        If length > 0 Then
            inputbuffer = New Byte(length - 1) {}
            Dim read As Integer = request.InputStream.Read(inputbuffer, 0, length)
        End If

        Return inputbuffer
    End Function

    Private Shared Function CheckSecurity() As Boolean
        ' Future, maybe we have a login page?  Or when operating out of https?
        Return True
    End Function

    Private Shared Function HandleGETs(context As HttpContext, request As String) As String
        Dim param As New List(Of String)()

        Select Case context.Request.QueryString("Command").ToUpper()

            Case "LOGIN", "GETWEBSETTINGS", "KEEPALIVE", "GETREPORTDATA", "GETTERMSANDCONDITIONS", "GETPRIVACYSTATEMENT"

        End Select

        Return Nothing

    End Function

    Private Shared Function HandlePOSTs(context As HttpContext, requestBuffer As Byte()) As String

        Dim response As String = String.Empty

        Select Case context.Request.QueryString("Command").ToUpper()
            Case "GETISSUES"
                'return ServiceWrapper.GetIssues(requestBuffer);

                'case "EXECUTEBUSINESS":
                '    return ServiceWrapper.ExecuteBusiness(context, requestBuffer);

                'case "EXECUTEBUSINESSMULTIPART":
                '    return ServiceWrapper.ExecuteBusinessMultipart(context, requestBuffer);



        End Select

        Return Nothing

    End Function

End Class

