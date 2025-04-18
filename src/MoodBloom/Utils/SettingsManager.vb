Imports System.IO
Imports System.Text.Json

Public Class SettingsManager

    Public Class Config
        Public Property Username As String
        Public Property EncryptedRootKey As String
        Public Property StoragePath As String
        Public Property Theme As String = "Light"
    End Class

    Private Const ConfigFileName As String = "config.json"

    Public Shared Function ConfigExists(ByVal basePath As String) As Boolean
        Dim fullPath As String = Path.Combine(basePath, ConfigFileName)
        Return File.Exists(fullPath)
    End Function

    Public Shared Sub SaveConfig(ByVal cfg As Config, ByVal basePath As String)
        Dim options As JsonSerializerOptions = New JsonSerializerOptions With {
            .WriteIndented = True
        }
        Dim json As String = JsonSerializer.Serialize(Of Config)(cfg, options)
        Dim fullPath As String = Path.Combine(basePath, ConfigFileName)
        File.WriteAllText(fullPath, json)
    End Sub

    Public Shared Function LoadConfig(ByVal basePath As String) As Config
        Dim fullPath As String = Path.Combine(basePath, ConfigFileName)
        Dim json As String = File.ReadAllText(fullPath)
        Dim cfg As Config = JsonSerializer.Deserialize(Of Config)(json)
        Return cfg
    End Function

End Class
