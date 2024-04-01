<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FormArticulo.aspx.cs" Inherits="gestor_web.FormArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">

            <h1 style="margin-bottom: 40px;">Informacion del Articulo</h1>

            <!-- Primer mitad pantalla -->

            <div class="col-6">
                <div class="mb-3">
                    <label for="txtCodigo" class="form-label">Codigo</label>
                    <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtDescripcion" class="form-label">Descripcion</label>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="TxtDescripcion" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtMarca" class="form-label">Marca</label>
                    <asp:DropDownList runat="server" CssClass="form-select" ID="ddlMarca"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="txtCategoria" class="form-label">Categoria</label>
                    <asp:DropDownList runat="server" CssClass="form-select" ID="ddlCategoria"></asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label for="txtPrecio" class="form-label">Precio</label>
                    <asp:TextBox runat="server" ID="TxtPrecio" CssClass="form-control" />
                </div>
            </div>

            <!-- Segunda mitad pantalla -->
            <div class="col-6">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <label for="txtImagen" class="form-label">Imagen</label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtImagen" CssClass="form-control" />
                                <asp:Button Text="Cargar" CssClass="btn btn-primary" runat="server" OnClick="btnCargar_Click" />
                            </div>
                        </div>
                        <img src="<% = urlImagen %>" class="img-thumbnail mx-auto d-block" alt="Texto Alternativo" onerror="this.src='https://us.123rf.com/450wm/surfupvector/surfupvector1908/surfupvector190802662/129243509-icono-de-l%C3%ADnea-de-arte-denegado-censura-no-hay-foto-no-hay-imagen-disponible-rechazar-o-cancelar.jpg';" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <div class="col-6">
                <div class="mb-3">
                    <asp:Button Text="Aceptar" CssClass="btn btn-primary" ID="Button1" runat="server" OnClick="btnAceptar_Click" />
                    <a href="Default.aspx">Cancelar</a>
                    <br />
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <asp:Button Text="Eliminar" CssClass="btn btn-danger" ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" />
                        </div>

                        <%if (ConfirmaEliminacion)
                            {  %>
                        <div class="mb-3">
                            <asp:CheckBox Text="Confirmar Eliminacion" ID="chkConfirmarEliminacion" runat="server" />
                            <asp:Button Text="Eliminar" CssClass="btn btn-outline-danger" ID="btnConfirmaEliminacion" OnClick="btnConfirmaEliminacion_Click" runat="server" />
                        </div>
                        <%} %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
