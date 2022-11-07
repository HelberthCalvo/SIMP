<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GanttChart.aspx.cs" Inherits="SIMP.GanttChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Diagrama Gantt | SIMP </title>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/css/GanttStyle.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/prettify/r298/prettify.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
            font-family: Helvetica, Arial, sans-serif;
            font-size: 13px;
            padding: 0 0 50px 0;
        }

        h1 {
            margin: 40px 0 20px 0;
        }

        h2 {
            font-size: 1.5em;
            padding-bottom: 3px;
            border-bottom: 1px solid #DDD;
            margin-top: 50px;
            margin-bottom: 25px;
        }

        table th:first-child {
            width: 150px;
        }

        .github-corner:hover .octo-arm {
            animation: octocat-wave 560ms ease-in-out
        }

        @keyframes octocat-wave {
            0%, 100% {
                transform: rotate(0)
            }

            20%, 60% {
                transform: rotate(-25deg)
            }

            40%, 80% {
                transform: rotate(10deg)
            }
        }

        @media (max-width:500px) {
            .github-corner:hover .octo-arm {
                animation: none
            }

            .github-corner .octo-arm {
                animation: octocat-wave 560ms ease-in-out
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <h1 style="font-size: 2.3rem">Diagrama de Gantt</h1>
                <div class="gantt"></div>
                <div class="text-center">
                    <a class="btn btn-primary" href="Proyecto.aspx">Volver</a>
                </div>
            </div>
            <script src="Scripts/jquery-3.4.1.min.js"></script>
            <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-cookie/1.4.1/jquery.cookie.min.js"></script>
            <script src="Scripts/jquery.fn.gantt.js"></script>
            <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
            <script src="//cdnjs.cloudflare.com/ajax/libs/prettify/r298/prettify.min.js"></script>

            <script>

                var demoSource;

                function CargarDatos(data) {
                    demoSource = data;
                }

                $(function () {
                    "use strict";

                    $(".gantt").gantt({
                        source: demoSource,
                        navigate: "scroll",
                        scale: "weeks",
                        maxScale: "months",
                        minScale: "days",
                        dow: ["D", "L", "K", "M", "J", "V", "S"],
                        months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Deciembre"],
                        itemsPerPage: 10,
                        scrollToToday: false,
                        useCookie: true,
                        onRender: function () {
                            if (window.console && typeof console.log === "function") {
                                console.log("chart rendered");
                            }
                        }
                    });
                    prettyPrint();

                });
            </script>

        </div>
    </form>
</body>
</html>
