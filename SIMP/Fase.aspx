<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fase.aspx.cs" Inherits="SIMP.Fase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="hdnIdFase" runat="server" />
            <asp:HiddenField ID="hdnIdProyecto" runat="server" />
            <h2 class="fs-4">Mantenimiento de Fases</h2>
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
                        <label class="form-label">Proyecto:</label>
                        <asp:DropDownList runat="server" ID="ddlProyectos" CssClass="form-control"></asp:DropDownList>
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
                        <asp:GridView ID="gvFases" CssClass="table-responsive table customize-table v-middle"
                            DataKeyNames="Id,
                                          IdProyecto,
                                          IdEstado,
                                          Nombre,
                                          Descripcion,
                                          NombreProyecto,
                                          NombreEstado"
                            OnPreRender="gvFases_PreRender"
                            OnRowCommand="gvFases_RowCommand"
                            AutoGenerateColumns="false"
                            HeaderStyle-CssClass="table-dark"
                            Width="100%"
                            runat="server">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" />
                                <asp:BoundField DataField="IdProyecto" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" HeaderText="IdProyecto" />
                                <asp:BoundField DataField="IdEstado" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" HeaderText="IdEstado" />
                                <asp:BoundField DataField="Nombre" HeaderText="Fase" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="NombreProyecto" HeaderText="Proyecto" />
                                <asp:BoundField DataField="NombreEstado" HeaderText="Estado" />
                                <asp:ButtonField CommandName="Editar" ControlStyle-CssClass="text-secondary" Text="<i class='fas fa-xl fa-edit'></i>" />
                                <asp:ButtonField CommandName="CambiarEstado" ControlStyle-CssClass="text-warning" Text="<i class='fa-sharp fa-xl fa-solid fa-rotate'></i>" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
