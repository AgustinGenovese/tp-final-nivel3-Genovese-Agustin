using dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gestor_web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : ""; //condición ? expresión_si_verdadero : expresión_si_falso

            if (id != "")
            {
                UsuarioData data = new UsuarioData();
                Usuario nuevo = data.listarPorId(id);
                txtEmail.Text = nuevo.Email;
                txtEmail.BackColor = System.Drawing.Color.AliceBlue;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            Usuario usuario = new Usuario();
            UsuarioData data = new UsuarioData();

            try
            {
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                if (data.Loguear(usuario))
                {
                    Session.Add("Usuario", usuario);
                    if (usuario.Admin == true)
                    {
                        Response.Redirect("ListaArticulos.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("ArticulosCartas.aspx", false);
                    }
                }
                else
                {
                    Session.Add("error", "user o pass incorrectos");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
            {
                Response.Redirect("Registro.aspx", false);
            }
        }
    }
}