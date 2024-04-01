<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="gestor_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 10px;
        }
    </style>
    <script>
        function validar() {

            //capturar el control. 
            const txtEmail = document.getElementById("txtEmail");
            const txtContra = document.getElementById("txtPassword");
            var contador = 0;

            if (txtEmail.value == "") {
                txtEmail.classList.add("is-invalid");
                txtEmail.classList.remove("is-valid");
                txtContra.classList.remove("is-invalid");
                txtContra.classList.remove("is-valid");
                return false;
            } else {
                txtEmail.classList.remove("is-invalid");
                txtEmail.classList.add("is-valid");
                contador += 1;
            }

            if (txtContra.value == "") {
                txtContra.classList.add("is-invalid");
                txtContra.classList.remove("is-valid");
                return false;
            } else {
                txtContra.classList.remove("is-invalid");
                txtContra.classList.add("is-valid");
                contador += 1;
            }

            if (contador == 2) {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="display: flex; justify-content: center; align-items: center; margin-top: 3rem;">
        <div class="card" style="width: 25rem;">
            <div class="card-body">
                <h2 style="text-align: center;">Login</h2>
                <div class="mb-1">
                    <label class="form-label">Email</label>
                    <asp:TextBox runat="server" CssClass="form-control" ClientIDMode="Static" ID="txtEmail" />
                    <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Formato incorrecto." ControlToValidate="txtEmail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Password</label>
                    <asp:TextBox runat="server" CssClass="form-control" ClientIDMode="Static" ID="txtPassword" TextMode="Password" />
                </div>
                <div class="mb-3">
                    <asp:Button Text="Ingresar" CssClass="btn btn-primary" ID="btnLogin" OnClick="btnLogin_Click" OnClientClick="return validar()" runat="server" />
                    <a href="/">Cancelar</a>
                </div>
                <div class="mb-3">
                    <a href="/">¿Olvidaste tu contraseña?</a>
                </div>
                <div class="mb-3">
                    <asp:Button Text="Registrarse" class="btn btn-success" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
