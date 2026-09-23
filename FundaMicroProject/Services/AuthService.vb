Imports FundaMicroProject.DAL
Imports FundaMicroProject.Models
Imports FundaMicroProject.Security

Namespace BLL
    Public Class AuthService
        Private ReadOnly _usuarioDal As New UsuarioDAL()

        ''' <summary>
        ''' Valida las credenciales aplicando hashing a la clave en texto plano.
        ''' </summary>
        Public Function IniciarSesion(ByVal usuario As String, ByVal passwordPlano As String) As Usuario
            If String.IsNullOrWhiteSpace(usuario) OrElse String.IsNullOrWhiteSpace(passwordPlano) Then
                Throw New ArgumentException("Debe ingresar usuario y contraseña.")
            End If

            ' Generar hash SHA-256 antes de consultar
            Dim hashCalculado As String = HashHelper.GenerateSHA256(passwordPlano.Trim())

            ' Consultar a la capa de datos
            Dim usuarioValido As Usuario = _usuarioDal.ValidarCredenciales(usuario.Trim(), hashCalculado)

            If usuarioValido Is Nothing Then
                Throw New UnauthorizedAccessException("Usuario o contraseña incorrectos, o cuenta inactiva.")
            End If

            Return usuarioValido
        End Function
    End Class
End Namespace