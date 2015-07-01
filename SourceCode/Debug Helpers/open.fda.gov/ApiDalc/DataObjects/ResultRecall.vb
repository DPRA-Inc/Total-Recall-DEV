#Region " Imports "

Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    ''' <summary>
    ''' Recall Result
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ResultRecall

#Region " Public Properties "

        Public Property KeyWord As String = String.Empty

        Public Property City As String
        Public Property Classification As String
        Public Property Code_info As String
        Public Property Country As String
        Public Property Distribution_Pattern As String
        Public Property Event_Id As String
        Public Property Initial_Firm_Notification As String
        Public Property Openfda As Object
        Public Property Product_Description As String
        Public Property Product_Quantity As String
        Public Property Product_Type As String
        Public Property Reason_For_Recall As String
        Public Property Recall_Initiation_Date As String
        Public Property Recall_Number As String
        Public Property Recalling_Firm As String
        Public Property Report_Date As String
        Public Property State As String
        Public Property Status As String
        Public Property Voluntary_Mandated As String

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Converts JSON Data to List
        ''' </summary>
        ''' <param name="jsondata">JSON Object Data</param>
        ''' <returns>List of Recall Data</returns>
        ''' <remarks></remarks>
        Public Shared Function CnvJsonDataToList(jsondata As JObject) As List(Of ResultRecall)

            Dim result As New List(Of ResultRecall)

            For Each obj In jsondata.GetValue("results")

                Dim tmp As New ResultRecall

                tmp.City = CStr(obj("city"))
                tmp.Classification = CStr(obj("classification"))
                tmp.Code_info = CStr(obj("code_info"))
                tmp.Country = CStr(obj("country"))
                tmp.Distribution_Pattern = CStr(obj("distribution_pattern"))
                tmp.Event_Id = CStr(obj("event_id"))
                tmp.Initial_Firm_Notification = CStr(obj("initial_firm_notification"))
                'tmp.openfda = zz("city")
                tmp.Product_Description = CStr(obj("product_description"))
                tmp.Product_Quantity = CStr(obj("product_quantity"))
                tmp.Product_Type = CStr(obj("product_type"))
                tmp.Reason_For_Recall = CStr(obj("reason_for_recall"))
                tmp.Recall_Initiation_Date = CStr(obj("recall_initiation_date"))
                tmp.Recall_Number = CStr(obj("recall_number"))
                tmp.Recalling_Firm = CStr(obj("recalling_firm"))
                tmp.Report_Date = CStr(obj("report_date"))
                tmp.State = CStr(obj("state"))
                tmp.Status = CStr(obj("status"))
                tmp.Voluntary_Mandated = CStr(obj("voluntary_mandated"))

                result.Add(tmp)

            Next

            Return result

        End Function

        ''' <summary>
        ''' Converts JSON Data to List
        ''' </summary>
        ''' <param name="jsondata">JSON String Data</param>
        ''' <returns>List of Recall Data</returns>
        ''' <remarks></remarks>
        Public Shared Function CnvJsonDataToList(jsondata As String) As List(Of ResultRecall)

            Dim jo As JObject = JObject.Parse(jsondata)

            Return CnvJsonDataToList(jo)

        End Function

        Public Overrides Function ToString() As String
            Return String.Format("{0} - {1}", Recall_Initiation_Date, Classification)
        End Function

#End Region

    End Class

End Namespace
