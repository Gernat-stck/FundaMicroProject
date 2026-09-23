Imports System.Collections.Specialized.BitVector32
Imports FundaMicroProject.BLL
Imports FundaMicroProject.Models
Imports FundaMicroProject.Security

Public Class Usuarios
    Inherits BasePage


    Private ReadOnly _service As New UsuarioService()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarGrilla()
        End If
    End Sub

    Private Sub CargarGrilla()
        Try
            gvUsuarios.DataSource = _service.ObtenerUsuarios()
            gvUsuarios.DataBind()
        Catch ex As Exception
            MostrarAlerta("Error al cargar usuarios: " & ex.Message, False)
        End Try
    End Sub

    Protected Sub btnNuevoUsuario_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        litModalTitulo.Text = "Registrar Nuevo Usuario"
        litLabelPassword.Text = "Contraseña *"
        txtPassword.Attributes("required") = "required"
        lblPassHelp.Visible = False
        AbrirModal()
    End Sub

    Protected Sub btnGuardarUsuario_Click(sender As Object, e As EventArgs)
        Try
            Dim u As New Usuario With {
                .NombreUsuario = txtNombreUsuario.Text.Trim(),
                .NombreCompleto = txtNombreCompleto.Text.Trim(),
                .Estado = chkEstado.Checked
            }

            If Not String.IsNullOrEmpty(hfIdUsuario.Value) Then
                u.IdUsuario = Convert.ToInt32(hfIdUsuario.Value)
            End If

            _service.GuardarUsuario(u, txtPassword.Text)
            MostrarAlerta("Usuario procesado exitosamente.", True)
            LimpiarFormulario()
            CargarGrilla()
        Catch ex As Exception
            MostrarAlerta(ex.Message, False)
            AbrirModal()
        End Try
    End Sub

    Protected Sub gvUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "EditarUsuario" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            Dim u As Usuario = _service.ObtenerUsuario(id)
            If u IsNot Nothing Then
                hfIdUsuario.Value = u.IdUsuario.ToString()
                txtNombreUsuario.Text = u.NombreUsuario
                txtNombreCompleto.Text = u.NombreCompleto
                txtPassword.Text = ""
                chkEstado.Checked = u.Estado

                ' Modo edición: contraseña no obligatoria
                litModalTitulo.Text = "Editar Usuario #" & u.IdUsuario
                litLabelPassword.Text = "Nueva Contraseña"
                txtPassword.Attributes.Remove("required")
                lblPassHelp.Visible = True

                AbrirModal()
            End If
        End If
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx", True)
    End Sub

    Private Sub AbrirModal()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopModalUser", "abrirModalUsuario();", True)
    End Sub

    Private Sub LimpiarFormulario()
        hfIdUsuario.Value = ""
        txtNombreUsuario.Text = ""
        txtNombreCompleto.Text = ""
        txtPassword.Text = ""
        chkEstado.Checked = True
        txtPassword.Attributes("required") = "required"
        litLabelPassword.Text = "Contraseña *"
        lblPassHelp.Visible = False
    End Sub

    Private Sub MostrarAlerta(ByVal mensaje As String, ByVal esExito As Boolean)
        pnlMensaje.Visible = True
        pnlMensaje.CssClass = If(esExito, "alert alert-success alert-dismissible fade show", "alert alert-danger alert-dismissible fade show")
        litMensaje.Text = mensaje
    End Sub
End Class