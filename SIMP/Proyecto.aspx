<%@ Page Title="Mantenimiento Proyecto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proyecto.aspx.cs" Inherits="SIMP.Proyecto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="hdnIdProyecto" runat="server" />
            <asp:HiddenField ID="hdnIdCliente" runat="server" />
            <h2 class="fs-4">Proyectos</h2>
            <hr />
            <div class="row">
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Nombre *</label>
                        <asp:TextBox runat="server" ID="txbNombre" CssClass="form-control" placeholder="Nombre del proyecto" ToolTip="Por favor rellene este campo."></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Descripción *</label>
                        <asp:TextBox runat="server" ID="txbDescripcion" CssClass="form-control" placeholder="Descripción" ToolTip="Por favor rellene este campo."></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Cliente</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtNombreCliente" CssClass="form-control" runat="server" placeholder="Seleccione un cliente" Enabled="false"/>
                            <div class="input-group-append">
                                <asp:LinkButton CssClass="btn btn-secondary" ID="btnModalCliente" OnClick="btnModalCliente_Click" runat="server" >
                                    <i class="fa-solid fa-magnifying-glass"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fecha Inicio *</label>
                        <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha de inicio" ID="txbFechaInicio" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                            TargetControlID="txbFechaInicio" PopupButtonID="txbFechaInicio" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fecha Estimada *</label>
                        <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha estimada de finalización" ID="txbFechaEstimada" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                            TargetControlID="txbFechaEstimada" PopupButtonID="txbFechaEstimada" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="mb-4">
                        <label class="lead fs-6">Campos requeridos *</label>
                    </div>
                </div>
            </div>
            <div class="row text-center mt-3 mb-5">
                <div class="col-lg-12">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:LinkButton ID="btnGuardar" class="btn btn-primary rounded-pill px-4" runat="server" OnClick="btnGuardar_Click">Guardar</asp:LinkButton>
                            <asp:LinkButton ID="btnCancelar" class="btn btn-danger rounded-pill px-4" runat="server" OnClick="btnCancelar_Click">Cancelar</asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row pb-4">
                <div class="col-sm-12">
                    <div class="table-responsive">

                        <asp:GridView ID="gvProyectos" CssClass="table-responsive table customize-table v-middle"
                            DataKeyNames="Id,
                                          IdCliente,
                                          Nombre,
                                          Nombre_Cliente,
                                          Descripcion,
                                          Fecha_Inicio,
                                          Fecha_Estimada,
                                          NombreEstado"
                            OnPreRender="gvProyectos_PreRender"
                            OnRowCommand="gvProyectos_RowCommand"
                            AutoGenerateColumns="false"
                            HeaderStyle-CssClass="table-dark"
                            Width="100%"
                            runat="server">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" />
                                <asp:BoundField DataField="IdCliente" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" HeaderText="IdCliente" />
                                <asp:BoundField DataField="Nombre" HeaderText="Proyecto" />
                                <asp:BoundField DataField="Nombre_Cliente" HeaderText="Cliente" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="Fecha_Inicio" HeaderText="Fecha Inicio" />
                                <asp:BoundField DataField="Fecha_Estimada" HeaderText="Fecha Estimada" />
                                <asp:BoundField DataField="NombreEstado" HeaderText="Estado" />
                                <asp:ButtonField CommandName="Editar" HeaderText="Editar" ControlStyle-CssClass="text-secondary" Text="<i class='fas fa-xl fa-edit'></i>" />
                                <asp:ButtonField CommandName="CambiarEstado" HeaderText="Estado" ControlStyle-CssClass="text-warning" Text="<i class='fa-sharp fa-xl fa-solid fa-rotate'></i>" />
                                <asp:ButtonField CommandName="Finalizar" HeaderText="Finalizar" ControlStyle-CssClass="text-success" Text="<i class='fa-sharp fa-xl fa-solid fa-circle-check'></i>" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal Cliente -->
    <div class="modal fade" id="modalCliente" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Seleccione un cliente</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="row pb-4">
                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvModalCliente" CssClass="table-responsive table customize-table v-middle"
                                            DataKeyNames="Id, Nombre"
                                            OnRowCommand="gvModalCliente_RowCommand"
                                            AutoGenerateColumns="false"
                                            HeaderStyle-CssClass="table-dark"
                                            Width="100%"
                                            runat="server">
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Cliente" />
                                                <asp:ButtonField CommandName="Seleccionar" HeaderText="Seleccionar" ControlStyle-CssClass="btn btn-primary" Text="Seleccionar" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
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
            InitializeDataTableWithParameter('<%= gvProyectos.ClientID %>');
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
