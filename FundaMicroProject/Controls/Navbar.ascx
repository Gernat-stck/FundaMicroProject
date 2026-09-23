<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Navbar.ascx.vb" Inherits="FundaMicroProject.Navbar" %>

<nav class="navbar navbar-expand-lg navbar-dark bg-dark px-4 shadow-sm mb-4">
    <a class="navbar-brand fw-bold" href="Clientes.aspx">FUNDAMICRO</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navMenu">
        <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navMenu">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item">
                <asp:HyperLink ID="lnkClientes" runat="server" NavigateUrl="~/Clientes.aspx" CssClass="nav-link">Clientes</asp:HyperLink>
            </li>
            <li class="nav-item">
                <asp:HyperLink ID="lnkUsuarios" runat="server" NavigateUrl="~/Usuarios.aspx" CssClass="nav-link">Usuarios</asp:HyperLink>
            </li>
            <li class="nav-item">
                <asp:HyperLink ID="lnkBitacora" runat="server" NavigateUrl="~/Bitacora.aspx" CssClass="nav-link">Bitácora de Auditoría</asp:HyperLink>
            </li>
        </ul>
        <div class="d-flex align-items-center gap-3">
            <span class="text-light small">
                Usuario: <strong><asp:Literal ID="litUsuarioNavbar" runat="server"></asp:Literal></strong>
            </span>
            <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-outline-danger btn-sm" OnClick="btnCerrarSesion_Click" CausesValidation="false" />
        </div>
    </div>
</nav>