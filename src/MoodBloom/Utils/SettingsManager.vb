Imports System.IO
Imports System.Text.Json

Public Class SettingsManager
    Public Class Config
        Public Property Username As String
        Public Property EncryptedRootKey As String
        Public Property StoragePath As String
        Public Property Theme As String = "Light"
    End Class

    Private Const ConfigFileName = "config.json"

    Public Shared Function ConfigExists(basePath As String) As Boolean
        Return File.Exists(Path.Combine(basePath, ConfigFileName))
    End Function

    Public Shared Sub SaveConfig(cfg As Config, basePath As String)
        Dim json = JsonSerializer.Serialize(cfg, New JsonSerializerOptions With {.WriteIndented = True})
        File.WriteAllText(Path.Combine(basePath, ConfigFileName), json)
    End Sub

    Public Shared Function LoadConfig(basePath As String) As Config
        Dim path = path.Combine(basePath, ConfigFileName)
        Dim json = File.ReadAllText(path)
        Return JsonSerializer.Deserialize(Of Config)(json)
    End Function
End Class
