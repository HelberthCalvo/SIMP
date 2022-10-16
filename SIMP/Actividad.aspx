<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Actividad.aspx.cs" Inherits="SIMP.Actividad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="hdnIdActividad" runat="server" />
            <h2 class="fs-4">Mantenimiento de Actividades</h2>
            <hr />
            <div class="row">
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Descripción:</label>
                        <asp:TextBox runat="server" ID="txbDescripcion" CssClass="form-control" placeholder="Descripción"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Proyecto:</label>
                        <asp:DropDownList runat="server" ID="ddlProyecto" OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fase:</label>
                        <asp:DropDownList runat="server" ID="ddlFase" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Usuario:</label>
                        <asp:DropDownList runat="server" ID="ddlUsuario" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fecha Inicio:</label>
                        <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha de inicio" ID="txbFechaInicio" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txbFechaInicio" PopupButtonID="txbFechaInicio"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fecha Finalización:</label>
                        <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha estimada de finalización" ID="txbFechaEstimada" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txbFechaEstimada" PopupButtonID="txbFechaEstimada"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
            </div>
            <div class="row text-center mt-3 mb-5">
                <div class="col-lg-12">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:LinkButton ID="btnGuardar" class="btn btn-primary rounded-pill px-4" runat="server" OnClick="btnGuardar_Click">Guardar</asp:LinkButton>
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
                                <asp:BoundField DataField="Id" HeaderText="Id" />
                                <asp:BoundField DataField="IdFase" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" HeaderText="IdFase" />
                                <asp:BoundField DataField="IdUsuario" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" HeaderText="IdUsuario" />
                                <asp:BoundField DataField="NombreFase"  HeaderText="Fase" />
                                <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="Fecha_Inicio" HeaderText="Fecha Inicio" />
                                <asp:BoundField DataField="Fecha_Estimada" HeaderText="Fecha Estimada" />
                                <asp:BoundField DataField="NombreEstado" HeaderText="Estado" />
                                <asp:ButtonField CommandName="Editar" ControlStyle-CssClass="text-secondary" Text="<i class='fas fa-xl fa-edit'></i>" />
                                <asp:ButtonField CommandName="CambiarEstado" ControlStyle-CssClass="text-warning" Text="<i class='fa-sharp fa-xl fa-solid fa-rotate'></i>" />
                                <asp:ButtonField CommandName="Finalizar" ControlStyle-CssClass="text-success" Text="<i class='fa-sharp fa-xl fa-solid fa-circle-check'></i>" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
