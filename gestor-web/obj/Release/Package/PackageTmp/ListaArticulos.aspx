<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ListaArticulos.aspx.cs" Inherits="gestor_web.ListaArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <h1 class="display-5">Lista de Artículos</h1>
        <div class="row">
            <div class="col-6">
                <div class="mb-3">
                    <asp:Label Text="Filtrar" runat="server" />
                    <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" />
                </div>
            </div>
            <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
                <div class="mb-3">
                    <asp:CheckBox
                        CssClass="" ID="chkAvanzado" runat="server"
                        AutoPostBack="true"
                        OnCheckedChanged="chkAvanzado_CheckedChanged" />
                    <asp:Label Text="Filtro Avanzado" runat="server" />
                </div>
            </div>
        </div>

        <%if (chkAvanzado.Checked)
            { %>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Campo" ID="lblCampo" runat="server" />
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                        <asp:ListItem Text="Codigo" />
                        <asp:ListItem Text="Marca" />
                        <asp:ListItem Text="Categoria" />
                        <asp:ListItem Text="Precio" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Criterio" runat="server" />
                    <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Filtro" runat="server" />
                    <asp:TextBox runat="server" ID="txtFiltroAvanzado" AutoPostBack="true" CssClass="form-control"  />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>
        <%} %>

        <div class="row">
            <asp:GridView ID="dgvArticulos" runat="server" CssClass="table"
                AutoGenerateColumns="false" DataKeyNames="Id"
                OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged1"
                OnPageIndexChanging="dgvArticulos_PageIndexChanging1"
                AllowPaging="true" PageSize="7">

                <Columns>
                    <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Marca" DataField="Marca.descripcion" />
                    <asp:BoundField HeaderText="Categoria" DataField="Categoria.descripcion" />
                    <asp:BoundField HeaderText="Precio" DataField="Precio" />
                    <asp:CommandField HeaderText="Accion" ShowSelectButton="true" SelectText="Seleccionar" />
                </Columns>
            </asp:GridView>

        </div>
        <a href="FormArticulo.aspx" cssclass="btn btn-primary">Agregar</a>
    </div>

</asp:Content>
