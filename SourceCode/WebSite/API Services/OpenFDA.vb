
#Region " Imports "

Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Public Class OpenFDA

    Public Shared HostURL As String = "https://api.fda.gov/"

    Private _search As String
    Private _count As String
    Private _limit As Integer = 0

    Private _resultSet As String
    Private _meta As JObject
    Private _results As JObject

    Private endPointType As OpenFDAApiEndPoints

    Public Function GetOpenFDAEndPoint(endpoint As OpenFDAApiEndPoints) As String

        endPointType = endpoint

        Dim result As String = String.Empty

        Dim endPT As String = GetEnumDefault(endpoint)
        result = AddFS(HostURL) & endPT & ".json?"

        Return result

    End Function

    Function buildUrl(ByVal endPointType As OpenFDAApiEndPoints, Optional ByVal limit As Integer = 0) As String

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

            sb.Append("&limit=")
            sb.Append(_limit)

        End If

        'uri = New Uri(res & _search & _limit)
        uri = New Uri(sb.ToString)

        Return uri.ToString

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

#Region " Search "

    Public Sub ResetSearch()
        _search = String.Empty
    End Sub

    Public Function AddSearchFilter(ByVal type As FDAFilterTypes, ByVal filters As List(Of String)) As String

        Dim param As String = String.Empty

        Dim endpointType As OpenFDAApiEndPoints = OpenFDAApiEndPoints.FoodRecall

        ' desc     -- product_Description, Reason_For_Recall, Code_Info
        ' Location -- Distribution_Pattern & State & Country
        ' Status   -- Status (ongoing, completed, terminated)
        ' Date     -- Report_Date & recall_initation_date
        ' Severity -- Classification

        '??
        ' Recalling_Firm

        Dim tmp As String = String.Empty

        Dim tmpItm As String
        For Each itm In filters
            tmpItm = itm.Replace(" ", "+")
            If tmpItm.Contains("+") Then
                tmpItm = """" & tmpItm & """"
            End If
            tmp += tmpItm & "+"
        Next
        If String.IsNullOrEmpty(tmp) Then
            tmp = tmp.Substring(0, tmp.Length - 1)
        End If


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

Public Enum FDAFilterTypes

    RecallReason
    Classification
    Region
    [Date]

End Enum

Public Enum OpenFDAApiEndPoints

    '<System.ComponentModel.Description("drug/event")>
    '<System.ComponentModel.DisplayNameAttribute("drug/event")>
    <System.ComponentModel.DefaultValueAttribute("drug/event")>
    DrugEvent

    <System.ComponentModel.DefaultValueAttribute("drug/label")>
    DrugLabel

    <System.ComponentModel.DefaultValueAttribute("drug/enforcement")>
    DrugRecall


    <System.ComponentModel.DefaultValueAttribute("device/event")>
    DeviceEvent

    <System.ComponentModel.DefaultValueAttribute("device/enforcement")>
    DeviceRecall


    <System.ComponentModel.DefaultValueAttribute("food/enforcement")>
    FoodRecall

End Enum

'An encoded value for the category of individual submitting the report.
Public Enum PrimarySourceQualification

    Physician = 1
    Pharmacist = 2
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


'patient.patientsex
'string
'The sex of the patient.
'0 = Unknown
'1 = Male
'2 = Female



'patient.drug.actiondrug
'string
'Actions taken with the drug
'1 = Drug withdrawn
'2 = Dose reduced
'3 = Dose increased
'4 = Dose not changed
'5 = Unknown
'6 = Not applicable


'patient.drug.drugcumulativedosageunit
'string
'The unit for drugcumulativedosagenumb
'001 = kg kilogram(s)
'002 = G gram(s)
'003 = Mg milligram(s)
'004 = μg microgram(s)


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

'patient.drug.drugcharacterization
'Reported role of the drug in the adverse event.
'1 = Suspect drug
'2 = Concomitant drug
'3 = Interacting drug



'patient.drug.drugtreatmentdurationunit
'The unit for patient.drug.drugtreatmentduration
'801 = Year
'802 = Month
'803 = Week
'804 = Day
'805 = Hour
'806 = Minute


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


Public Class MetaResults

    Public Property limit As Integer = 0
    Public Property skip As Integer = 0
    Public Property total As Integer = 0

    Public Shared Function cnvJsonData(jsondata As String) As MetaResults

        Dim jo As JObject = JObject.Parse(jsondata)

        Return cnvJsonData(jo)

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

End Class

#End Region