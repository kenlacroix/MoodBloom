Imports System
Imports System.Windows.Forms
Imports MoodBloom.Services
Imports MoodBloom.UI

Module Program
    <STAThread>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Show first‑run wizard
        Dim wizard As New FirstRunForm()
        wizard.ShowDialog()
        If wizard.WasCancelled Then Return

        ' Launch main UI
        Dim encSvc As New EncryptionService()
        Application.Run(New MainForm(wizard.SelectedPath, encSvc, wizard.DerivedRootKey))
    End Sub
End Module