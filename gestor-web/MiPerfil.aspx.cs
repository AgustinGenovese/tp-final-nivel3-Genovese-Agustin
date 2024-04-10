using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gestor_web
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario user = (Usuario)Session["usuario"];
                txtEmail.Text = user.Email;
                txtEmail.ReadOnly = true;
                txtEmail.BackColor = System.Drawing.Color.AliceBlue;
                txtNombre.Text = user.Nombre;
                txtApellido.Text = user.Apellido;

                if (user.ImagenPerfil != null)
                {
                    imgNuevoPerfil.ImageUrl = "./images/" + user.ImagenPerfil;
                }
                else
                {
                    imgNuevoPerfil.ImageUrl = "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg";
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            try
            {
                UsuarioData data = new UsuarioData();

                //Escribir img
                Usuario user = (Usuario)Session["usuario"];

                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./images/");
                    string imgUser = "perfil-" + user.Id + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + imgUser);
                    user.ImagenPerfil = imgUser;
                }

                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                data.actualizar(user);

                //leer imagen perfil
                if (user.ImagenPerfil != null)
                {
                    imgNuevoPerfil.ImageUrl = "./images/" + user.ImagenPerfil;
                }
                else
                {
                    imgNuevoPerfil.ImageUrl = "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg";
                }

                //Leer img para barra nav
                Image img = (Image)Master.FindControl("imgAvatar"); //Busca control imgAvatar en la master
                img.ImageUrl = "./images/" + user.ImagenPerfil;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}