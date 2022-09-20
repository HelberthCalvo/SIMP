<%@ Page Title="Mantenimiento Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SIMP.Pages.Cliente.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="fs-4">Mantenimiento de Clientes</h2>
    <hr />

    <asp:Button runat="server" ID="btnCrear" CssClass="btn btn-primary" Text="Agregar Nuevo" OnClick="btnCrear_Click" />

    <div class="card mt-4">
        <div class="card-header">
            Listado de Clientes
        </div>
        <div class="card-body">
            <table class="table table-bordered table-striped">
                <thead class="table-light">
                    <tr>
                        <th scope="col">First</th>
                        <th scope="col">Last</th>
                        <th scope="col">Handle</th>
                        <th scope="col">Opciones:</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>XXXXX</td>
                        <td>XXXXX</td>
                        <td>XXXXX</td>
                        <td>XXXXX</td>
                    </tr>
                    <tr>
                        <td>XXXXX</td>
                        <td>XXXXX</td>
                        <td>XXXXX</td>
                        <td>XXXXX</td>
                    </tr>
                    <tr>
                        <td>XXXXX</td>
                        <td>XXXXX</td>
                        <td>XXXXX</td>
                        <td>XXXXX</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
