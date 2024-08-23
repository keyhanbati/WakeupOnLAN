<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.txtMac = New System.Windows.Forms.TextBox()
        Me.lblMac = New System.Windows.Forms.Label()
        Me.btnTrunOn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtMac
        '
        Me.txtMac.Location = New System.Drawing.Point(78, 13)
        Me.txtMac.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMac.Name = "txtMac"
        Me.txtMac.Size = New System.Drawing.Size(134, 27)
        Me.txtMac.TabIndex = 0
        Me.txtMac.Text = "F0-2F-74-DC-58-5E"
        '
        'lblMac
        '
        Me.lblMac.AutoSize = True
        Me.lblMac.Location = New System.Drawing.Point(18, 17)
        Me.lblMac.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMac.Name = "lblMac"
        Me.lblMac.Size = New System.Drawing.Size(41, 20)
        Me.lblMac.TabIndex = 1
        Me.lblMac.Text = "MAC"
        '
        'btnTrunOn
        '
        Me.btnTrunOn.Location = New System.Drawing.Point(234, 10)
        Me.btnTrunOn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTrunOn.Name = "btnTrunOn"
        Me.btnTrunOn.Size = New System.Drawing.Size(100, 35)
        Me.btnTrunOn.TabIndex = 2
        Me.btnTrunOn.Text = "Turn on"
        Me.btnTrunOn.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(347, 59)
        Me.Controls.Add(Me.btnTrunOn)
        Me.Controls.Add(Me.lblMac)
        Me.Controls.Add(Me.txtMac)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Wakeup On LAN"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMac As System.Windows.Forms.TextBox
    Friend WithEvents lblMac As System.Windows.Forms.Label
    Friend WithEvents btnTrunOn As System.Windows.Forms.Button

End Class
