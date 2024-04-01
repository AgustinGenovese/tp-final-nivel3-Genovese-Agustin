<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ArticulosCartas.aspx.cs" Inherits="gestor_web.ArticulosCartas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="d-flex justify-content-between align-items-center">
            <h1 class="display-5">Catalogo de Artículos</h1>
            <div>
                <asp:Button ID="btnFavoritos" runat="server" CssClass="btn btn-success position-relative" Text="Favoritos" OnClick="btnFavoritos_Click" />
                <asp:Label ID="lblMensaje" runat="server" CssClass="position-absolute translate-middle badge rounded-pill bg-warning" Visible="false"></asp:Label>
            </div>
        </div>

        <div class="container-fluid">
            <div class="row">
                <div class="col-md-3" style="margin-top: 30px">
                    <div class="col-11">
                        <div class="mb-3">
                            <div style="margin-bottom: 5px">
                                <asp:Label Text="Filtrar por nombre" runat="server" />
                            </div>
                            <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" />
                        </div>
                        <div class="mb-3">
                            <asp:CheckBox
                                ID="chkAvanzado" runat="server"
                                AutoPostBack="true"
                                OnCheckedChanged="chkAvanzado_CheckedChanged1" />
                            <asp:Label Text="Filtro Avanzado" runat="server" />
                        </div>
                    </div>

                    <%if (chkAvanzado.Checked)
                        { %>
                    <div class="col-9">
                        <div class="mb-3">
                            <asp:Label Text="Campo" ID="lblCampo" runat="server" />
                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                                <asp:ListItem Text="Marca" />
                                <asp:ListItem Text="Categoria" />
                                <asp:ListItem Text="Precio" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-9">
                        <div class="mb-3">
                            <asp:Label Text="Criterio" runat="server" />
                            <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-9">
                        <div class="mb-3">
                            <asp:Label Text="Filtro" runat="server" />
                            <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="col-9">
                        <div class="mb-3">
                            <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                    <%} %>
                </div>

                <div class="col-md-9">

                    <%if (Favoritos)
                        { %>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header">
                                        Articulos Seleccionados
                                    </div>
                                    <div class="card-body">
                                        <div class="row row-cols-2 row-cols-md-3 g-4" style="margin-top: 10px">
                                            <asp:Repeater runat="server" ID="repRepetidorFavoritos">
                                                <ItemTemplate>
                                                    <div class="col">
                                                        <div class="card" style="transition: transform 0.3s ease;" onmouseover="this.style.transform='scale(1.05)'" onmouseout="this.style.transform='scale(1)'">
                                                            <div style="display: flex; justify-content: center;">
                                                                <img src="<%#Eval("Imagen")%>" class="card-img-top" alt="..." style="max-width: 50%; height: 180px;" onerror="this.src='https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg'">
                                                            </div>
                                                            <div class="card-body">
                                                                <h5 class="card-title" style="margin-bottom: 20px;"><%#Eval("Nombre")%></h5>
                                                                <p class="card-text">
                                                                    Marca: <%#Eval("Marca.descripcion")%>
                                                                    <br />
                                                                    Categoria: <%#Eval("Categoria.descripcion")%>
                                                                    <br />
                                                                </p>
                                                                <p>
                                                                    <%#Eval("Descripcion")%>
                                                                </p>
                                                                <p class="card-text">$<%#Eval("Precio")%></p>
                                                                <div>
                                                                    <asp:Button Text="Eliminar" CssClass="btn btn-outline-danger" ID="btnEliminarFav" OnClick="btnEliminarFav_Click" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%} %>

                    <div class="row row-cols-2 row-cols-md-3 g-4" style="margin-top: 10px">
                        <asp:Repeater runat="server" ID="repRepetidor">
                            <ItemTemplate>
                                <div class="col">
                                    <div class="card" style="transition: transform 0.3s ease;" onmouseover="this.style.transform='scale(1.05)'" onmouseout="this.style.transform='scale(1)'">
                                        <div style="display: flex; justify-content: center;">
                                            <img src="<%#Eval("Imagen")%>" class="card-img-top" alt="..." style="max-width: 50%; height: 180px;" onerror="this.src='https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg'">
                                        </div>
                                        <div class="card-body">
                                            <h5 class="card-title" style="margin-bottom: 20px;"><%#Eval("Nombre")%></h5>
                                            <p class="card-text">
                                                Marca: <%#Eval("Marca.descripcion")%>
                                                <br />
                                                Categoria: <%#Eval("Categoria.descripcion")%>
                                                <br />
                                            </p>
                                            <p>
                                                <%#Eval("Descripcion")%>
                                            </p>
                                            <p class="card-text">$<%#Eval("Precio")%></p>
                                            <asp:Button Text="Añadir a favoritos" runat="server" CssClass="btn btn-outline-primary" ID="btnEjemplo" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" OnClick="btnEjemplo_Click" />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
