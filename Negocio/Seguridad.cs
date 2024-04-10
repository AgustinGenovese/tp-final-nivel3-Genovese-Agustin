using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            Usuario trainee = user != null ? (Usuario)user : null;
            if (trainee != null && trainee.Id != 0)
                return true;
            else
                return false;
        }

        public static bool esAdmin(object user)
        {
            Usuario trainee = user != null ? (Usuario)user : null;
            return trainee != null ? trainee.Admin : false;
        }

    }
}
