<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SIMP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicar Sesión | SIMP</title>
    <link rel="stylesheet" href="Content/bootstrap.css" />
    <link rel="stylesheet" href="Content/Site.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400&display=swap" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-style">
            <div class="container login-font">
                <div class="row justify-content-center">
                    <div class="login-title col-lg-4 d-flex align-items-center">
                        <h1 class="text-center mb-4">SIMP | Iniciar Sesión</h1>
                    </div>
                    <div class="col-lg-5 login-form">
                        <div class="mb-4">
                            <label for="inputEmail">Usuario</label>
                            <input class="form-control mt-2" id="inputEmail" type="email" placeholder="name@example.com" />
                        </div>
                        <div class="mb-5">
                            <label for="inputPassword">Contraseña</label>
                            <input class="form-control mt-2" id="inputPassword" type="password" placeholder="*******" />
                        </div>
                        <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                            <a class="btn login-btn w-100" href="index.html">Iniciar Sesión</a>
                        </div>
                        <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                            <a class="btn btn-white w-100" href="index.html" style="border: 1px solid #E9E9E9">Iniciar Sesión con Google</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
