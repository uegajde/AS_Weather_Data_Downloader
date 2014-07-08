Partial Class Main

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
End Class
