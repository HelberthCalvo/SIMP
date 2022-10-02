<%@ Page Title="Mantenimiento Proyecto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proyecto.aspx.cs" Inherits="SIMP.Proyecto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="idProyecto" runat="server" />
            <h2 class="fs-4">Mantenimiento de Proyectos</h2>
            <hr />
            <div class="row">
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox runat="server" ID="txbNombre" CssClass="form-control" placeholder="Nombre del proyecto"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Descripción:</label>
                        <asp:TextBox runat="server" ID="txbDescripcion" CssClass="form-control" placeholder="Descripción"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Cliente:</label>
                        <asp:DropDownList runat="server" ID="ddlClientes" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fecha Inicio:</label>
                        <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha de inicio" ID="txbFechaInicio" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                            TargetControlID="txbFechaInicio" PopupButtonID="txbFechaInicio"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="mb-4">
                        <label class="form-label">Fecha Finalización:</label>
                        <asp:TextBox runat="server" TextMode="DateTime" placeholder="Fecha estimada de finalización" ID="txbFechaEstimada" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
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
                                          Fecha_Estimada"
                            OnPreRender="gvProyectos_PreRender"
                            OnRowCommand="gvProyectos_RowCommand"
                            AutoGenerateColumns="false"
                            HeaderStyle-CssClass="table-dark"
                            Width="100%"
                            runat="server">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" />
                                <asp:BoundField DataField="IdCliente" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" HeaderText="IdCliente" />
                                <asp:BoundField DataField="Nombre" HeaderText="Proyecto" />
                                <asp:BoundField DataField="Nombre_Cliente" HeaderText="Cliente" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="Fecha_Inicio" HeaderText="Fecha de Inicio" />
                                <asp:BoundField DataField="Fecha_Estimada" HeaderText="Fecha Estimada" />
                                <asp:ButtonField CommandName="Editar" ControlStyle-CssClass="text-secondary" Text="<i class='fas fa-xl fa-edit'></i>" />
                                <asp:ButtonField CommandName="Eliminar" ControlStyle-CssClass="text-danger" Text="<i class='fas fa-xl fa-trash-alt'></i>" />
                                <asp:ButtonField CommandName="Finalizar" ControlStyle-CssClass="btn btn-success rounded-pill" Text="Finalizar Proyecto" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
