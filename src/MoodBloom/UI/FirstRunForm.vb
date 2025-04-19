Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports MaterialSkin
Imports MaterialSkin.Controls
Imports MoodBloom.Services

Namespace UI
    Public Class FirstRunForm
        Inherits MaterialForm

        ' Backing fields
        Private _wasCancelled As Boolean = True
        Private _selectedPath As String = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MoodBloom")
        Private _derivedRootKey() As Byte = Array.Empty(Of Byte)

        ' Public read‑only properties
        Public ReadOnly Property WasCancelled() As Boolean
            Get
                Return _wasCancelled
            End Get
        End Property

        Public ReadOnly Property SelectedPath() As String
            Get
                Return _selectedPath
            End Get
        End Property

        Public ReadOnly Property DerivedRootKey() As Byte()
            Get
                Return _derivedRootKey
            End Get
        End Property

        ' UI controls
        Private txtUsername As MaterialTextBox2
        Private txtPassword As MaterialTextBox2
        Private txtConfirm As MaterialTextBox2
        Private txtFolder As MaterialTextBox2
        Private btnBrowse As MaterialButton
        Private btnNext As MaterialButton
        Private btnCancel As MaterialButton

        Public Sub New()
            ' MaterialSkin setup
            Dim manager As MaterialSkinManager = MaterialSkinManager.Instance
            manager.AddFormToManage(Me)
            manager.Theme = MaterialSkinManager.Themes.LIGHT
            manager.ColorScheme = New ColorScheme(
                Primary.Blue600,
                Primary.Blue900,
                Primary.Blue200,
                Accent.LightBlue200,
                TextShade.WHITE)

            InitializeComponent()
        End Sub

        Private Sub InitializeComponent()
            Me.Text = "First Run – MoodBloom"
            Me.Size = New Size(500, 400)

            ' Username
            txtUsername = New MaterialTextBox2() With {
                .Hint = "Username",
                .Location = New Point(50, 80),
                .Size = New Size(400, 48)
            }
            Controls.Add(txtUsername)

            ' Password
            txtPassword = New MaterialTextBox2() With {
                .Hint = "Password",
                .UseSystemPasswordChar = True,
                .Location = New Point(50, 140),
                .Size = New Size(400, 48)
            }
            Controls.Add(txtPassword)

            ' Confirm
            txtConfirm = New MaterialTextBox2() With {
                .Hint = "Confirm Password",
                .UseSystemPasswordChar = True,
                .Location = New Point(50, 200),
                .Size = New Size(400, 48)
            }
            Controls.Add(txtConfirm)

            ' Folder picker
            txtFolder = New MaterialTextBox2() With {
                .Hint = "Storage Folder (optional)",
                .Location = New Point(50, 260),
                .Size = New Size(320, 48),
                .ReadOnly = True,
                .Text = _selectedPath
            }
            Controls.Add(txtFolder)

            btnBrowse = New MaterialButton() With {
                .Text = "Browse",
                .Location = New Point(380, 260),
                .Size = New Size(75, 36)
            }
            AddHandler btnBrowse.Click, AddressOf OnBrowse
            Controls.Add(btnBrowse)

            ' Next
            btnNext = New MaterialButton() With {
                .Text = "Next",
                .Location = New Point(300, 320),
                .Size = New Size(75, 36)
            }
            AddHandler btnNext.Click, AddressOf OnNext
            Controls.Add(btnNext)

            ' Cancel
            btnCancel = New MaterialButton() With {
                .Text = "Cancel",
                .Location = New Point(390, 320),
                .Size = New Size(75, 36)
            }
            AddHandler btnCancel.Click, AddressOf OnCancel
            Controls.Add(btnCancel)
        End Sub

        Private Sub OnBrowse(sender As Object, e As EventArgs)
            Using dlg As New FolderBrowserDialog()
                If dlg.ShowDialog() = DialogResult.OK Then
                    _selectedPath = dlg.SelectedPath
                    txtFolder.Text = _selectedPath
                End If
            End Using
        End Sub

        Private Sub OnNext(sender As Object, e As EventArgs)
            ' Validate inputs
            If String.IsNullOrWhiteSpace(txtUsername.Text) OrElse
               String.IsNullOrEmpty(txtPassword.Text) OrElse
               txtPassword.Text <> txtConfirm.Text Then
                MessageBox.Show("Please enter a username and matching passwords.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Derive root key
            Dim encSvc As New EncryptionService()
            Dim salt() As Byte = New Byte(15) {} ' TODO: generate random salt and persist
            _derivedRootKey = encSvc.DeriveKey(txtPassword.Text, salt, 100_000)

            _wasCancelled = False
            Me.Close()
        End Sub

        Private Sub OnCancel(sender As Object, e As EventArgs)
            _wasCancelled = True
            Me.Close()
        End Sub

    End Class
End Namespace