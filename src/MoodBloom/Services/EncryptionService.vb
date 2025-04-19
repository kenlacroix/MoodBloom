Imports System.Security.Cryptography

Public Class EncryptionService

    ''' <summary>
    ''' Derives a 256‑bit AES key from a password via PBKDF2.
    ''' </summary>
    Public Function DeriveKey(password As String, salt As Byte(), iterations As Integer) As Byte()
        Using deriveBytes As New Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256)
            Return deriveBytes.GetBytes(32)
        End Using
    End Function

    ''' <summary>
    ''' Placeholder for future AES‑CBC+HMAC encryption.
    ''' </summary>
    Public Function Encrypt(plaintext As Byte(), key As Byte()) As Byte()
        Throw New NotImplementedException()
    End Function

    ''' <summary>
    ''' Placeholder for future AES‑CBC+HMAC decryption.
    ''' </summary>
    Public Function Decrypt(ciphertext As Byte(), key As Byte()) As Byte()
        Throw New NotImplementedException()
    End Function

End Class
