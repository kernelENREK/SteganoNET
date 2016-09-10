Imports System.Drawing.Drawing2D

Namespace Helpers

    ''' <summary>
    ''' Helper for bitmap manipulation
    ''' </summary>
    Public NotInheritable Class BitmapHelper

        ''' <summary>
        ''' Display a image in a picturebox with correct aspect ratio according picturebox size
        ''' </summary>
        ''' <param name="inputfile">image file we want to display</param>
        ''' <param name="pictureBoxRender">picturebox control that shows image file</param>
        ''' <param name="clearColor">background color for picturebox</param>
        Public Shared Sub CreateThumbnail(inputfile As String, pictureBoxRender As PictureBox, clearColor As Color)
            If pictureBoxRender.Width <= 1 OrElse pictureBoxRender.Height <= 1 Then
                Return
            End If

            Try
                Using original As Bitmap = DirectCast(Image.FromFile(inputfile), Bitmap)
                    Dim aspectRatio As Single = original.Width / CSng(original.Height)
                    Dim outH As Integer = 0
                    Dim outW As Integer = 0

                    If aspectRatio < 1 Then
                        ' Portrait image
                        outH = pictureBoxRender.Height
                        outW = CInt(outH * aspectRatio)
                        If outW > pictureBoxRender.Width Then
                            aspectRatio = original.Height / CSng(original.Width)
                            outW = pictureBoxRender.Width
                            outH = CInt(outW * aspectRatio)
                        End If
                    Else
                        ' Landscape image
                        aspectRatio = original.Height / CSng(original.Width)
                        outW = pictureBoxRender.Width
                        outH = CInt(outW * aspectRatio)
                        If outH > pictureBoxRender.Height Then
                            outH = pictureBoxRender.Height
                            aspectRatio = original.Width / CSng(original.Height)
                            outW = CInt(outH * aspectRatio)
                        End If
                    End If

                    Dim bmThumbnail As New Bitmap(outW, outH)
                    Dim gr As Graphics = Graphics.FromImage(bmThumbnail)

                    gr.Clear(clearColor)

                    gr.CompositingQuality = CompositingQuality.HighQuality
                    gr.SmoothingMode = SmoothingMode.HighQuality
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality

                    gr.DrawImage(original, New Rectangle(0, 0, outW, outH), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel)
                    pictureBoxRender.Image = bmThumbnail
                End Using

                GC.Collect()
                ' forces garbage collector to run. This is not recommended but can be used if situations arise. (like this)
                GC.WaitForPendingFinalizers()

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

    End Class

End Namespace
