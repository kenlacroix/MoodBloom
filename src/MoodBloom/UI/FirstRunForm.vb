Imports MaterialSkin
Imports MaterialSkin.Controls

Public Class FirstRunForm
    Inherits MaterialForm

    Private txtUsername As MaterialTextBox2
    Private txtPassword As MaterialTextBox2
    Private txtConfirm As MaterialTextBox2
    Private txtFolder As MaterialTextBox2
    Private btnBrowse As MaterialButton
    Private btnNext As MaterialButton
    Private btnCancel As MaterialButton

    Public Sub New()
        ' Initialize MaterialSkin manager
        Dim manager = MaterialSkinManager.Instance
        manager.AddFormToManage(Me)
        manager.Theme = MaterialSkinManager.Themes.LIGHT
        manager.ColorScheme = New ColorScheme(Primary.Blue600, Primary.Blue900, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE)

        ' Build UI controls
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "MoodBloom – First Run"
        Me.Size = New Size(500, 400)

        ' Username
        txtUsername = New MaterialTextBox2() With {
            .Hint = "Username",
            .Location = New Point(50, 100),
            .Size = New Size(400, 48)
        }
        Controls.Add(txtUsername)

        ' Password
        txtPassword = New MaterialTextBox2() With {
            .Hint = "Password",
            .UseSystemPasswordChar = True,
            .Location = New Point(50, 160),
            .Size = New Size(400, 48)
        }
        Controls.Add(txtPassword)

        ' Confirm
        txtConfirm = New MaterialTextBox2() With {
            .Hint = "Confirm Password",
            .UseSystemPasswordChar = True,
            .Location = New Point(50, 220),
            .Size = New Size(400, 48)
        }
        Controls.Add(txtConfirm)

        ' Folder picker
        txtFolder = New MaterialTextBox2() With {
            .Hint = "Storage Folder (optional)",
            .Location = New Point(50, 280),
            .Size = New Size(320, 48),
            .ReadOnly = True
        }
        Controls.Add(txtFolder)

        btnBrowse = New MaterialButton() With {
            .Text = "Browse",
            .Location = New Point(380, 280),
            .Size = New Size(70, 36)
        }
        AddHandler btnBrowse.Click, AddressOf OnBrowse
        Controls.Add(btnBrowse)

        ' Next & Cancel
        btnNext = New MaterialButton() With {
            .Text = "Next",
            .Location = New Point(300, 340),
            .Size = New Size(75, 36)
        }
        AddHandler btnNext.Click, AddressOf OnNext
        Controls.Add(btnNext)

        btnCancel = New MaterialButton() With {
            .Text = "Cancel",
            .Location = New Point(390, 340),
            .Size = New Size(75, 36)
        }
        AddHandler btnCancel.Click, AddressOf Sub(s, e) Me.Close()
        Controls.Add(btnCancel)
    End Sub

    Private Sub OnBrowse(sender As Object, e As EventArgs)
        Using dlg As New FolderBrowserDialog()
            If dlg.ShowDialog() = DialogResult.OK Then
                txtFolder.Text = dlg.SelectedPath
            End If
        End Using
    End Sub

    Private Sub OnNext(sender As Object, e As EventArgs)
        ' TODO: validate inputs, derive root key, save config, then:
        ' If successful, Me.Close() and Application.Run(MainForm)
    End Sub
End Class
