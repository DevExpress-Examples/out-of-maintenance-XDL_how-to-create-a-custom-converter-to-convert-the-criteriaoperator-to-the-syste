Imports System
Imports System.Data
Imports System.Windows.Forms

Namespace CriteriaVisitorExample
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            CreateData()
        End Sub

        Private Sub CreateData()
            Dim data As New DataTable()
            data.Columns.Add("ColumnA")
            data.Columns.Add("ColumnB", GetType(Integer))
            data.Columns.Add("ColumnC", GetType(Date))
            filterControl1.SourceControl = data
        End Sub

        Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
            labelControl1.Text = CustomCriteriaToStringParser.Process(filterControl1.FilterCriteria)
        End Sub
    End Class
End Namespace