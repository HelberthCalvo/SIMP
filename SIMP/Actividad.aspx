<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Actividad.aspx.cs" Inherits="SIMP.Actividad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
        <ContentTemplate>
            <div class=" row pt-4 pb-4" runat="server" id="mensajePermiso" visible="false">
                <div class="alert alert-danger col-lg-10 col-md-10 col-sm-10 col-xs-10 text-center offset-1 " role="alert">
                    <i class="fa fa-info-circle"></i>&nbsp;
                  <asp:Label ID="lblMensajePermisos" runat="server"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="hdnIdProyecto" runat="server" />
            <asp:HiddenField ID="hdnIdFase" runat="server" />
            <asp:HiddenField ID="hdnIdActividad" runat="server" />
            <asp:HiddenField ID="hdnIdUsuario" runat="server" />
            <h2 class="fs-4">Actividades</h2>
            <hr />
            <div class="row">
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Descripción *</label>
                        <asp:TextBox runat="server" ID="txbDescripcion" CssClass="form-control" placeholder="Descripción"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fecha Inicio *</label>
                        <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha de inicio" ID="txbFechaInicio" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txbFechaInicio" PopupButtonID="txbFechaInicio"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fecha Estimada *</label>
                        <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha estimada de finalización" ID="txbFechaEstimada" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txbFechaEstimada" PopupButtonID="txbFechaEstimada"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Proyecto</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtNombreProyecto" CssClass="form-control" runat="server" placeholder="Seleccione un proyecto" Enabled="false" />
                            <div class="input-group-append">
                                <asp:LinkButton CssClass="btn btn-secondary" ID="btnModalProyecto" OnClick="btnModalProyecto_Click" runat="server">
                                    <i class="fa-solid fa-magnifying-glass"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fase</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtNombreFase" CssClass="form-control" runat="server" placeholder="Seleccione una fase" Enabled="false" />
                            <div class="input-group-append">
                                <asp:LinkButton CssClass="btn btn-secondary" ID="btnModalFase" OnClick="btnModalFase_Click" runat="server">
                                    <i class="fa-solid fa-magnifying-glass"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Usuario</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtNombreUsuario" CssClass="form-control" runat="server" placeholder="Seleccione un usuario" Enabled="false" />
                            <div class="input-group-append">
                                <asp:LinkButton CssClass="btn btn-secondary" ID="btnModalUsuario" OnClick="btnModalUsuario_Click" runat="server">
                                    <i class="fa-solid fa-magnifying-glass"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row pb-4">
                <div class="col-sm-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvActividad" CssClass="table-responsive table customize-table v-middle"
                            DataKeyNames="Id,
                                          IdFase,
                                          IdUsuario,
                                          NombreFase,
                                          NombreUsuario,
                                          Descripcion,
                                          Fecha_Inicio,
                                          Fecha_Estimada,
                                          NombreEstado"
                            OnPreRender="gvActividad_PreRender"
                            OnRowCommand="gvActividad_RowCommand"
                            AutoGenerateColumns="false"
                            HeaderStyle-CssClass="table-dark"
                            Width="100%"
                            runat="server">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" />
                                <asp:BoundField DataField="IdFase" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" HeaderText="IdFase" />
                                <asp:BoundField DataField="IdUsuario" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" HeaderText="IdUsuario" />
                                <asp:BoundField DataField="NombreFase" HeaderText="Fase" />
                                <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Actividad" />
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

    <!-- Modal Proyecto -->
    <div class="modal fade" id="modalProyecto" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Seleccione un proyecto</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="row pb-4">
                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvModalProyecto" CssClass="table-responsive table customize-table v-middle"
                                            DataKeyNames="Id, Nombre"
                                            OnRowCommand="gvModalProyecto_RowCommand"
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

    <!-- Modal Fase -->
    <div class="modal fade" id="modalFase" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Seleccione una fase</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="row pb-4">
                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvModalFase" CssClass="table-responsive table customize-table v-middle"
                                            DataKeyNames="Id, Nombre"
                                            OnRowCommand="gvModalFase_RowCommand"
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

    <!-- Modal Fase -->
    <div class="modal fade" id="modalUsuario" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Seleccione un usuario</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="row pb-4">
                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvModalUsuario" CssClass="table-responsive table customize-table v-middle"
                                            DataKeyNames="Id, Nombre"
                                            OnRowCommand="gvModalUsuario_RowCommand"
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
            InitializeDataTableWithParameter('<%= gvActividad.ClientID %>');
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
