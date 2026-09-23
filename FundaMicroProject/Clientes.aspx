<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Clientes.aspx.vb" Inherits="FundaMicroProject.Clientes" %>
<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Mantenimiento de Clientes</title>
    <!-- Estilos -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.0/font/bootstrap-icons.css" />
    
    <!-- Scripts en el Head para que existan antes del renderizado del Form -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
    function abrirModal() {
        var modalEl = document.getElementById('modalCliente');
        // @ts-ignore
       var myModal = bootstrap.Modal.getOrCreateInstance(modalEl);
      myModal.show();
    }
    </script>
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>

        <!-- Navbar global -->
       <uc:Navbar runat="server" ID="ucNavbar" />

        <div class="container pb-5">
            <!-- Mensaje de notificación -->
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-dismissible fade show" role="alert">
                <asp:Literal ID="litMensaje" runat="server"></asp:Literal>
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </asp:Panel>

            <!-- Encabezado y botón de Nuevo Cliente -->
            <div class="card shadow-sm border-0 mb-4">
                <div class="card-body d-flex justify-content-between align-items-center">
                    <div>
                        <h4 class="mb-0 fw-bold">Cartera de Clientes</h4>
                        <p class="text-muted small mb-0">Gestión y mantenimiento de registros</p>
                    </div>
                    <asp:Button ID="btnNuevoCliente" runat="server" Text="+ Nuevo Cliente" CssClass="btn btn-primary" OnClick="btnNuevoCliente_Click" CausesValidation="false" />
                </div>
            </div>

            <!-- Grilla de Clientes -->
            <div class="card shadow-sm border-0">
                <div class="card-body p-0 table-responsive">
                    <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-hover table-striped align-middle mb-0" 
                        DataKeyNames="IdCliente" OnRowCommand="gvClientes_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="IdCliente" HeaderText="ID" ItemStyle-CssClass="fw-bold text-center" HeaderStyle-CssClass="text-center" />
                            <asp:BoundField DataField="DocumentoIdentidad" HeaderText="Documento" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="EditarCliente" 
                                        CommandArgument='<%# Eval("IdCliente") %>' CssClass="btn btn-sm btn-outline-primary me-1" CausesValidation="false">
                                        Editar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarCliente" 
                                        CommandArgument='<%# Eval("IdCliente") %>' CssClass="btn btn-sm btn-outline-danger" 
                                        OnClientClick="return confirm('¿Confirma que desea eliminar este cliente?');" CausesValidation="false">
                                        Eliminar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="p-4 text-center text-muted">No se encontraron clientes registrados.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- MODAL BOOTSTRAP PARA AGREGAR / EDITAR CLIENTE -->
        <div class="modal fade" id="modalCliente" tabindex="-1" aria-labelledby="modalClienteLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header bg-dark text-white">
                        <h5 class="modal-title fs-6" id="modalClienteLabel">
                            <asp:Literal ID="litModalTitulo" runat="server" Text="Registrar Cliente"></asp:Literal>
                        </h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hfIdCliente" runat="server" Value="" />

                        <div class="mb-3">
                            <label class="form-label small fw-bold">Documento de Identidad *</label>
                            <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ej: 01234567-8"></asp:TextBox>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label small fw-bold">Nombres *</label>
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label small fw-bold">Apellidos *</label>
                                <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label small fw-bold">Teléfono</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej: 7890-1234"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label small fw-bold">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="correo@ejemplo.com"></asp:TextBox>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label class="form-label small fw-bold">Dirección</label>
                            <asp:TextBox ID="txtDireccion" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer bg-light">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-sm px-3" OnClick="btnGuardar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>