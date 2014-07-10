''about download
''參考 http://www.blueshop.com.tw/board/fum20050124191756kkc/BRD20061213015535PG2.html 

''about format
''參考 http://fecbob.pixnet.net/blog/post/38111549-vb.net%E6%97%A5%E6%9C%9F%E5%9E%8B%E8%B3%87%E6%96%99%E8%99%95%E7%90%86%E6%96%B9%E6%B3%95
''     http://msdn.microsoft.com/zh-tw/library/3eaydw6e.aspx

''Developed by Lin Zhe-Hui

Imports System.Threading
Imports System.Threading.Thread

Public Class Main
    Dim RecordNum As Integer
    Dim Time As String
    Dim TimeForName As String
    Dim DoenloadThread As New System.Threading.Thread(AddressOf Download)
    Dim D As Integer
    Dim DN(9) As Integer
    Dim DS(9) As Integer
    ''______________________________________________________________________
    Dim TempString As String
    Dim DisplayUI As Boolean
    Dim ErrorLog(9) As Boolean
    Dim Enable(9) As Boolean
    Dim Period(9) As Integer
    Dim Radar_density As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Recoed Start
        Time = TimeString(0) + TimeString(1) + TimeString(3) + TimeString(4) + TimeString(6) + TimeString(7)
        RecordNum = FreeFile()
        FileOpen(RecordNum, Application.StartupPath + "\Record.txt", OpenMode.Append)
        PrintLine(RecordNum, DateString + "_" + TimeString + "_" + Time + "_" + "Downloading Task is Starting.")

        'Default
        DisplayUI = False
        For i = 1 To 8
            DS(i) = 0
            DN(i) = 0
            Enable(i) = True
            ErrorLog(i) = False
        Next
        Period(1) = 1
        Period(2) = 2
        Period(4) = 18
        Period(5) = 18
        Period(6) = 18
        Period(7) = 18
        Period(8) = 18
        Radar_density = 30

        'Read config.txt
        If Not Dir(Application.StartupPath + "\config.txt", vbArchive) = "" Then Call ReadConfig()

        'Download
        If DisplayUI = True Then
            DoenloadThread.Start()
        Else
            Call Download()
        End If
    End Sub

    Private Sub Done()
        PrintLine(RecordNum, DateString + "_" + TimeString + "_" + Time + "_" + "Downloading Task is Done.")
        PrintLine(RecordNum, "________________________________________________________")
        FileClose(RecordNum)
        End
    End Sub

    Private Sub PreRefreshState()
        If DisplayUI = True Then
            Me.Invoke(New MethodInvoker(AddressOf RefreshState))
        Else
            Call RefreshState()
        End If
    End Sub

    Private Sub RefreshState()
        If DS(1) = 1 Then
            Me.D1.BackColor = Color.Yellow
            Me.D1.Text = "日本氣象廳天氣圖 (下載中)"
        ElseIf DS(1) = 2 Then
            Me.D1.BackColor = Color.LightGreen
            Me.D1.Text = "日本氣象廳天氣圖 : 下載了" + DN(1).ToString + "個檔案 (已完成)"
        ElseIf DS(1) = 0 Then
            Me.D1.Text = "日本氣象廳天氣圖 (等待中)"
        ElseIf DS(1) = 3 Then
            Me.D1.Text = "日本氣象廳天氣圖 (取消)"
        End If
        If DS(2) = 1 Then
            Me.D2.BackColor = Color.Yellow
            Me.D2.Text = "CWB地面天氣圖 (下載中)"
        ElseIf DS(2) = 2 Then
            Me.D2.BackColor = Color.LightGreen
            Me.D2.Text = "CWB地面天氣圖 : 下載了" + DN(2).ToString + "個檔案 (已完成)"
        ElseIf DS(2) = 0 Then
            Me.D2.Text = "CWB地面天氣圖 (等待中)"
        ElseIf DS(2) = 3 Then
            Me.D2.Text = "CWB地面天氣圖 (取消)"
        End If
        If DS(3) = 1 Then
            Me.D3.BackColor = Color.Yellow
            Me.D3.Text = "斜溫圖 (下載中)"
        ElseIf DS(3) = 2 Then
            Me.D3.BackColor = Color.LightGreen
            Me.D3.Text = "斜溫圖 : 下載了" + DN(3).ToString + "個檔案 (已完成)"
        ElseIf DS(3) = 0 Then
            Me.D3.Text = "斜溫圖 (等待中)"
        ElseIf DS(3) = 3 Then
            Me.D3.Text = "斜溫圖 (取消)"
        End If
        If DS(4) = 1 Then
            Me.D4.BackColor = Color.Yellow
            Me.D4.Text = "雷達回波圖 (下載中)"
        ElseIf DS(4) = 2 Then
            Me.D4.BackColor = Color.LightGreen
            Me.D4.Text = "雷達回波圖 : 下載了" + DN(4).ToString + "個檔案 (已完成)"
        ElseIf DS(4) = 0 Then
            Me.D4.Text = "雷達回波圖 (等待中)"
        ElseIf DS(4) = 3 Then
            Me.D4.Text = "雷達回波圖 (取消)"
        End If
        If DS(5) = 1 Then
            Me.D5.BackColor = Color.Yellow
            Me.D5.Text = "衛星雲圖 (下載中)"
        ElseIf DS(5) = 2 Then
            Me.D5.BackColor = Color.LightGreen
            Me.D5.Text = "衛星雲圖 : 下載了" + DN(5).ToString + "個檔案 (已完成)"
        ElseIf DS(5) = 0 Then
            Me.D5.Text = "衛星雲圖 (等待中)"
        ElseIf DS(5) = 3 Then
            Me.D5.Text = "衛星雲圖 (取消)"
        End If
        If DS(6) = 1 Then
            Me.D6.BackColor = Color.Yellow
            Me.D6.Text = "溫度分布圖 (下載中)"
        ElseIf DS(6) = 2 Then
            Me.D6.BackColor = Color.LightGreen
            Me.D6.Text = "溫度分布圖 : 下載了" + DN(6).ToString + "個檔案 (已完成)"
        ElseIf DS(6) = 0 Then
            Me.D6.Text = "溫度分布圖 (等待中)"
        ElseIf DS(6) = 3 Then
            Me.D6.Text = "溫度分布圖 (取消)"
        End If
        If DS(7) = 1 Then
            Me.D7.BackColor = Color.Yellow
            Me.D7.Text = "日累積雨量圖 (下載中)"
        ElseIf DS(7) = 2 Then
            Me.D7.BackColor = Color.LightGreen
            Me.D7.Text = "日累積雨量圖 : 下載了" + DN(7).ToString + "個檔案 (已完成)"
        ElseIf DS(7) = 0 Then
            Me.D7.Text = "日累積雨量圖 (等待中)"
        ElseIf DS(7) = 3 Then
            Me.D7.Text = "日累積雨量圖 (取消)"
        End If
        If DS(8) = 1 Then
            Me.D8.BackColor = Color.Yellow
            Me.D8.Text = "流線圖 (下載中)"
        ElseIf DS(8) = 2 Then
            Me.D8.BackColor = Color.LightGreen
            Me.D8.Text = "流線圖 : 下載了" + DN(8).ToString + "個檔案 (已完成)"
        ElseIf DS(8) = 0 Then
            Me.D8.Text = "流線圖 (等待中)"
        ElseIf DS(8) = 3 Then
            Me.D8.Text = "流線圖 (取消)"
        End If

    End Sub
End Class
