Imports System
Imports System.Windows.Forms

Module Program
    <STAThread>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        ' TODO: replace with your wizard entry form
        Application.Run(New FirstRunForm())
    End Sub
End Module
