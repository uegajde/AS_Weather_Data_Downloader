<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意:  以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.D1 = New System.Windows.Forms.Label()
        Me.D2 = New System.Windows.Forms.Label()
        Me.D3 = New System.Windows.Forms.Label()
        Me.D4 = New System.Windows.Forms.Label()
        Me.D5 = New System.Windows.Forms.Label()
        Me.D6 = New System.Windows.Forms.Label()
        Me.D7 = New System.Windows.Forms.Label()
        Me.D8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'D1
        '
        Me.D1.AutoSize = True
        Me.D1.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.D1.Location = New System.Drawing.Point(12, 9)
        Me.D1.Name = "D1"
        Me.D1.Size = New System.Drawing.Size(161, 13)
        Me.D1.TabIndex = 5
        Me.D1.Text = "日本氣象廳天氣圖 (等待中)"
        '
        'D2
        '
        Me.D2.AutoSize = True
        Me.D2.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.D2.Location = New System.Drawing.Point(12, 22)
        Me.D2.Name = "D2"
        Me.D2.Size = New System.Drawing.Size(150, 13)
        Me.D2.TabIndex = 6
        Me.D2.Text = "CWB地面天氣圖 (等待中)"
        '
        'D3
        '
        Me.D3.AutoSize = True
        Me.D3.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.D3.Location = New System.Drawing.Point(12, 35)
        Me.D3.Name = "D3"
        Me.D3.Size = New System.Drawing.Size(96, 13)
        Me.D3.TabIndex = 7
        Me.D3.Text = "斜溫圖 (等待中)"
        '
        'D4
        '
        Me.D4.AutoSize = True
        Me.D4.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.D4.Location = New System.Drawing.Point(12, 48)
        Me.D4.Name = "D4"
        Me.D4.Size = New System.Drawing.Size(122, 13)
        Me.D4.TabIndex = 8
        Me.D4.Text = "雷達回波圖 (等待中)"
        '
        'D5
        '
        Me.D5.AutoSize = True
        Me.D5.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.D5.Location = New System.Drawing.Point(12, 61)
        Me.D5.Name = "D5"
        Me.D5.Size = New System.Drawing.Size(109, 13)
        Me.D5.TabIndex = 9
        Me.D5.Text = "衛星雲圖 (等待中)"
        '
        'D6
        '
        Me.D6.AutoSize = True
        Me.D6.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.D6.Location = New System.Drawing.Point(12, 74)
        Me.D6.Name = "D6"
        Me.D6.Size = New System.Drawing.Size(122, 13)
        Me.D6.TabIndex = 10
        Me.D6.Text = "溫度分布圖 (等待中)"
        '
        'D7
        '
        Me.D7.AutoSize = True
        Me.D7.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.D7.Location = New System.Drawing.Point(12, 87)
        Me.D7.Name = "D7"
        Me.D7.Size = New System.Drawing.Size(135, 13)
        Me.D7.TabIndex = 11
        Me.D7.Text = "日累積雨量圖 (等待中)"
        '
        'D8
        '
        Me.D8.AutoSize = True
        Me.D8.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.D8.Location = New System.Drawing.Point(12, 100)
        Me.D8.Name = "D8"
        Me.D8.Size = New System.Drawing.Size(96, 13)
        Me.D8.TabIndex = 12
        Me.D8.Text = "流線圖 (等待中)"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(319, 127)
        Me.Controls.Add(Me.D8)
        Me.Controls.Add(Me.D7)
        Me.Controls.Add(Me.D6)
        Me.Controls.Add(Me.D5)
        Me.Controls.Add(Me.D4)
        Me.Controls.Add(Me.D3)
        Me.Controls.Add(Me.D2)
        Me.Controls.Add(Me.D1)
        Me.Name = "Form1"
        Me.Text = "WeatherDataDownloader"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents D1 As System.Windows.Forms.Label
    Friend WithEvents D2 As System.Windows.Forms.Label
    Friend WithEvents D3 As System.Windows.Forms.Label
    Friend WithEvents D4 As System.Windows.Forms.Label
    Friend WithEvents D5 As System.Windows.Forms.Label
    Friend WithEvents D6 As System.Windows.Forms.Label
    Friend WithEvents D7 As System.Windows.Forms.Label
    Friend WithEvents D8 As System.Windows.Forms.Label

End Class
