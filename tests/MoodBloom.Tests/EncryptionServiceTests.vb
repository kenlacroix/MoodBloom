Imports Xunit

Public Class EncryptionServiceTests
    <Fact>
    Public Sub DeriveKey_SameInput_ProducesSameKey()
        Dim salt = New Byte(15) {} ' 16‑byte zero salt
        Dim svc As New EncryptionService()
        Dim k1 = svc.DeriveKey("pass", salt, 10000)
        Dim k2 = svc.DeriveKey("pass", salt, 10000)
        Assert.Equal(k1, k2)
    End Sub
End Class
