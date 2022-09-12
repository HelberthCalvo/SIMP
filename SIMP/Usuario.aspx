<%@ Page Title="Mantenimiento de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="SIMP.Usuario" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>Mantenimiento de Usuario</h2>
            <div class="row">
                <div class="form-group">
                    <div class="col-md-4">
                        <label class="control-label">ID:</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtId"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Nombre:</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtNombre"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Primer Apellido:</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtPrimer_Apellido"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Segundo Apellido:</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtSegundo_Apellido"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Usuario:</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtUsuario"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Contraseña:</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtContrasena"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Rol:</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtRol"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Estado:</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtEstado"></asp:TextBox>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="form-actions">
                                <div class="card-body d-flex flex-row justify-content-center flex-wrap">
                                    <asp:LinkButton ID="btnGuardar" class="btn btn-success rounded-pill px-4" runat="server" OnClick="btnGuardar_Click">Guardar</asp:LinkButton>
                                </div>
                            </div>
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
                        <asp:GridView ID="gvUsuarios" CssClass="table-responsive table customize-table v-middle"
                            DataKeyNames="Id,
                                            Nombre, 
                                            Primer_Apellido,
                                            Segundo_Apellido,
                                            Usuario,
                                            Contrasena,
                                            Rol,
                                            Estado"
                            OnPreRender="gvUsuarios_PreRender"
                            OnRowCommand="gvUsuarios_RowCommand"
                            AutoGenerateColumns="false"
                            HeaderStyle-CssClass="table-dark"
                            Width="100%"
                            runat="server">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Primer_Apellido" HeaderText="Primer Apellido" />
                                <asp:BoundField DataField="Segundo_Apellido" HeaderText="Segundo Apellido" />
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="Contrasena" HeaderText="Contraseña" />
                                <asp:BoundField DataField="Rol" HeaderText="Rol" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
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
