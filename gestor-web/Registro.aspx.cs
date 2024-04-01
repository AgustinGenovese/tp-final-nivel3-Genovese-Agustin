using Dominio;
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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirmarRegistro_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            try
            {
                Usuario usuario = new Usuario();
                UsuarioData data = new UsuarioData();
                EmailService emailService = new EmailService();

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text; 
                usuario.Pass = txtPassword.Text;
                usuario.Admin = false;
                int id = data.Registrarse(usuario);

               // emailService.armarCorreo(usuario.Email, "Bienvenido/a " + usuario.Apellido);
                //emailService.enviarEmail();

                Session.Add("usuario", usuario);
                Response.Redirect("Default.aspx?id=" + id, false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx"); ;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }

    }
}