Imports System.Collections.Specialized.BitVector32
Imports FundaMicroProject.BLL
Imports FundaMicroProject.Models
Imports FundaMicroProject.Security

Public Class Clientes
    Inherits BasePage


    Private ReadOnly _service As New ClienteService()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarGrilla()
        End If
    End Sub

    Private Sub CargarGrilla()
        Try
            gvClientes.DataSource = _service.ObtenerClientes()
            gvClientes.DataBind()
        Catch ex As Exception
            MostrarAlerta("Error al cargar clientes: " & ex.Message, False)
        End Try
    End Sub

    Protected Sub btnNuevoCliente_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        litModalTitulo.Text = "Registrar Nuevo Cliente"
        AbrirModalCliente()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Try
            Dim cliente As New Cliente With {
                .DocumentoIdentidad = txtDocumento.Text.Trim(),
                .Nombres = txtNombres.Text.Trim(),
                .Apellidos = txtApellidos.Text.Trim(),
                .Telefono = txtTelefono.Text.Trim(),
                .Email = txtEmail.Text.Trim(),
                .Direccion = txtDireccion.Text.Trim()
            }

            If String.IsNullOrEmpty(hfIdCliente.Value) Then
                _service.RegistrarCliente(cliente, UsuarioSesion)
                MostrarAlerta("Cliente agregado correctamente.", True)
            Else
                cliente.IdCliente = Convert.ToInt32(hfIdCliente.Value)
                _service.ActualizarCliente(cliente, UsuarioSesion)
                MostrarAlerta("Cliente actualizado correctamente.", True)
            End If

            LimpiarFormulario()
            CargarGrilla()
        Catch ex As Exception
            MostrarAlerta(ex.Message, False)
            ' Si falla la validación, dejamos la modal abierta para que corrija
            AbrirModalCliente()
        End Try
    End Sub

    Protected Sub gvClientes_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim id As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "EditarCliente" Then
            Dim c As Cliente = _service.ObtenerCliente(id)
            If c IsNot Nothing Then
                hfIdCliente.Value = c.IdCliente.ToString()
                txtDocumento.Text = c.DocumentoIdentidad
                txtNombres.Text = c.Nombres
                txtApellidos.Text = c.Apellidos
                txtTelefono.Text = c.Telefono
                txtEmail.Text = c.Email
                txtDireccion.Text = c.Direccion

                litModalTitulo.Text = "Editar Cliente #" & c.IdCliente
                ' Solución al bug: Abre la modal con los datos cargados en el cliente
                AbrirModalCliente()
            Else
                MostrarAlerta("No se pudo recuperar la información del cliente.", False)
            End If

        ElseIf e.CommandName = "EliminarCliente" Then
            Try
                _service.EliminarCliente(id, UsuarioSesion)
                MostrarAlerta("Cliente eliminado con éxito.", True)
                CargarGrilla()
            Catch ex As Exception
                MostrarAlerta("Error al eliminar: " & ex.Message, False)
            End Try
        End If
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx", True)
    End Sub

    Private Sub AbrirModalCliente()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopModal", "abrirModal();", True)
    End Sub

    Private Sub LimpiarFormulario()
        hfIdCliente.Value = ""
        txtDocumento.Text = ""
        txtNombres.Text = ""
        txtApellidos.Text = ""
        txtTelefono.Text = ""
        txtEmail.Text = ""
        txtDireccion.Text = ""
    End Sub

    Private Sub MostrarAlerta(ByVal mensaje As String, ByVal esExito As Boolean)
        pnlMensaje.Visible = True
        pnlMensaje.CssClass = If(esExito, "alert alert-success alert-dismissible fade show", "alert alert-danger alert-dismissible fade show")
        litMensaje.Text = mensaje
    End Sub
End Class