'本程式是下載部分參考 http://www.blueshop.com.tw/board/fum20050124191756kkc/BRD20061213015535PG2.html 
'Developed by Lin Zhe-Hui

Imports System.Threading
Imports System.Threading.Thread

Public Class Form1
    Dim RecordNum As Integer
    Dim Time As String
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

    Private Sub ReadConfig()
        Dim ConfigFileNum As Integer
        Dim StartPoint As Integer
        ConfigFileNum = FreeFile()
        FileOpen(ConfigFileNum, Application.StartupPath + "\config.txt", OpenMode.Input)

        ''---General---
        TempString = LineInput(ConfigFileNum)  '---General---
        TempString = LineInput(ConfigFileNum)  'Display=true
        TempString = Trim(TempString)
        StartPoint = InStr(TempString, "=", CompareMethod.Text) + 1
        If Mid(TempString, StartPoint) = "true" Then DisplayUI = True
        ''---D1-JMA_Weather_Chart---
        D = 1
        TempString = LineInput(ConfigFileNum)  '---D1-JMA_Weather_Chart---
        TempString = LineInput(ConfigFileNum)  'Enabled=true
        Call GetEnabled()
        TempString = LineInput(ConfigFileNum)  'ErrorLog=false
        Call GetErrorLog()
        TempString = LineInput(ConfigFileNum)  'Period(day)=1
        Call GetPeriod()
        ''---D2-CWB_Surface_Weather_Chart---
        D = 2
        TempString = LineInput(ConfigFileNum)  '---D2-CWB_Surface_Weather_Chart---
        TempString = LineInput(ConfigFileNum)  'Enabled=true
        Call GetEnabled()
        TempString = LineInput(ConfigFileNum)  'ErrorLog=false
        Call GetErrorLog()
        TempString = LineInput(ConfigFileNum)  'Period(day)=2
        Call GetPeriod()
        ''---D3-CWB_skew---
        D = 3
        TempString = LineInput(ConfigFileNum)  '---D3-CWB_skew---
        TempString = LineInput(ConfigFileNum)  'Enabled=true
        Call GetEnabled()
        TempString = LineInput(ConfigFileNum)  'ErrorLog=false
        Call GetErrorLog()
        ''---D4-CWB_Radar---
        D = 4
        TempString = LineInput(ConfigFileNum)  '---D4-CWB_Radar---
        TempString = LineInput(ConfigFileNum)  'Enabled=true
        Call GetEnabled()
        TempString = LineInput(ConfigFileNum)  'ErrorLog=false
        Call GetErrorLog()
        TempString = LineInput(ConfigFileNum)  'Period(hour)=18
        Call GetPeriod()
        TempString = LineInput(ConfigFileNum)  'Density(min)=30
        Call GetDensity()
        ''---D5-CWB_Satellite---
        D = 5
        TempString = LineInput(ConfigFileNum)  '---D5-CWB_Satellite---
        TempString = LineInput(ConfigFileNum)  'Enabled=true
        Call GetEnabled()
        TempString = LineInput(ConfigFileNum)  'ErrorLog=false
        Call GetErrorLog()
        TempString = LineInput(ConfigFileNum)  'Period(hour)=18
        Call GetPeriod()
        ''---D6-CWB_Surface_Temperature---
        D = 6
        TempString = LineInput(ConfigFileNum)  '---D6-CWB_Surface_Temperature---
        TempString = LineInput(ConfigFileNum)  'Enabled=true
        Call GetEnabled()
        TempString = LineInput(ConfigFileNum)  'ErrorLog=false
        Call GetErrorLog()
        TempString = LineInput(ConfigFileNum)  'Period(hour)=18
        Call GetPeriod()
        ''---D7-CWB_Precipitation---
        D = 7
        TempString = LineInput(ConfigFileNum)  '---D7-CWB_Precipitation---
        TempString = LineInput(ConfigFileNum)  'Enabled=true
        Call GetEnabled()
        TempString = LineInput(ConfigFileNum)  'ErrorLog=false
        Call GetErrorLog()
        TempString = LineInput(ConfigFileNum)  'Period(hour)=18
        Call GetPeriod()
        ''---D8-CWB_Streamline---
        D = 8
        TempString = LineInput(ConfigFileNum)  '---D8-CWB_Streamline---
        TempString = LineInput(ConfigFileNum)  'Enabled=true
        Call GetEnabled()
        TempString = LineInput(ConfigFileNum)  'ErrorLog=false
        Call GetErrorLog()
        TempString = LineInput(ConfigFileNum)  'Period(hour)=18
        Call GetPeriod()

        FileClose(ConfigFileNum)
    End Sub

    Private Sub GetEnabled()
        Dim StartPoint As Integer
        TempString = Trim(TempString)
        StartPoint = InStr(TempString, "=", CompareMethod.Text) + 1
        If Mid(TempString, StartPoint) = "false" Then
            Enable(D) = False
            DS(D) = 3
        End If
    End Sub
    Private Sub GetErrorLog()
        Dim StartPoint As Integer
        TempString = Trim(TempString)
        StartPoint = InStr(TempString, "=", CompareMethod.Text) + 1
        If Mid(TempString, StartPoint) = "true" Then ErrorLog(D) = True
    End Sub
    Private Sub GetPeriod()
        Dim StartPoint As Integer
        TempString = Trim(TempString)
        StartPoint = InStr(TempString, "=", CompareMethod.Text) + 1
        TempString = Mid(TempString, StartPoint)
        Try
            Period(D) = Convert.ToInt16(TempString)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetDensity()
        Dim StartPoint As Integer
        TempString = Trim(TempString)
        StartPoint = InStr(TempString, "=", CompareMethod.Text) + 1
        TempString = Mid(TempString, StartPoint)
        Try
            Radar_density = Convert.ToInt16(TempString)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Done()
        PrintLine(RecordNum, DateString + "_" + TimeString + "_" + Time + "_" + "Downloading Task is Done.")
        PrintLine(RecordNum, "________________________________________________________")
        FileClose(RecordNum)
        End
    End Sub

    Private Sub Download()
        Dim StartTimeString As String
        Dim Hour As String
        Dim StartHour As Integer
        Dim Min As String
        Dim LastDay As Integer
        Dim LastDayString As String

        '日本氣象廳天氣圖_D1
        'http://www.hbc.co.jp/tecweather/archive/pdf/ASAS_042509.pdf  アジア地上解析天気図(ASAS) (every 6 hr)
        'http://www.hbc.co.jp/tecweather/archive/pdf/AUPQ78_042509.pdf アジア850hPa・700hPa天気図(AUPQ78) (every 12 hr)
        'http://www.hbc.co.jp/tecweather/archive/pdf/AUPQ35_042509.pdf アジア500hPa・300hPa天気図(AUPQ35) (every 12 hr)
        Me.D = 1
        If Me.Enable(D) = True Then
            Me.DS(D) = 1
            Call PreRefreshState()
            If Dir(Application.StartupPath + "\日本氣象廳天氣圖", vbDirectory) = "" Then MkDir(Application.StartupPath + "\日本氣象廳天氣圖")
            For i = 0 To Me.Period(D) Step 1
                LastDay = Microsoft.VisualBasic.Val(DateString(8)) * 10 + Microsoft.VisualBasic.Val(DateString(9)) - i
                LastDayString = LastDay.ToString
                If LastDayString.Length = 1 Then LastDayString = "0" + LastDayString
                DownloadFile("http://www.hbc.jp/tecweather/archive/pdf/ASAS_" + DateString(5) + DateString(6) + LastDayString + "21.pdf", Application.StartupPath + "\日本氣象廳天氣圖", "ASAS_" + DateString(5) + DateString(6) + LastDayString + "21.pdf")
                DownloadFile("http://www.hbc.jp/tecweather/archive/pdf/ASAS_" + DateString(5) + DateString(6) + LastDayString + "15.pdf", Application.StartupPath + "\日本氣象廳天氣圖", "ASAS_" + DateString(5) + DateString(6) + LastDayString + "15.pdf")
                DownloadFile("http://www.hbc.jp/tecweather/archive/pdf/ASAS_" + DateString(5) + DateString(6) + LastDayString + "09.pdf", Application.StartupPath + "\日本氣象廳天氣圖", "ASAS_" + DateString(5) + DateString(6) + LastDayString + "09.pdf")
                DownloadFile("http://www.hbc.jp/tecweather/archive/pdf/ASAS_" + DateString(5) + DateString(6) + LastDayString + "03.pdf", Application.StartupPath + "\日本氣象廳天氣圖", "ASAS_" + DateString(5) + DateString(6) + LastDayString + "03.pdf")
                DownloadFile("http://www.hbc.jp/tecweather/archive/pdf/AUPQ78_" + DateString(5) + DateString(6) + LastDayString + "21.pdf", Application.StartupPath + "\日本氣象廳天氣圖", "AUPQ78_" + DateString(5) + DateString(6) + LastDayString + "21.pdf")
                DownloadFile("http://www.hbc.jp/tecweather/archive/pdf/AUPQ78_" + DateString(5) + DateString(6) + LastDayString + "09.pdf", Application.StartupPath + "\日本氣象廳天氣圖", "AUPQ78_" + DateString(5) + DateString(6) + LastDayString + "09.pdf")
                DownloadFile("http://www.hbc.jp/tecweather/archive/pdf/AUPQ35_" + DateString(5) + DateString(6) + LastDayString + "21.pdf", Application.StartupPath + "\日本氣象廳天氣圖", "AUPQ35_" + DateString(5) + DateString(6) + LastDayString + "21.pdf")
                DownloadFile("http://www.hbc.jp/tecweather/archive/pdf/AUPQ35_" + DateString(5) + DateString(6) + LastDayString + "09.pdf", Application.StartupPath + "\日本氣象廳天氣圖", "AUPQ35_" + DateString(5) + DateString(6) + LastDayString + "09.pdf")
            Next
            Me.DS(D) = 2
            Call PreRefreshState()
        End If

        'CWB地面天氣圖_D2
        'http://www.cwb.gov.tw/V7/forecast/fcst/Data/2014-0508-0600_SFCcombo.jpg (every 6 hr)
        Me.D = 2
        If Me.Enable(D) = True Then
            Me.DS(D) = 1
            Call PreRefreshState()
            If Dir(Application.StartupPath + "\CWB地面天氣圖", vbDirectory) = "" Then MkDir(Application.StartupPath + "\CWB地面天氣圖")
            For i = 0 To Me.Period(D) Step 1
                LastDay = Microsoft.VisualBasic.Val(DateString(8)) * 10 + Microsoft.VisualBasic.Val(DateString(9)) - i
                LastDayString = LastDay.ToString
                If LastDayString.Length = 1 Then LastDayString = "0" + LastDayString
                DownloadFile("http://www.cwb.gov.tw/V7/forecast/fcst/Data/" + DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + LastDayString + "-0600_SFCcombo.jpg", Application.StartupPath + "\CWB地面天氣圖", DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + LastDayString + "-0600_SFCcombo.jpg")
                DownloadFile("http://www.cwb.gov.tw/V7/forecast/fcst/Data/" + DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + LastDayString + "-1200_SFCcombo.jpg", Application.StartupPath + "\CWB地面天氣圖", DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + LastDayString + "-1200_SFCcombo.jpg")
                DownloadFile("http://www.cwb.gov.tw/V7/forecast/fcst/Data/" + DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + LastDayString + "-1800_SFCcombo.jpg", Application.StartupPath + "\CWB地面天氣圖", DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + LastDayString + "-1800_SFCcombo.jpg")
                DownloadFile("http://www.cwb.gov.tw/V7/forecast/fcst/Data/" + DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + LastDayString + "-0000_SFCcombo.jpg", Application.StartupPath + "\CWB地面天氣圖", DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + LastDayString + "-0000_SFCcombo.jpg")
            Next
            Me.DS(D) = 2
            Call PreRefreshState()
        End If

        '斜溫圖_D3
        Me.D = 3
        If Me.Enable(D) = True Then
            Me.DS(D) = 1
            Call PreRefreshState()
            If Dir(Application.StartupPath + "\斜溫圖", vbDirectory) = "" Then MkDir(Application.StartupPath + "\斜溫圖")
            DownloadFile("http://www.cwb.gov.tw/V7/station/Data/SKW_46692.pdf", Application.StartupPath + "\斜溫圖", "SKW_46692_" + DateString + "_" + Time + ".pdf")
            DownloadFile("http://www.cwb.gov.tw/V7/station/Data/SKW_46699.pdf", Application.StartupPath + "\斜溫圖", "SKW_46699_" + DateString + "_" + Time + ".pdf")
            DownloadFile("http://www.cwb.gov.tw/V7/station/Data/SKW_46750.pdf", Application.StartupPath + "\斜溫圖", "SKW_46750_" + DateString + "_" + Time + ".pdf")
            Me.DS(3) = 2
            Call PreRefreshState()
        End If

        '雷達回波圖_D4
        'http://www.cwb.gov.tw/V7/observe/radar/Data/MOS2_1024/2014-04-20_1148.2MOS0.jpg
        Me.D = 4
        If Me.Enable(D) = True Then
            Me.DS(D) = 1
            Call PreRefreshState()
            StartTimeString = DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + "-" + DateString(8) + DateString(9)
            StartHour = Microsoft.VisualBasic.Val(TimeString(0)) * 10 + Microsoft.VisualBasic.Val(TimeString(1))
            If Dir(Application.StartupPath + "\雷達回波圖", vbDirectory) = "" Then MkDir(Application.StartupPath + "\雷達回波圖")
            For i = 0 To Me.Period(D)
                If StartHour - i >= 0 Then
                    Hour = (StartHour - i).ToString
                Else
                    LastDay = Microsoft.VisualBasic.Val(DateString(8)) * 10 + Microsoft.VisualBasic.Val(DateString(9)) - 1
                    StartTimeString = DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + "-"
                    If LastDay.ToString.Length = 1 Then StartTimeString = StartTimeString + "0"
                    StartTimeString = StartTimeString + LastDay.ToString()
                    Hour = (StartHour - i + 24).ToString
                End If
                If Hour.Length = 1 Then Hour = "0" + Hour
                For j = 0 To 54 Step Me.Radar_density
                    Min = j.ToString
                    If Min.Length = 1 Then Min = "0" + Min
                    DownloadFile("http://www.cwb.gov.tw/V7/observe/radar/Data/MOS2_1024/" + StartTimeString + "_" + Hour + Min + ".2MOS0.jpg", Application.StartupPath + "\雷達回波圖", StartTimeString + "_" + Hour + Min + ".2MOS0.jpg")
                Next
            Next
            Me.DS(D) = 2
            Call PreRefreshState()
        End If

        '衛星雲圖_D5 
        'http://www.cwb.gov.tw/V7/observe/satellite/Data/HSAO/HSAO-2014-04-20-11-30.jpg 
        'http://www.cwb.gov.tw/V7/observe/satellite/Data/HS1Q/HS1Q-2014-04-20-11-30.jpg
        Me.D = 5
        If Enable(D) = True Then
            Me.DS(D) = 1
            Call PreRefreshState()
            StartTimeString = DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + "-" + DateString(8) + DateString(9)
            StartHour = Microsoft.VisualBasic.Val(TimeString(0)) * 10 + Microsoft.VisualBasic.Val(TimeString(1))
            If Dir(Application.StartupPath + "\衛星雲圖", vbDirectory) = "" Then MkDir(Application.StartupPath + "\衛星雲圖")
            For i = 0 To Me.Period(D)
                If StartHour - i >= 0 Then
                    Hour = (StartHour - i).ToString
                Else
                    LastDay = Microsoft.VisualBasic.Val(DateString(8)) * 10 + Microsoft.VisualBasic.Val(DateString(9)) - 1
                    StartTimeString = DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + "-"
                    If LastDay.ToString.Length = 1 Then StartTimeString = StartTimeString + "0"
                    StartTimeString = StartTimeString + LastDay.ToString()
                    Hour = (StartHour - i + 24).ToString
                End If
                If Hour.Length = 1 Then Hour = "0" + Hour
                '整點
                DownloadFile("http://www.cwb.gov.tw/V7/observe/satellite/Data/HSAO/HSAO-" + StartTimeString + "-" + Hour + "-00.jpg", Application.StartupPath + "\衛星雲圖", "HSAO-" + StartTimeString + "-" + Hour + "-00.jpg") '可見光
                DownloadFile("http://www.cwb.gov.tw/V7/observe/satellite/Data/HS1Q/HS1Q-" + StartTimeString + "-" + Hour + "-00.jpg", Application.StartupPath + "\衛星雲圖", "HS1Q-" + StartTimeString + "-" + Hour + "-00.jpg") '色調強化
                '半點
                DownloadFile("http://www.cwb.gov.tw/V7/observe/satellite/Data/HSAO/HSAO-" + StartTimeString + "-" + Hour + "-30.jpg", Application.StartupPath + "\衛星雲圖", "HSAO-" + StartTimeString + "-" + Hour + "-30.jpg") '可見光
                DownloadFile("http://www.cwb.gov.tw/V7/observe/satellite/Data/HS1Q/HS1Q-" + StartTimeString + "-" + Hour + "-30.jpg", Application.StartupPath + "\衛星雲圖", "HS1Q-" + StartTimeString + "-" + Hour + "-30.jpg") '色調強化
            Next
            Me.DS(D) = 2
            Call PreRefreshState()
        End If

        '溫度分布圖_D6 
        'http://www.cwb.gov.tw/V7/observe/temperature/Data/2014-04-20_2000.GTP.jpg
        Me.D = 6
        If Me.Enable(D) = True Then
            Me.DS(D) = 1
            Call PreRefreshState()
            StartTimeString = DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + "-" + DateString(8) + DateString(9)
            StartHour = Microsoft.VisualBasic.Val(TimeString(0)) * 10 + Microsoft.VisualBasic.Val(TimeString(1))
            If Dir(Application.StartupPath + "\溫度分布圖 ", vbDirectory) = "" Then MkDir(Application.StartupPath + "\溫度分布圖 ")
            For i = 0 To Me.Period(D)
                If StartHour - i >= 0 Then
                    Hour = (StartHour - i).ToString
                Else
                    LastDay = Microsoft.VisualBasic.Val(DateString(8)) * 10 + Microsoft.VisualBasic.Val(DateString(9)) - 1
                    StartTimeString = DateString(0) + DateString(1) + DateString(2) + DateString(3) + "-" + DateString(5) + DateString(6) + "-" + LastDay.ToString
                    Hour = (StartHour - i + 24).ToString
                End If
                If Hour.Length = 1 Then Hour = "0" + Hour
                DownloadFile("http://www.cwb.gov.tw/V7/observe/temperature/Data/" + StartTimeString + "_" + Hour + "00.GTP.jpg", Application.StartupPath + "\溫度分布圖", StartTimeString + "_" + Hour + "00.GTP.jpg")
            Next
            Me.DS(D) = 2
            Call PreRefreshState()
        End If

        '日累積雨量圖_D7
        'http://www.cwb.gov.tw/V7/observe/rainfall/Data/hk420200.jpg
        Me.D = 7
        If Me.Enable(D) = True Then
            Me.DS(D) = 1
            Call PreRefreshState()
            StartTimeString = DateString(6) + DateString(8) + DateString(9)
            StartHour = Microsoft.VisualBasic.Val(TimeString(0)) * 10 + Microsoft.VisualBasic.Val(TimeString(1))
            If Dir(Application.StartupPath + "\日累積雨量圖", vbDirectory) = "" Then MkDir(Application.StartupPath + "\日累積雨量圖")
            For i = 0 To Me.Period(D)
                If StartHour - i >= 0 Then
                    Hour = (StartHour - i).ToString
                Else
                    LastDay = Microsoft.VisualBasic.Val(DateString(8)) * 10 + Microsoft.VisualBasic.Val(DateString(9)) - 1
                    StartTimeString = DateString(6) + LastDay.ToString
                    If LastDay.ToString.Length = 1 Then StartTimeString = DateString(6) + "0" + LastDay.ToString
                    Hour = (StartHour - i + 24).ToString
                End If
                If Hour.Length = 1 Then Hour = "0" + Hour
                DownloadFile("http://www.cwb.gov.tw/V7/observe/rainfall/Data/hk" + StartTimeString + Hour + "0.jpg", Application.StartupPath + "\日累積雨量圖", "HSAO-" + StartTimeString + "-" + Hour + "-00.jpg") '可見光
                DownloadFile("http://www.cwb.gov.tw/V7/observe/rainfall/Data/hk" + StartTimeString + Hour + "3.jpg", Application.StartupPath + "\日累積雨量圖", "HSAO-" + StartTimeString + "-" + Hour + "-30.jpg") '可見光
            Next
            Me.DS(D) = 2
            Call PreRefreshState()
        End If

        '流線圖_D8
        'http://www.cwb.gov.tw/V7/forecast/nwp/Data/GFS/GFS_14041918_DS2-GE_000.gif
        Me.D = 8
        If Me.Enable(D) = True Then
            Me.DS(D) = 1
            Call PreRefreshState()
            StartTimeString = DateString(2) + DateString(3) + DateString(5) + DateString(6) + DateString(8) + DateString(9)
            StartHour = Microsoft.VisualBasic.Val(TimeString(0)) * 10 + Microsoft.VisualBasic.Val(TimeString(1))
            If Dir(Application.StartupPath + "\流線圖 ", vbDirectory) = "" Then MkDir(Application.StartupPath + "\流線圖 ")
            For i = 0 To Me.Period(D)
                If StartHour - i - 6 >= 0 Then
                    Hour = (StartHour - i - 6).ToString
                Else
                    LastDay = Microsoft.VisualBasic.Val(DateString(8)) * 10 + Microsoft.VisualBasic.Val(DateString(9)) - 1
                    StartTimeString = DateString(2) + DateString(3) + DateString(5) + DateString(6) + LastDay.ToString
                    If LastDay.ToString.Length = 1 Then StartTimeString = DateString(2) + DateString(3) + DateString(5) + DateString(6) + "0" + LastDay.ToString
                    Hour = (StartHour - i + 16).ToString
                End If
                If Hour.Length = 1 Then Hour = "0" + Hour
                If (StartHour - i + 16) Mod 6 = 0 Then DownloadFile("http://www.cwb.gov.tw/V7/forecast/nwp/Data/GFS/GFS_" + StartTimeString + Hour + "_DS2-GE_000.gif", Application.StartupPath + "\流線圖", "GFS_" + StartTimeString + Hour + "_DS2-GE_000.gif")
            Next
            Me.DS(D) = 2
            Call PreRefreshState()
        End If

        Sleep(2000)
        If DisplayUI = True Then
            Me.Invoke(New MethodInvoker(AddressOf Done))
        Else
            Call Done()
        End If
    End Sub

    '用WebClient來下載
    '要使用的話呼叫DownloadFile並傳入3個參數,1是要下載的檔案的url,2是要存放在那邊,3是要存放的檔名,下載完成會傳回檔案的存放路徑,失敗的話 則回傳空字串
    'ex : DownloadFile("http://www.morpheus2005.altervista.org/xp%20Gallery/Windows%20XP%20Logo%20-%20Coloured%20Glass.jpg", "C:\", "aa.jpg") 
    Public Function DownloadFile(ByVal strUrl As String, ByVal strFile As String, ByVal strFileName As String) As String
        Try
            Dim strFilePath As String = strFile + "\" + strFileName
            Dim dwl As New System.Net.WebClient()
            dwl.DownloadFile(strUrl, strFilePath)
            dwl.Dispose()
            DN(D) = DN(D) + 1
            Return strFilePath
        Catch ex As Exception
            'MessageBox.Show("錯誤:" + ex.ToString, "錯誤通知", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If ErrorLog(D) = True Then PrintLine(RecordNum, DateString + "_" + TimeString + "_" + Time + "_" + "Error! file=" + strUrl + "   " + ex.ToString)
            Return ""
        End Try
    End Function

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
