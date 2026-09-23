Imports System.Collections.Specialized.BitVector32
Imports FundaMicroProject.BLL
Imports FundaMicroProject.Security

Public Class Bitacora
    Inherits BasePage

    Private ReadOnly _service As New BitacoraService()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarBitacora()
        End If
    End Sub

    Private Sub CargarBitacora()
        gvBitacora.DataSource = _service.ObtenerBitacora()
        gvBitacora.DataBind()
    End Sub

    Protected Sub btnRefrescar_Click(sender As Object, e As EventArgs)
        CargarBitacora()
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx", True)
    End Sub
End Class