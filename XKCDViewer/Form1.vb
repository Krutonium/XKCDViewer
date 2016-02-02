Imports XKCD.Net.XKCD
Imports System.Net
Imports System.IO

Public Class XKCDViewer
    Dim WS As New WebClient
    Dim CurrentComic As Integer
    Dim MaxComic As Integer = 0
    Private Sub XKCDViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.XKCD_Favicon
        ChangeComic(0, True)
    End Sub

    Private Sub cmdPrev_Click(sender As Object, e As EventArgs) Handles cmdPrev.Click
        ChangeComic(CurrentComic - 1)
    End Sub

    Private Sub cmdNext_Click(sender As Object, e As EventArgs) Handles cmdNext.Click
        ChangeComic(CurrentComic + 1)
    End Sub
    Private Sub cmdNewset_Click(sender As Object, e As EventArgs) Handles cmdNewset.Click
        ChangeComic(MaxComic)
    End Sub

    Private Sub cmdFirst_Click(sender As Object, e As EventArgs) Handles cmdFirst.Click
        ChangeComic(1)
    End Sub
    Private Sub cmdRandom_Click(sender As Object, e As EventArgs) Handles cmdRandom.Click
        Dim Rand As New Random
        Randomize()
        ChangeComic(Rand.Next(1, MaxComic))
    End Sub


    Private Sub ChangeComic(ByVal ID As Integer, Optional ByVal InitialSetup As Boolean = False)

        If InitialSetup = True Then
            Dim XKCD As New XKCD.Net.XKCD
            Dim Out As XKCDComic = XKCD.GetCurrentComic()
            MaxComic = Out.num
            ChangeComic(Out.num)
        Else
            Try
                Dim XKCD As New XKCD.Net.XKCD
                Dim Out As XKCDComic = XKCD.GetComicByID(ID)
                Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(WS.DownloadData(Out.img)))
                PictureBox1.Image = tImage
                CurrentComic = Out.num
                If CurrentComic = MaxComic Then
                    cmdNext.Enabled = False
                Else
                    cmdNext.Enabled = True
                End If
                If CurrentComic = 1 Then
                    cmdPrev.Enabled = False
                Else
                    cmdPrev.Enabled = True
                End If
                Me.Text = "Comic " & CurrentComic & ": " & Out.title
                Tipper.SetToolTip(PictureBox1, Out.alt)
            Catch ex As Exception
                Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(WS.DownloadData("http://globalgeeknews.com/wp-content/uploads/2011/05/Super-Mario-Bros-404-error.jpg")))
                PictureBox1.Image = tImage
                If CurrentComic = MaxComic Then
                    cmdNext.Enabled = False
                Else
                    cmdNext.Enabled = True
                End If
                If CurrentComic = 1 Then
                    cmdPrev.Enabled = False
                Else
                    cmdPrev.Enabled = True
                End If
                Me.Text = "Comic: " & ID & ": This image 404'd. It either does not exist, or is not an image."
                Tipper.SetToolTip(PictureBox1, "This image 404'd.")
                CurrentComic = ID
            End Try

        End If
    End Sub


End Class
