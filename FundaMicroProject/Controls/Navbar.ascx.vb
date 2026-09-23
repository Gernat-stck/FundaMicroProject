Public Class Navbar
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Mostrar nombre de usuario en sesión
            If Session("NombreCompleto") IsNot Nothing AndAlso Session("Usuario") IsNot Nothing Then
                litUsuarioNavbar.Text = $"{Session("NombreCompleto")} ({Session("Usuario")})"
            End If

            ' Resaltar automáticamente la pestaña activa según la URL actual
            MarcarMenuActivo()
        End If
    End Sub

    Private Sub MarcarMenuActivo()
        Dim paginaActual As String = System.IO.Path.GetFileName(Request.Url.AbsolutePath).ToLower()

        Select Case paginaActual
            Case "clientes.aspx"
                lnkClientes.CssClass &= " active fw-bold"
            Case "usuarios.aspx"
                lnkUsuarios.CssClass &= " active fw-bold"
            Case "bitacora.aspx"
                lnkBitacora.CssClass &= " active fw-bold"
        End Select
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("~/Login.aspx", True)
    End Sub
End Class