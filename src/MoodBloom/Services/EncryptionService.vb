Imports System.Security.Cryptography

Public Class EncryptionService

    ''' <summary>
    ''' Derives a 256‑bit AES key from a password via PBKDF2.
    ''' </summary>
    Public Function DeriveKey(ByVal password As String, ByVal salt As Byte(), ByVal iterations As Integer) As Byte()
        Using deriveBytes As New Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256)
            Return deriveBytes.GetBytes(32) ' 256 bits
        End Using
    End Function

    ''' <summary>
    ''' Encrypts plaintext bytes with AES‑CBC. (HMAC not implemented yet)
    ''' </summary>
    Public Function Encrypt(ByVal plaintext As Byte(), ByVal key As Byte()) As Byte()
        Throw New NotImplementedException()
    End Function

    ''' <summary>
    ''' Decrypts ciphertext bytes with AES‑CBC. (HMAC not implemented yet)
    ''' </summary>
    Public Function Decrypt(ByVal ciphertext As Byte(), ByVal key As Byte()) As Byte()
        Throw New NotImplementedException()
    End Function

End Class
