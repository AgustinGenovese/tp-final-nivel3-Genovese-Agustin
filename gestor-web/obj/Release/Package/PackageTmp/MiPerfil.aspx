<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="gestor_web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="display: flex; justify-content: center; align-items: center; margin-top: 1rem;">
        <div class="card" style="width: 70rem;">
            <div class="card-body">
                <h2>Mi Perfil</h2>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label class="form-label">Email</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />

                        </div>
                        <div class="mb-3">
                            <label class="form-label">Nombre</label>
                            <asp:TextBox runat="server" CssClass="form-control" AutoPostBack="true" ID="txtNombre" />
                            <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="El nombre es requerido" ControlToValidate="txtNombre" runat="server" />
                            <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Numero de caracteres fuera de rango" ControlToValidate="txtNombre" ValidationExpression="^.{1,20}$" runat="server" />
                            <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Formato incorrecto." ControlToValidate="txtNombre" ValidationExpression="^[a-zA-Z\s]+$" runat="server" />
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Apellido</label>
                            <asp:TextBox ID="txtApellido" runat="server" AutoPostBack="true" CssClass="form-control" />
                            <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="El apellido es requerido" ControlToValidate="txtApellido" runat="server" />
                            <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Numero de caracteres fuera de rango" ControlToValidate="txtApellido" ValidationExpression="^.{1,20}$" runat="server" />
                            <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Formato incorrecto" ControlToValidate="txtApellido" ValidationExpression="^[a-zA-Z\s]+$" runat="server" />
                        </div>
                        <div class="mb-3" style="margin-top: 3rem;">
                            <asp:Button Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" ID="Button2" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label class="form-label">Imagen Perfil</label>
                            <input type="file" id="txtImagen" runat="server" class="form-control" />
                        </div>
                        <div style="display: flex; justify-content: center;">
                            <asp:Image ID="imgNuevoPerfil" runat="server" CssClass="img-fluid mb-3" Style="max-width: 300px; max-height: 400px; width: auto; height: auto;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
