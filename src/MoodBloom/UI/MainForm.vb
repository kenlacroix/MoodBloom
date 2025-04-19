Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports MaterialSkin
Imports MaterialSkin.Controls
Imports MoodBloom.Models
Imports MoodBloom.Services

Public Class MainForm
    Inherits MaterialForm

    Private _storageService As FileStorageService
    Private _rootKey As Byte()
    Private cmbMood As MaterialComboBox
    Private txtTags As MaterialTextBox2
    Private txtEntry As MaterialMultiLineTextBox2
    Private btnSave As MaterialButton

    Public Sub New(storagePath As String, encryptionService As EncryptionService, rootKey As Byte())
        ' Initialize services
        _rootKey = rootKey
        _storageService = New FileStorageService(storagePath, encryptionService, rootKey)

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
        LoadSavedTags()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "MoodBloom – Journal"
        Me.Size = New Size(600, 500)

        ' Mood selector
        cmbMood = New MaterialComboBox() With {
            .Location = New Point(30, 80),
            .Size = New Size(200, 48)
        }
        cmbMood.Items.AddRange(New Object() {"Happy", "Sad", "Excited", "Grateful", "Anxious"})
        Controls.Add(cmbMood)

        ' Tags
        txtTags = New MaterialTextBox2() With {
            .Hint = "Tags (comma-separated)",
            .Location = New Point(250, 80),
            .Size = New Size(300, 48)
        }
        Controls.Add(txtTags)

        ' Entry text (multiline)
        txtEntry = New MaterialMultiLineTextBox2() With {
            .Hint = "Write your entry here...",
            .Location = New Point(30, 150),
            .Size = New Size(520, 240)
        }
        Controls.Add(txtEntry)

        ' Save button
        btnSave = New MaterialButton() With {
            .Text = "Save Entry",
            .Location = New Point(480, 410),
            .Size = New Size(100, 36)
        }
        AddHandler btnSave.Click, AddressOf OnSaveEntry
        Controls.Add(btnSave)
    End Sub

    Private Sub LoadSavedTags()
        Dim entries As List(Of JournalEntry) = _storageService.LoadAllEntries()
        Dim tagSet As New HashSet(Of String)()
        For Each entry As JournalEntry In entries
            For Each tagItem As String In entry.Tags
                tagSet.Add(tagItem)
            Next
        Next
        ' TODO: integrate tagSet into autocomplete logic
    End Sub

    Private Sub OnSaveEntry(sender As Object, e As EventArgs)
        Dim moodValue As String = If(cmbMood.SelectedItem IsNot Nothing, cmbMood.SelectedItem.ToString(), String.Empty)
        Dim tagsList As List(Of String) = New List(Of String)(txtTags.Text.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries))
        Dim entry As New JournalEntry() With {
            .Timestamp = DateTime.UtcNow,
            .Mood = moodValue,
            .Tags = tagsList,
            .Text = txtEntry.Text
        }

        _storageService.AppendEntry(entry)
        MessageBox.Show("Entry saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        txtEntry.Clear()
        txtTags.Clear()
    End Sub
End Class