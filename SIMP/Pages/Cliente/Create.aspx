<%@ Page Title="Crear Nuevo Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="SIMP.Pages.Cliente.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="m-auto" style="width: 95%">
        <h2 class="fs-4">Agregar Cliente</h2>
        <br />
        <div class="row">
            <div class="col-lg-12">
                <div class="mb-4">
                    <label class="form-label">Nombre:</label>
                    <asp:TextBox runat="server" ID="txbNombre" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                </div>
            </div>
            <div class="col-lg-12">
                <div class="mb-4">
                    <label class="form-label">Primer Apellido:</label>
                    <asp:TextBox runat="server" ID="txbApellido1" CssClass="form-control" placeholder="Primer Apellido"></asp:TextBox>
                </div>
            </div>
            <div class="col-lg-12">
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
                    <asp:TextBox runat="server" TextMode="Phone" ID="txbTelefono" CssClass="form-control" placeholder="50688554466"></asp:TextBox>
                </div>
            </div>
        </div>
        <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregar_Click" />
    </div>
</asp:Content>
