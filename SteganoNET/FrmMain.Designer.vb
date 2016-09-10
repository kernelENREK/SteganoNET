<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.PicRender = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GrpBoxStegano_Set = New System.Windows.Forms.GroupBox()
        Me.BtnSecretFile_Cancel = New System.Windows.Forms.Button()
        Me.PicWarning = New System.Windows.Forms.PictureBox()
        Me.LblSecretFile_Set = New System.Windows.Forms.Label()
        Me.BtnHide = New System.Windows.Forms.Button()
        Me.BtnSecretFile_Set = New System.Windows.Forms.Button()
        Me.TxtSecretText_Set = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GrpBoxStegano = New System.Windows.Forms.GroupBox()
        Me.BtnSaveSecretFile = New System.Windows.Forms.Button()
        Me.TxtSecretFile = New System.Windows.Forms.TextBox()
        Me.TxtSecretText = New System.Windows.Forms.TextBox()
        Me.LblSteganoContent = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.FileSystemTree1 = New SteganoNET.FileSystemTree()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.PicRender, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GrpBoxStegano_Set.SuspendLayout()
        CType(Me.PicWarning, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpBoxStegano.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 12)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.FileSystemTree1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(932, 489)
        Me.SplitContainer1.SplitterDistance = 292
        Me.SplitContainer1.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.PicRender)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Size = New System.Drawing.Size(636, 489)
        Me.SplitContainer2.SplitterDistance = 328
        Me.SplitContainer2.TabIndex = 0
        '
        'PicRender
        '
        Me.PicRender.BackColor = System.Drawing.SystemColors.Control
        Me.PicRender.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PicRender.Location = New System.Drawing.Point(0, 0)
        Me.PicRender.Name = "PicRender"
        Me.PicRender.Size = New System.Drawing.Size(636, 328)
        Me.PicRender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicRender.TabIndex = 0
        Me.PicRender.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.GrpBoxStegano_Set)
        Me.Panel1.Controls.Add(Me.GrpBoxStegano)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(636, 157)
        Me.Panel1.TabIndex = 0
        '
        'GrpBoxStegano_Set
        '
        Me.GrpBoxStegano_Set.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrpBoxStegano_Set.Controls.Add(Me.BtnSecretFile_Cancel)
        Me.GrpBoxStegano_Set.Controls.Add(Me.PicWarning)
        Me.GrpBoxStegano_Set.Controls.Add(Me.LblSecretFile_Set)
        Me.GrpBoxStegano_Set.Controls.Add(Me.BtnHide)
        Me.GrpBoxStegano_Set.Controls.Add(Me.BtnSecretFile_Set)
        Me.GrpBoxStegano_Set.Controls.Add(Me.TxtSecretText_Set)
        Me.GrpBoxStegano_Set.Controls.Add(Me.Label2)
        Me.GrpBoxStegano_Set.Controls.Add(Me.Label1)
        Me.GrpBoxStegano_Set.Location = New System.Drawing.Point(3, 83)
        Me.GrpBoxStegano_Set.Name = "GrpBoxStegano_Set"
        Me.GrpBoxStegano_Set.Size = New System.Drawing.Size(630, 71)
        Me.GrpBoxStegano_Set.TabIndex = 1
        Me.GrpBoxStegano_Set.TabStop = False
        Me.GrpBoxStegano_Set.Text = "New stegano content"
        Me.GrpBoxStegano_Set.Visible = False
        '
        'BtnSecretFile_Cancel
        '
        Me.BtnSecretFile_Cancel.Image = Global.SteganoNET.My.Resources.Resources.delete_16x16
        Me.BtnSecretFile_Cancel.Location = New System.Drawing.Point(321, 35)
        Me.BtnSecretFile_Cancel.Name = "BtnSecretFile_Cancel"
        Me.BtnSecretFile_Cancel.Size = New System.Drawing.Size(24, 23)
        Me.BtnSecretFile_Cancel.TabIndex = 7
        Me.BtnSecretFile_Cancel.UseVisualStyleBackColor = True
        Me.BtnSecretFile_Cancel.Visible = False
        '
        'PicWarning
        '
        Me.PicWarning.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PicWarning.Image = Global.SteganoNET.My.Resources.Resources.warning
        Me.PicWarning.Location = New System.Drawing.Point(527, 40)
        Me.PicWarning.Name = "PicWarning"
        Me.PicWarning.Size = New System.Drawing.Size(16, 16)
        Me.PicWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PicWarning.TabIndex = 4
        Me.PicWarning.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PicWarning, "WARNING: Selected image does not have width dividable by 4. This may corrupt atta" &
        "ched 'hidden' data")
        Me.PicWarning.Visible = False
        '
        'LblSecretFile_Set
        '
        Me.LblSecretFile_Set.AutoSize = True
        Me.LblSecretFile_Set.Location = New System.Drawing.Point(351, 40)
        Me.LblSecretFile_Set.Name = "LblSecretFile_Set"
        Me.LblSecretFile_Set.Size = New System.Drawing.Size(22, 13)
        Me.LblSecretFile_Set.TabIndex = 6
        Me.LblSecretFile_Set.Text = "xxx"
        '
        'BtnHide
        '
        Me.BtnHide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnHide.Enabled = False
        Me.BtnHide.Location = New System.Drawing.Point(549, 35)
        Me.BtnHide.Name = "BtnHide"
        Me.BtnHide.Size = New System.Drawing.Size(75, 23)
        Me.BtnHide.TabIndex = 5
        Me.BtnHide.Text = "Hide!"
        Me.BtnHide.UseVisualStyleBackColor = True
        '
        'BtnSecretFile_Set
        '
        Me.BtnSecretFile_Set.Location = New System.Drawing.Point(272, 35)
        Me.BtnSecretFile_Set.Name = "BtnSecretFile_Set"
        Me.BtnSecretFile_Set.Size = New System.Drawing.Size(48, 23)
        Me.BtnSecretFile_Set.TabIndex = 4
        Me.BtnSecretFile_Set.Text = "..."
        Me.BtnSecretFile_Set.UseVisualStyleBackColor = True
        '
        'TxtSecretText_Set
        '
        Me.TxtSecretText_Set.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSecretText_Set.Location = New System.Drawing.Point(273, 13)
        Me.TxtSecretText_Set.Name = "TxtSecretText_Set"
        Me.TxtSecretText_Set.Size = New System.Drawing.Size(351, 20)
        Me.TxtSecretText_Set.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(262, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Select the file you want to 'hide' in the selected image:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(261, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Enter the text you want to 'hide' in the selected image:"
        '
        'GrpBoxStegano
        '
        Me.GrpBoxStegano.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrpBoxStegano.Controls.Add(Me.BtnSaveSecretFile)
        Me.GrpBoxStegano.Controls.Add(Me.TxtSecretFile)
        Me.GrpBoxStegano.Controls.Add(Me.TxtSecretText)
        Me.GrpBoxStegano.Controls.Add(Me.LblSteganoContent)
        Me.GrpBoxStegano.Location = New System.Drawing.Point(3, 3)
        Me.GrpBoxStegano.Name = "GrpBoxStegano"
        Me.GrpBoxStegano.Size = New System.Drawing.Size(630, 81)
        Me.GrpBoxStegano.TabIndex = 0
        Me.GrpBoxStegano.TabStop = False
        Me.GrpBoxStegano.Text = "Stegano content"
        Me.GrpBoxStegano.Visible = False
        '
        'BtnSaveSecretFile
        '
        Me.BtnSaveSecretFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSaveSecretFile.Location = New System.Drawing.Point(549, 51)
        Me.BtnSaveSecretFile.Name = "BtnSaveSecretFile"
        Me.BtnSaveSecretFile.Size = New System.Drawing.Size(75, 23)
        Me.BtnSaveSecretFile.TabIndex = 3
        Me.BtnSaveSecretFile.Text = "Save"
        Me.BtnSaveSecretFile.UseVisualStyleBackColor = True
        Me.BtnSaveSecretFile.Visible = False
        '
        'TxtSecretFile
        '
        Me.TxtSecretFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSecretFile.BackColor = System.Drawing.Color.Aqua
        Me.TxtSecretFile.Location = New System.Drawing.Point(9, 54)
        Me.TxtSecretFile.Name = "TxtSecretFile"
        Me.TxtSecretFile.ReadOnly = True
        Me.TxtSecretFile.Size = New System.Drawing.Size(534, 20)
        Me.TxtSecretFile.TabIndex = 2
        Me.TxtSecretFile.Visible = False
        '
        'TxtSecretText
        '
        Me.TxtSecretText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSecretText.BackColor = System.Drawing.Color.Aqua
        Me.TxtSecretText.Location = New System.Drawing.Point(9, 32)
        Me.TxtSecretText.Name = "TxtSecretText"
        Me.TxtSecretText.ReadOnly = True
        Me.TxtSecretText.Size = New System.Drawing.Size(534, 20)
        Me.TxtSecretText.TabIndex = 1
        Me.TxtSecretText.Visible = False
        '
        'LblSteganoContent
        '
        Me.LblSteganoContent.AutoSize = True
        Me.LblSteganoContent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSteganoContent.ForeColor = System.Drawing.Color.Blue
        Me.LblSteganoContent.Location = New System.Drawing.Point(6, 16)
        Me.LblSteganoContent.Name = "LblSteganoContent"
        Me.LblSteganoContent.Size = New System.Drawing.Size(115, 13)
        Me.LblSteganoContent.TabIndex = 0
        Me.LblSteganoContent.Text = "LblSteganoContent"
        '
        'FileSystemTree1
        '
        Me.FileSystemTree1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FileSystemTree1.FileExtensions = "*"
        Me.FileSystemTree1.Location = New System.Drawing.Point(0, 0)
        Me.FileSystemTree1.Name = "FileSystemTree1"
        Me.FileSystemTree1.RootDrive = Nothing
        Me.FileSystemTree1.Size = New System.Drawing.Size(292, 489)
        Me.FileSystemTree1.TabIndex = 2
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(956, 513)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.PicRender, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GrpBoxStegano_Set.ResumeLayout(False)
        Me.GrpBoxStegano_Set.PerformLayout()
        CType(Me.PicWarning, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpBoxStegano.ResumeLayout(False)
        Me.GrpBoxStegano.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FileSystemTree1 As FileSystemTree
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents PicRender As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GrpBoxStegano As GroupBox
    Friend WithEvents TxtSecretText As TextBox
    Friend WithEvents LblSteganoContent As Label
    Friend WithEvents BtnSaveSecretFile As Button
    Friend WithEvents TxtSecretFile As TextBox
    Friend WithEvents GrpBoxStegano_Set As GroupBox
    Friend WithEvents LblSecretFile_Set As Label
    Friend WithEvents BtnHide As Button
    Friend WithEvents BtnSecretFile_Set As Button
    Friend WithEvents TxtSecretText_Set As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents PicWarning As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents BtnSecretFile_Cancel As Button
End Class
