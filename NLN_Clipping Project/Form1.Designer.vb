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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.delete_btn = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.clip_btn = New System.Windows.Forms.Button()
        Me.clear_btn = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblx = New System.Windows.Forms.Label()
        Me.lbly = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.load_btn = New System.Windows.Forms.Button()
        Me.save_btn = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.windowClr_btn = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(4, 17)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(87, 199)
        Me.ListBox1.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(377, -58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Item List"
        '
        'delete_btn
        '
        Me.delete_btn.Location = New System.Drawing.Point(5, 222)
        Me.delete_btn.Name = "delete_btn"
        Me.delete_btn.Size = New System.Drawing.Size(132, 35)
        Me.delete_btn.TabIndex = 8
        Me.delete_btn.Text = "DELETE SELECTED"
        Me.delete_btn.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Location = New System.Drawing.Point(3, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(357, 297)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'clip_btn
        '
        Me.clip_btn.BackColor = System.Drawing.SystemColors.Control
        Me.clip_btn.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.clip_btn.Location = New System.Drawing.Point(5, 261)
        Me.clip_btn.Name = "clip_btn"
        Me.clip_btn.Size = New System.Drawing.Size(132, 35)
        Me.clip_btn.TabIndex = 11
        Me.clip_btn.Text = "CLIP MODE"
        Me.clip_btn.UseVisualStyleBackColor = False
        '
        'clear_btn
        '
        Me.clear_btn.Location = New System.Drawing.Point(560, 178)
        Me.clear_btn.Name = "clear_btn"
        Me.clear_btn.Size = New System.Drawing.Size(133, 37)
        Me.clear_btn.TabIndex = 10
        Me.clear_btn.Text = "CLEAR ALL"
        Me.clear_btn.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(102, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "X-axis"
        '
        'lblx
        '
        Me.lblx.AutoSize = True
        Me.lblx.Location = New System.Drawing.Point(102, 30)
        Me.lblx.Name = "lblx"
        Me.lblx.Size = New System.Drawing.Size(13, 13)
        Me.lblx.TabIndex = 30
        Me.lblx.Text = "0"
        '
        'lbly
        '
        Me.lbly.AutoSize = True
        Me.lbly.Location = New System.Drawing.Point(102, 62)
        Me.lbly.Name = "lbly"
        Me.lbly.Size = New System.Drawing.Size(13, 13)
        Me.lbly.TabIndex = 31
        Me.lbly.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(102, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 13)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Y-axis"
        '
        'load_btn
        '
        Me.load_btn.Location = New System.Drawing.Point(560, 271)
        Me.load_btn.Margin = New System.Windows.Forms.Padding(1)
        Me.load_btn.Name = "load_btn"
        Me.load_btn.Size = New System.Drawing.Size(132, 35)
        Me.load_btn.TabIndex = 33
        Me.load_btn.Text = "LOAD"
        Me.load_btn.UseVisualStyleBackColor = True
        '
        'save_btn
        '
        Me.save_btn.Location = New System.Drawing.Point(560, 230)
        Me.save_btn.Margin = New System.Windows.Forms.Padding(1)
        Me.save_btn.Name = "save_btn"
        Me.save_btn.Size = New System.Drawing.Size(132, 37)
        Me.save_btn.TabIndex = 32
        Me.save_btn.Text = "SAVE"
        Me.save_btn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ListBox1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblx)
        Me.GroupBox1.Controls.Add(Me.clip_btn)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.delete_btn)
        Me.GroupBox1.Controls.Add(Me.lbly)
        Me.GroupBox1.Location = New System.Drawing.Point(364, 10)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox1.Size = New System.Drawing.Size(375, 300)
        Me.GroupBox1.TabIndex = 34
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "CONFIGURATIONS"
        '
        'windowClr_btn
        '
        Me.windowClr_btn.Location = New System.Drawing.Point(560, 135)
        Me.windowClr_btn.Name = "windowClr_btn"
        Me.windowClr_btn.Size = New System.Drawing.Size(133, 37)
        Me.windowClr_btn.TabIndex = 32
        Me.windowClr_btn.Text = "CLEAR WINDOW"
        Me.windowClr_btn.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 320)
        Me.Controls.Add(Me.load_btn)
        Me.Controls.Add(Me.save_btn)
        Me.Controls.Add(Me.windowClr_btn)
        Me.Controls.Add(Me.clear_btn)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Form1"
        Me.Text = "NLN CLIPPING FORM"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents delete_btn As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents clip_btn As System.Windows.Forms.Button
    Friend WithEvents clear_btn As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblx As System.Windows.Forms.Label
    Friend WithEvents lbly As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents load_btn As System.Windows.Forms.Button
    Friend WithEvents save_btn As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents windowClr_btn As System.Windows.Forms.Button

End Class
