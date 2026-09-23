Imports FundaMicroProject.BLL
Imports FundaMicroProject.Models

Public Class Login
    Inherits System.Web.UI.Page

    Private ReadOnly _authService As New AuthService()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session.Clear()
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Try
            Dim usuario As Usuario = _authService.IniciarSesion(txtUsuario.Text, txtPassword.Text)

            ' Asignar variables de sesión
            Session("Usuario") = usuario.NombreUsuario
            Session("NombreCompleto") = usuario.NombreCompleto

            ' Redireccionar a la pantalla principal
            Response.Redirect("Clientes.aspx", False)
            Context.ApplicationInstance.CompleteRequest()

        Catch ex As UnauthorizedAccessException
            MostrarError(ex.Message)
        Catch ex As ArgumentException
            MostrarError(ex.Message)
        Catch ex As Exception
            MostrarError("Ocurrió un error inesperado al iniciar sesión: " & ex.Message)
        End Try
    End Sub

    Private Sub MostrarError(ByVal mensaje As String)
        pnlError.Visible = True
        litError.Text = mensaje
    End Sub
End Class