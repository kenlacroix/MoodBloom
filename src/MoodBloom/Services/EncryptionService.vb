Imports System
Imports System.IO
Imports System.Security.Cryptography

Public Class EncryptionService

    ''' <summary>
    ''' Derives a 256‑bit AES key from a password via PBKDF2.
    ''' </summary>
    Public Function DeriveKey(password As String, salt As Byte(), iterations As Integer) As Byte()
        Using deriveBytes As New Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256)
            Return deriveBytes.GetBytes(32) ' 256 bits
        End Using
    End Function

    ''' <summary>
    ''' Encrypts plaintext with AES‑CBC, prefixing the IV to the ciphertext.
    ''' </summary>
    Public Function Encrypt(plaintext As Byte(), key As Byte()) As Byte()
        Using aes As Aes = Aes.Create()
            aes.Key = key
            aes.Mode = CipherMode.CBC
            aes.Padding = PaddingMode.PKCS7
            aes.GenerateIV()

            Using ms As New MemoryStream()
                ' Write IV at the front
                ms.Write(aes.IV, 0, aes.IV.Length)

                Using cs As New CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(plaintext, 0, plaintext.Length)
                    cs.FlushFinalBlock()
                End Using

                Return ms.ToArray()
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Decrypts ciphertext produced by this.Encrypt (expects IV prefix).
    ''' </summary>
    Public Function Decrypt(ciphertext As Byte(), key As Byte()) As Byte()
        Using aes As Aes = Aes.Create()
            aes.Key = key
            aes.Mode = CipherMode.CBC
            aes.Padding = PaddingMode.PKCS7

            ' Extract IV
            Dim ivLength As Integer = aes.BlockSize \ 8
            Dim iv(ivLength - 1) As Byte
            Array.Copy(ciphertext, 0, iv, 0, ivLength)
            aes.IV = iv

            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write)
                    ' Decrypt the rest of the buffer
                    cs.Write(ciphertext, ivLength, ciphertext.Length - ivLength)
                    cs.FlushFinalBlock()
                End Using

                Return ms.ToArray()
            End Using
        End Using
    End Function

End Class
