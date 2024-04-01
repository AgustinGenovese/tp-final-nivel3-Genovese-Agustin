using dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gestor_web
{
    public partial class FormArticulo : System.Web.UI.Page
    {
        public string urlImagen { get; set; }
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            ConfirmaEliminacion = false;
            try
            {
                if (!IsPostBack) 
                {
                    //Configuracion inicial de la Pantalla
                    CategoriaData negocioCategoria = new CategoriaData();                  
                    List<Categoria> listaCategoria = negocioCategoria.listar();

                    ddlCategoria.DataSource = listaCategoria;
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataBind();

                    MarcaData negocioMarca = new MarcaData();
                    List<Marca> listaMarca = negocioMarca.listar();

                    ddlMarca.DataSource = listaMarca;
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataBind();

                    //Configuracion para pantalla modificacion
                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : ""; //condición ? expresión_si_verdadero : expresión_si_falso
                    
                    if (id != "") 
                    {
                        ArticulosData negocioArticulo = new ArticulosData();
                        Articulo seleccionado = negocioArticulo.listarPorId(id);

                        txtCodigo.Text = seleccionado.Codigo;
                        txtNombre.Text = seleccionado.Nombre;
                        TxtDescripcion.Text = seleccionado.Descripcion;
                        TxtPrecio.Text = seleccionado.Precio.ToString();
                        txtImagen.Text = seleccionado.Imagen;
                        urlImagen = txtImagen.Text;

                        ddlCategoria.SelectedValue = seleccionado.Categoria.id.ToString() ;
                        ddlMarca.SelectedValue = seleccionado.Marca.id.ToString();
                         
                        txtCodigo.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }

        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            urlImagen = txtImagen.Text;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = TxtDescripcion.Text;
                nuevo.Precio = decimal.Parse(TxtPrecio.Text);
                nuevo.Imagen = txtImagen.Text;

                nuevo.Marca = new Marca();
                nuevo.Marca.id = int.Parse(ddlMarca.SelectedValue);

                nuevo.Categoria = new Categoria();
                nuevo.Categoria.id = int.Parse(ddlCategoria.SelectedValue);

                ArticulosData data = new ArticulosData();                            

                if (Request.QueryString["Id"] != null)
                {
                    nuevo.Id = int.Parse(Request.QueryString["Id"]);
                    data.modificarConSP(nuevo);
                }
                else
                {
                    data.agregarConSP(nuevo);
                }

                Response.Redirect("Default.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmarEliminacion.Checked)
                {
                    ArticulosData data = new ArticulosData();
                    data.eliminar(int.Parse(Request.QueryString["Id"]));
                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx");
            }
        }
    }
}