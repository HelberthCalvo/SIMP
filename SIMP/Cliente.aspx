<%@ Page Title="Mantenimiento Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="SIMP.Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2 class="fs-4">Mantenimiento de Clientes</h2>
            <hr />
            <div class="row">
                <asp:HiddenField runat="server" ID="txtId"/>
                <div class="col-lg-12">
                    <div class="mb-4">
                        <label class="form-label">Nombre:</label>
                        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="mb-4">
                        <label class="form-label">Primer Apellido:</label>
                        <asp:TextBox runat="server" ID="txtApellido1" CssClass="form-control" placeholder="Primer Apellido"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="mb-4">
                        <label class="form-label">Segundo Apellido:</label>
                        <asp:TextBox runat="server" ID="txtApellido2" CssClass="form-control" placeholder="Segundo Apellido"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Email:</label>
                        <asp:TextBox runat="server" TextMode="Email" ID="txtEmail" CssClass="form-control" placeholder="ejemplo@gmail.com"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Teléfono:</label>
                        <asp:TextBox runat="server" TextMode="Phone" ID="txtTelefono" CssClass="form-control" placeholder="88554466"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <br />
                                <asp:LinkButton ID="btnGuardar" class="btn btn-success rounded-pill px-4" runat="server" OnClick="btnGuardar_Click">Guardar</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
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
                                <asp:ButtonField CommandName="editar" Text="<i class='fas fa-2x fa-edit'></i>" />
                                <asp:ButtonField CommandName="eliminar" Text="<i class='fas fa-2x fa-trash-alt'></i>" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
