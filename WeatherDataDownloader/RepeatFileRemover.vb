'Code Info
'https://msdn.microsoft.com/zh-tw/library/system.security.cryptography.sha256%28v=vs.110%29.aspx?cs-save-lang=1&cs-lang=vb#code-snippet-2

Imports System
Imports System.IO
Imports System.Security.Cryptography
Imports System.Windows.Forms

Public Class RepeatFileRemover

    Public Shared Sub RepeatFileRemoverMain(ByVal directory As String)
        Try
            Dim validfiles As Integer = 0
            Dim unrepeathashs(2000, 32) As Byte
            Dim repeatcheck As Boolean 'true = have same hash = have same file
            ' Create a DirectoryInfo object representing the specified directory.
            Dim dir As New DirectoryInfo(directory)
            ' Get the FileInfo objects for every file in the directory.
            Dim files As FileInfo() = dir.GetFiles()
            ' Initialize a SHA256 hash object.
            Dim mySHA256 As SHA256 = SHA256Managed.Create()
            Dim hashValue() As Byte
            ' Compute and print the hash values for each file in directory.
            Dim fInfo As FileInfo
            For Each fInfo In files
                ' Create a fileStream for the file.
                Dim fileStream As FileStream = fInfo.Open(FileMode.Open)
                ' Be sure it's positioned to the beginning of the stream.
                fileStream.Position = 0
                ' Compute the hash of the fileStream.
                hashValue = mySHA256.ComputeHash(fileStream)
                ' Close the file.
                fileStream.Close()
                ' check hash with preious files' hash.
                repeatcheck = False
                For i = 1 To validfiles
                    repeatcheck = True
                    For j = 0 To 31
                        If Not hashValue(j) = unrepeathashs(i, j) Then
                            repeatcheck = False
                            Exit For
                        End If
                    Next
                    If repeatcheck = True Then
                        Exit For
                    End If
                Next
                If repeatcheck = True Then ' new and write into list.
                    fInfo.Delete()
                ElseIf repeatcheck = False Then ' repeat and remove.
                    validfiles = validfiles + 1
                    For j = 0 To 31
                        unrepeathashs(validfiles, j) = hashValue(j)
                    Next
                End If
            Next fInfo
            Return
        Catch DExc As DirectoryNotFoundException
            Console.WriteLine("Error: The directory specified could not be found.")
        Catch IOExc As IOException
            Console.WriteLine("Error: A file in the directory could not be accessed.")
        End Try
    End Sub
End Class


