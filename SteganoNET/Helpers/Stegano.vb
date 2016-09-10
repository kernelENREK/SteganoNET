Namespace Helpers

    ''' <summary>
    ''' Helper for steganography stuff
    ''' </summary>
    Public NotInheritable Class Stegano

#Region "       ### LSB Steganography for dummies: HOW-TO ###"

        ' How it works?
        '
        ' We store information in LSB (Least Significant Bit) for each value from bitmap pixels
        '
        ' When we use LockBits and Marshal.Copy for the bitmap, we store the information in byte Array called bmpBytes
        ' Each item from bmpBytes represents a color value depending on sourceBitmap bits per pixel (bpp)
        ' For example if we use a 24 bpp bitmap, the byte Array bmpBytes represent the Blue, Green and Red values for each pixel:
        '
        ' bmpBytes [0]: Blue  value for pixel in (0,0) coordinate
        ' bmpBytes [1]: Green value for pixel in (0,0)
        ' bmpBytes [2]: Red   value for pixel in (0,0)
        ' bmpBytes [3]: Blue  value for pixel in (1,0)
        ' bmpBytes [4]: Green value for pixel in (1,0)
        ' bmpBytes [5]: Red   value for pixel in (1,0)
        ' bmpBytes [6]: Blue  value for pixel in (2,0)
        ' bmpBytes [7]: Green value for pixel in (2,0)
        ' ...
        '
        ' For a 24 bpp bitmap the byte Array is something like:
        ' pixel pixel pixel pixel
        '  BGR   BGR   BGR   BGR
        '
        ' To 'hide' a single BYTE in a 24 bpp image we need '2 pixels' and the B,G components from 3rd pixel:
        ' pixel pixel pixel pixel pixel pixel pixel pixel 
        '  BGR   BGR   BGR   BGR   BGR   BRG   BGR   BGR  <--- pixel component (Blue Green Red)
        '  012   345   670   123   456   701   234   567  <--- bits (0 to 7) from BYTES we want to 'hide'
        '  |   BYTE 1   ||    BYTE 2     ||   BYTE 3   |  <--- BYTES we want to 'hide'
        '
        ' For 32 bpp bitmap the byte Array includes Alpha value (also known as transparency channel), so is something like:
        ' pixel pixel pixel pixel 
        ' BGRA  BGRA  BGRA  BRGA
        '
        ' To 'hide' a single BYTE in a 32 bpp image we need exactly '2 pixels':
        ' pixel pixel pixel pixel pixel pixel pixel pixel 
        ' BGRA  BGRA  BGRA  BGRA  BGRA  BRGA  BGRA  BGRA  <--- pixel component (Blue Green Red Alpha)
        ' 0123  4567  0123  4567  0123  4567  0123  4567  <--- bits (0 to 7) from BYTES we want to 'hide'
        ' | BYTE 1 |  | BYTE 2 |  | BYTE 3 |  | BYTE 4 |  <--- BYTES we want to 'hide'
        '
        ' -----------------------------------------------------------------------------------------
        ' Encoding Example
        '
        ' For a simple example, we have a full Blue semitransparent (128 Alpha) 32 bpp image:
        ' pixel(0,0)   pixel(1,0)   pixel(2,0)   pixel(3,0)   pixel(4,0) ... pixel(x coord, y coord)
        ' [0] B: 255   [4] B: 255   [8] B: 255   [12] B: 255  [16] B: 255    [n-3] B: 255
        ' [1] G: 0     [5] G: 0     [9] G: 0     [13] G: 0    [17] G: 0      [n-2] G: 0
        ' [2] R: 0     [6] R: 0    [10] R: 0     [14] R: 0    [18] R: 0      [n-1] R: 0
        ' [3] A: 128   [7] A: 128  [11] A: 128   [15] A: 128  [19] A: 128      [n] A: 128
        '
        ' We want to hide text 'Hello' into that full blue semitransparent 32 bpp image.
        '
        '         Decimal  Hex     Binary
        ' H --->     72    0x48    0100 1000
        ' e --->    101    0x65    0110 0101
        ' l --->    108    0x6C    0110 1100
        ' l --->    108    0x6C    0110 1100 
        ' o --->    111    0x6F    0110 1111
        '
        ' Let's for char 'H':      0100 1000
        '                          7654 3210 <--- bits   
        '
        ' If bit value is 0 the new pixel value is: pixel[x] = pixel[x] AND 254 (254 in binary: 1111 1110)
        ' If bit value is 1 the new pixel value is: pixel[x] = pixel[x] OR 1    (  1 in binary: 0000 0001)
        '
        ' bit   value   pixel                                   result                    new pixel[x]   new pixel[x] (decimal)
        ' 0     0       (0,0) B: 255 in binary ---> 1111 1111   1111 1111 AND 1111 1110 = 1111 1110 ---> 254 
        ' 1     0       (0,0) G: 0   in binary ---> 0000 0000   0000 0000 AND 1111 1110 = 0000 0000 ---> 0
        ' 2     0       (0,0) R: 0   in binary ---> 0000 0000   0000 0000 AND 1111 1110 = 0000 0000 ---> 0
        ' 3     1       (0,0) A: 128 in binary ---> 1000 0000   1000 0000 OR  0000 0001 = 1000 0001 ---> 129
        ' 4     0       (1,0) B: 255 in binary ---> 1111 1111   1111 1111 AND 1111 1110 = 1111 1110 ---> 254
        ' 5     0       (1,0) G: 0   in binary ---> 0000 0000   0000 0000 AND 1111 1110 = 0000 0000 ---> 0
        ' 6     1       (1,0) R: 0   in binary ---> 0000 0000   0000 0000 OR  0000 0001 = 0000 0001 ---> 1
        ' 7     0       (1,0) A: 128 in binary ---> 1000 0000   1000 0000 AND 1111 1110 = 1000 0000 ---> 128
        '
        ' Summarizing:
        '
        '              Before encoding 'H'    After encoding 'H'
        ' pixel(0,0)   [0] B: 255             [0] B: 254
        ' pixel(0,0)   [1] G: 0               [1] G: 0
        ' pixel(0,0)   [2] R: 0               [2] R: 0
        ' pixel(0,0)   [3] A: 128             [3] A: 129
        ' pixel(1,0)   [4] B: 255             [4] B: 254
        ' pixel(1,0)   [5] G: 0               [5] G: 0
        ' pixel(1,0)   [6] R: 0               [6] R: 1
        ' pixel(1,0)   [7] A: 128             [7] A: 128
        '
        ' For 'e', 'l', 'l' and 'o' the procedure is basically the the same.
        '
        ' When we finished to encode all the byte Array we save the bmpBytes to a new png (or bmp) file. 
        ' This new file containts the new values for each pixel. That pixel changes may not be noticeable to the human eye.
        ' Also remember depending original pixel[x] value and byte we want to encode sometimes pixel[x] not changes.
        '
        ' -----------------------------------------------------------------------------------------
        ' Decoding Example
        ' 
        ' Decoding bytes its quite simple. We need to read 8 bytes step from byte Array bmpBytes.
        ' Then we need to read LSB from each that 8 bytes.
        '
        '                                     _____ LSB
        '                                    |
        '                                    V
        ' pixel(0,0) [0] B: 254 ---> 1111 1110 _____________
        ' pixel(0,0) [1] G: 0   ---> 0000 0000 ____________ |
        ' pixel(0,0) [2] R: 0   ---> 0000 0000 ___________ ||
        ' pixel(0,0) [3] A: 129 ---> 1000 0001 __________ |||
        ' pixel(1,0) [4] B: 254 ---> 1111 1110 ________  ||||
        ' pixel(1,0) [5] G: 0   ---> 0000 0000 _______ | ||||
        ' pixel(1,0) [6] R: 1   ---> 0000 0001 ______ || ||||
        ' pixel(1,0) [7] A: 128 ---> 1000 0000 _____ ||| ||||
        '                                           |||| ||||
        '                                           VVVV VVVV
        ' Decoded Byte: LSB values for the 8 bytes: 0100 1000
        '
        '                             Decimal  Hex    ASCII 
        ' And finally, 0100 000 --->     72    0x48   'H'
        '
        ' -----------------------------------------------------------------------------------------
        ' How we detect if the image have a 'hidden' text and/or 'hidden' file?
        '
        ' We 'hide' a custom header into first bytes to set up hidden text and/or file. 
        ' When decode the file we check for that headers. 
        ' If the headers were found we known have text and/or file in the image.
        '
        ' for example:
        '
        ' Only hidden text:
        ' 0x29aTEXT_GOES_HERE|bytes...
        '
        ' Only hidden file:
        ' 0x29a|0x666FILE_NAME|132167|bytes...
        '                         ^
        '                         |___ we have a file named FILE_NAME with 132,167 Bytes 'hidden' into bitmap
        '
        ' Hidden text and file:
        ' 0x29aTEXT_GOES_HERE|0x666FILE_NAME|132167|bytes...
        '
        ' -----------------------------------------------------------------------------------------
        ' How many Bytes can we 'hide' into bitmap?
        '
        ' Well, this basically it depends of 3 factors: bitmap width, bitmap height and bitmap bpp (bits per pixel)
        '
        ' The formula is: Bytes = ((width * height * (bpp/8))  / 8
        '
        ' Example #1: an 1024 x 768 and 24 bpp bitmap
        '             Bytes = (1024*768*(24/8)) / 8 = 294,912 Bytes ---> 288 KBytes
        ' Example #2: an 1024 x 768 and 32 bpp bitmal
        '             Bytes = (1024*768*(32/8)) / 8 = 393,216 Bytes ---> 384 KBytes
        ' Example #3: an 1920 x 1200 and 24 bpp bitmap
        '             Bytes = (1920*1200*(24/8)) / 8 = 864,000 Bytes ---> 843 KBytes
        '
        ' This KBytes values are independent from source bitmap format, that is:
        ' Example #1:
        ' We have a picture.jpg (file size, for example, 61 KBytes). Is a 1024 x 768 and 24 bpp bitmap.
        ' 1024x768@24 bpp ---> we can hide 288 KBytes.
        ' Even jpg file size (in your HDD is 61 KBytes) we can hide a 288 KBytes file into that jpg file
        '
        ' Example #2:
        ' We have a picture.png (file size, for example, 483 Kbytes). Is a 1024 x 768 and 24 bpp bitmap.
        ' 1024x768@24 bpp ---> we can hide 288 KBytes
        '
        ' As you can see, 'hidden size' does not depend on then soruce file format, depends of width, height and bpp

#End Region

        ''' <summary>
        ''' Header for hiding a text
        ''' </summary>
        Private Const HEADER_SECRET_TEXT As String = "0x29a"

        ''' <summary>
        ''' Header for attach a file of any type
        ''' </summary>
        Private Const HEADER_SECRET_FILE As String = "0x666"

        ''' <summary>
        ''' Split chacacter for header
        ''' </summary>
        Private Const HEADER_SPLIT_DATA As String = Chr(28)

        ''' <summary>
        ''' Returns how many Bytes we can 'hide' as steganography in the image
        ''' </summary>
        ''' <param name="sourceBitmap">image we use for steganography</param>
        ''' <returns></returns>
        Public Shared Function GetMaxSteganoSize(sourceBitmap As Bitmap) As Integer
            Dim bpp = Image.GetPixelFormatSize(sourceBitmap.PixelFormat)

            Return (sourceBitmap.Width * sourceBitmap.Height * (bpp / 8)) \ 8
        End Function

        ''' <summary>
        ''' Function for decode steganography from image
        ''' </summary>
        ''' <param name="sourceFile">image source file. The image should be png, jpg and bmp format. Can be any bpp</param>
        ''' <param name="steganoInfo">Information decoded from the sourceFile</param>
        ''' <param name="bytesAttach">Byte for file to hide in the sourceFile. This file can be any file type</param>
        ''' <returns></returns>
        Public Shared Function GetStegano(sourceFile As String, ByRef steganoInfo As SteganoInfo, ByRef bytesAttach As Byte()) As Boolean
            Dim sourceBitmap As New Bitmap(sourceFile)

            Dim rectBmp As New Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height)
            Dim bmpData As System.Drawing.Imaging.BitmapData = sourceBitmap.LockBits(rectBmp, Imaging.ImageLockMode.ReadWrite, sourceBitmap.PixelFormat)
            Dim bmpPtr As IntPtr = bmpData.Scan0
            Dim bmpTotalBytes As Integer = bmpData.Stride * sourceBitmap.Height
            Dim bmpBytes(bmpTotalBytes - 1) As Byte

            System.Runtime.InteropServices.Marshal.Copy(bmpPtr, bmpBytes, 0, bmpTotalBytes)

            ' TODO: Improvement for non dividable by 4 image width: http://stackoverflow.com/questions/16775436/c-sharp-remove-bitmap-padding

            Dim binaryString As String = ""
            Dim octetString As String = ""
            Dim state As Integer = 0
            Dim header As String = String.Empty
            Dim strTotalAttachedBytes As String = String.Empty
            Dim totalAttachedBytes As Integer = 0

            Dim attachedBytes As ArrayList = Nothing

            If (IsNothing(steganoInfo)) Then steganoInfo = New SteganoInfo()

            For i As Integer = 0 To bmpBytes.Length - 1
                binaryString = Convert.ToString(bmpBytes(i), 2)
                octetString &= binaryString.Substring(binaryString.Length - 1, 1)

                If (octetString.Length = 8) Then
                    Select Case state
                        Case 0
                            header &= Chr(Convert.ToInt32(octetString, 2))
                            If (header.Length = HEADER_SECRET_TEXT.Length) Then
                                If (header = HEADER_SECRET_TEXT) Then
                                    steganoInfo.SecretText = String.Empty
                                    state = 1
                                Else
                                    state = 2
                                End If
                                header = String.Empty
                            End If
                        Case 1 ' decode text
                            Dim ascii As Integer = Convert.ToInt32(octetString, 2)
                            If (ascii = Asc(HEADER_SPLIT_DATA)) Then
                                state = 2
                            Else
                                steganoInfo.SecretText &= Chr(ascii)
                            End If
                        Case 2 ' check for attached file
                            header &= Chr(Convert.ToInt32(octetString, 2))
                            If (header.Length = HEADER_SECRET_TEXT.Length) Then
                                If (header = HEADER_SECRET_FILE) Then
                                    steganoInfo.SecretFile = String.Empty
                                    state = 3 'get filename for attached file
                                Else
                                    Exit For
                                End If
                                header = String.Empty
                            End If
                        Case 3 ' get filename for attached file
                            Dim ascii As Integer = Convert.ToInt32(octetString, 2)
                            If (ascii = Asc(HEADER_SPLIT_DATA)) Then
                                state = 4 'get total bytes for attached file
                            Else
                                steganoInfo.SecretFile &= Chr(ascii)
                            End If
                        Case 4 ' get total bytes attached file
                            Dim ascii As Integer = Convert.ToInt32(octetString, 2)
                            If (ascii = Asc(HEADER_SPLIT_DATA)) Then
                                totalAttachedBytes = Convert.ToInt32(strTotalAttachedBytes)
                                attachedBytes = New ArrayList()
                                state = 5 'get bytes for attached file                            
                            Else
                                strTotalAttachedBytes &= Chr(ascii)
                            End If
                        Case 5 ' get bytes
                            attachedBytes.Add(Convert.ToByte(octetString, 2))
                            If (attachedBytes.Count = totalAttachedBytes) Then
                                bytesAttach = CType(attachedBytes.ToArray(GetType(Byte)), Byte())
                                Exit For
                            End If
                    End Select

                    octetString = String.Empty
                End If
            Next

            System.Runtime.InteropServices.Marshal.Copy(bmpBytes, 0, bmpPtr, bmpTotalBytes)
            sourceBitmap.UnlockBits(bmpData)

            Return (Not String.IsNullOrEmpty(steganoInfo.SecretText) Or Not IsNothing(bytesAttach))

        End Function

        ''' <summary>
        ''' Method for encode steganography into image (sourceBitmap)
        ''' </summary>
        ''' <param name="sourceBitmap">Source bitmap where we are going to encode the data</param>
        ''' <param name="saveToFilename">Name for the new image containing the steganography data. By default the new image will be a png file</param>
        ''' <param name="steganoInfo">Information we want to encode to the sourceBitmap</param>      
        Public Shared Sub CreateStegano(sourceBitmap As Bitmap, saveToFilename As String, steganoInfo As SteganoInfo)
            Dim steganoBytes As List(Of Byte) = New List(Of Byte)()

            Dim header As String = String.Empty
            header = String.Format("{0}{1}{2}", HEADER_SECRET_TEXT, steganoInfo.SecretText, HEADER_SPLIT_DATA)

            Dim bytesAttach As Byte() = Nothing
            If (Not IsNothing(steganoInfo.SecretFile)) Then

                Dim fs As IO.FileStream = IO.File.OpenRead(steganoInfo.SecretFile)
                Using br As IO.BinaryReader = New IO.BinaryReader(fs)
                    bytesAttach = br.ReadBytes(br.BaseStream.Length)
                End Using

                header &= String.Format("{0}{1}{2}{3}{2}",
                                        HEADER_SECRET_FILE,
                                        IO.Path.GetFileName(steganoInfo.SecretFile),
                                        HEADER_SPLIT_DATA,
                                        bytesAttach.Length.ToString())
            End If

            Dim binaryString As String = ""
            For i As Integer = 0 To header.Length - 1
                binaryString = Convert.ToString(Asc(header.Substring(i, 1)), 2).PadLeft(8, "0")
                For k As Integer = 0 To binaryString.Length - 1
                    If (binaryString.Substring(k, 1) = "1") Then
                        steganoBytes.Add(1)
                    Else
                        steganoBytes.Add(0)
                    End If
                Next
            Next

            If (Not IsNothing(bytesAttach)) Then
                For i As Integer = 0 To bytesAttach.Length - 1
                    binaryString = Convert.ToString(bytesAttach(i), 2).PadLeft(8, "0")
                    For k As Integer = 0 To binaryString.Length - 1
                        If (binaryString.Substring(k, 1) = "1") Then
                            steganoBytes.Add(1)
                        Else
                            steganoBytes.Add(0)
                        End If
                    Next
                Next
            End If

            Dim rectBmp As New Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height)
            Dim bmpData As System.Drawing.Imaging.BitmapData = sourceBitmap.LockBits(rectBmp, Imaging.ImageLockMode.ReadWrite, sourceBitmap.PixelFormat)
            Dim bmpPtr As IntPtr = bmpData.Scan0
            Dim bmpTotalBytes As Integer = bmpData.Stride * sourceBitmap.Height
            Dim bmpBytes(bmpTotalBytes - 1) As Byte

            System.Runtime.InteropServices.Marshal.Copy(bmpPtr, bmpBytes, 0, bmpTotalBytes)

            Dim bmpValue As Byte = 0
            For i As Integer = 0 To steganoBytes.Count - 1
                bmpValue = bmpBytes(i)
                If (steganoBytes(i) = 0) Then
                    bmpBytes(i) = bmpValue And 254  ' setup LSB to 0 (xxxx xxx0)
                Else
                    bmpBytes(i) = bmpValue Or 1     ' setup LSB to 1 (xxxx xxx1)
                End If
            Next

            System.Runtime.InteropServices.Marshal.Copy(bmpBytes, 0, bmpPtr, bmpTotalBytes)
            sourceBitmap.UnlockBits(bmpData)

            If (saveToFilename.ToLower().EndsWith(".png")) Then
                sourceBitmap.Save(saveToFilename, System.Drawing.Imaging.ImageFormat.Png)
            Else
                sourceBitmap.Save(saveToFilename, System.Drawing.Imaging.ImageFormat.Bmp)
            End If
        End Sub

    End Class

    Public Class SteganoInfo
        Public Property SecretText As String
        Public Property SecretFile As String
    End Class

End Namespace
