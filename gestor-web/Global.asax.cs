using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace gestor_web
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            Session.Add("error", exc.ToString());
            Server.Transfer("Error.aspx");
        }
    }
}