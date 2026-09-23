Imports System.Data
Imports FundaMicroProject.DAL
Imports FundaMicroProject.Models
Imports FundaMicroProject.Security

Namespace BLL
    Public Class UsuarioService
        Private ReadOnly _usuarioDal As New UsuarioDAL()

        Public Function ObtenerUsuarios() As DataTable
            Return _usuarioDal.ListarTodos()
        End Function

        Public Function ObtenerUsuario(ByVal id As Integer) As Usuario
            Return _usuarioDal.ObtenerPorId(id)
        End Function

        Public Sub GuardarUsuario(ByVal u As Usuario, ByVal passwordPlana As String)
            If String.IsNullOrWhiteSpace(u.NombreUsuario) OrElse String.IsNullOrWhiteSpace(u.NombreCompleto) Then
                Throw New ArgumentException("El nombre de usuario y el nombre completo son obligatorios.")
            End If

            If u.IdUsuario = 0 Then
                ' Inserción: Contraseña obligatoria
                If String.IsNullOrWhiteSpace(passwordPlana) Then
                    Throw New ArgumentException("La contraseña es obligatoria para nuevos usuarios.")
                End If
                u.PasswordHash = HashHelper.GenerateSHA256(passwordPlana.Trim())
                _usuarioDal.Insertar(u)
            Else
                ' Edición: Solo genera nuevo hash si escribieron contraseña
                Dim cambiarPass As Boolean = Not String.IsNullOrWhiteSpace(passwordPlana)
                If cambiarPass Then
                    u.PasswordHash = HashHelper.GenerateSHA256(passwordPlana.Trim())
                End If
                _usuarioDal.Actualizar(u, cambiarPass)
            End If
        End Sub
    End Class
End Namespace