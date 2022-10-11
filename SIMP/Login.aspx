<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SIMP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicar Sesión | SIMP</title>
    <link rel="stylesheet" href="Content/bootstrap.css" />
    <link rel="stylesheet" href="Content/Site.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400&display=swap" rel="stylesheet" />
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="login-style">
                    <div class="container login-font">
                        <div class="row justify-content-center">
                            <div class="login-title col-lg-4 d-flex align-items-center">
                                <h1 class="text-center mb-4">SIMP | Iniciar Sesión</h1>
                            </div>
                            <div class="col-lg-5 login-form">
                                <div class="mb-4">
                                    <label for="inputEmail">Usuario:</label>
                                    <asp:TextBox runat="server" ID="txtNombreUsuario" CssClass="form-control mt-2" placeholder="enavarro"></asp:TextBox>
                                </div>
                                <div class="pb-5">
                                    <label for="inputPassword">Contraseña:</label>
                                    <asp:TextBox runat="server" TextMode="Password" ID="txtContrasena" CssClass="form-control mt-2" placeholder="*******"></asp:TextBox>
                                </div>
                                <asp:LinkButton ID="btnIniciarSesion" class="btn login-btn w-100" runat="server" OnClick="btnIniciarSesion_Click">Iniciar Sesión</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
