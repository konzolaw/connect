Imports GMap.NET
Imports GMap.NET.WindowsForms
Imports Mapsui.UI

Public Class Gotosearch
    Private Sub Gotosearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up the GMapControl
        ' GMapControl1.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance
        ' GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache
        'GMapControl1.SetPositionByKeywords("Nairobi, kenya") ' Set initial map position

        ' Optional: Customize map settings (zoom level, etc.)
        'GMapControl1.Zoom = 10
    End Sub





    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim RefferalForm As New RefferalForm()

        ' Show Form1 and hide the current form
        RefferalForm.Show()
        Me.Hide()
    End Sub


    '  Private Sub InitializeComponent()
    ' Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
    '  SuspendLayout()
    ' 
    ' Form1
    ' 
    '   resources.ApplyResources(Me, "$this")
    '   Name = "Gotosearch"
    '  ResumeLayout(False)

    ' End Sub
End Class