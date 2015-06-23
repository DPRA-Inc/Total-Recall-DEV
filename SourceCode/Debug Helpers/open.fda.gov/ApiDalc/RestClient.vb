Imports System.Net

Public Class RestClient
    Implements IRestClient

    Public Function Execute(url As String) As String Implements IRestClient.Execute

        Dim result As String = String.Empty
        Dim webClient = New Net.WebClient()

        webClient.Headers.Clear()

        Try

            result = webClient.DownloadString(url)

        Catch ex As Net.WebException

            Dim throwException As Boolean = True

            If ex.Response IsNot Nothing AndAlso TypeOf ex.Response Is HttpWebResponse Then

                If DirectCast(ex.Response, System.Net.HttpWebResponse).StatusCode = HttpStatusCode.NotFound Then
                    throwException = False
                End If

            End If

            If throwException Then
                Throw
            End If

        Catch ex As Exception
            Throw
        End Try

        Return result

    End Function

End Class
