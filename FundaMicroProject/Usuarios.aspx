<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Usuarios.aspx.vb" Inherits="FundaMicroProject.Usuarios" %>

<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Gestión de Usuarios</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
function abrirModalUsuario() {
    var modalEl = document.getElementById('modalUsuario');
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

            <div class="card shadow-sm border-0 mb-4">
                <div class="card-body d-flex justify-content-between align-items-center">
                    <div>
                        <h4 class="mb-0 fw-bold">Control de Usuarios</h4>
                        <p class="text-muted small mb-0">Gestión de accesos y credenciales del sistema</p>
                    </div>
                    <asp:Button ID="btnNuevoUsuario" runat="server" Text="+ Nuevo Usuario" CssClass="btn btn-primary" OnClick="btnNuevoUsuario_Click" CausesValidation="false" />
                </div>
            </div>

            <!-- Grilla de Usuarios -->
            <div class="card shadow-sm border-0">
                <div class="card-body p-0 table-responsive">
                    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-hover table-striped align-middle mb-0"
                        DataKeyNames="IdUsuario" OnRowCommand="gvUsuarios_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="IdUsuario" HeaderText="ID" ItemStyle-CssClass="fw-bold text-center" HeaderStyle-CssClass="text-center" />
                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                            <asp:TemplateField HeaderText="Estado" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <span class='badge <%# If(Convert.ToBoolean(Eval("Estado")), "bg-success", "bg-danger") %>'>
                                        <%# If(Convert.ToBoolean(Eval("Estado")), "Activo", "Inactivo") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Registro" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="EditarUsuario"
                                        CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn btn-sm btn-outline-primary" CausesValidation="false">
                                        Editar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- MODAL AGREGAR / EDITAR USUARIO -->
        <div class="modal fade" id="modalUsuario" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header bg-dark text-white">
                        <h5 class="modal-title fs-6">
                            <asp:Literal ID="litModalTitulo" runat="server" Text="Registrar Usuario"></asp:Literal></h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hfIdUsuario" runat="server" Value="" />

                        <div class="mb-3">
                            <label class="form-label small fw-bold">Nombre de Usuario *</label>
                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label class="form-label small fw-bold">Nombre Completo *</label>
                            <asp:TextBox ID="txtNombreCompleto" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label class="form-label small fw-bold">
                                <asp:Literal ID="litLabelPassword" runat="server" Text="Contraseña *"></asp:Literal>
                            </label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <!-- Texto aclaratorio solo visible al editar -->
                            <asp:Label ID="lblPassHelp" runat="server" CssClass="text-muted d-block small mt-1" Visible="false">
                                 (Deje en blanco si desea conservar la contraseña actual)
                            </asp:Label>
                        </div>
                        <div class="form-check form-switch mb-2">
                            <input type="checkbox" class="form-check-input" id="chkEstado" runat="server" checked="checked" />
                            <label class="form-check-label small fw-bold" for="chkEstado">Usuario Activo</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
