<%@ Page Title="Mantenimiento de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="SIMP.Usuario" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div class="row">
        <div class="form-group">
            <div class="col-md-4">
                <label class="control-label">ID:</label>
                <asp:TextBox class="form-control" runat="server" ID="Id"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label>Nombre:</label>
                <asp:TextBox class="form-control" runat="server" ID="Nombre"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label>Primer Apellido:</label>
                <asp:TextBox class="form-control" runat="server" ID="PrimerApellido"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label>Segundo Apellido:</label>
                <asp:TextBox class="form-control" runat="server" ID="SegundoApellido"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label>Usuario:</label>
                <asp:TextBox class="form-control" runat="server" ID="Usuario1"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label>Contraseña:</label>
                <asp:TextBox class="form-control" runat="server" ID="Contraseña"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label>Rol:</label>
                <asp:TextBox class="form-control" runat="server" ID="Rol"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label>Estado:</label>
                <asp:TextBox class="form-control" runat="server" ID="Estado"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <br />
                <asp:Button runat="server" class="btn btn-primary" Text="Guardar" />
            </div>

        </div>
    </div>

</asp:Content>
