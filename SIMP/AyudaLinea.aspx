<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Content/bootstrap.css" />
    <script src="Content/plantilla/js/bootstrap.js"></script>

    <style>
        .encabezado {
            background-image: url("../../Content/img/bg-login.jpg");
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center;
            min-height: 15rem;
            color: white;
        }

            .encabezado h1 {
                padding-top: 5rem;
            }

        .estilo-ayuda {
            width: 85%;
            margin: 0 auto;
        }

        .estilo-ayuda div{
            text-align: center;
            margin: 3.5rem 0;
        }

        .estilo-ayuda ul{
            list-style: none;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="text-center encabezado">
            <h1>Ayuda en Línea</h1>
        </div>
        <div class="estilo-ayuda">
            <div class="row">
                <div class="col-lg-6" style="border-right: 1px solid #d9d9d9;">
                    <h5 class="card-title pb-2">Manual de Usuario</h5>
                    <a class="btn btn-primary" href="Content/ManualUsuarioSIMP.pdf" target="_blank">Ver</a>
                </div>
                <div class="col-lg-6">
                    <h5 class="card-title">Desarrollado por:</h5>
                    <h6 class="card-subtitle mb-2 text-muted"></h6>
                    <ul>
                        <li>Helberth Calvo Picado</li>
                        <li>Esteban Navarro Martínez</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="text-center pb-5">
            <a class="btn btn-primary" href="Proyecto.aspx">Volver al menú</a>
        </div>
        <hr />
        <div class="mt-2 text-center">
            <span style="display: block;">Version: 1.0</span>
        </div>
    </form>
</body>