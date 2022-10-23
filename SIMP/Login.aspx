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
    <link href="Content/css/LoginStyle.css" rel="stylesheet" />
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-6 col-md-7 intro-section">
                            <div class="brand-wrapper">
                                <img src="Content/img/logo-sitsa.png" width="40%" height="auto" />
                            </div>
                            <div class="intro-content-wrapper">
                                <h1 class="intro-title">Bienvenido a SIMP</h1>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-5 form-section">
                            <div class="login-wrapper">
                                <h2 class="login-title text-center">Iniciar Sesión</h2>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtNombreUsuario" CssClass="form-control" placeholder="Nombre de usuario" ToolTip="Por favor rellene este campo."></asp:TextBox>
                                </div>
                                <div class="form-group mb-3">
                                    <asp:TextBox runat="server" TextMode="Password" ID="txtContrasena" CssClass="form-control" placeholder="Contraseña" ToolTip="Por favor rellene este campo."></asp:TextBox>
                                </div>
                                <div class="d-flex justify-content-between align-items-center mb-5 mt-5">
                                    <asp:LinkButton ID="btnIniciarSesion" class="btn login-btn w-100" runat="server" OnClick="btnIniciarSesion_Click">Iniciar Sesión</asp:LinkButton>
                                </div>
                                <p class="login-wrapper-footer-text">Olvidó su contraseña? <a href="#!" class="text-reset">Recuperar contraseña</a></p>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
