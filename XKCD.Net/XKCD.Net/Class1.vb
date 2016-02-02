Imports System.Net
Imports Newtonsoft.Json

Public Class XKCD
    Public Function GetCurrentComic()
        Dim WC As New WebClient
        Dim CurrentComic As String = WC.DownloadString("http://xkcd.com/info.0.json")
        Dim LastComic As XKCDComic = JsonConvert.DeserializeObject(Of XKCDComic)(CurrentComic)
        Return LastComic
    End Function

    Public Function GetComicByID(ByVal ID As Integer)
        Try
            Dim WC As New WebClient
            Dim Comic As String = WC.DownloadString("http://xkcd.com/" & ID & "/info.0.json")
            Dim LastComic As XKCDComic = JsonConvert.DeserializeObject(Of XKCDComic)(Comic)
            Return LastComic
        Catch ex As Exception
            Throw New ComicNotFoundException("Info for comic " & ID & "Could not be retrieved.", ex)
        End Try
    End Function


    Class XKCDComic
        Public Property month As String
        Public Property num As Integer
        Public Property link As String
        Public Property year As String
        Public Property news As String
        Public Property safe_title As String
        Public Property transcript As String
        Public Property alt As String
        Public Property img As String
        Public Property title As String
        Public Property day As String
    End Class

End Class




<Serializable()>
Public Class ComicNotFoundException : Inherits System.Exception
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(ByVal message As String, ByVal innerException As System.Exception)
        MyBase.New(message, innerException)
    End Sub
End Class
