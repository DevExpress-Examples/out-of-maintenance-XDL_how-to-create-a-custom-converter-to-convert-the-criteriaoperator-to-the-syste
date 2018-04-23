Imports Microsoft.VisualBasic
Imports System
Namespace CriteriaVisitorExample
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.filterControl1 = New DevExpress.XtraEditors.FilterControl()
			Me.panelControl1 = New DevExpress.XtraEditors.PanelControl()
			Me.simpleButton1 = New DevExpress.XtraEditors.SimpleButton()
			Me.labelControl1 = New DevExpress.XtraEditors.LabelControl()
			CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.panelControl1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' filterControl1
			' 
			Me.filterControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.filterControl1.Location = New System.Drawing.Point(0, 0)
			Me.filterControl1.Name = "filterControl1"
			Me.filterControl1.Size = New System.Drawing.Size(592, 336)
			Me.filterControl1.TabIndex = 0
			' 
			' panelControl1
			' 
			Me.panelControl1.Controls.Add(Me.simpleButton1)
			Me.panelControl1.Controls.Add(Me.labelControl1)
			Me.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.panelControl1.Location = New System.Drawing.Point(0, 336)
			Me.panelControl1.Name = "panelControl1"
			Me.panelControl1.Size = New System.Drawing.Size(592, 35)
			Me.panelControl1.TabIndex = 1
			' 
			' simpleButton1
			' 
			Me.simpleButton1.Location = New System.Drawing.Point(12, 7)
			Me.simpleButton1.Name = "simpleButton1"
			Me.simpleButton1.Size = New System.Drawing.Size(75, 23)
			Me.simpleButton1.TabIndex = 2
			Me.simpleButton1.Text = "&Apply"
'			Me.simpleButton1.Click += New System.EventHandler(Me.simpleButton1_Click);
			' 
			' labelControl1
			' 
			Me.labelControl1.Location = New System.Drawing.Point(93, 17)
			Me.labelControl1.Name = "labelControl1"
			Me.labelControl1.Size = New System.Drawing.Size(0, 13)
			Me.labelControl1.TabIndex = 2
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(592, 371)
			Me.Controls.Add(Me.filterControl1)
			Me.Controls.Add(Me.panelControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
			CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.panelControl1.ResumeLayout(False)
			Me.panelControl1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private filterControl1 As DevExpress.XtraEditors.FilterControl
		Private panelControl1 As DevExpress.XtraEditors.PanelControl
		Private WithEvents simpleButton1 As DevExpress.XtraEditors.SimpleButton
		Private labelControl1 As DevExpress.XtraEditors.LabelControl
	End Class
End Namespace

