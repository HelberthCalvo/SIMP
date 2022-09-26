<%@ Page Title="Mantenimiento de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="SIMP.Usuario" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <h2>Mantenimiento de Usuario</h2>

            <div class="row">
                <%--                <div class="col-lg-12">
                    <div class="mb-4">
                        <label class="form-label">Nombre:</label>
                        <asp:TextBox runat="server" ID="txtId" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                    </div>
                </div>--%>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Nombre:</label>
                        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Primer Apellido:</label>
                        <asp:TextBox runat="server" ID="txtPrimer_Apellido" CssClass="form-control" placeholder="Primer Apellido"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Segundo Apellido:</label>
                        <asp:TextBox runat="server" ID="txtSegundo_Apellido" CssClass="form-control" placeholder="Segundo Apellido"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Usuario:</label>
                        <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Contraseña:</label>
                        <asp:TextBox runat="server" TextMode="Password" ID="txtContrasena" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Rol:</label>
                        <asp:TextBox runat="server" ID="txtRol" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label class="form-label">Estado:</label>
                        <asp:TextBox runat="server" ID="txtEstado" CssClass="form-control"></asp:TextBox>
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

                <%--<asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregar_Click" />--%>
        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row pb-4">
                <div class="col-sm-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvUsuarios" CssClass="table-responsive table customize-table v-middle"
                            DataKeyNames="Nombre, 
                                          Primer_Apellido,
                                          Segundo_Apellido,
                                          Usuario,
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
