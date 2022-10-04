<%@ Page Title="Mantenimiento Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="SIMP.Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="idCliente" runat="server" />
            <h2 class="fs-4">Mantenimiento de Clientes</h2>
            <hr />
            <div class="row">
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Nombre:</label>
                        <asp:TextBox runat="server" ID="txbNombre" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Primer Apellido:</label>
                        <asp:TextBox runat="server" ID="txbApellido1" CssClass="form-control" placeholder="Primer Apellido"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Segundo Apellido:</label>
                        <asp:TextBox runat="server" ID="txbApellido2" CssClass="form-control" placeholder="Segundo Apellido"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Email:</label>
                        <asp:TextBox runat="server" TextMode="Email" ID="txbEmail" CssClass="form-control" placeholder="ejemplo@gmail.com"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Teléfono:</label>
                        <asp:TextBox runat="server" TextMode="Phone" ID="txbTelefono" CssClass="form-control" placeholder="88554466"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-lg-12">
                    <div class="mb-4">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btnGuardar" class="btn btn-primary rounded-pill px-4" runat="server" OnClick="btnGuardar_Click">Guardar</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row pb-4">
                <div class="col-sm-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvClientes" CssClass="table-responsive table customize-table v-middle"
                            DataKeyNames="Id,
                                          Nombre, 
                                          Primer_Apellido,
                                          Segundo_Apellido,
                                          Correo_Electronico,
                                          Telefono"
                            OnPreRender="gvClientes_PreRender"
                            OnRowCommand="gvClientes_RowCommand"
                            AutoGenerateColumns="false"
                            HeaderStyle-CssClass="table-dark"
                            Width="100%"
                            runat="server">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Primer_Apellido" HeaderText="Primer Apellido" />
                                <asp:BoundField DataField="Segundo_Apellido" HeaderText="Segundo Apellido" />
                                <asp:BoundField DataField="Correo_Electronico" HeaderText="Correo Electronico" />
                                <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                                <asp:ButtonField CommandName="Editar" ControlStyle-CssClass="text-secondary" Text="<i class='fas fa-xl fa-edit'></i>" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
