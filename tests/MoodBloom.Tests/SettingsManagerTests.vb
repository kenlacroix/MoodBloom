Imports Xunit
Imports System.IO

Public Class SettingsManagerTests
    <Fact>
    Public Sub SaveLoadConfig_RoundTrip_Works()
        Dim tmp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())
        Directory.CreateDirectory(tmp)
        Dim cfg As New SettingsManager.Config With {
          .Username = "alice",
          .EncryptedRootKey = "deadbeef",
          .StoragePath = "C:\foo",
          .Theme = "Dark"
        }
        SettingsManager.SaveConfig(cfg, tmp)
        Dim loaded = SettingsManager.LoadConfig(tmp)
        Assert.Equal(cfg.Username, loaded.Username)
        Assert.Equal(cfg.EncryptedRootKey, loaded.EncryptedRootKey)
        Assert.Equal(cfg.Theme, loaded.Theme)
    End Sub
End Class
