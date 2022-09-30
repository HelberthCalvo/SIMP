<%@ Page Title="Mantenimiento de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="SIMP.Usuario" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="form" runat="server">
        <ContentTemplate>
            <h2 class="fs-4">Mantenimiento de Usuario</h2>
            <hr />
            <div class="row">
                <asp:HiddenField runat="server" ID="txtId" />
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
                        <asp:DropDownList runat="server" ID="ddlRol" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <label style="color: black !important">Estado</label>
                        <br />
                        <div class="form-check">
                            <asp:RadioButton ID="rdbActivo" style="color:black !important; background:white !important" class="form-check-input" Checked="true" GroupName="groupEstado" runat="server" />
                            <label class="form-check-label" style="color: black !important" for="MainContent_rdbActivo">Activo</label>
                        </div>
                        <div class="form-check">
                            <asp:RadioButton ID="rdbInactivo" style="color:black !important; background:white !important" class="form-check-input" GroupName="groupEstado" runat="server" />
                            <label class="form-check-label" style="color: black !important" for="MainContent_rdbInactivo">Inactivo</label>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="mb-4">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <br />
                                <asp:LinkButton ID="btnGuardar" class="btn btn-success rounded-pill px-4" runat="server" OnClick="btnGuardar_Click">Guardar</asp:LinkButton>
                                <asp:LinkButton ID="btnLimpiar" class="btn btn-secondary rounded-pill px-4" runat="server" OnClick="btnLimpiar_Click">Limpiar</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>

                <div class="row pb-4">
                    <div class="col-sm-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvUsuarios" CssClass="table-responsive table customize-table v-middle"
                                DataKeyNames="Id, Nombre, Primer_Apellido, Segundo_Apellido, Usuario, Rol, Estado"
                                OnPreRender="gvUsuarios_PreRender"
                                OnRowCommand="gvUsuarios_RowCommand"
                                OnRowDataBound="gvUsuarios_RowDataBound"
                                AutoGenerateColumns="false"
                                HeaderStyle-CssClass="table-dark"
                                Width="100%"
                                runat="server"  >
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />
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
                <%--<asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregar_Click" />--%>
        </ContentTemplate>

    </asp:UpdatePanel>


    <script>
        Type.registerNamespace('Sys.WebForms');
        Sys.WebForms.Res = {
            "PRM_UnknownToken": "Unknown token: \u0027{0}\u0027.",
            "PRM_MissingPanel": "Could not find UpdatePanel with ID \u0027{0}\u0027. If it is being updated dynamically then it must be inside another UpdatePanel.",
            "PRM_ServerError": "An unknown error occurred while processing the request on the server. The status code returned from the server was: {0}",
            "PRM_ParserError": "The message received from the server could not be parsed.",
            "PRM_TimeoutError": "The server request timed out.",
            "PRM_ParserErrorDetails": "Error parsing near \u0027{0}\u0027.",
            "PRM_CannotRegisterTwice": "The PageRequestManager cannot be initialized more than once."
        };
    </script>
</asp:Content>

