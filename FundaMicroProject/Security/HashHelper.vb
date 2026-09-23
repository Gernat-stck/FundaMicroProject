Imports System.Security.Cryptography
Imports System.Text

Namespace Security
    Public Class HashHelper
        ''' <summary>
        ''' Convierte una cadena de texto en un hash SHA-256 en formato hexadecimal en minúsculas.
        ''' </summary>
        Public Shared Function GenerateSHA256(ByVal plainText As String) As String
            If String.IsNullOrEmpty(plainText) Then Return String.Empty

            Using sha256 As SHA256 = SHA256.Create()
                Dim bytesText As Byte() = Encoding.UTF8.GetBytes(plainText)
                Dim bytesHash As Byte() = sha256.ComputeHash(bytesText)

                Dim sb As New StringBuilder()
                For Each b As Byte In bytesHash
                    sb.Append(b.ToString("x2"))
                Next
                Return sb.ToString()
            End Using
        End Function
    End Class
End Namespace