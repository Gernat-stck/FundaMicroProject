<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Bitacora.aspx.vb" Inherits="FundaMicroProject.Bitacora" %>
<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Bitácora de Auditoría</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <!-- Navbar global -->
        <uc:Navbar runat="server" ID="ucNavbar" />

        <div class="container pb-5">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h4 class="mb-0 fw-bold text-secondary">Registro de Actividades (Bitácora)</h4>
                <asp:Button ID="btnRefrescar" runat="server" Text="Actualizar Datos" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRefrescar_Click" />
            </div>

            <div class="card shadow-sm border-0">
                <div class="card-body p-0 table-responsive">
                    <asp:GridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-hover table-striped align-middle mb-0 small">
                        <Columns>
                            <asp:BoundField DataField="IdBitacora" HeaderText="# ID" ItemStyle-CssClass="fw-bold text-center" HeaderStyle-CssClass="text-center" />
                            <asp:TemplateField HeaderText="Acción" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <span class='badge <%# If(Eval("Accion").ToString() = "AGREGAR", "bg-success", If(Eval("Accion").ToString() = "EDITAR", "bg-warning text-dark", "bg-danger")) %>'>
                                        <%# Eval("Accion") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IdClienteAfectado" HeaderText="ID Cliente" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                            <asp:BoundField DataField="Detalle" HeaderText="Detalle de Operación" />
                            <asp:BoundField DataField="UsuarioResponsable" HeaderText="Usuario" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                            <asp:BoundField DataField="FechaHora" HeaderText="Fecha y Hora" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="p-4 text-center text-muted">No existen registros en la bitácora actualmente.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>