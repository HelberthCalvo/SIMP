<%@ Page Title="Seguridad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Seguridad.aspx.cs" Inherits="SIMP.Seguridad" %>


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
        <ContentTemplate>
            <asp:HiddenField ID="hfIdPerfil" runat="server" />
            <asp:HiddenField ID="hfIdUsuario" runat="server" />
            <asp:HiddenField ID="hdfPermisoEnviarCorreos" Value="0" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <style>
        .nav-pills .nav-link.active,
        .nav-pills .show > .nav-link {
            color: #fff;
            background-color: #343a40 !important;
        }
    </style>

    <h2 class="fs-4">Seguridad</h2>
    <hr />

    <div class="col-12">

        <div id="example-manipulation" class="mt-2 wizard clearfix" role="application">

            <ul class="nav nav-pills mb-3 nav-fill" id="pills-tab" role="tablist">
                <li class="nav-item" role="presentation">
                    <button class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Perfiles</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="pills-contact-tab" data-bs-toggle="pill" data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact" aria-selected="false">Usuarios</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Permisos</button>
                </li>

            </ul>
            <div class="tab-content" id="pills-tabContent">
                <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                    <section id="example-manipulation-p-0" role="tabpanel" aria-labelledby="example-manipulation-h-0" class="body current" aria-hidden="false">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="form-actions">
                                    <div class="card-body d-flex flex-row justify-content-center flex-wrap">
                                        <asp:LinkButton ID="btnGuardarPerfil" OnClick="btnGuardarPerfil_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                            Guardar
                                        </asp:LinkButton>
                                        <asp:Button ID="btnCancelarPerfil" Text="Cancelar" OnClick="btnCancelarPerfil_Click" class="btn btn-danger rounded-pill px-4" runat="server" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvPerfil" CssClass="table-responsive table customize-table v-middle" Style="width: 100%!important"
                                    DataKeyNames="ID, Descripcion, NombreEstado" OnPreRender="gvPerfil_PreRender"
                                    OnRowCommand="gvPerfil_RowCommand"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-CssClass="table-dark"
                                    runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="Descripcion" HeaderText="Perfil" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="NombreEstado" HeaderText="Estado" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Editar" CommandName="editar" ControlStyle-CssClass="text-secondary" Text="<i class='fas fa-xl fa-edit'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Eliminar" CommandName="eliminar" ControlStyle-CssClass="text-danger" Text="<i class='fas fa-xl fa-trash-alt'></i>" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <HeaderStyle BackColor="#4285f4" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" BorderStyle="Solid" BorderColor="Black" />

                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </section>
                </div>

                <div class="tab-pane fade" id="pills-contact" role="tabpanel" aria-labelledby="pills-contact-tab">
                    <section role="tabpanel" aria-labelledby="example-manipulation-h-0" class="body current" aria-hidden="false">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="form-actions">
                                    <div class="card-body d-flex flex-row justify-content-center flex-wrap">
                                        <asp:LinkButton ID="btnGuardarUsuario" OnClick="btnGuardarUsuario_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                        Guardar
                                        </asp:LinkButton>
                                        <asp:Button ID="btnCancelarUsuario" Text="Cancelar" OnClick="btnCancelarUsuario_Click" class="btn btn-danger rounded-pill px-4" runat="server" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvUsuario" CssClass="table-responsive table customize-table v-middle" Style="width: 100%!important"
                                    DataKeyNames="ID,Usuario_Sistema,Nombre,Correo,Perfil,Contrasena,Estado,Cambio_Clave,PerfilNombre,NombreEstado" OnPreRender="gvUsuario_PreRender"
                                    OnRowCommand="gvUsuario_RowCommand"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-CssClass="table-dark"
                                    runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="Usuario_Sistema" HeaderText="Usuario" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Correo" HeaderText="Correo Electrónico" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PerfilNombre" HeaderText="Perfil" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <%--<asp:BoundField DataField="Contrasena" HeaderText="Contraseña" />--%>
                                        <asp:BoundField DataField="NombreEstado" HeaderText="Estado" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Editar" ControlStyle-CssClass="text-secondary" CommandName="editar" Text="<i class='fas fa-xl fa-edit'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Cambiar Contraseña" ControlStyle-CssClass="text-warning" CommandName="cambiarContrasena" Text="<i class='fas fa-xl fa-retweet'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Eliminar" CommandName="eliminar" ControlStyle-CssClass="text-danger" Text="<i class='fas fa-xl fa-trash-alt'></i>" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <HeaderStyle BackColor="#4285f4" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" BorderStyle="Solid" BorderColor="Black" />

                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </section>

                </div>
                <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                    <section role="tabpanel" aria-labelledby="example-manipulation-h-0" class="body current" aria-hidden="false">


                        <asp:UpdatePanel ID="upNum4" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-12 col-md-6 col-lg-6">
                                    <div class="mb-3">
                                        <label class="control-label" style="color: black !important;">Perfil</label>
                                        <asp:DropDownList ID="ddlPerfilPermisos" Style="color: black !important; border: 3px solid #d8eeef !important;" AutoPostBack="true" OnSelectedIndexChanged="ddlPerfilPermisos_SelectedIndexChanged" DataValueField="ID" DataTextField="Descripcion" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="table-responsive col-lg-6 col-md-6 col-sm-6 col-xs-6  pt-4 pb-4">
                                    <asp:TreeView ID="tvPermisos" runat="server" OnTreeNodeCheckChanged="tvPermisos_TreeNodeCheckChanged"
                                        Class="TreeView">
                                    </asp:TreeView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>



                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="form-actions">
                                    <div class="card-body d-flex flex-row justify-content-center flex-wrap">
                                        <asp:LinkButton ID="btnGuardarPermisos" OnClick="btnGuardarPermisos_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                            Guardar
                                        </asp:LinkButton>
                                        <asp:Button ID="btnCancelarPermisos" Text="Cancelar" OnClick="btnCancelarPermisos_Click" class="btn btn-danger rounded-pill px-4" runat="server" />
                                    </div>
                                </div>
                            </ContentTemplate>
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
            InitializeDataTableWithParameter('<%= gvPerfil.ClientID %>');
            InitializeDataTableWithParameter('<%= gvUsuario.ClientID %>');
        }

        /*FUNCIONAMIENTO PARA EL TREEVIEW */
        function postBackByObject() {
            $("#haceLoading").val("NO");
            $(".loading-panel").attr("style", "display:none");
            var o = window.event.srcElement; // obteniendo el elemento clickeado
            if (o.tagName == "INPUT" && o.type == "checkbox") // verificando si se trata del checkbox
            {
                __doPostBack('<%= tvPermisos.ClientID %>', ''); // haciendo un postback al treeview para que entre al evento tvPermisos_TreeNodeCheckChanged
            }

        }

        function quitarEventoSobreTextoTreeViewNode() {
            $("[id *= 'tvPermisos'] table a[id *= tvPermisost]").removeAttr("onclick");
            $("[id *= 'tvPermisos'] table a[id *= tvPermisost]").removeAttr("href");
            $("[id *= 'tvPermisos'] table a[id *= tvPermisost]").css("color", "black");
            $("[id *= 'tvPermisos'] table a[id *= tvPermisost]").css("text-decoration", "none");
            $("[id *= 'tvPermisos'] table a[id *= tvPermisost]").css("font-size", "1.2em");
        }


        function chosenSelect() {
            $(".chosen-select").chosen();
            $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
        }
    </script>

</asp:Content>
