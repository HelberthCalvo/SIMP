<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="SIMP.Bitacora" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2 class="fs-4">Bitácora</h2>
            <hr />
            <div class="row mt-4">
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
                        <label class="form-label">Fecha Final *</label>
                        <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha Final" ID="txbFechaFinal" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                            TargetControlID="txbFechaFinal" PopupButtonID="txbFechaFinal" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
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
                    <asp:LinkButton ID="btnBuscar" OnClick="btnBuscar_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                            Buscar
                    </asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvBitacoras" CssClass="table-responsive table customize-table v-middle"
                DataKeyNames="Id,
                              Tabla, 
                              Accion,
                              Datos_Anteriores,
                              Datos_Nuevos,
                              Usuario,
                              Fecha"
                OnPreRender="gvBitacoras_PreRender"
                OnRowCommand="gvBitacoras_RowCommand"
                AutoGenerateColumns="false"
                HeaderStyle-CssClass="table-dark"
                Width="100%"
                runat="server">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" />
                    <asp:BoundField DataField="Tabla" HeaderText="Tabla" />
                    <asp:BoundField DataField="Accion" HeaderText="Accion" />
                    <asp:BoundField DataField="Datos_Anteriores" HeaderText="Datos Anteriores" />
                    <asp:BoundField DataField="Datos_Nuevos" HeaderText="Datos Nuevos" />
                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                </Columns>
            </asp:GridView>


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
            InitializeDataTableWithParameter('<%= gvBitacoras.ClientID %>');
        }

        function solonumeros(e) {

            var charCode = (e.which) ? e.which : e.keyCode
            if (((charCode == 8) || (charCode == 46)
                || (charCode >= 35 && charCode <= 40)
                || (charCode >= 48 && charCode <= 57)
                || (charCode >= 96 && charCode <= 105) || (charCode == 9))) {
                return true;
            }
            else {
                return false;
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
