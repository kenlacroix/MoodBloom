Imports System
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports MoodBloom.Models

Namespace Services
    ''' <summary>
    ''' Handles persistence of journal entries using AES encryption.
    ''' </summary>
    Public Class FileStorageService
        Private ReadOnly _storageFolder As String
        Private ReadOnly _encryptionService As EncryptionService
        Private ReadOnly _rootKey As Byte()
        Private Const DataFileName As String = "journal.dat"

        ''' <summary>
        ''' Initialize storage service with folder, encryption service, and decrypted root key.
        ''' </summary>
        Public Sub New(storageFolder As String, encryptionService As EncryptionService, rootKey As Byte())
            _storageFolder = storageFolder
            _encryptionService = encryptionService
            _rootKey = rootKey
            If Not Directory.Exists(_storageFolder) Then
                Directory.CreateDirectory(_storageFolder)
            End If
        End Sub

        ''' <summary>
        ''' Appends an entry to the encrypted journal file.
        ''' </summary>
        Public Sub AppendEntry(entry As JournalEntry)
            ' Serialize entry to JSON
            Dim json As String = JsonSerializer.Serialize(Of JournalEntry)(entry, New JsonSerializerOptions() With {.WriteIndented = False})
            Dim plaintext As Byte() = Encoding.UTF8.GetBytes(json)
            ' Encrypt plaintext
            Dim ciphertext As Byte() = _encryptionService.Encrypt(plaintext, _rootKey)
            ' Write length-prefixed blob
            Dim filePath As String = Path.Combine(_storageFolder, DataFileName)
            Using fs As New FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None)
                ' Write blob length (4 bytes)
                Dim lengthBytes As Byte() = BitConverter.GetBytes(ciphertext.Length)
                fs.Write(lengthBytes, 0, lengthBytes.Length)
                ' Write blob
                fs.Write(ciphertext, 0, ciphertext.Length)
            End Using
        End Sub

        ''' <summary>
        ''' Loads and decrypts all entries from the journal file.
        ''' </summary>
        Public Function LoadAllEntries() As List(Of JournalEntry)
            Dim results As New List(Of JournalEntry)()
            Dim filePath As String = Path.Combine(_storageFolder, DataFileName)
            If Not File.Exists(filePath) Then
                Return results
            End If

            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
                While fs.Position < fs.Length
                    ' Read length prefix
                    Dim lengthBuffer(3) As Byte
                    fs.Read(lengthBuffer, 0, lengthBuffer.Length)
                    Dim blobLength As Integer = BitConverter.ToInt32(lengthBuffer, 0)
                    ' Read encrypted blob
                    Dim cipherBuffer As Byte() = New Byte(blobLength - 1) {}
                    fs.Read(cipherBuffer, 0, blobLength)
                    ' Decrypt
                    Dim plaintext As Byte() = _encryptionService.Decrypt(cipherBuffer, _rootKey)
                    Dim entryJson As String = Encoding.UTF8.GetString(plaintext)
                    ' Deserialize
                    Dim entry As JournalEntry = JsonSerializer.Deserialize(Of JournalEntry)(entryJson)
                    results.Add(entry)
                End While
            End Using

            Return results
        End Function
    End Class
End Namespace