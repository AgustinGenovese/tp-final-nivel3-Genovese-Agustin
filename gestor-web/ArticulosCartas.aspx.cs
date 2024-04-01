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

namespace gestor_web
{
    public partial class ArticulosCartas : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        public bool Favoritos { get; set; }

        public List<Articulo> ListaArticulo { get; set; }

        public List<Articulo> listaFavoritos { get; set; }

        public ArticulosCartas()
        {
            listaFavoritos = new List<Articulo>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ArticulosData negocio = new ArticulosData();
                ListaArticulo = negocio.listarConSP();
                repRepetidor.DataSource = ListaArticulo;
                repRepetidor.DataBind();

                Favoritos = false;
            }
        }

        protected void btnEjemplo_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;

            ArticulosData data = new ArticulosData();
            Articulo articuloFavorito = data.listarPorId(valor);
            List<Articulo> listaFavoritosSinRepetir = new List<Articulo>();


            if (Session["listaFavoritos"] == null)
            {
                listaFavoritos.Add(articuloFavorito);
                Session["listaFavoritos"] = listaFavoritos;
                listaFavoritosSinRepetir = listaFavoritos;
            }
            else
            {
                listaFavoritos = (List<Articulo>)Session["listaFavoritos"];
                listaFavoritos.Add(articuloFavorito);
                listaFavoritosSinRepetir = listaFavoritos.GroupBy(x => x.Id).Select(g => g.First()).ToList();
                Session["listaFavoritos"] = listaFavoritosSinRepetir;
            }

            repRepetidorFavoritos.DataSource = listaFavoritosSinRepetir;
            repRepetidorFavoritos.DataBind();

            lblMensaje.Visible = true;
            string cantidadArticulosUnicos = Convert.ToString(listaFavoritosSinRepetir.Count);
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

        protected void btnFavoritos_Click(object sender, EventArgs e)
        {
            if (Session["Favoritos"] != null)
            {
                Favoritos = (bool)Session["Favoritos"];
                Session.Add("favoritos", !Favoritos);
            }
            else
            {
                Favoritos = true;
                Session.Add("favoritos", !Favoritos);
            }
        }

        protected void btnEliminarFav_Click(object sender, EventArgs e)
        {
            int valor = int.Parse(((Button)sender).CommandArgument);

            List<Articulo> listaFavoritos = (List<Articulo>)Session["listaFavoritos"];

            Articulo articuloAEliminar = listaFavoritos.FirstOrDefault(x => x.Id == valor);
            if (articuloAEliminar != null)
            {
                listaFavoritos.Remove(articuloAEliminar);
            }

            Session["listaFavoritos"] = listaFavoritos;

            repRepetidorFavoritos.DataSource = listaFavoritos;
            repRepetidorFavoritos.DataBind();

            lblMensaje.Visible = true;
            lblMensaje.Text = listaFavoritos.Count.ToString();
        }
    }
}