Public Class FrmMain

#Region "Variables"

    ''' <summary>
    ''' The image file we selected from FileSystemTree
    ''' </summary>
    Private _selectedFile As IO.FileInfo

    ''' <summary>
    ''' How many bytes can we encode to the selected image file
    ''' </summary>
    Private _selectedFileMaxBytesAttach As Integer

    ''' <summary>
    ''' File we want to attach (encode) into the image file we selected from FileSystemTree
    ''' </summary>
    Private _newAttachedFile As String

#End Region

#Region "Constructor"

    Public Sub New()

        InitializeComponent()

        ' Credits to FileSystem TreeView:
        ' https://code.msdn.microsoft.com/windowsdesktop/File-System-Tree-View-3a28325c

        FileSystemTree1.FileExtensions = ".bmp;.gif;jpg;.png"

        LblSteganoContent.Text = String.Empty
        LblSecretFile_Set.Text = String.Empty

        Me.Text = Application.ProductName
    End Sub

#End Region

#Region "Steganography: Decode info"

    Private Sub FileSystemTree1_FileSelected(sender As Object, e As FileInfoEventArgs) Handles FileSystemTree1.FileSelected
        Try
            GrpBoxStegano.Visible = True
            GrpBoxStegano_Set.Visible = True

            _selectedFile = e.File
            Helpers.BitmapHelper.CreateThumbnail(e.File.FullName, PicRender, Color.Transparent)

            Dim sourceBitmap As New Bitmap(e.File.FullName)
            _selectedFileMaxBytesAttach = Helpers.Stegano.GetMaxSteganoSize(sourceBitmap)

            If (_selectedFileMaxBytesAttach < (1024 * 1024)) Then
                GrpBoxStegano_Set.Text = String.Format("New stegano content (Max.: {0:0.00} {1})", _selectedFileMaxBytesAttach / 1024, "KB")
            Else
                GrpBoxStegano_Set.Text = String.Format("New stegano content (Max.: {0:0.00} {1})", (_selectedFileMaxBytesAttach / (1024 * 1024)), "MB")
            End If

            If (sourceBitmap.Width Mod 4 = 0) Then
                PicWarning.Visible = False
            Else
                PicWarning.Visible = True
            End If

            Dim bytes As Byte() = Nothing
            Dim si As Helpers.SteganoInfo = Nothing

            Me.Cursor = Cursors.WaitCursor
            Dim boolsteganoContent As Boolean = Helpers.Stegano.GetStegano(e.File.FullName, si, bytes)
            Me.Cursor = Cursors.Default

            TxtSecretText.Visible = boolsteganoContent
            TxtSecretFile.Visible = boolsteganoContent
            BtnSaveSecretFile.Visible = boolsteganoContent

            Dim steganoContent As Integer = 0

            If (boolsteganoContent) Then
                If (Not String.IsNullOrWhiteSpace(si.SecretText)) Then
                    steganoContent = 1
                    TxtSecretText.Text = si.SecretText
                Else
                    TxtSecretText.Visible = False
                End If

                If (Not String.IsNullOrWhiteSpace(si.SecretFile)) Then
                    steganoContent += 2
                    TxtSecretFile.Text = si.SecretFile
                    BtnSaveSecretFile.Tag = bytes
                Else
                    TxtSecretFile.Visible = False
                    BtnSaveSecretFile.Visible = False
                End If

                Select Case steganoContent
                    Case 1
                        LblSteganoContent.Text = "Hidden text in the selected image"
                    Case 2
                        LblSteganoContent.Text = "Hidden file in the selected image"
                    Case 3
                        LblSteganoContent.Text = "Hidden text and file in the selected image"
                End Select
            Else
                LblSteganoContent.Text = "No hidden data in the selected image"
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Save attached file to HDD
    Private Sub BtnSaveSecretFile_Click(sender As Object, e As EventArgs) Handles BtnSaveSecretFile.Click
        Try
            Dim path As String = IO.Path.GetDirectoryName(_selectedFile.FullName)

            Using dlg = New SaveFileDialog()
                dlg.InitialDirectory = path
                dlg.FileName = TxtSecretFile.Text

                If (dlg.ShowDialog() = DialogResult.OK) Then
                    Dim fullSaveTo As String = dlg.FileName
                    If (IO.File.Exists(fullSaveTo)) Then
                        IO.File.Delete(fullSaveTo)
                    End If

                    Dim fs As IO.FileStream = IO.File.Create(fullSaveTo)
                    Using bw As IO.BinaryWriter = New IO.BinaryWriter(fs)
                        bw.Write(BtnSaveSecretFile.Tag)
                        bw.Flush()
                        bw.Close()
                    End Using

                    MessageBox.Show(String.Format("{0} has been saved successfully!", fullSaveTo), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    FileSystemTree1.Collapse(IO.Path.GetDirectoryName(fullSaveTo))
                    FileSystemTree1.Expand(IO.Path.GetDirectoryName(fullSaveTo))
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Steganography: Encode info"

    Private Sub TxtSecretText_Set_TextChanged(sender As Object, e As EventArgs) Handles TxtSecretText_Set.TextChanged
        If (IsNothing(_selectedFile)) Then Exit Sub

        If (Not String.IsNullOrWhiteSpace(TxtSecretText_Set.Text)) Then
            BtnHide.Enabled = True
        Else
            If (Not String.IsNullOrEmpty(_newAttachedFile)) Then
                BtnHide.Enabled = True
            Else
                BtnHide.Enabled = False
            End If
        End If
    End Sub

    Private Sub BtnSecretFile_Set_Click(sender As Object, e As EventArgs) Handles BtnSecretFile_Set.Click
        If (IsNothing(_selectedFile)) Then
            MessageBox.Show("Select an image first!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using dlg = New OpenFileDialog()
            If (dlg.ShowDialog() = DialogResult.OK) Then
                _newAttachedFile = dlg.FileName
                Dim fi As New IO.FileInfo(dlg.FileName)
                If (fi.Length > _selectedFileMaxBytesAttach) Then
                    MessageBox.Show(String.Format("Can not 'hide' the file {0} into selected image. Max size allowed is {1} bytes. File to attach size: {2} bytes",
                                                  dlg.FileName, _selectedFileMaxBytesAttach, fi.Length),
                                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _newAttachedFile = String.Empty
                    LblSecretFile_Set.Text = String.Empty
                    BtnSecretFile_Cancel.Visible = False
                    If (String.IsNullOrWhiteSpace(TxtSecretText_Set.Text)) Then BtnHide.Enabled = False
                Else
                    LblSecretFile_Set.Text = IO.Path.GetFileName(dlg.FileName)
                    BtnHide.Enabled = True
                    BtnSecretFile_Cancel.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub BtnSecretFile_Cancel_Click(sender As Object, e As EventArgs) Handles BtnSecretFile_Cancel.Click
        _newAttachedFile = String.Empty
        LblSecretFile_Set.Text = String.Empty
        If (String.IsNullOrWhiteSpace(TxtSecretText_Set.Text)) Then BtnHide.Enabled = False
        BtnSecretFile_Cancel.Visible = False
    End Sub

    Private Sub BtnHide_Click(sender As Object, e As EventArgs) Handles BtnHide.Click
        Try
            Dim si As Helpers.SteganoInfo = New Helpers.SteganoInfo()
            If (Not String.IsNullOrWhiteSpace(TxtSecretText_Set.Text)) Then
                si.SecretText = TxtSecretText_Set.Text.Trim()
            End If
            If (Not String.IsNullOrEmpty(_newAttachedFile)) Then
                si.SecretFile = _newAttachedFile
            End If

            Dim path As String = IO.Path.GetDirectoryName(_selectedFile.FullName)

            Using dlg = New SaveFileDialog()
                dlg.InitialDirectory = path
                dlg.FileName = String.Format("{0}_new", IO.Path.GetFileNameWithoutExtension(_selectedFile.FullName))
                dlg.Filter = "PNG files (*.png)|*.png|Bitmap files (*.bmp)|*.bmp"
                dlg.DefaultExt = "png"
                dlg.FilterIndex = 1

                If (dlg.ShowDialog() = DialogResult.OK) Then
                    Dim fullSaveTo As String = dlg.FileName
                    If (IO.File.Exists(fullSaveTo)) Then
                        IO.File.Delete(fullSaveTo)
                    End If

                    Dim sourceBitmap As New Bitmap(_selectedFile.FullName)
                    Me.Cursor = Cursors.WaitCursor
                    Helpers.Stegano.CreateStegano(sourceBitmap, fullSaveTo, si)
                    Me.Cursor = Cursors.Default

                    MessageBox.Show(String.Format("{0} has been generated successfully!", fullSaveTo), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    FileSystemTree1.Collapse(IO.Path.GetDirectoryName(fullSaveTo))
                    FileSystemTree1.Expand(IO.Path.GetDirectoryName(fullSaveTo))

                    _newAttachedFile = String.Empty
                    LblSecretFile_Set.Text = String.Empty
                    TxtSecretText_Set.Text = String.Empty
                    BtnSecretFile_Cancel.Visible = False
                End If
            End Using

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

End Class
