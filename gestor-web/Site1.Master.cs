using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gestor_web
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page is Default || Page is Registro || Page is Error))
            {
                if (!Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    if (((Usuario)Session["usuario"]).ImagenPerfil != null)
                    {
                        imgAvatar.ImageUrl = "./images/" + ((Usuario)Session["usuario"]).ImagenPerfil;
                    }
                    else
                    {
                        imgAvatar.ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/005/544/718/small/profile-icon-design-free-vector.jpg";
                    }
                }
            }
        }

        protected void Desloguearme_Click(object sender, EventArgs e)
        {
            Session.Remove("usuario");
            Response.Redirect("Default.aspx", false);
        }
    }
}