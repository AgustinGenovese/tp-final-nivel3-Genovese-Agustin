<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="gestor_web.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h3>Hubo un error</h3>
        <div class="mb-3">
            <asp:Label Text="text" ID="lblMensaje" runat="server" />
        </div>
    </div>

</asp:Content>
