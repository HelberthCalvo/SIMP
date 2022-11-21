﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporte.aspx.cs" Inherits="SIMP.Reporte" MasterPageFile="~/Site.Master" %>


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

    <h2 class="fs-4">Reportes</h2>
    <hr />

    <div class="col-12">

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



        <div id="example-manipulation" class="mt-2 wizard clearfix" role="application">

            <ul class="nav nav-pills mb-3 nav-fill" id="pills-tab" role="tablist">
                <li class="nav-item" role="presentation">
                    <button class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Progreso Proyecto</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="pills-contact-tab" data-bs-toggle="pill" data-bs-target="#pills-contact" type="button" role="tab" aria-controls="pills-contact" aria-selected="false">Tiempos</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Carga Trabajo</button>
                </li>

            </ul>
            <div class="tab-content" id="pills-tabContent">
                <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                    <section id="example-manipulation-p-0" role="tabpanel" aria-labelledby="example-manipulation-h-0" class="body current" aria-hidden="false">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>

                                <div class="row mt-4">
                                    <div class="col-lg-4">
                                        <div class="mb-4">
                                            <label class="form-label">Fecha Inicio *</label>
                                            <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha de inicio" ID="txbFechaInicioProgreso" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                TargetControlID="txbFechaInicioProgreso" PopupButtonID="txbFechaInicioProgreso" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="mb-4">
                                            <label class="form-label">Fecha Estimada *</label>
                                            <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha estimada de finalización" ID="txbFechaFinalProgreso" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                TargetControlID="txbFechaFinalProgreso" PopupButtonID="txbFechaFinalProgreso" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
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
                                        <asp:LinkButton ID="btnBuscarProgresoProyecto" OnClick="btnBuscarProgresoProyecto_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                            Buscar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvProgresoProyecto" CssClass="table-responsive table customize-table v-middle" Style="width: 100%!important"
                                    DataKeyNames="IdProyecto, Nombre_Proyecto, Nombre_Fase, Nombre_Actividad, Fecha_Inicio, Porcentaje" OnPreRender="gvProgresoProyecto_PreRender"
                                    OnRowCommand="gvProgresoProyecto_RowCommand"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-CssClass="table-dark"
                                    runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre_Proyecto" HeaderText="Proyecto" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Fase" HeaderText="Nombre_Fase" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Actividad" HeaderText="Nombre_Actividad" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha_Inicio" HeaderText="Fecha" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Generar PDF" CommandName="GenerarPDF" Text="<i class='fas fa-xl fa-file-pdf'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Enviar Correo" CommandName="EnviarCorreo" Text="<i class='fas fa-xl fa-envelope'></i>" ItemStyle-HorizontalAlign="Center" />
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
                                    <div class="col-lg-4">
                                        <div class="mb-4">
                                            <label class="form-label">Fecha Inicio *</label>
                                            <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha de inicio" ID="txbFechaInicioTiempos" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                TargetControlID="txbFechaInicioTiempos" PopupButtonID="txbFechaInicioTiempos" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="mb-4">
                                            <label class="form-label">Fecha Estimada *</label>
                                            <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha estimada de finalización" ID="txbFechaFinalTiempos" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                                TargetControlID="txbFechaFinalTiempos" PopupButtonID="txbFechaFinalTiempos" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
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
                                        <asp:LinkButton ID="btnBuscarTiempos" OnClick="btnBuscarTiempos_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                            Buscar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvTiempo" CssClass="table-responsive table customize-table v-middle" Style="width: 100%!important"
                                    DataKeyNames="IdProyecto,Nombre_Cliente,Nombre_Proyecto,Nombre_Fase,Nombre_Actividad,Nombre_Usuario,Horas_Estimadas,Horas_Reales,Fecha" OnPreRender="gvTiempo_PreRender"
                                    OnRowCommand="gvTiempo_RowCommand"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-CssClass="table-dark"
                                    runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre_Cliente" HeaderText="Cliente" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Proyecto" HeaderText="Proyecto" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Fase" HeaderText="Fase" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Actividad" HeaderText="Actividad" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Usuario" HeaderText="Usuario" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Horas_Estimadas" HeaderText="Horas Estimadas" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Horas_Reales" HeaderText="Horas Reales" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Generar PDF" CommandName="GenerarPDF" Text="<i class='fas fa-xl fa-file-pdf'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Enviar Correo" CommandName="EnviarCorreo" Text="<i class='fas fa-xl fa-envelope'></i>" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <HeaderStyle BackColor="#4285f4" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" BorderStyle="Solid" BorderColor="Black" />

                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </section>

                </div>
                <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                    <section role="tabpanel" aria-labelledby="example-manipulation-h-0" class="body current" aria-hidden="false">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="row mt-4">
                                    <div class="col-lg-4">
                                        <div class="mb-4">
                                            <label class="form-label">Fecha Inicio *</label>
                                            <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha de inicio" ID="txbFechaInicioCargaTrabajo" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                                TargetControlID="txbFechaInicioCargaTrabajo" PopupButtonID="txbFechaInicioCargaTrabajo" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="mb-4">
                                            <label class="form-label">Fecha Estimada *</label>
                                            <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha estimada de finalización" ID="txbFechaFinalCargaTrabajo" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                                TargetControlID="txbFechaFinalCargaTrabajo" PopupButtonID="txbFechaFinalCargaTrabajo" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
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
                                        <asp:LinkButton ID="btnBuscarCargaTrabajo" OnClick="btnBuscarCargaTrabajo_Click" class="btn btn-primary rounded-pill px-4" runat="server">
                                            Buscar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvCargaTrabajo" CssClass="table-responsive table customize-table v-middle" Style="width: 100%!important"
                                    DataKeyNames="IdUsuario,Nombre_Cliente,Nombre_Proyecto,Nombre_Fase,Nombre_Actividad,Nombre_Usuario,Fecha" OnPreRender="gvCargaTrabajo_PreRender"
                                    OnRowCommand="gvCargaTrabajo_RowCommand"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-CssClass="table-dark"
                                    runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre_Cliente" HeaderText="Cliente" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Proyecto" HeaderText="Proyecto" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Fase" HeaderText="Fase" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Actividad" HeaderText="Actividad" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Nombre_Usuario" HeaderText="Usuario" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Generar PDF" CommandName="GenerarPDF" Text="<i class='fas fa-xl fa-file-pdf'></i>" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField HeaderText="Enviar Correo" CommandName="EnviarCorreo" Text="<i class='fas fa-xl fa-envelope'></i>" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <HeaderStyle BackColor="#4285f4" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" BorderStyle="Solid" BorderColor="Black" />

                                </asp:GridView>
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
            InitializeDataTableWithParameter('<%= gvProgresoProyecto.ClientID %>');
            InitializeDataTableWithParameter('<%= gvTiempo.ClientID %>');
            InitializeDataTableWithParameter('<%= gvCargaTrabajo.ClientID %>');
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
