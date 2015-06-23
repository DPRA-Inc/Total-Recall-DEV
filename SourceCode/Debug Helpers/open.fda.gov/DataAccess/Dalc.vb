#Region " Imports "

Imports System.Configuration
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports DataObjects

'Imports DataObjects

#End Region

''' <summary>
''' Public DALC
''' </summary>
''' <remarks></remarks>
Public Class Dalc
    Inherits Dao

#Region " Public Properties "

    ''' <summary>
    ''' Database connection string
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ConnectionString As String
        Get
            Return MyBase.GetConnectString
        End Get
    End Property

#End Region

#Region " Execute SQL "

    ''' <summary>
    ''' Executes SQL Query Command
    ''' </summary>
    ''' <param name="sqlCommand">SQL to execute</param>
    ''' <returns>Dictionary list of string keyed by a string</returns>
    ''' <remarks></remarks>
    Public Function ExecuteSQLQueryCommand(ByVal sqlCommand As String) As Dictionary(Of Integer, Dictionary(Of String, String))

        Dim connection As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim reader As SqlDataReader = Nothing
        Dim schemaTable As DataTable

        Dim columnNames As New List(Of String)

        Dim rowData As Dictionary(Of String, String)
        Dim dataResults As New Dictionary(Of Integer, Dictionary(Of String, String))


        Try

            connection = InitializeConnection()

            cmd = New SqlCommand(sqlCommand, connection)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 60

            reader = cmd.ExecuteReader
            schemaTable = reader.GetSchemaTable()

            If reader.HasRows Then

                Dim row As DataRow
                Dim column As DataColumn

                For Each row In schemaTable.Rows

                    For Each column In schemaTable.Columns

                        'Debug.WriteLine(String.Format("{0} = {1}", _
                        '  column.ColumnName, row(column)))

                        If column.ColumnName = "ColumnName" Then
                            columnNames.Add(row(column))
                        End If

                    Next

                Next

            End If

            Dim cnt As Integer = 0

            While (reader.Read)

                cnt += 1

                rowData = New Dictionary(Of String, String)

                For Each col As String In columnNames
                    rowData.Add(col, reader(col).ToString)
                Next

                dataResults.Add(cnt, rowData)

            End While

            Return dataResults

        Catch ex As Exception
            Throw
        Finally

            If reader IsNot Nothing Then

                If Not reader.IsClosed Then
                    reader.Close()
                End If

            End If

            If connection IsNot Nothing Then
                CleanupConnection(connection)
            End If

        End Try

    End Function

    ''' <summary>
    ''' Executes SQL Command
    ''' </summary>
    ''' <param name="sqlCommands"></param>
    ''' <returns>result</returns>
    ''' <remarks></remarks>
    Public Function ExecuteSQLCommand(ByVal sqlCommands As String) As Integer

        Dim connection As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim reader As SqlDataReader = Nothing
        Dim dataResults As Integer

        Try

            connection = InitializeConnection()

            cmd = New SqlCommand(sqlCommands, connection)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 60

            dataResults = cmd.ExecuteNonQuery()

            Return dataResults

        Catch ex As Exception
            Throw
        Finally

            If reader IsNot Nothing Then

                If Not reader.IsClosed Then
                    reader.Close()
                End If

            End If

            If connection IsNot Nothing Then
                CleanupConnection(connection)
            End If

        End Try

    End Function

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Get All Person Objects
    ''' </summary>
    ''' <returns>List of Person objects</returns>
    ''' <remarks></remarks>
    Public Function GetAllPerson() As List(Of Person)

        Dim dataResults As New List(Of Person)

        dataResults.Add(New Person With {.FirstName = "Thomas", .LastName = "Kroll"})
        dataResults.Add(New Person With {.FirstName = "Lee", .LastName = "Raulerson"})
        dataResults.Add(New Person With {.FirstName = "Steve", .LastName = "Frost"})

        Return dataResults 'force return without lookup

        Dim connection As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim reader As SqlDataReader = Nothing

        Try

            Dim sb As New Text.StringBuilder

            sb.Append("Select * ")
            sb.Append("From Person ")

            connection = InitializeConnection()

            cmd = New SqlCommand(sb.ToString, connection)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 60

            reader = cmd.ExecuteReader

            While (reader.Read)

                Dim obj As New Person

                With obj

                    Integer.TryParse(reader("PersonKey").ToString, .Key)
                    '.Key = reader("VesselKey").ToString
                    .FirstName = reader("FirstName").ToString
                    .LastName = reader("LastName").ToString

                End With

                dataResults.Add(obj)

            End While

            Return dataResults

        Catch ex As Exception
            Throw
        Finally

            If reader IsNot Nothing Then

                If Not reader.IsClosed Then
                    reader.Close()
                End If

            End If

            If connection IsNot Nothing Then
                CleanupConnection(connection)
            End If

        End Try

    End Function

    ''' <summary>
    ''' Get Person by First Name
    ''' </summary>
    ''' <param name="value">First Name</param>
    ''' <returns>List of Person objects</returns>
    ''' <remarks></remarks>
    Public Function GetPersonByFName(ByVal value As String) As List(Of Person)

        Dim dataResults As New List(Of Person)

        dataResults.Add(New Person With {.FirstName = "Thomas", .LastName = "Kroll"})
        dataResults.Add(New Person With {.FirstName = "Lee", .LastName = "Raulerson"})
        dataResults.Add(New Person With {.FirstName = "Steve", .LastName = "Frost"})

        Dim filteredResults = (From el In dataResults Where el.FirstName.ToLower.Trim = value.ToLower.Trim Select el).ToList

        Return filteredResults 'return a list without looking up data

        Dim connection As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim reader As SqlDataReader = Nothing

        Try

            Dim sb As New Text.StringBuilder

            sb.Append("Select * ")
            sb.Append("From Person ")

            connection = InitializeConnection()

            cmd = New SqlCommand(sb.ToString, connection)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 60

            reader = cmd.ExecuteReader

            While (reader.Read)

                Dim obj As New Person

                With obj

                    Integer.TryParse(reader("PersonKey").ToString, .Key)
                    '.Key = reader("VesselKey").ToString
                    .FirstName = reader("FirstName").ToString
                    .LastName = reader("LastName").ToString

                End With

                If obj.FirstName.ToLower.Trim = value.ToLower.Trim Then
                    dataResults.Add(obj)
                End If

            End While

            Return dataResults

        Catch ex As Exception
            Throw
        Finally

            If reader IsNot Nothing Then

                If Not reader.IsClosed Then
                    reader.Close()
                End If

            End If

            If connection IsNot Nothing Then
                CleanupConnection(connection)
            End If

        End Try
    End Function

#End Region

End Class
