<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.tbSearchTypeNotes = New System.Windows.Forms.TextBox()
        Me.dtSerchTypeOptions = New System.Windows.Forms.DateTimePicker()
        Me.cbSerchTypeOptions = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.spMaxResultSize = New System.Windows.Forms.NumericUpDown()
        Me.lbMetaResultsTotal = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.tbExactURL = New System.Windows.Forms.TextBox()
        Me.lbEndPoints = New System.Windows.Forms.Label()
        Me.cbEndPoints = New System.Windows.Forms.ComboBox()
        Me.btnAddFilter = New System.Windows.Forms.Button()
        Me.tbFilter = New System.Windows.Forms.TextBox()
        Me.lbFdaUrl = New System.Windows.Forms.Label()
        Me.lbFilterList = New System.Windows.Forms.ListBox()
        Me.lbSearch = New System.Windows.Forms.Label()
        Me.cbFilterTypes = New System.Windows.Forms.ComboBox()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btGetRecallDetails = New System.Windows.Forms.Button()
        Me.tbShopItem = New System.Windows.Forms.TextBox()
        Me.TreeView2 = New System.Windows.Forms.TreeView()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.btCheckShoppingListForRecalls = New System.Windows.Forms.Button()
        Me.btAddToShoppingList = New System.Windows.Forms.Button()
        Me.tbShoppingItem = New System.Windows.Forms.TextBox()
        Me.lbShoppingList = New System.Windows.Forms.ListBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.cknToLineFeed = New System.Windows.Forms.CheckBox()
        Me.ckSpecifyRecordFetch = New System.Windows.Forms.CheckBox()
        Me.lbSpecifyRecordFetch = New System.Windows.Forms.Label()
        Me.spSpecifyRecordFetch = New System.Windows.Forms.NumericUpDown()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.tbExactURL_2 = New System.Windows.Forms.TextBox()
        Me.ckOngoing_2 = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.spMaxResultSize_2 = New System.Windows.Forms.NumericUpDown()
        Me.btSearch_2 = New System.Windows.Forms.Button()
        Me.tbSearchFieldValue = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbSearchField = New System.Windows.Forms.ComboBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cbEndPoints_2 = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.spMaxResultSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.spSpecifyRecordFetch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spMaxResultSize_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(931, 708)
        Me.TabControl1.TabIndex = 13
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.tbSearchTypeNotes)
        Me.TabPage1.Controls.Add(Me.dtSerchTypeOptions)
        Me.TabPage1.Controls.Add(Me.cbSerchTypeOptions)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.spMaxResultSize)
        Me.TabPage1.Controls.Add(Me.lbMetaResultsTotal)
        Me.TabPage1.Controls.Add(Me.btnSearch)
        Me.TabPage1.Controls.Add(Me.tbExactURL)
        Me.TabPage1.Controls.Add(Me.lbEndPoints)
        Me.TabPage1.Controls.Add(Me.cbEndPoints)
        Me.TabPage1.Controls.Add(Me.btnAddFilter)
        Me.TabPage1.Controls.Add(Me.tbFilter)
        Me.TabPage1.Controls.Add(Me.lbFdaUrl)
        Me.TabPage1.Controls.Add(Me.lbFilterList)
        Me.TabPage1.Controls.Add(Me.lbSearch)
        Me.TabPage1.Controls.Add(Me.cbFilterTypes)
        Me.TabPage1.Controls.Add(Me.TreeView1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(923, 682)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "OpenFDA Endpoint Test"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'tbSearchTypeNotes
        '
        Me.tbSearchTypeNotes.Location = New System.Drawing.Point(17, 99)
        Me.tbSearchTypeNotes.Multiline = True
        Me.tbSearchTypeNotes.Name = "tbSearchTypeNotes"
        Me.tbSearchTypeNotes.ReadOnly = True
        Me.tbSearchTypeNotes.Size = New System.Drawing.Size(201, 95)
        Me.tbSearchTypeNotes.TabIndex = 29
        '
        'dtSerchTypeOptions
        '
        Me.dtSerchTypeOptions.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtSerchTypeOptions.Location = New System.Drawing.Point(228, 74)
        Me.dtSerchTypeOptions.Name = "dtSerchTypeOptions"
        Me.dtSerchTypeOptions.Size = New System.Drawing.Size(121, 20)
        Me.dtSerchTypeOptions.TabIndex = 28
        Me.dtSerchTypeOptions.Visible = False
        '
        'cbSerchTypeOptions
        '
        Me.cbSerchTypeOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSerchTypeOptions.FormattingEnabled = True
        Me.cbSerchTypeOptions.Location = New System.Drawing.Point(228, 73)
        Me.cbSerchTypeOptions.Name = "cbSerchTypeOptions"
        Me.cbSerchTypeOptions.Size = New System.Drawing.Size(121, 21)
        Me.cbSerchTypeOptions.TabIndex = 27
        Me.cbSerchTypeOptions.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(484, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Result Size (max)"
        '
        'spMaxResultSize
        '
        Me.spMaxResultSize.Location = New System.Drawing.Point(578, 77)
        Me.spMaxResultSize.Name = "spMaxResultSize"
        Me.spMaxResultSize.Size = New System.Drawing.Size(75, 20)
        Me.spMaxResultSize.TabIndex = 25
        '
        'lbMetaResultsTotal
        '
        Me.lbMetaResultsTotal.AutoSize = True
        Me.lbMetaResultsTotal.Location = New System.Drawing.Point(754, 79)
        Me.lbMetaResultsTotal.Name = "lbMetaResultsTotal"
        Me.lbMetaResultsTotal.Size = New System.Drawing.Size(138, 13)
        Me.lbMetaResultsTotal.TabIndex = 24
        Me.lbMetaResultsTotal.Text = "Result set of api call (count)"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(673, 72)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 23
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'tbExactURL
        '
        Me.tbExactURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbExactURL.Location = New System.Drawing.Point(228, 48)
        Me.tbExactURL.Name = "tbExactURL"
        Me.tbExactURL.Size = New System.Drawing.Size(664, 20)
        Me.tbExactURL.TabIndex = 22
        '
        'lbEndPoints
        '
        Me.lbEndPoints.AutoSize = True
        Me.lbEndPoints.Location = New System.Drawing.Point(12, 48)
        Me.lbEndPoints.Name = "lbEndPoints"
        Me.lbEndPoints.Size = New System.Drawing.Size(58, 13)
        Me.lbEndPoints.TabIndex = 21
        Me.lbEndPoints.Text = "End Points"
        '
        'cbEndPoints
        '
        Me.cbEndPoints.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEndPoints.FormattingEnabled = True
        Me.cbEndPoints.Location = New System.Drawing.Point(97, 45)
        Me.cbEndPoints.Name = "cbEndPoints"
        Me.cbEndPoints.Size = New System.Drawing.Size(121, 21)
        Me.cbEndPoints.TabIndex = 20
        '
        'btnAddFilter
        '
        Me.btnAddFilter.Location = New System.Drawing.Point(360, 71)
        Me.btnAddFilter.Name = "btnAddFilter"
        Me.btnAddFilter.Size = New System.Drawing.Size(75, 23)
        Me.btnAddFilter.TabIndex = 19
        Me.btnAddFilter.Text = "Add"
        Me.btnAddFilter.UseVisualStyleBackColor = True
        '
        'tbFilter
        '
        Me.tbFilter.Location = New System.Drawing.Point(228, 73)
        Me.tbFilter.Name = "tbFilter"
        Me.tbFilter.Size = New System.Drawing.Size(121, 20)
        Me.tbFilter.TabIndex = 18
        '
        'lbFdaUrl
        '
        Me.lbFdaUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbFdaUrl.Location = New System.Drawing.Point(14, 19)
        Me.lbFdaUrl.Name = "lbFdaUrl"
        Me.lbFdaUrl.Size = New System.Drawing.Size(903, 23)
        Me.lbFdaUrl.TabIndex = 17
        Me.lbFdaUrl.Text = "Open FDA API Url"
        '
        'lbFilterList
        '
        Me.lbFilterList.FormattingEnabled = True
        Me.lbFilterList.Location = New System.Drawing.Point(228, 99)
        Me.lbFilterList.Name = "lbFilterList"
        Me.lbFilterList.Size = New System.Drawing.Size(207, 95)
        Me.lbFilterList.TabIndex = 16
        '
        'lbSearch
        '
        Me.lbSearch.AutoSize = True
        Me.lbSearch.Location = New System.Drawing.Point(14, 74)
        Me.lbSearch.Name = "lbSearch"
        Me.lbSearch.Size = New System.Drawing.Size(68, 13)
        Me.lbSearch.TabIndex = 15
        Me.lbSearch.Text = "Search Type"
        '
        'cbFilterTypes
        '
        Me.cbFilterTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterTypes.FormattingEnabled = True
        Me.cbFilterTypes.Location = New System.Drawing.Point(97, 72)
        Me.cbFilterTypes.Name = "cbFilterTypes"
        Me.cbFilterTypes.Size = New System.Drawing.Size(121, 21)
        Me.cbFilterTypes.TabIndex = 14
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.Location = New System.Drawing.Point(8, 212)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(907, 462)
        Me.TreeView1.TabIndex = 13
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Button2)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.btGetRecallDetails)
        Me.TabPage2.Controls.Add(Me.tbShopItem)
        Me.TabPage2.Controls.Add(Me.TreeView2)
        Me.TabPage2.Controls.Add(Me.NumericUpDown1)
        Me.TabPage2.Controls.Add(Me.btCheckShoppingListForRecalls)
        Me.TabPage2.Controls.Add(Me.btAddToShoppingList)
        Me.TabPage2.Controls.Add(Me.tbShoppingItem)
        Me.TabPage2.Controls.Add(Me.lbShoppingList)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(923, 682)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Shopping List"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 127)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Resuts:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(514, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(149, 13)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "of selected Shopping List Item"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(403, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(177, 13)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Double Click to remove item from list"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(213, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 13)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Current Shopping List:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "New Item:"
        '
        'btGetRecallDetails
        '
        Me.btGetRecallDetails.Location = New System.Drawing.Point(406, 102)
        Me.btGetRecallDetails.Name = "btGetRecallDetails"
        Me.btGetRecallDetails.Size = New System.Drawing.Size(102, 23)
        Me.btGetRecallDetails.TabIndex = 33
        Me.btGetRecallDetails.Text = "Get Recall Details"
        Me.btGetRecallDetails.UseVisualStyleBackColor = True
        '
        'tbShopItem
        '
        Me.tbShopItem.Location = New System.Drawing.Point(406, 76)
        Me.tbShopItem.Name = "tbShopItem"
        Me.tbShopItem.Size = New System.Drawing.Size(100, 20)
        Me.tbShopItem.TabIndex = 32
        '
        'TreeView2
        '
        Me.TreeView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView2.Location = New System.Drawing.Point(6, 153)
        Me.TreeView2.Name = "TreeView2"
        Me.TreeView2.Size = New System.Drawing.Size(909, 523)
        Me.TreeView2.TabIndex = 31
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(840, 125)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(75, 20)
        Me.NumericUpDown1.TabIndex = 30
        '
        'btCheckShoppingListForRecalls
        '
        Me.btCheckShoppingListForRecalls.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btCheckShoppingListForRecalls.Location = New System.Drawing.Point(751, 29)
        Me.btCheckShoppingListForRecalls.Name = "btCheckShoppingListForRecalls"
        Me.btCheckShoppingListForRecalls.Size = New System.Drawing.Size(118, 23)
        Me.btCheckShoppingListForRecalls.TabIndex = 29
        Me.btCheckShoppingListForRecalls.Text = "Check Shopping List"
        Me.btCheckShoppingListForRecalls.UseVisualStyleBackColor = True
        '
        'btAddToShoppingList
        '
        Me.btAddToShoppingList.Location = New System.Drawing.Point(121, 30)
        Me.btAddToShoppingList.Name = "btAddToShoppingList"
        Me.btAddToShoppingList.Size = New System.Drawing.Size(75, 23)
        Me.btAddToShoppingList.TabIndex = 28
        Me.btAddToShoppingList.Text = "Add"
        Me.btAddToShoppingList.UseVisualStyleBackColor = True
        '
        'tbShoppingItem
        '
        Me.tbShoppingItem.Location = New System.Drawing.Point(15, 32)
        Me.tbShoppingItem.Name = "tbShoppingItem"
        Me.tbShoppingItem.Size = New System.Drawing.Size(100, 20)
        Me.tbShoppingItem.TabIndex = 27
        '
        'lbShoppingList
        '
        Me.lbShoppingList.FormattingEnabled = True
        Me.lbShoppingList.Location = New System.Drawing.Point(216, 32)
        Me.lbShoppingList.Name = "lbShoppingList"
        Me.lbShoppingList.Size = New System.Drawing.Size(181, 108)
        Me.lbShoppingList.TabIndex = 26
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.cknToLineFeed)
        Me.TabPage3.Controls.Add(Me.ckSpecifyRecordFetch)
        Me.TabPage3.Controls.Add(Me.lbSpecifyRecordFetch)
        Me.TabPage3.Controls.Add(Me.spSpecifyRecordFetch)
        Me.TabPage3.Controls.Add(Me.Button1)
        Me.TabPage3.Controls.Add(Me.tbExactURL_2)
        Me.TabPage3.Controls.Add(Me.ckOngoing_2)
        Me.TabPage3.Controls.Add(Me.Label9)
        Me.TabPage3.Controls.Add(Me.spMaxResultSize_2)
        Me.TabPage3.Controls.Add(Me.btSearch_2)
        Me.TabPage3.Controls.Add(Me.tbSearchFieldValue)
        Me.TabPage3.Controls.Add(Me.Label8)
        Me.TabPage3.Controls.Add(Me.cbSearchField)
        Me.TabPage3.Controls.Add(Me.RichTextBox1)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.cbEndPoints_2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(923, 682)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'cknToLineFeed
        '
        Me.cknToLineFeed.AutoSize = True
        Me.cknToLineFeed.Checked = True
        Me.cknToLineFeed.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cknToLineFeed.Location = New System.Drawing.Point(834, 32)
        Me.cknToLineFeed.Name = "cknToLineFeed"
        Me.cknToLineFeed.Size = New System.Drawing.Size(93, 17)
        Me.cknToLineFeed.TabIndex = 37
        Me.cknToLineFeed.Text = "\n > LineFeed"
        Me.cknToLineFeed.UseVisualStyleBackColor = True
        '
        'ckSpecifyRecordFetch
        '
        Me.ckSpecifyRecordFetch.AutoSize = True
        Me.ckSpecifyRecordFetch.Location = New System.Drawing.Point(485, 56)
        Me.ckSpecifyRecordFetch.Name = "ckSpecifyRecordFetch"
        Me.ckSpecifyRecordFetch.Size = New System.Drawing.Size(129, 17)
        Me.ckSpecifyRecordFetch.TabIndex = 36
        Me.ckSpecifyRecordFetch.Text = "Specify Record Fetch"
        Me.ckSpecifyRecordFetch.UseVisualStyleBackColor = True
        '
        'lbSpecifyRecordFetch
        '
        Me.lbSpecifyRecordFetch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbSpecifyRecordFetch.AutoSize = True
        Me.lbSpecifyRecordFetch.Location = New System.Drawing.Point(622, 57)
        Me.lbSpecifyRecordFetch.Name = "lbSpecifyRecordFetch"
        Me.lbSpecifyRecordFetch.Size = New System.Drawing.Size(52, 13)
        Me.lbSpecifyRecordFetch.TabIndex = 35
        Me.lbSpecifyRecordFetch.Text = "Record #"
        Me.lbSpecifyRecordFetch.Visible = False
        '
        'spSpecifyRecordFetch
        '
        Me.spSpecifyRecordFetch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spSpecifyRecordFetch.Location = New System.Drawing.Point(680, 55)
        Me.spSpecifyRecordFetch.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.spSpecifyRecordFetch.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spSpecifyRecordFetch.Name = "spSpecifyRecordFetch"
        Me.spSpecifyRecordFetch.Size = New System.Drawing.Size(75, 20)
        Me.spSpecifyRecordFetch.TabIndex = 34
        Me.spSpecifyRecordFetch.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spSpecifyRecordFetch.Visible = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(834, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(81, 23)
        Me.Button1.TabIndex = 33
        Me.Button1.Text = "URL Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'tbExactURL_2
        '
        Me.tbExactURL_2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbExactURL_2.Location = New System.Drawing.Point(3, 2)
        Me.tbExactURL_2.Name = "tbExactURL_2"
        Me.tbExactURL_2.Size = New System.Drawing.Size(825, 20)
        Me.tbExactURL_2.TabIndex = 32
        '
        'ckOngoing_2
        '
        Me.ckOngoing_2.AutoSize = True
        Me.ckOngoing_2.Checked = True
        Me.ckOngoing_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckOngoing_2.Location = New System.Drawing.Point(485, 30)
        Me.ckOngoing_2.Name = "ckOngoing_2"
        Me.ckOngoing_2.Size = New System.Drawing.Size(90, 17)
        Me.ckOngoing_2.TabIndex = 31
        Me.ckOngoing_2.Text = "Ongoing Only"
        Me.ckOngoing_2.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(586, 31)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 13)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "Result Size (max)"
        '
        'spMaxResultSize_2
        '
        Me.spMaxResultSize_2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spMaxResultSize_2.Location = New System.Drawing.Point(680, 29)
        Me.spMaxResultSize_2.Name = "spMaxResultSize_2"
        Me.spMaxResultSize_2.Size = New System.Drawing.Size(75, 20)
        Me.spMaxResultSize_2.TabIndex = 29
        '
        'btSearch_2
        '
        Me.btSearch_2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btSearch_2.Location = New System.Drawing.Point(834, 53)
        Me.btSearch_2.Name = "btSearch_2"
        Me.btSearch_2.Size = New System.Drawing.Size(81, 23)
        Me.btSearch_2.TabIndex = 28
        Me.btSearch_2.Text = "Search"
        Me.btSearch_2.UseVisualStyleBackColor = True
        '
        'tbSearchFieldValue
        '
        Me.tbSearchFieldValue.Location = New System.Drawing.Point(228, 56)
        Me.tbSearchFieldValue.Name = "tbSearchFieldValue"
        Me.tbSearchFieldValue.Size = New System.Drawing.Size(241, 20)
        Me.tbSearchFieldValue.TabIndex = 27
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 56)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Search Type"
        '
        'cbSearchField
        '
        Me.cbSearchField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearchField.FormattingEnabled = True
        Me.cbSearchField.Location = New System.Drawing.Point(101, 54)
        Me.cbSearchField.Name = "cbSearchField"
        Me.cbSearchField.Size = New System.Drawing.Size(121, 21)
        Me.cbSearchField.TabIndex = 25
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.Location = New System.Drawing.Point(3, 87)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(912, 587)
        Me.RichTextBox1.TabIndex = 24
        Me.RichTextBox1.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "End Points"
        '
        'cbEndPoints_2
        '
        Me.cbEndPoints_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEndPoints_2.FormattingEnabled = True
        Me.cbEndPoints_2.Location = New System.Drawing.Point(101, 25)
        Me.cbEndPoints_2.Name = "cbEndPoints_2"
        Me.cbEndPoints_2.Size = New System.Drawing.Size(121, 21)
        Me.cbEndPoints_2.TabIndex = 22
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(751, 74)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(118, 23)
        Me.Button2.TabIndex = 40
        Me.Button2.Text = "FrontPage"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(931, 708)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.spMaxResultSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.spSpecifyRecordFetch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spMaxResultSize_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents spMaxResultSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbMetaResultsTotal As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents tbExactURL As System.Windows.Forms.TextBox
    Friend WithEvents lbEndPoints As System.Windows.Forms.Label
    Friend WithEvents cbEndPoints As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddFilter As System.Windows.Forms.Button
    Friend WithEvents tbFilter As System.Windows.Forms.TextBox
    Friend WithEvents lbFdaUrl As System.Windows.Forms.Label
    Friend WithEvents lbFilterList As System.Windows.Forms.ListBox
    Friend WithEvents lbSearch As System.Windows.Forms.Label
    Friend WithEvents cbFilterTypes As System.Windows.Forms.ComboBox
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TreeView2 As System.Windows.Forms.TreeView
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents btCheckShoppingListForRecalls As System.Windows.Forms.Button
    Friend WithEvents btAddToShoppingList As System.Windows.Forms.Button
    Friend WithEvents tbShoppingItem As System.Windows.Forms.TextBox
    Friend WithEvents lbShoppingList As System.Windows.Forms.ListBox
    Friend WithEvents btGetRecallDetails As System.Windows.Forms.Button
    Friend WithEvents tbShopItem As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbSerchTypeOptions As System.Windows.Forms.ComboBox
    Friend WithEvents dtSerchTypeOptions As System.Windows.Forms.DateTimePicker
    Friend WithEvents tbSearchTypeNotes As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents btSearch_2 As System.Windows.Forms.Button
    Friend WithEvents tbSearchFieldValue As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbSearchField As System.Windows.Forms.ComboBox
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbEndPoints_2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents spMaxResultSize_2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ckOngoing_2 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents tbExactURL_2 As System.Windows.Forms.TextBox
    Friend WithEvents ckSpecifyRecordFetch As System.Windows.Forms.CheckBox
    Friend WithEvents lbSpecifyRecordFetch As System.Windows.Forms.Label
    Friend WithEvents spSpecifyRecordFetch As System.Windows.Forms.NumericUpDown
    Friend WithEvents cknToLineFeed As System.Windows.Forms.CheckBox
    Friend WithEvents Button2 As System.Windows.Forms.Button

End Class
