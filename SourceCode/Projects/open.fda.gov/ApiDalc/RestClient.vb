Imports System.Net

Public Class RestClient
    Implements IRestClient

    Private _apiKeyList As New List(Of String)({"XJbQx98xnJ1c2UQ9PmT31fhrjfifhNZHrxkJqxpN",
                                                "If79nXLMcUgAMEOKabW2xZwZWGduzxH6exXNyWXY",
                                                "2wVtRR9NZp16SiQPFCzPg2F1yPybEzmfCYtNZt7H",
                                                "VbLkhcEM5IFDA4DE22yVEfNp2EVDvhP2nPZLdNME",
                                                "0GhxVK46VsvckW4PqjOhYpVHbhH1wdd7ylTOwbdp"})
    Private Property _openFdaWebApiKey As String '= _apiKeyList(2)

    Public Function Execute(url As String) As String Implements IRestClient.Execute

        Dim result As String = String.Empty
        Dim webClient As New Net.WebClient()

        webClient.Headers.Clear()

        Try

            If Not String.IsNullOrEmpty(_openFdaWebApiKey) Then

                If Not url.Contains("?api_key=") Then
                    url = url.Replace("?", String.Format("?api_key={0}", _openFdaWebApiKey))
                Else
                    'TODO: Get the apiKey and compair to apiKeyString
                    ' Replace apiKey if needed
                End If

            End If

            result = webClient.DownloadString(url)

        Catch ex As Net.WebException

            Dim throwException As Boolean = True

            If ex.Response IsNot Nothing AndAlso TypeOf ex.Response Is HttpWebResponse Then

                Dim openFdaWebApiStatusCode As HttpStatusCode = DirectCast(ex.Response, System.Net.HttpWebResponse).StatusCode

                If DirectCast(ex.Response, System.Net.HttpWebResponse).StatusCode = HttpStatusCode.NotFound Then
                    throwException = False
                End If

                Select Case openFdaWebApiStatusCode
                    Case HttpStatusCode.BadRequest ' 400 Bad Request.  
                        'Throw (New Exception("Bad Request to OpenFda's WebApi"))
                        Throw New Exception(ex.Message)
                    Case CType(429, HttpStatusCode) ' 429 Too Many Requests.  
                        'Throw (New Exception("Bad Request to OpenFda's WebApi"))
                        Throw New Exception(ex.Message)

                End Select

            End If

            If throwException Then
                Throw New Exception(ex.Message)
            End If

        Catch ex As Exception
            Throw
        End Try

        Return result

    End Function

End Class
