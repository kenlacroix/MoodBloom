Imports System
Imports System.Collections.Generic

Namespace Models
    ''' <summary>
    ''' Represents a single journal entry with timestamp, mood, tags, and text.
    ''' </summary>
    Public Class JournalEntry
        Public Property Timestamp As DateTime
        Public Property Mood As String
        Public Property Tags As List(Of String)
        Public Property Text As String

        Public Sub New()
            Timestamp = DateTime.UtcNow
            Mood = String.Empty
            Tags = New List(Of String)()
            Text = String.Empty
        End Sub
    End Class
End Namespace