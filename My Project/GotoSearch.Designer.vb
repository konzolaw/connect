<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GotoSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Gotosearch))
        Panel3 = New Panel()
        ComboBox1 = New ComboBox()
        Button14 = New Button()
        TextBox8 = New TextBox()
        TextBox7 = New TextBox()
        ComboBox6 = New ComboBox()
        Label8 = New Label()
        Label6 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Button8 = New Button()
        Splitter1 = New Splitter()
        Splitter2 = New Splitter()
        LinkLabel1 = New LinkLabel()
        DataGridView1 = New DataGridView()
        HospitalName = New DataGridViewTextBoxColumn()
        HospitalUHID = New DataGridViewTextBoxColumn()
        HospitalAddress = New DataGridViewTextBoxColumn()
        ServicesOffered = New DataGridViewTextBoxColumn()
        PictureBox1 = New PictureBox()
        Label1 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label7 = New Label()
        LinkLabel2 = New LinkLabel()
        Panel3.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.PaleTurquoise
        Panel3.Controls.Add(Label7)
        Panel3.Controls.Add(Label5)
        Panel3.Controls.Add(Label4)
        Panel3.Controls.Add(Label1)
        Panel3.Controls.Add(ComboBox1)
        Panel3.Controls.Add(Button14)
        Panel3.Controls.Add(TextBox8)
        Panel3.Controls.Add(TextBox7)
        Panel3.Controls.Add(ComboBox6)
        Panel3.Controls.Add(Label8)
        Panel3.Controls.Add(Label6)
        Panel3.Controls.Add(Label3)
        Panel3.Controls.Add(Label2)
        Panel3.Controls.Add(Button8)
        resources.ApplyResources(Panel3, "Panel3")
        Panel3.Name = "Panel3"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        resources.ApplyResources(ComboBox1, "ComboBox1")
        ComboBox1.Name = "ComboBox1"
        ' 
        ' Button14
        ' 
        Button14.BackColor = SystemColors.ActiveCaption
        resources.ApplyResources(Button14, "Button14")
        Button14.Name = "Button14"
        Button14.UseVisualStyleBackColor = False
        ' 
        ' TextBox8
        ' 
        resources.ApplyResources(TextBox8, "TextBox8")
        TextBox8.Name = "TextBox8"
        ' 
        ' TextBox7
        ' 
        resources.ApplyResources(TextBox7, "TextBox7")
        TextBox7.Name = "TextBox7"
        ' 
        ' ComboBox6
        ' 
        ComboBox6.FormattingEnabled = True
        resources.ApplyResources(ComboBox6, "ComboBox6")
        ComboBox6.Name = "ComboBox6"
        ' 
        ' Label8
        ' 
        resources.ApplyResources(Label8, "Label8")
        Label8.Name = "Label8"
        ' 
        ' Label6
        ' 
        resources.ApplyResources(Label6, "Label6")
        Label6.Name = "Label6"
        ' 
        ' Label3
        ' 
        resources.ApplyResources(Label3, "Label3")
        Label3.Name = "Label3"
        ' 
        ' Label2
        ' 
        resources.ApplyResources(Label2, "Label2")
        Label2.Name = "Label2"
        ' 
        ' Button8
        ' 
        Button8.BackColor = Color.Transparent
        resources.ApplyResources(Button8, "Button8")
        Button8.Name = "Button8"
        Button8.UseVisualStyleBackColor = False
        ' 
        ' Splitter1
        ' 
        resources.ApplyResources(Splitter1, "Splitter1")
        Splitter1.Name = "Splitter1"
        Splitter1.TabStop = False
        ' 
        ' Splitter2
        ' 
        resources.ApplyResources(Splitter2, "Splitter2")
        Splitter2.Name = "Splitter2"
        Splitter2.TabStop = False
        ' 
        ' LinkLabel1
        ' 
        resources.ApplyResources(LinkLabel1, "LinkLabel1")
        LinkLabel1.Name = "LinkLabel1"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {HospitalName, HospitalUHID, HospitalAddress, ServicesOffered})
        resources.ApplyResources(DataGridView1, "DataGridView1")
        DataGridView1.Name = "DataGridView1"
        ' 
        ' HospitalName
        ' 
        resources.ApplyResources(HospitalName, "HospitalName")
        HospitalName.Name = "HospitalName"
        ' 
        ' HospitalUHID
        ' 
        resources.ApplyResources(HospitalUHID, "HospitalUHID")
        HospitalUHID.Name = "HospitalUHID"
        ' 
        ' HospitalAddress
        ' 
        resources.ApplyResources(HospitalAddress, "HospitalAddress")
        HospitalAddress.Name = "HospitalAddress"
        ' 
        ' ServicesOffered
        ' 
        resources.ApplyResources(ServicesOffered, "ServicesOffered")
        ServicesOffered.Name = "ServicesOffered"
        ' 
        ' PictureBox1
        ' 
        resources.ApplyResources(PictureBox1, "PictureBox1")
        PictureBox1.Name = "PictureBox1"
        PictureBox1.TabStop = False
        ' 
        ' Label1
        ' 
        resources.ApplyResources(Label1, "Label1")
        Label1.Name = "Label1"
        ' 
        ' Label4
        ' 
        resources.ApplyResources(Label4, "Label4")
        Label4.Name = "Label4"
        ' 
        ' Label5
        ' 
        resources.ApplyResources(Label5, "Label5")
        Label5.Name = "Label5"
        ' 
        ' Label7
        ' 
        resources.ApplyResources(Label7, "Label7")
        Label7.Name = "Label7"
        ' 
        ' LinkLabel2
        ' 
        resources.ApplyResources(LinkLabel2, "LinkLabel2")
        LinkLabel2.Name = "LinkLabel2"
        LinkLabel2.TabStop = True
        ' 
        ' Gotosearch
        ' 
        AllowDrop = True
        resources.ApplyResources(Me, "$this")
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.MediumTurquoise
        Controls.Add(LinkLabel2)
        Controls.Add(PictureBox1)
        Controls.Add(DataGridView1)
        Controls.Add(LinkLabel1)
        Controls.Add(Splitter2)
        Controls.Add(Splitter1)
        Controls.Add(Panel3)
        FormBorderStyle = FormBorderStyle.None
        Name = "Gotosearch"
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button14 As Button
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents ComboBox6 As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button8 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents Splitter2 As Splitter
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents HospitalName As DataGridViewTextBoxColumn
    Friend WithEvents HospitalUHID As DataGridViewTextBoxColumn
    Friend WithEvents HospitalAddress As DataGridViewTextBoxColumn
    Friend WithEvents ServicesOffered As DataGridViewTextBoxColumn
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LinkLabel2 As LinkLabel
End Class
