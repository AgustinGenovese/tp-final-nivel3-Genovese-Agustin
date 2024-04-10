using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using System.Net.NetworkInformation;
using System.Collections;
using Negocio;

namespace gestor_web
{
    public partial class ArticulosCartas : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        public bool Favoritos { get; set; }

        public List<Articulo> ListaArticulo { get; set; }

        public List<Articulo> listaFavoritos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ArticulosData negocio = new ArticulosData();
                ListaArticulo = negocio.listarConSP();
                repRepetidor.DataSource = ListaArticulo;
                repRepetidor.DataBind();

                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con"); ;

                Favoritos = false;
            }
        }

        protected void btnEjemplo_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
            int valorEntero = int.Parse(valor);

            Favorito nuevo = new Favorito();
            nuevo.IdArticulo = valorEntero;
            Usuario usuario = (Usuario)Session["usuario"];
            nuevo.idUser = usuario.Id;

            FavoritosData negocioFavorito = new FavoritosData();
            negocioFavorito.AgregarFavorito(nuevo);
            List<Favorito> listaFavoritos = negocioFavorito.listarConSP();

            ArticulosData negocio = new ArticulosData();
            ListaArticulo = negocio.listarConSP();

            if (listaFavoritos.Count != 0)
            {
                List<Articulo> favoritosFiltrados = new List<Articulo>();

                favoritosFiltrados = ListaArticulo
                    .Where(articulo =>
                        listaFavoritos.Any(favorito =>
                            articulo.Id == favorito.IdArticulo &&
                            favorito.idUser == usuario.Id))
                    .ToList();

                string cantidadArticulosUnicos = Convert.ToString(favoritosFiltrados.Count);
                lblMensaje.Text = cantidadArticulosUnicos;
            }
        }

        protected void btnFavoritos_Click(object sender, EventArgs e)
        {

            FavoritosData negocioFavorito = new FavoritosData();
            List<Favorito> listaFavoritos = negocioFavorito.listarConSP();

            ArticulosData negocio = new ArticulosData();
            ListaArticulo = negocio.listarConSP();

            List<Articulo> favoritosFiltrados = new List<Articulo>();

            Usuario usuario = (Usuario)Session["usuario"];

            if (listaFavoritos.Count != 0)
            {
                favoritosFiltrados = ListaArticulo
                    .Where(articulo =>
                        listaFavoritos.Any(favorito =>
                            articulo.Id == favorito.IdArticulo &&
                            favorito.idUser == usuario.Id))
                    .ToList();

                repRepetidorFavoritos.DataSource = favoritosFiltrados;
                repRepetidorFavoritos.DataBind();

                string cantidadArticulosUnicos = Convert.ToString(favoritosFiltrados.Count);
                lblMensaje.Text = cantidadArticulosUnicos;
            }

            Favoritos = Session["Favoritos"] != null ? (bool)Session["Favoritos"] : true;
            Session["favoritos"] = !Favoritos;
        }

        protected void btnEliminarFav_Click(object sender, EventArgs e)
        {
            int valor = int.Parse(((Button)sender).CommandArgument);

            FavoritosData negocioFavoritos = new FavoritosData();
            negocioFavoritos.eliminar(valor);
            List<Favorito> listaFavoritos = negocioFavoritos.listarConSP();

            ArticulosData negocio = new ArticulosData();
            ListaArticulo = negocio.listarConSP();

            List<Articulo> favoritosFiltrados = new List<Articulo>();

            Usuario usuario = (Usuario)Session["usuario"];

            favoritosFiltrados = ListaArticulo
                    .Where(articulo =>
                        listaFavoritos.Any(favorito =>
                            articulo.Id == favorito.IdArticulo &&
                            favorito.idUser == usuario.Id))
                    .ToList();

            repRepetidorFavoritos.DataSource = favoritosFiltrados;
            repRepetidorFavoritos.DataBind();

            string cantidadArticulosUnicos = Convert.ToString(favoritosFiltrados.Count);
            lblMensaje.Text = cantidadArticulosUnicos;

        }

        protected void chkAvanzado_CheckedChanged1(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            ArticulosData negocio = new ArticulosData();
            List<Articulo> lista = negocio.listarConSP();
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            repRepetidor.DataSource = listaFiltrada;
            repRepetidor.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticulosData negocio = new ArticulosData();
                repRepetidor.DataSource = negocio.filtrar(
                    ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(),
                    txtFiltroAvanzado.Text);
                repRepetidor.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}