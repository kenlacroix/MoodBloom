Imports System.Security.Cryptography
Imports System.Text

Public Class EncryptionService
    ''' <summary>
    ''' Derives a 256‑bit AES key from a password via PBKDF2.
    ''' </summary>
    Public Function DeriveKey(password As String, salt As Byte(), iterations As Integer) As Byte()
        Using deriveBytes = New Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256)
            Return deriveBytes.GetBytes(32) ' 256 bits
        End Using
    End Function

    ''' <summary>
    ''' Encrypts plaintext bytes with AES‑CBC plus HMAC.
    ''' (Implementation TODO)
    ''' </summary>
    Public Function Encrypt(plaintext As Byte(), key As Byte()) As Byte()
        ' TODO: AES + HMAC
        Throw New NotImplementedException()
    End Function

    ''' <summary>
    ''' Decrypts ciphertext bytes with AES‑CBC plus HMAC verification.
    ''' (Implementation TODO)
    ''' </summary>
    Public Function Decrypt(ciphertext As Byte(), key As Byte()) As Byte()
        ' TODO: AES + HMAC
        Throw New NotImplementedException()
    End Function
End Class
