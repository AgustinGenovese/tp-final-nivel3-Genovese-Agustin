using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class EmailService
    {
        public  string cuerpo { get; set; }
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("ff01e6ec42e120", "78e6c7a7b640fb");
            server.EnableSsl = true;
            server.Port = 2525;
            server.Host = "smtp.mailtrap.io";
            cuerpo = "<h1 style=\"color: #007bff;\">¡Hola!</h1>\r\n\r\n<p>En nombre de todo el equipo de Gestor Web, quiero extenderte una cálida bienvenida a nuestra comunidad en línea. Estamos encantados de que te hayas unido a nosotros y esperamos que tu experiencia con nosotros sea excepcional.</p>\r\n\r\n<p>Como nuevo miembro, tendrás acceso a una amplia gama de recursos, funciones y oportunidades para explorar y participar. Siempre estamos abiertos a tus comentarios, sugerencias y preguntas, así que no dudes en ponerte en contacto con nuestro equipo de soporte si necesitas ayuda o tienes alguna inquietud.</p>\r\n\r\n<p>Para comenzar, te invitamos a explorar nuestro sitio web y descubrir todo lo que tenemos para ofrecer. ¡Esperamos que encuentres justo lo que estás buscando y que disfrutes de tu tiempo con nosotros!</p>\r\n\r\n<p>¡Bienvenido/a a nuestra comunidad!</p>\r\n\r\n<p>Saludos cordiales,</p>";
        }

        public void armarCorreo(string emailDestino, string asunto)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@gestorweb.com");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpo;
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
