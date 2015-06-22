
Imports ApiDalc

Public Class Form1

#Region " Private Members "

    Private _filteringList As Dictionary(Of String, List(Of String))
    Private _shoppingList As HashSet(Of String)

#End Region

#Region " Constructors "

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim ToolTip1 As New ToolTip()
        Dim ToolTip2 As New ToolTip()
        Dim ToolTip3 As New ToolTip()

        ' Set up the delays for the ToolTip.
        With ToolTip1
            .AutoPopDelay = 5000
            .InitialDelay = 1000
            .ReshowDelay = 500
        End With
        With ToolTip2
            .AutoPopDelay = 5000
            .InitialDelay = 1000
            .ReshowDelay = 500
        End With
        With ToolTip3
            .AutoPopDelay = 5000
            .InitialDelay = 1000
            .ReshowDelay = 500
        End With

        ToolTip1.SetToolTip(Me.cbFilterTypes, Me.cbFilterTypes.Tag)

        ToolTip2.SetToolTip(Me.cbFilterTypes, Me.cbFilterTypes.Tag)


        ToolTip3.SetToolTip(Me.tbSearchFieldValue, "Leave value empty/blank to check if field exist (any value *)")


#If DEBUG Then
#Else

        tbExactURL.Visible = False
        tbShopItem.Enabled = False
        NumericUpDown1.Visible = False

#End If

        'Populate EndPointTypes
        cbEndPoints.Items.Clear()

        For Each itm In [Enum].GetValues(GetType(OpenFDAApiEndPoints))
            cbEndPoints.Items.Add(itm.ToString())
            cbEndPoints_2.Items.Add(itm.ToString())
        Next
        cbEndPoints.SelectedIndex = 0
        cbEndPoints.SelectedItem = OpenFDAApiEndPoints.FoodRecall.ToString
        cbEndPoints_2.SelectedIndex = 0
        cbEndPoints_2.SelectedItem = OpenFDAApiEndPoints.FoodRecall.ToString

        Me.cbSearchField.DropDownWidth = 250

        'Populate Search/Filter list
        cbFilterTypes.Items.Clear()

        For Each itm In [Enum].GetValues(GetType(FDAFilterTypes))
            cbFilterTypes.Items.Add(itm.ToString())
        Next
        cbFilterTypes.SelectedIndex = 0
        cbFilterTypes.SelectedItem = FDAFilterTypes.RecallReason.ToString

        'Re
        Me.ResetFilterList()
        Me.RichTextBox1.Text = String.Empty

    End Sub

#End Region

#Region " Private Methods "

    Private Sub ResetFilterList()
        _filteringList = New Dictionary(Of String, List(Of String))
    End Sub

    Private Sub btnAddFilter_Click(sender As Object, e As EventArgs) Handles btnAddFilter.Click

        'Debug.Write(cbFilterTypes.SelectedItem)


        Dim tmpFilterText As String = String.Empty
        If tbFilter.Visible Then
            tmpFilterText = tbFilter.Text
        ElseIf cbSerchTypeOptions.Visible Then
            tmpFilterText = cbSerchTypeOptions.Text
        ElseIf dtSerchTypeOptions.Visible Then
            tmpFilterText = String.Format("{0:yyyyMMdd}", dtSerchTypeOptions.Value)
        End If



        Dim query = (From el In lbFilterList.Items Where el = tmpFilterText Select el).FirstOrDefault()

        If query Is Nothing And Not String.IsNullOrEmpty(tmpFilterText) Then
            lbFilterList.Items.Add(tmpFilterText)
        End If

        If Not String.IsNullOrEmpty(cbFilterTypes.SelectedItem) Then

            Dim key As String = cbFilterTypes.SelectedItem.ToString
            If Not _filteringList.ContainsKey(key) Then
                _filteringList.Add(key, New List(Of String))
            End If

            Dim fList As New List(Of String)
            For Each itm In lbFilterList.Items
                fList.Add(itm.ToString)
            Next

            _filteringList(key) = fList

        End If

        If tbFilter.Visible Then
            tbFilter.Text = String.Empty
            tbFilter.Focus()
        ElseIf cbSerchTypeOptions.Visible Then
            cbSerchTypeOptions.Focus()
        End If


    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        TreeView1.Nodes.Clear()


#If DEBUG Then

#Else

#End If

        Dim ept As OpenFDAApiEndPoints = DirectCast([Enum].Parse(GetType(OpenFDAApiEndPoints), cbEndPoints.Text), OpenFDAApiEndPoints)

        Dim fda As New OpenFDA
        'fda.ResetSearch()

        For Each fr As KeyValuePair(Of String, List(Of String)) In _filteringList

            Dim fl As New List(Of String)
            Dim key As String = fr.Key

            Dim ft As FDAFilterTypes = DirectCast([Enum].Parse(GetType(FDAFilterTypes), fr.Key), FDAFilterTypes)

            For Each itm As String In _filteringList(key)
                fl.Add(itm)
            Next

            fda.AddSearchFilter(ept, ft, fl)

        Next


        'Dim filterList As New List(Of String)

        'For Each itm As String In lbFilterList.Items
        '    filterList.Add(itm)
        'Next

        'fda.AddSearchFilter(FDAFilterTypes.RecallReason, filterList)
        'Dim ept As OpenFDAApiEndPoints = DirectCast([Enum].Parse(GetType(OpenFDAApiEndPoints), cbEndPoints.Text), OpenFDAApiEndPoints)
        'Dim apiUrl As String = fda.buildUrl(ept, 10)


        Dim result As Object
        If String.IsNullOrEmpty(tbExactURL.Text) Then

            Dim apiUrl As String = fda.buildUrl(ept, spMaxResultSize.Value)
            lbFdaUrl.Text = apiUrl

            result = fda.Execute(apiUrl)

        Else

            'Determint EndPointType by parsing the URL
            Dim tmpUrl As String = tbExactURL.Text.ToLower

            Dim tmpendPointType As String
            tmpUrl = tmpUrl.Replace(OpenFDA.HostURL.ToLower, String.Empty)
            tmpUrl = tmpUrl.Remove(tmpUrl.IndexOf("?"))
            tmpUrl = tmpUrl.Remove(tmpUrl.IndexOf("."))
            tmpendPointType = tmpUrl

            ept = cnvStringToEndPointType(tmpendPointType)

            lbFdaUrl.Text = tbExactURL.Text
            result = fda.ExecuteExact(tbExactURL.Text)

            'result = fda.ExecuteExact("https://api.fda.gov/drug/event.json?&search=_exists_:patient.drug.drugintervaldosagedefinition")
            'result = fda.ExecuteExact("https://api.fda.gov/drug/event.json?&search=_exists_:patient.drug.drugtreatmentdurationunit")
            'result = fda.ExecuteExact("https://api.fda.gov/drug/event.json?&search=_exists_:patient.drug.drugcumulativedosagenumb")
            ' result = fda.ExecuteExact("https://api.fda.gov/drug/event.json?&search=_exists_:patient.drug.drugcumulativedosageunit")
            'result = fda.ExecuteExact("https://api.fda.gov/drug/event.json?&search=_exists_:patient.drug.drugrecurreadministration")

        End If

        '
        'Dim RecallResultLst As List(Of ResultRecall) = ResultRecall.cnvJsonDataToList(result)

        'Dim mr As MetaResults = fda.getMetaResults()
        Dim mr As MetaResults = MetaResults.cnvJsonData(result)

        lbMetaResultsTotal.Text = String.Format("Total Results: {0}", mr.Total)


        'TreeView1.Nodes.Clear()
        'TreeView1.Nodes.Add("results")
        If Not String.IsNullOrEmpty(result) Then

            Select Case ept
                Case OpenFDAApiEndPoints.DeviceRecall, OpenFDAApiEndPoints.DrugRecall, OpenFDAApiEndPoints.FoodRecall
                    PopulateTree_Recall(result)

                Case OpenFDAApiEndPoints.DrugEvent
                    PopulateTree_DrugEvent(result)

            End Select

        End If


        'Dim mr As MetaResults = MetaResults.cnvJsonData(result)
        'For Each itm As ResultRecall In RecallResultLst

        '    Dim nd As New TreeNode("Event ID: " & itm.event_id)
        '    Dim key As Integer = TreeView1.Nodes.Add(nd)

        '    With TreeView1.Nodes(key).Nodes

        '        .Add("Product Type: " & itm.product_type)
        '        .Add("Status: " & itm.status)
        '        .Add("voluntary_mandated: " & itm.voluntary_mandated)
        '        .Add("Classification: " & itm.classification)

        '        .Add("State: " & itm.state)
        '        .Add("City: " & itm.city)
        '        .Add("Country: " & itm.country)
        '        .Add("Distribution Pattern: " & itm.distribution_pattern)

        '        .Add("code_info: " & itm.code_info)
        '        .Add("reason_for_recall: " & itm.reason_for_recall)
        '        .Add("product_description: " & itm.product_description)
        '        .Add("product_quantity: " & itm.product_quantity)

        '        .Add("recall_number: " & itm.recall_number)
        '        '.Add("voluntary_mandated: " & itm.voluntary_mandated)
        '        .Add("recalling_firm: " & itm.recalling_firm)
        '        .Add("initial_firm_notification: " & itm.initial_firm_notification)
        '        '.Add("openfda: " & itm.openfda)
        '        '.Add("product_type: " & itm.product_type)
        '        .Add("report_date: " & itm.report_date)
        '        .Add("recall_initiation_date: " & itm.recall_initiation_date)

        '    End With

        'Next

        'TreeView1.EndUpdate()

    End Sub


    Private Sub lbFilterList_DoubleClick(sender As Object, e As EventArgs) Handles lbFilterList.DoubleClick

        Dim lb As ListBox = CType(sender, ListBox)

        If lb.SelectedIndex > -1 Then
            lb.Items.RemoveAt(lb.SelectedIndex)
        End If

        Dim key As String = cbFilterTypes.SelectedItem.ToString
        If _filteringList.ContainsKey(key) Then
            Dim nList As New List(Of String)
            For Each itm In lb.Items
                nList.Add(itm)
            Next
            _filteringList(key) = nList
        End If

    End Sub

    Private Sub cbFilterTypes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterTypes.SelectedIndexChanged

        lbFilterList.Items.Clear()

        Dim cb As ComboBox = CType(sender, ComboBox)
        Dim key = cb.Text

        If _filteringList IsNot Nothing Then

            If _filteringList.ContainsKey(key) Then

                For Each itm In _filteringList(key)
                    lbFilterList.Items.Add(itm)
                Next

            End If

        End If

        Dim sb As New System.Text.StringBuilder
        tbFilter.Visible = False
        cbSerchTypeOptions.Visible = False
        dtSerchTypeOptions.Visible = False

        Select Case key.ToLower
            Case "classification"

                Me.cbFilterTypes.Tag = "search field = classification"
                Me.btnAddFilter.Tag = ""

                cbSerchTypeOptions.Visible = True
                cbSerchTypeOptions.Items.Clear()

                sb.AppendLine("Class   I = Dangerous or defective...")
                sb.AppendLine("Class  II = cause a temporary health problem...")
                sb.AppendLine("Class III = violate FDA labeling or manufacturing laws...")

                Dim items As Array
                items = System.Enum.GetValues(GetType(enumClassification))
                'Dim item As String
                Dim tmpEnum As enumClassification
                For Each item In items

                    tmpEnum = DirectCast([Enum].Parse(GetType(enumClassification), item), enumClassification)
                    cbSerchTypeOptions.Items.Add(GetEnumDefault(tmpEnum))

                    'If itm.distribution_pattern.Contains(tmpEnum.ToString) OrElse
                    '    itm.distribution_pattern.Contains(GetEnumDescription(tmpEnum)) OrElse
                    '    recallData.isNationWide Then

                    '    recallData.Regions.Add(tmpState.ToString)

                    'End If

                Next



            Case "region"

                Me.cbFilterTypes.Tag = "search field = state & distribution_pattern"
                Me.btnAddFilter.Tag = "Add specified region as a filter option"

                cbSerchTypeOptions.Visible = True
                cbSerchTypeOptions.Items.Clear()

                Dim items As Array
                items = System.Enum.GetValues(GetType(EnumStates))
                'Dim item As String
                Dim tmpEnum As EnumStates

                cbSerchTypeOptions.Items.Add("Nationwide")
                For Each item In items

                    tmpEnum = DirectCast([Enum].Parse(GetType(EnumStates), item), EnumStates)
                    cbSerchTypeOptions.Items.Add(tmpEnum.ToString)

                    'If itm.distribution_pattern.Contains(tmpEnum.ToString) OrElse
                    '    itm.distribution_pattern.Contains(GetEnumDescription(tmpEnum)) OrElse
                    '    recallData.isNationWide Then

                    '    recallData.Regions.Add(tmpState.ToString)

                    'End If

                Next



            Case "date"

                Me.cbFilterTypes.Tag = "search field = report_date & recall_initiation_date"
                Me.btnAddFilter.Tag = "Add specified date as a filter option"

                dtSerchTypeOptions.Visible = True

                sb.AppendLine("Single date = Report or Initial Notification Date on specified date")
                sb.AppendLine("Multiple dates = Report or Initial Notification Date occuring within specified date range.")

            Case Else

                Me.cbFilterTypes.Tag = "search field = product_description & reason_for_recall"
                Me.btnAddFilter.Tag = ""

                tbFilter.Visible = True

        End Select

        tbSearchTypeNotes.Text = sb.ToString

        Me.btnAddFilter.Tag = String.Format("Add specified {0} as a filter option", key)

        ToolTip1.SetToolTip(Me.cbFilterTypes, Me.cbFilterTypes.Tag)
        ToolTip1.SetToolTip(Me.btnAddFilter, Me.btnAddFilter.Tag)

    End Sub

    Private Sub cbEndPoints_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbEndPoints.SelectedIndexChanged

        ResetFilterList()
        lbFilterList.Items.Clear()
        TreeView1.Nodes.Clear()
        lbFdaUrl.Text = String.Empty

        Dim cb As ComboBox = CType(sender, ComboBox)

        Dim showSearchFilterControls As Boolean = True

        If Not String.IsNullOrEmpty(cb.SelectedItem) Then

            tbSearchTypeNotes.Text = String.Empty
            If cb.SelectedItem.ToString.ToLower.Contains("recall") Then
                showSearchFilterControls = True
            Else
                showSearchFilterControls = False
                tbSearchTypeNotes.Text = "Filtering of this OpenFDA endpoint not available at this time"
            End If

        End If

#If DEBUG Then

#Else

        lbSearch.Visible = showSearchFilterControls
        cbFilterTypes.Visible = showSearchFilterControls
        tbFilter.Visible = showSearchFilterControls
        btnAddFilter.Visible = showSearchFilterControls
        lbFilterList.Visible = showSearchFilterControls

#End If


    End Sub

    Private Sub PopulateTree_Recall(result As Object)

        Dim RecallResultLst As List(Of ResultRecall) = ResultRecall.cnvJsonDataToList(result)

        TreeView1.Nodes.Clear()

        For Each itm As ResultRecall In RecallResultLst

            Dim nd As New TreeNode("Event ID: " & itm.Event_Id)
            Dim key As Integer = TreeView1.Nodes.Add(nd)

            With TreeView1.Nodes(key).Nodes

                .Add("Product Type: " & itm.Product_Type)
                .Add("Status: " & itm.Status)
                .Add("voluntary_mandated: " & itm.Voluntary_Mandated)
                .Add("Classification: " & itm.Classification)

                .Add("State: " & itm.State)
                .Add("City: " & itm.City)
                .Add("Country: " & itm.Country)
                .Add("Distribution Pattern: " & itm.Distribution_Pattern)

                .Add("code_info: " & itm.Code_info)
                .Add("reason_for_recall: " & itm.Reason_For_Recall)
                .Add("product_description: " & itm.Product_Description)
                .Add("product_quantity: " & itm.Product_Quantity)

                .Add("recall_number: " & itm.Recall_Number)
                '.Add("voluntary_mandated: " & itm.voluntary_mandated)
                .Add("recalling_firm: " & itm.Recalling_Firm)
                .Add("initial_firm_notification: " & itm.Initial_Firm_Notification)
                '.Add("openfda: " & itm.openfda)
                '.Add("product_type: " & itm.product_type)
                .Add("report_date: " & itm.Report_Date)
                .Add("recall_initiation_date: " & itm.Recall_Initiation_Date)

            End With

        Next

        TreeView1.EndUpdate()

    End Sub

    Private Sub PopulateTree_DrugEvent(result As Object)

        Dim AdverseDrugEventList As List(Of AdverseDrugEvent) = AdverseDrugEvent.cnvJsonDataToList(result)

        TreeView1.Nodes.Clear()

        For Each itm As AdverseDrugEvent In AdverseDrugEventList

            Dim nd As New TreeNode("Case ID: " & itm.SafetyReportId)
            Dim key As Integer = TreeView1.Nodes.Add(nd)

            With TreeView1.Nodes(key).Nodes

                .Add("Company Number: " & itm.CompanyNumb)
                .Add("Fulfill Expedite Criteria: " & itm.FulfillExpediteCriteria)
                .Add("Primary Source: -- OBJECT") ' Qualification : ReportCountry
                .Add("Receipt Date: " & itm.ReceiptDate)
                .Add("Receive Date: " & itm.ReceiveDate)
                .Add("Transmission Date: " & itm.transmissiondate)

                .Add("Receiver: " & itm.Receiver)

                .Add("Sender: -- OBJECT")
                .Add("Serious: " & itm.Serious)
                .Add("Seriousness Congenital Anomali: " & itm.SeriousnessCongenitalanomali)
                ' 1 = The adverse event resulted in death, a life threatening condition, hospitalization, disability, congenital anomali, or other serious condition.
                ' 2 = The adverse event did not result in any of the above.

                'seriousnesscongenitalanomali
                'This value is 1 if the adverse event resulted in a congenital anomali, and absent otherwise

                .Add("seriousness death: " & itm.SeriousnessDeath)
                .Add("seriousness disabling: " & itm.SeriousnessDisabling)
                .Add("seriousness life threatening: " & itm.SeriousnessLifeThreatening)
                .Add("Seriousness Hospitalization: " & itm.SeriousnessHospitalization)
                .Add("Seriousness Other: " & itm.SeriousnessOther)

                .Add("Safety Report Id: " & itm.SafetyReportId)
                .Add("Safety Report Version: " & itm.SafetyReportVersion)


                .Add("occur country: " & itm.OccurCountry)

                '.Add("product_description: " & itm.product_description)
                '.Add("product_quantity: " & itm.product_quantity)

                '.Add("recall_number: " & itm.recall_number)
                ''.Add("voluntary_mandated: " & itm.voluntary_mandated)
                '.Add("recalling_firm: " & itm.recalling_firm)
                '.Add("initial_firm_notification: " & itm.initial_firm_notification)
                ''.Add("openfda: " & itm.openfda)
                ''.Add("product_type: " & itm.product_type)
                '.Add("report_date: " & itm.report_date)
                Dim patientNode As TreeNode = .Add("Patient: ")
                patientNode.Nodes.Add("Patient Gender: " & itm.Patient.PatientSex.ToString)

                Dim drugNode As TreeNode = patientNode.Nodes.Add("Drugs: ")
                Dim drugCnt As Integer = 0
                For Each drug In itm.Patient.Drug
                    drugCnt += 1
                    Dim drugItemNode As TreeNode = drugNode.Nodes.Add("{0})", drugCnt)

                    drugItemNode.Nodes.Add("Medicinal Product: " & drug.MedicinalProduct)
                    drugItemNode.Nodes.Add("Drug Dosage Form: " & drug.DrugDosageForm)
                    drugItemNode.Nodes.Add("Drug Indication: " & drug.DrugIndication)

                    drugItemNode.Nodes.Add("Action Drug: " & GetEnumDescription(drug.ActionDrug))
                    'drugItemNode.Nodes.Add("Action Drug: " & IIf(drug.actiondrug = 0, "", drug.actiondrug.ToString))

                    drugItemNode.Nodes.Add("Drug Cumulative Dosage numb: " & drug.DrugCumulativeDosageNumb)
                    drugItemNode.Nodes.Add("Drug Cumulative Dosage unit: " & GetEnumDescription(drug.DrugCumulativeDosageUnit))

                    drugItemNode.Nodes.Add("Drug Treatment Duration: " & drug.DrugTreatmentDuration)
                    drugItemNode.Nodes.Add("Drug Treatment Duration Unit: " & GetEnumDescription(drug.DrugTreatmentDurationUnit))

                    'drugItemNode.Nodes.Add("Drug cumulative dosage numb: " & IIf(drug.drugcumulativedosagenumb = 0, "", drug.drugcumulativedosagenumb.ToString))
                    drugItemNode.Nodes.Add("Drug Additional: " & drug.DrugAdditional)
                    drugItemNode.Nodes.Add("Drugadministrationroute: " & drug.Drugadministrationroute)
                    drugItemNode.Nodes.Add("Drug Authorization Numb: " & drug.DrugAuthorizationNumb)
                    drugItemNode.Nodes.Add("Drug Characterization: " & GetEnumDescription(drug.DrugCharacterization))
                    drugItemNode.Nodes.Add("Drug Dosage Text: " & drug.DrugDosageText)

                    drugItemNode.Nodes.Add("Drug StartDate: " & drug.DrugStartDate)
                    drugItemNode.Nodes.Add("Drug EndDate: " & drug.DrugEndDate)

                    drugItemNode.Nodes.Add("Drug Recurre Administration: " & GetEnumDescription(drug.DrugRecurreAdministration))

                    '

                    drugItemNode.Nodes.Add("Drug Interval Dosage Unit Numb: " & drug.DrugIntervalDosageUnitNumb)
                    'drugItemNode.Nodes.Add("drugtreatmentdurationunit: " & drug.drugtreatmentdurationunit)
                    drugItemNode.Nodes.Add("Drug Interval Dosage Definition: " & GetEnumDescription(drug.DrugIntervalDosageDefinition))
                    'drugItemNode.Nodes.Add("drugintervaldosagedefinition: " & IIf(drug.drugintervaldosagedefinition = 0, "", drug.drugintervaldosagedefinition.ToString))



                    '

                    drugItemNode.Nodes.Add("Open FDA: -- OBJECT")

                Next

                Dim reactionNode As TreeNode = patientNode.Nodes.Add("Reactions: ")
                Dim reactionCnt As Integer = 0
                For Each reaction In itm.Patient.Reaction
                    reactionCnt += 1
                    Dim reactionItemNode As TreeNode = reactionNode.Nodes.Add("{0})", reactionCnt)

                    reactionItemNode.Nodes.Add("Reaction Meddrapt: " & reaction.ReactionMedDrapt)
                    reactionItemNode.Nodes.Add("Reaction Meddraversionpt: " & reaction.ReactionMeddraversionPt)
                    reactionItemNode.Nodes.Add("Reaction Outcome: " & GetEnumDescription(reaction.ReactionOutcome))

                Next

            End With

        Next

        TreeView1.EndUpdate()

    End Sub


    Private Sub PopulateTree_ShoppingList_RecallSearch(results As List(Of RecallSearchResultData), Optional ByVal detailView As Boolean = False)

        TreeView2.Nodes.Clear()

        'Dim tmpList = (From el In results Group el By el.KeyWord, el.Type Into g = Group Select New With {.keyword = KeyWord, .type = Type, .data = g}).ToList
        Dim tmpList = (From el In results Group el By el.KeyWord, el.Type Into g = Group Select New With {.keyword = KeyWord, .data = g}).ToList

        Dim NodeName As String = String.Empty

        Dim parentNode As TreeNode
        For Each itm In tmpList

            If detailView Then
                NodeName = itm.keyword & "  (" & itm.data(0).Count & ")"
            Else
                NodeName = itm.keyword
            End If

            parentNode = TreeView2.Nodes.Add(NodeName)

            'Dim cNode As TreeNode = parentNode.Nodes.Add(itm.type)
            Dim nodeCnt As Integer = 0
            For Each citm In itm.data

                If detailView Then
                    nodeCnt += 1
                    NodeName = citm.Type & "  (" & nodeCnt & ")"
                Else
                    NodeName = citm.Type & "  (" & citm.Count & ")"
                End If

                Dim cNode As TreeNode = parentNode.Nodes.Add(NodeName)

                'cNode.Nodes.Add("cnt: " & citm.Count)
                cNode.Nodes.Add("Classification: " & citm.Classification)
                cNode.Nodes.Add("Description_1: " & citm.Description_1)
                cNode.Nodes.Add("Description_2: " & citm.Description_2)
                cNode.Nodes.Add("Nationwide: " & citm.IsNationWide)

                Dim stList As String = String.Empty
                'If citm.isNationWide Then
                '    stList = "All"
                'Else
                'End If
                For Each st In citm.Regions
                    stList += st & ","
                Next
                If Not String.IsNullOrEmpty(stList) Then
                    stList = stList.Substring(0, stList.Length - 1)
                End If


                cNode.Nodes.Add("Regions: " & stList)
            Next
            'cNode.Nodes.Add(itm.data.)
            'Debug.WriteLine(itm.keyword, itm.type, itm.data)
        Next


    End Sub


    Private Sub btAddToShoppingList_Click(sender As Object, e As EventArgs) Handles btAddToShoppingList.Click

        If _shoppingList Is Nothing Then
            ResetShoppingList()
        End If

        If Not String.IsNullOrEmpty(tbShoppingItem.Text) Then

            If Not _shoppingList.Contains(tbShoppingItem.Text.ToLower) Then

                _shoppingList.Add(tbShoppingItem.Text.ToLower)
                lbShoppingList.Items.Add(tbShoppingItem.Text)

            End If

        End If

        tbShoppingItem.Text = String.Empty
        tbShoppingItem.Focus()

    End Sub

    Private Sub ResetShoppingList()

        _shoppingList = New HashSet(Of String)
        lbShoppingList.Items.Clear()

    End Sub

    Private Sub btCheckShoppingListForRecalls_Click(sender As Object, e As EventArgs) Handles btCheckShoppingListForRecalls.Click

        Dim wrapper As New ShopAwareService
        Dim ShoppingList As New List(Of String)

        For Each item In lbShoppingList.Items
            ShoppingList.Add(item)
        Next

        Dim results As List(Of RecallSearchResultData) = wrapper.GetRecallsSummary(ShoppingList)

        PopulateTree_ShoppingList_RecallSearch(results)


    End Sub

    Private Sub tbShoppingItem_KeyDown(sender As Object, e As KeyEventArgs) Handles tbShoppingItem.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            'Put whatever you want here.  
            'btAddToShoppingList_Click(sender, e)
            btAddToShoppingList_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub tbFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles tbFilter.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            'Put whatever you want here.  
            'btnAddFilter_Click(sender, e)
            btnAddFilter_Click(Nothing, Nothing)
        End If

    End Sub



    Private Sub lbShoppingList_DoubleClick(sender As Object, e As EventArgs) Handles lbShoppingList.DoubleClick

        Dim lb As ListBox = CType(sender, ListBox)

        If lb.SelectedIndex > -1 Then

            _shoppingList.Remove(lb.SelectedItem.tolower)
            lb.Items.RemoveAt(lb.SelectedIndex)

        End If

        Dim key As String = cbFilterTypes.SelectedItem.ToString
        If _filteringList.ContainsKey(key) Then
            Dim nList As New List(Of String)
            For Each itm In lb.Items
                nList.Add(itm)
            Next
            _filteringList(key) = nList
        End If

    End Sub

    Private Sub lbShoppingList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbShoppingList.SelectedIndexChanged

        Dim lb As ListBox = CType(sender, ListBox)
        tbShopItem.Text = lb.SelectedItem

    End Sub

    Private Sub btGetRecallDetails_Click(sender As Object, e As EventArgs) Handles btGetRecallDetails.Click

        Dim wrapper As New ShopAwareService

        'Dim ShoppingList As New List(Of String)

        'For Each item In lbShoppingList.Items
        '    ShoppingList.Add(item)
        'Next

        Dim results As List(Of RecallSearchResultData) = wrapper.GetRecallsDetail(tbShopItem.Text)

        PopulateTree_ShoppingList_RecallSearch(results, True)

    End Sub

    Private Sub ToolTip1_Draw(sender As Object, e As DrawToolTipEventArgs)

    End Sub


    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim fda As New OpenFDA
        Dim result As Object

        Dim tmpUrl As String = tbExactURL_2.Text.ToLower

        'Dim ept As OpenFDAApiEndPoints
        'Dim tmpendPointType As String
        'tmpUrl = tmpUrl.Replace(OpenFDA.HostURL.ToLower, String.Empty)
        'tmpUrl = tmpUrl.Remove(tmpUrl.IndexOf("?"))
        'tmpUrl = tmpUrl.Remove(tmpUrl.IndexOf("."))
        'tmpendPointType = tmpUrl

        'ept = cnvStringToEndPointType(tmpendPointType)

        result = fda.ExecuteExact(tbExactURL_2.Text)


        If cknToLineFeed.Checked Then
            RichTextBox1.Text = result.ToString.Replace("\n", vbLf)
        Else
            RichTextBox1.Text = result
        End If

    End Sub

    Private Sub btSearch_2_Click(sender As Object, e As EventArgs) Handles btSearch_2.Click

        If String.IsNullOrEmpty(cbEndPoints_2.Text) Then
            Return
        End If

        Dim ept As OpenFDAApiEndPoints = DirectCast([Enum].Parse(GetType(OpenFDAApiEndPoints), cbEndPoints_2.Text), OpenFDAApiEndPoints)

        Dim fda As New OpenFDA


        Dim result As Object
        Dim searchField As String = cbSearchField.Text.ToLower
        Dim searchFieldValue As String = tbSearchFieldValue.Text
        Dim maxSize As Integer = spMaxResultSize_2.Value

        If searchField.ToLower = "-no filter-" Then
        ElseIf Not searchField.ToLower = "-no filter-" AndAlso String.IsNullOrEmpty(searchFieldValue) Then
            fda.SearchFieldExists(searchField)
        Else
            fda.SearchOnFieldByValue(searchField, searchFieldValue)
        End If

        Dim onGoningOnly As Boolean = ckOngoing_2.Checked

        If ckSpecifyRecordFetch.Checked Then
            maxSize = 0
        End If

        Dim apiUrl As String = fda.buildUrl(ept, maxSize, onGoningOnly)

        If ckSpecifyRecordFetch.Checked Then
            If ckOngoing_2.Checked Then
                If Not apiUrl.Contains("&search") Then
                    apiUrl += "&search=status:""ongoing"""
                End If
            End If
            apiUrl += String.Format("&limit=1&skip={0}", spSpecifyRecordFetch.Value - 1)
        End If

        tbExactURL_2.Text = apiUrl
        result = fda.Execute(apiUrl)

        If cknToLineFeed.Checked Then
            RichTextBox1.Text = result.ToString.Replace("\n", vbLf)
        Else
            RichTextBox1.Text = result
        End If

        'If searchFieldValue.Length > 1 Then
        FindAndHighlightText(RichTextBox1, searchFieldValue, Color.Red, False)
        'End If

        FindAndHighlightText(RichTextBox1, searchField, Color.Blue, True)

    End Sub

    Private Sub FindAndHighlightText(ByRef rtb As RichTextBox, ByVal keyword As String, ByVal color As System.Drawing.Color, Optional ByVal makeItBold As Boolean = False)

        Dim index As Integer = 0
        Do While index > -1

            ' index = rtb.Find(keyword, index, RichTextBoxFinds.None)
            index = rtb.Find(keyword, index, RichTextBoxFinds.WholeWord)

            If index > -1 Then
                rtb.Select(index, keyword.Length)
                rtb.SelectionColor = color
                If makeItBold Then
                    rtb.SelectionFont = New Font(rtb.SelectionFont, FontStyle.Bold)
                End If

                index += keyword.Length
            End If

        Loop

    End Sub

    Private Sub cbEndPoints_2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbEndPoints_2.SelectedIndexChanged

        Dim tmpendPointType As String = cbEndPoints_2.Text
        Dim ept As OpenFDAApiEndPoints

        'Private Function cnvStringToEndPointType() As OpenFDAApiEndPoints
        ept = cnvStringToEndPointType(tmpendPointType)

        RichTextBox1.Text = String.Empty
        tbSearchFieldValue.Text = String.Empty
        cbSearchField.Items.Clear()
        Dim tmpItemList As List(Of String)

        Select Case ept
            Case OpenFDAApiEndPoints.DrugEvent
                tmpItemList = New List(Of String)({"safetyreport",
                                                   "safetyreportversion",
                                                   "receivedate",
                                                   "receivedateformat",
                                                   "receiptdate",
                                                   "receiptdateformat",
                                                   "serious",
                                                   "seriousnesscongenitalanomali",
                                                   "seriousnessdeath",
                                                   "seriousnessdisabling",
                                                   "seriousnesshospitalization",
                                                   "seriousnesslifethreatening",
                                                   "seriousnessother",
                                                   "transmissiondate",
                                                   "transmissiondateformat",
                                                   "duplicate",
                                                   "companynumb",
                                                   "occurcountry",
                                                   "primarysourcecountry",
                                                   "primarysource.qualification",
                                                   "primarysource.reportercountry",
                                                   "reportduplicate.duplicatesource",
                                                   "reportduplicate.duplicatenumb",
                                                   "sender.sendertype",
                                                   "sender.senderorganization",
                                                   "receiver.receivertype",
                                                   "receiver.receiverorganization",
                                                   "patient.patientonsetage",
                                                   "patient.patientonsetageunit",
                                                   "patient.patientsex",
                                                   "patient.patientweight",
                                                   "patient.patientdeath",
                                                   "patient.patientdeath.patientdeathdate",
                                                   "patient.patientdeath.patientdeathdateformat",
                                                   "patient.reaction.reactionmeddrapt",
                                                   "patient.reaction.reactionmeddraversionpt",
                                                   "patient.reaction.reactionoutcome",
                                                   "patient.drug.actiondrug",
                                                   "patient.drug.drugauthorizationnumb",
                                                   "patient.drug.drugcharacterization"
                                           })

            Case OpenFDAApiEndPoints.FoodRecall, OpenFDAApiEndPoints.DrugRecall, OpenFDAApiEndPoints.DeviceRecall
                tmpItemList = New List(Of String)({"reason_for_recall",
                                                   "status",
                                                   "distribution_pattern",
                                                   "product_quantity",
                                                   "recall_initiation_date",
                                                   "state",
                                                   "event_id",
                                                   "product_type",
                                                   "product_description",
                                                   "country",
                                                   "city",
                                                   "recalling_firm",
                                                   "report_date",
                                                   "voluntary_mandated",
                                                   "classification",
                                                   "code_info",
                                                   "openfda",
                                                   "initial_firm_notification"
                                                  })

                'Case OpenFDAApiEndPoints.DrugLabel
            Case OpenFDAApiEndPoints.DeviceEvent
                tmpItemList = New List(Of String)({"adverse_event_flag",
                                                   "product_problem_flag",
                                                   "date_of_event",
                                                   "date_report",
                                                   "date_received",
                                                   "number_devices_in_event",
                                                   "number_patients_in_event",
                                                   "report_number",
                                                   "report_source_code",
                                                   "health_professional",
                                                   "reporter_occupation_code",
                                                   "initial_report_to_fda",
                                                   "reprocessed_and_reused_flag",
                                                   "device_sequence_number",
                                                   "device_event_key",
                                                   "date_received",
                                                   "brand_name",
                                                   "generic_name",
                                                   "device_report_product_code",
                                                   "model_number",
                                                   "catalog_number",
                                                   "lot_number",
                                                   "other_id_number",
                                                   "expiration_date_of_device",
                                                   "device_age_text",
                                                   "device_availability",
                                                   "date_returned_to_manufacturer",
                                                   "device_evaluated_by_manufacturer",
                                                   "device_operator",
                                                   "implant_flag",
                                                   "date_removed_flag",
                                                   "manufacturer_d_name",
                                                   "manufacturer_d_address_1",
                                                   "manufacturer_d_address_2",
                                                   "manufacturer_d_city",
                                                   "manufacturer_d_state",
                                                   "manufacturer_d_country",
                                                   "manufacturer_d_zip_code",
                                                   "manufacturer_d_zip_code_ext",
                                                   "manufacturer_d_postal_code",
                                                   "patient_sequence_number",
                                                   "date_received",
                                                   "sequence_number_treatment",
                                                   "sequence_number_outcome",
                                                   "patient_sequence_number",
                                                   "text_type_code",
                                                   "text",
                                                   "mdr_text_key",
                                                   "date_received",
                                                   "type_of_report",
                                                   "date_facility_aware",
                                                   "report_date",
                                                   "report_to_fda",
                                                   "date_report_to_fda",
                                                   "report_to_manufacturer",
                                                   "date_report_to_manufacturer",
                                                   "event_location",
                                                   "distributor_name",
                                                   "distributor_address_1",
                                                   "distributor_address_2",
                                                   "distributor_city",
                                                   "distributor_state",
                                                   "distributor_zip_code",
                                                   "distributor_zip_code_ext",
                                                   "manufacturer_name",
                                                   "manufacturer_address1",
                                                   "manufacturer_address2",
                                                   "manufacturer_city",
                                                   "manufacturer_state",
                                                   "manufacturer_zip_code",
                                                   "manufacturer_zip_code_ext",
                                                   "manufacturer_country",
                                                   "manufacturer_postal_code",
                                                   "event_type",
                                                   "device_date_of_manufacture",
                                                   "single_use_flag",
                                                   "previous_use_code",
                                                   "remedial_action",
                                                   "removal_correction_number",
                                                   "manufacturer_contact_t_name",
                                                   "manufacturer_contact_f_name",
                                                   "manufacturer_contact_l_name",
                                                   "manufacturer_contact_street_1",
                                                   "manufacturer_contact_street_2",
                                                   "manufacturer_contact_city",
                                                   "manufacturer_contact_state",
                                                   "manufacturer_contact_zip_code",
                                                   "manufacturer_contact_zip_ext",
                                                   "manufacturer_contact_postal",
                                                   "manufacturer_contact_country",
                                                   "manufacturer_contact_pcountry",
                                                   "manufacturer_contact_area_code",
                                                   "manufacturer_contact_exchange",
                                                   "manufacturer_contact_extension",
                                                   "manufacturer_contact_pcity",
                                                   "manufacturer_contact_phone_number",
                                                   "manufacturer_contact_plocal",
                                                   "manufacturer_g1_name",
                                                   "manufacturer_g1_street_1",
                                                   "manufacturer_g1_street_2",
                                                   "manufacturer_g1_city",
                                                   "manufacturer_g1_state",
                                                   "manufacturer_g1_zip_code",
                                                   "manufacturer_g1_zip_ext",
                                                   "manufacturer_g1_postal_code",
                                                   "manufacturer_g1_country",
                                                   "date_manufacturer_received",
                                                   "source_type",
                                                   "event_key",
                                                   "mdr_report_key",
                                                   "manufacturer_link_flag_"
                                                  })
                'Case OpenFDAApiEndPoints.DeviceRecall
                'Case OpenFDAApiEndPoints.FoodRecall
            Case Else
                'ept = Nothing
                tmpItemList = New List(Of String)
                cbSearchField.Items.Add("")
                cbSearchField.Items.Clear()
        End Select
        tmpItemList.Insert(0, "-No Filter-")

        For Each itm As String In tmpItemList
            cbSearchField.Items.Add(itm)
        Next

        cbSearchField.SelectedIndex = 0
        Me.RichTextBox1.Text = String.Empty

    End Sub

    Private Function cnvStringToEndPointType(tmpendPointType As String) As OpenFDAApiEndPoints

        Dim ept As OpenFDAApiEndPoints

        'TODO enumerate thur enum
        'check check string value to enum.toString, GetEnumDescript(), GetEnumDefault
        ' If matched then ept = value

        Select Case tmpendPointType
            Case "drug/event", "DrugEvent", "drugevent", "Drug Event", "drug event"
                ept = OpenFDAApiEndPoints.DrugEvent
            Case "drug/enforcement", "DrugRecall", "drugrecall", "Drug Recall", "drug recall"
                ept = OpenFDAApiEndPoints.DrugRecall
            Case "drug/label", "DrugLabel", "druglabel", "Drug Label", "drug label"
                ept = OpenFDAApiEndPoints.DrugLabel
            Case "device/event", "DeviceEvent", "deviceevent", "Device Event", "device event"
                ept = OpenFDAApiEndPoints.DeviceEvent
            Case "device/enforcement", "DeviceRecall", "devicerecall", "Device Recall", "device recall"
                ept = OpenFDAApiEndPoints.DeviceRecall
            Case "food/enforcement", "FoodRecall", "foodrecall", "Food Recall", "food recall"
                ept = OpenFDAApiEndPoints.FoodRecall
            Case Else
                ept = Nothing
        End Select

        Return ept

    End Function


    Private Sub ckSpecifyRecordFetch_CheckedChanged(sender As Object, e As EventArgs) Handles ckSpecifyRecordFetch.CheckedChanged

        Dim ck As CheckBox = CType(sender, CheckBox)
        If Not ck.Checked Then
            spSpecifyRecordFetch.Value = 1
        End If

        spSpecifyRecordFetch.Visible = ck.Checked
        lbSpecifyRecordFetch.Visible = ck.Checked

        Label9.Visible = Not ck.Checked
        spMaxResultSize_2.Visible = Not ck.Checked

    End Sub

    Private Sub spSpecifyRecordFetch_ValueChanged(sender As Object, e As EventArgs) Handles spSpecifyRecordFetch.ValueChanged
        btSearch_2_Click(Nothing, Nothing)
    End Sub

    Private Sub cbSearchField_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSearchField.SelectedIndexChanged
        tbSearchFieldValue.Text = String.Empty
        btSearch_2_Click(Nothing, Nothing)
    End Sub

    Private Sub tbSearchFieldValue_KeyDown(sender As Object, e As KeyEventArgs) Handles tbSearchFieldValue.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            'Put whatever you want here.  
            'btAddToShoppingList_Click(sender, e)
            btSearch_2_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub spMaxResultSize_2_ValueChanged(sender As Object, e As EventArgs) Handles spMaxResultSize_2.ValueChanged
        btSearch_2_Click(Nothing, Nothing)
    End Sub

#End Region

End Class
