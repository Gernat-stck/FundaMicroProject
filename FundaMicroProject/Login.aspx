<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="FundaMicroProject.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Acceso al Sistema - Gestión de Clientes</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light d-flex align-items-center justify-content-center" style="min-height: 100vh;">
    <form id="form1" runat="server" class="w-100" style="max-width: 400px;">
        <div class="card shadow-sm p-4">
            <div class="text-center mb-4">
                <h4 class="fw-bold">Iniciar Sesión</h4>
                <p class="text-muted small">Sistema de Gestión de Clientes</p>
            </div>

            <!-- Panel de Alerta para errores -->
            <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger py-2 small">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </asp:Panel>

            <div class="mb-3">
                <label for="txtUsuario" class="form-label small fw-bold">Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su usuario" required="required"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtPassword" class="form-label small fw-bold">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su contraseña" required="required"></asp:TextBox>
            </div>

            <div class="d-grid mt-4">
                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
            </div>
        </div>
    </form>
</body>
</html>