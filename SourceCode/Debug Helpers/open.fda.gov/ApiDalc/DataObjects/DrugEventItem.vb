<Obsolete>
Public Class DrugEventItem
    Public AllData As New Dictionary(Of String, Object)

    Public Property SafetyReportID As String
    Public Property ReceivedDateString As String
    Public Property SenderOrganization As String


    Public Property PatientSex As String
    Public Property PatientAge As String
    Public Property PatientWeight As String
    Public Property PatientReactions As New List(Of String)

    Public Property DrugStartDate As String
    Public Property DrugEndDate As String

    Public Property Doseage As String
    Public Property Manufactures As New List(Of String)
    Public Property BrandNames As New List(Of String)
    Public Property GenericNames As New List(Of String)




    Public Shared Function Fill(item As Dictionary(Of String, Object)) As DrugEventItem

        Return New DrugEventItem(item)

    End Function

    Public Sub New()
    End Sub


    Private Sub CheckKeys(key As String, value As Object)

        Select Case key
            Case "safetyreportid" : SafetyReportID = value.ToString
            Case "receivedate" : ReceivedDateString = value.ToString
            Case "senderorganization" : SenderOrganization = value.ToString
            Case "patientonsetage" : PatientAge = value.ToString
            Case "patientweight" : PatientWeight = value.ToString
                '            Case "medicinalproduct" : Drug = value.ToString
            Case "manufacturer_name" : Manufactures = value
            Case "brand_name" : BrandNames = value
            Case "drugstartdate" : DrugStartDate = value.ToString
            Case "drugenddate" : DrugEndDate = value.ToString




        End Select

    End Sub


    Private Sub New(jsonItem As Dictionary(Of String, Object))
        Fetch(jsonItem)
    End Sub

    Private Sub Fetch(jsonItem As Dictionary(Of String, Object))

        For Each item As KeyValuePair(Of String, Object) In jsonItem
            Dim key As String = item.Key
            Dim value As Object = Nothing

            If item.Value IsNot Nothing Then

                value = item.Value.ToString

                If value.StartsWith("{") Then

                    value = MakeDictionaryObject(item.Value.ToString)

                ElseIf value.StartsWith("[") Then

                    value = MakeListObject(item.Value.ToString)

                End If

            End If

            CheckKeys(key, value)
            AllData.Add(key, value)

        Next



    End Sub

    'Private Function MakeListObject(data As String) As List(Of Dictionary(Of String, Object))

    '    Dim completeObject As New List(Of Dictionary(Of String, Object))
    '    'Dim obj = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(data)
    '    Dim obj As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(data)





    '    For i As Integer = 0 To obj.Count - 1
    '        Dim ele = obj(i)

    '        Dim listItem As New Dictionary(Of String, Object)

    '        For Each item As KeyValuePair(Of String, Object) In ele
    '            Dim key As String = item.Key
    '            Dim value As Object = Nothing

    '            If item.Value IsNot Nothing Then

    '                value = item.Value.ToString

    '                If value.StartsWith("{") Then

    '                    value = MakeDictionaryObject(item.Value.ToString)

    '                ElseIf value.StartsWith("[") Then

    '                    value = MakeListObject(item.Value.ToString)

    '                End If

    '            End If

    '            listItem.Add(key, value)

    '        Next

    '        completeObject.Add(listItem)

    '    Next

    '    Return completeObject

    'End Function

    Private Function MakeListObject(data As String) As List(Of Object)

        Dim completeObject As New List(Of Object)
        Dim obj As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(data)

        For i As Integer = 0 To obj.Count - 1
            Dim item = obj(i).ToString
            Dim value As Object = Nothing

            If item.StartsWith("{") Then

                value = MakeDictionaryObject(item.ToString)

            ElseIf item.StartsWith("[") Then

                value = MakeListObject(item.ToString)

            End If

            completeObject.Add(value)

        Next

        Return completeObject

    End Function

    Private Function MakeDictionaryObject(data As String) As Dictionary(Of String, Object)

        Dim completeObject As New Dictionary(Of String, Object)
        Dim obj = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(data)

        For Each item As KeyValuePair(Of String, Object) In obj
            Dim key As String = item.Key
            Dim value As Object = Nothing

            If item.Value IsNot Nothing Then

                value = item.Value.ToString

                If value.StartsWith("{") Then

                    value = MakeDictionaryObject(item.Value.ToString)

                ElseIf value.StartsWith("[") Then

                    value = MakeListObject(item.Value.ToString)

                End If

            End If

            completeObject.Add(key, value)

        Next

        Return completeObject

    End Function


End Class
