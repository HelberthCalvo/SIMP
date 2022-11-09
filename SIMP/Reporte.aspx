<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporte.aspx.cs" Inherits="SIMP.Reporte" MasterPageFile="~/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Style" runat="server">
    <%--    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-eOJMYsd53ii+scO/bJGFsiCZc+5NDVN2yr8+0RDqr0Ql0h+rP48ckxlpbzKgwra6" crossorigin="anonymous">--%>
    <%
//if (Session["Compañia"] == null || ((UsuarioEntidad)(Session["UsuarioSistema"])).Usuario_Sistema; == null)
//{
//    Response.Redirect("Login.aspx", false);
//}
    %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <contenttemplate>
            <asp:HiddenField ID="hfIdPerfil" runat="server" />
            <asp:HiddenField ID="hfIdUsuario" runat="server" />
            <asp:HiddenField ID="hdfPermisoEnviarCorreos" Value="0" runat="server" />
        </contenttemplate>
    </asp:UpdatePanel>

    <style>
        .nav-pills .nav-link.active,
        .nav-pills .show > .nav-link {
            color: #fff;
            background-color: #343a40 !important;
        }
    </style>

    <h2 class="fs-4">Reportes</h2>
    <hr />

    <div class="col-12">

        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
            <contenttemplate>
                <div class=" row pt-4 pb-4" runat="server" id="mensajePermiso" visible="false">
                    <div class="alert alert-danger col-lg-10 col-md-10 col-sm-10 col-xs-10 text-center offset-1 " role="alert">
                        <i class="fa fa-info-circle"></i>&nbsp;
                  <asp:Label ID="lblMensajePermisos" runat="server"></asp:Label>
                    </div>
                </div>
            </contenttemplate>
        </asp:UpdatePanel>



        <div id="example-manipulation" class="mt-2 wizard clearfix" role="application">

            <ul class="nav nav-pills mb-3 nav-fill" id="pills-tab" role="tablist">
                <li class="nav-item" role="presentation">
                    <button class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Progreso Proyecto</button>
                </li>
                <li class="nav-item" role="presentation" style="display: none">
                    <button class="nav-link" id="pills-contact-tab" data-bs-toggle="pill" data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact" aria-selected="false">Tiempos</button>
                </li>
                <li class="nav-item" role="presentation" style="display: none">
                    <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Carga Trabajo</button>
                </li>

            </ul>
            <div class="tab-content" id="pills-tabContent">
                <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                    <section id="example-manipulation-p-0" role="tabpanel" aria-labelledby="example-manipulation-h-0" class="body current" aria-hidden="false">
                        <asp:UpdatePanel runat="server">
                            <contenttemplate>
                                <div class="row mt-4">

                                    <div class="col-lg-6">
                                        <div class="mb-3">
                                            <label for="title-3" style="color: black !important">Descripción *</label>
                                            <asp:TextBox ID="txtDescripcionPerfil" Style="color: black !important; border: 3px solid #d8eeef !important;" placeholder="Ejemplo: perfil 01" CssClass="form-control mt-2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">

                                        <div class="mb-3">
                                            <label style="color: black !important">Estado</label>
                                            <br />
                                            <div class="form-check mt-1">
                                                <asp:RadioButton ID="rdbActivoPerfil" Style="color: black !important; background: white !important" Checked="true" class="form-check-input" GroupName="groupEstadoPerfil" runat="server" />

                                                <label class="form-check-label" style="color: black !important">Activo</label>
                                            </div>
                                            <div class="form-check">
                                                <asp:RadioButton ID="rdbInactivoPerfil" Style="color: black !important; background: white !important" class="form-check-input" GroupName="groupEstadoPerfil" runat="server" />
                                                <label class="form-check-label" style="color: black !important" for="customRadio22">Inactivo</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="mb-4">
                                            <label class="lead fs-6">Campos requeridos *</label>
                                        </div>
                                    </div>
                                </div>
                            </contenttemplate>
                        </asp:UpdatePanel>
                        <br />

                        <asp:UpdatePanel runat="server">
                            <contenttemplate>
                                <div class="form-actions">
                                    <div class="card-body d-flex flex-row justify-content-center flex-wrap">
                                        <asp:LinkButton ID="btnBuscarProgresoProyecto" OnClick="btnBuscarProgresoProyecto_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                            Buscar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </contenttemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel runat="server">
                            <contenttemplate>
                                <asp:GridView ID="gvProgresoProyecto" CssClass="table-responsive table customize-table v-middle" Style="width: 100%!important"
                                    DataKeyNames="IdProyecto, Nombre_Proyecto, Nombre_Fase, Fecha_Inicio, Porcentaje" OnPreRender="gvProgresoProyecto_PreRender"
                                    OnRowCommand="gvProgresoProyecto_RowCommand"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-CssClass="table-dark"
                                    runat="server">
                                    <columns>    
                                        <asp:BoundField DataField="Nombre_Proyecto" HeaderText="Proyecto" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Fase" HeaderText="Nombre_Fase" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha_Inicio" HeaderText="Fecha" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha_Inicio" HeaderText="Fecha" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Generar PDF" CommandName="GenerarPDF" Text="<i class='fas fa-xl fa-file-pdf'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Enviar Correo" CommandName="EnviarCorreo" Text="<i class='fas fa-xl fa-envelope'></i>" ItemStyle-HorizontalAlign="Center" />
                                    </columns>
                                    <headerstyle backcolor="#4285f4" font-bold="True" forecolor="White" horizontalalign="Center" borderstyle="Solid" bordercolor="Black" />

                                </asp:GridView>
                            </contenttemplate>
                        </asp:UpdatePanel>

                    </section>
                </div>

                <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
                    <section role="tabpanel" aria-labelledby="example-manipulation-h-0" class="body current" aria-hidden="false">
                        <asp:UpdatePanel runat="server">
                            <contenttemplate>
                                <div class="row mt-4">
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label" style="color: black !important;">Usuario *</label>
                                            <asp:TextBox ID="txtUsuario" Style="color: black !important; border: 3px solid #d8eeef !important;" placeholder="Ejemplo: admin" CssClass="form-control mt-2" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label" style="color: black !important;">Nombre *</label>
                                            <asp:TextBox ID="txtNombre" Style="color: black !important; border: 3px solid #d8eeef !important;" placeholder="Ejemplo: usuario 01" CssClass="form-control mt-2" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label" style="color: black !important;">Correo electrónico *</label>
                                            <asp:TextBox ID="txtCorreo" Style="color: black !important; border: 3px solid #d8eeef !important;" placeholder="Ejemplo: admin@mail.com" CssClass="form-control mt-2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label" style="color: black !important;">Perfil</label>
                                            <asp:DropDownList ID="ddlPerfil" DataValueField="ID" Style="color: black !important; border: 3px solid #d8eeef !important;" DataTextField="Descripcion" CssClass="form-control mt-2" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label">Contraseña</label>
                                            <asp:TextBox ID="txtContraseña" type="password" placeholder="Contraseña" CssClass="form-control mt-2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-6">
                                        <div class="mb-3">
                                            <label style="color: black !important;">Estado</label>
                                            <br />
                                            <div class="form-check mt-1">
                                                <asp:RadioButton Checked="true" Style="color: black !important; background: white !important" ID="rdbActivoUsuario" class="form-check-input" GroupName="groupEstadoUsuario" runat="server" />

                                                <label class="form-check-label" style="color: black !important;">Activo</label>
                                            </div>
                                            <div class="form-check">
                                                <asp:RadioButton ID="rdbInactivoUsuario" Style="color: black !important; background: white !important" class="form-check-input" GroupName="groupEstadoUsuario" runat="server" />
                                                <label class="form-check-label" for="customRadio22" style="color: black !important;">Inactivo</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="mb-4">
                                            <label class="lead fs-6">Campos requeridos *</label>
                                        </div>
                                    </div>
                                </div>
                            </contenttemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel runat="server">
                            <contenttemplate>
                                <div class="form-actions">
                                    <div class="card-body d-flex flex-row justify-content-center flex-wrap">
                                        <asp:LinkButton ID="btnBuscarTiempo" OnClick="btnBuscarTiempo_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                            Guardar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </contenttemplate>
                        </asp:UpdatePanel>


                        <asp:UpdatePanel runat="server">
                            <contenttemplate>
                                <asp:GridView ID="gvTiempo" CssClass="table-responsive table customize-table v-middle" Style="width: 100%!important"
                                    DataKeyNames="IdProyecto,Nombre_Proyecto,Fecha_Inicio,Fecha_Final,Porcentaje" OnPreRender="gvTiempo_PreRender"
                                    OnRowCommand="gvTiempo_RowCommand"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-CssClass="table-dark"
                                    runat="server">
                                    <columns>
                                        <asp:BoundField DataField="Usuario_Sistema" HeaderText="Usuario" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Correo" HeaderText="Correo" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PerfilNombre" HeaderText="Perfil" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <%--<asp:BoundField DataField="Contrasena" HeaderText="Contraseña" />--%>
                                        <asp:BoundField DataField="NombreEstado" HeaderText="Estado" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Modificar" ControlStyle-CssClass="text-secondary" CommandName="editar" Text="<i class='fas fa-xl fa-edit'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Cambiar Contraseña" ControlStyle-CssClass="text-warning" CommandName="cambiarContrasena" Text="<i class='fas fa-xl fa-retweet'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Eliminar" CommandName="eliminar" ControlStyle-CssClass="text-danger" Text="<i class='fas fa-xl fa-trash-alt'></i>" ItemStyle-HorizontalAlign="Center" />
                                    </columns>
                                    <headerstyle backcolor="#4285f4" font-bold="True" forecolor="White" horizontalalign="Center" borderstyle="Solid" bordercolor="Black" />

                                </asp:GridView>
                            </contenttemplate>
                        </asp:UpdatePanel>

                    </section>

                </div>
                <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                    <section role="tabpanel" aria-labelledby="example-manipulation-h-0" class="body current" aria-hidden="false">
                        <asp:UpdatePanel runat="server">
                            <contenttemplate>
                                <div class="row mt-4">
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label" style="color: black !important;">Usuario *</label>
                                            <asp:TextBox ID="TextBox1" Style="color: black !important; border: 3px solid #d8eeef !important;" placeholder="Ejemplo: admin" CssClass="form-control mt-2" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label" style="color: black !important;">Nombre *</label>
                                            <asp:TextBox ID="TextBox2" Style="color: black !important; border: 3px solid #d8eeef !important;" placeholder="Ejemplo: usuario 01" CssClass="form-control mt-2" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label" style="color: black !important;">Correo electrónico *</label>
                                            <asp:TextBox ID="TextBox3" Style="color: black !important; border: 3px solid #d8eeef !important;" placeholder="Ejemplo: admin@mail.com" CssClass="form-control mt-2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label" style="color: black !important;">Perfil</label>
                                            <asp:DropDownList ID="DropDownList1" DataValueField="ID" Style="color: black !important; border: 3px solid #d8eeef !important;" DataTextField="Descripcion" CssClass="form-control mt-2" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="control-label">Contraseña</label>
                                            <asp:TextBox ID="TextBox4" type="password" placeholder="Contraseña" CssClass="form-control mt-2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-6">
                                        <div class="mb-3">
                                            <label style="color: black !important;">Estado</label>
                                            <br />
                                            <div class="form-check mt-1">
                                                <asp:RadioButton Checked="true" Style="color: black !important; background: white !important" ID="RadioButton1" class="form-check-input" GroupName="groupEstadoUsuario" runat="server" />

                                                <label class="form-check-label" style="color: black !important;">Activo</label>
                                            </div>
                                            <div class="form-check">
                                                <asp:RadioButton ID="RadioButton2" Style="color: black !important; background: white !important" class="form-check-input" GroupName="groupEstadoUsuario" runat="server" />
                                                <label class="form-check-label" for="customRadio22" style="color: black !important;">Inactivo</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="mb-4">
                                            <label class="lead fs-6">Campos requeridos *</label>
                                        </div>
                                    </div>
                                </div>
                            </contenttemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel runat="server">
                            <contenttemplate>
                                <div class="form-actions">
                                    <div class="card-body d-flex flex-row justify-content-center flex-wrap">
                                        <asp:LinkButton ID="btnBuscarCargaTrabajo" OnClick="btnBuscarCargaTrabajo_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                            Guardar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </contenttemplate>
                        </asp:UpdatePanel>


                        <asp:UpdatePanel runat="server">
                            <contenttemplate>
                                <asp:GridView ID="gvCargaTrabajo" CssClass="table-responsive table customize-table v-middle" Style="width: 100%!important"
                                    DataKeyNames="ID,Usuario_Sistema,Nombre,Correo,Perfil,Contrasena,Estado,Cambio_Clave,PerfilNombre,NombreEstado" OnPreRender="gvCargaTrabajo_PreRender"
                                    OnRowCommand="gvCargaTrabajo_RowCommand"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-CssClass="table-dark"
                                    runat="server">
                                    <columns>
                                        <asp:BoundField DataField="Usuario_Sistema" HeaderText="Usuario" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Correo" HeaderText="Correo" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PerfilNombre" HeaderText="Perfil" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <%--<asp:BoundField DataField="Contrasena" HeaderText="Contraseña" />--%>
                                        <asp:BoundField DataField="NombreEstado" HeaderText="Estado" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Modificar" ControlStyle-CssClass="text-secondary" CommandName="editar" Text="<i class='fas fa-xl fa-edit'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Cambiar Contraseña" ControlStyle-CssClass="text-warning" CommandName="cambiarContrasena" Text="<i class='fas fa-xl fa-retweet'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Eliminar" CommandName="eliminar" ControlStyle-CssClass="text-danger" Text="<i class='fas fa-xl fa-trash-alt'></i>" ItemStyle-HorizontalAlign="Center" />
                                    </columns>
                                    <headerstyle backcolor="#4285f4" font-bold="True" forecolor="White" horizontalalign="Center" borderstyle="Solid" bordercolor="Black" />

                                </asp:GridView>
                            </contenttemplate>
                        </asp:UpdatePanel>

                    </section>
                </div>
            </div>

        </div>

    </div>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="scriptsPersonalizados" runat="server">

    <script type="text/javascript">
        quitarEventoSobreTextoTreeViewNode();
        document.addEventListener("DOMContentLoaded", function (event) {
            LoadTables();
            quitarEventoSobreTextoTreeViewNode();
            chosenSelect();
        });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        if (prm != null) {
            prm.add_beginRequest(function (sender, e) {
                $(".loading-panel").attr("style", "display:block");
                quitarEventoSobreTextoTreeViewNode();
                chosenSelect();
            });
            prm.add_endRequest(function (sender, e) {
                LoadTables();
                $(".loading-panel").attr("style", "display:none");
                quitarEventoSobreTextoTreeViewNode();
                chosenSelect();

            });
        }

        let LoadTables = () => {
            InitializeDataTableWithParameter('<%= gvProgresoProyecto.ClientID %>');
            InitializeDataTableWithParameter('<%= gvTiempo.ClientID %>');
            InitializeDataTableWithParameter('<%= gvCargaTrabajo.ClientID %>');
        }




        function chosenSelect() {
            $(".chosen-select").chosen();
            $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
        }
    </script>

</asp:Content>
