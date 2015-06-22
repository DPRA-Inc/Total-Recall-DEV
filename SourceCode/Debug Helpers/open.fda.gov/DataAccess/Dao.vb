
#Region " Imports "

Imports System.Configuration
Imports System.Data.SqlClient

#End Region

Public MustInherit Class Dao

#Region " Protected Methods "

    Protected Function InitializeConnection() As SqlConnection
        Dim cnn As SqlConnection

        Try

            ' initialize database connection
            cnn = New SqlConnection(GetConnectString())

            ' connect to the database
            cnn.Open()

        Catch ex As Exception
            Throw
        End Try

        Return cnn

    End Function


    Protected Sub CleanupConnection(ByRef cnn As SqlConnection)

        Try

            ' close the Database Connection
            If cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If

            ' and dispose of it
            cnn.Dispose()

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Protected Function GetQueryColumnNames(reader As SqlDataReader) As HashSet(Of String)

        Dim ColumnNames As New HashSet(Of String)
        If reader.HasRows Then

            Dim schemaTable As DataTable
            schemaTable = reader.GetSchemaTable()

            Dim row As DataRow
            Dim column As DataColumn


            For Each row In schemaTable.Rows
                For Each column In schemaTable.Columns
                    'Debug.WriteLine(String.Format("{0} = {1}", _
                    '  column.ColumnName, row(column)))

                    If column.ColumnName = "ColumnName" Then
                        ColumnNames.Add(row(column))
                    End If

                Next
            Next

        End If

        Return ColumnNames

    End Function

    Protected Overridable Function GetConnectString() As String
        Return ConfigurationManager.AppSettings.Get("ConnectionInfo")
    End Function

#End Region

End Class
