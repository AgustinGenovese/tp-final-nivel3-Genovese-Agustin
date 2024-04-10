<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="gestor_web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="display: flex; justify-content: center; align-items: center; margin-top: 2rem;">
        <div class="card" style="width: 30rem;">
            <div class="card-body">
                <h2 style="text-align: center;">Crea tu perfil</h2>
                <div class="mb-1">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" MaxLength ="20"  />
                    <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Formato incorrecto." ControlToValidate="txtNombre" ValidationExpression="^[a-zA-Z\s]+$" runat="server" />
                    <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Numero de caracteres fuera de rango" ControlToValidate="txtNombre" ValidationExpression="^.{1,20}$" runat="server" />
                </div>
                <div class="mb-1">
                    <label class="form-label">Apellido</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" MaxLength ="20"  />
                    <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Formato incorrecto." ControlToValidate="txtApellido" ValidationExpression="^[a-zA-Z\s]+$" runat="server" />
                    <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Numero de caracteres fuera de rango" ControlToValidate="txtApellido" ValidationExpression="^.{1,20}$" runat="server" />
                </div>
                <div class="mb-1">
                    <label class="form-label">Email</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"  />
                    <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="La dirección de correo electrónico tiene un formato incorrecto" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" runat="server" />
                </div>
                <div class="mb-1">
                    <label class="form-label">Password</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password"  />
                    <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="La contraseña debe tener al menos 8 caracteres e incluir al menos una letra mayúscula, una letra minúscula, un dígito y un carácter especial como @$!%*?&." ControlToValidate="txtPassword" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$" runat="server" />
                </div>
                <div class="d-flex justify-content-between">
                    <div class="mb-3">
                        <asp:Button Text="Registrarse" class="btn btn-success" ID="btnConfirmarRegistro" OnClick="btnConfirmarRegistro_Click" runat="server" />
                    </div>
                    <div class="mb-3">
                        <asp:Button Text="Volver" CssClass="btn btn-primary" ID="btnVolver" OnClick="btnVolver_Click" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
