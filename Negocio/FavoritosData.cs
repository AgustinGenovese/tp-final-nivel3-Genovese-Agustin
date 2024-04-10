using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using Microsoft.Win32;

namespace Negocio
{
    public class FavoritosData
    {
        public List<Favorito> listarConSP() //Procedimiento Almacenado
        {
            List<Favorito> lista = new List<Favorito>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedListarFavoritos");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Favorito aux = new Favorito
                    {
                        idUser = (int)datos.Lector["idUser"],
                        IdArticulo = (int)datos.Lector["IdArticulo"]
                    };

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarFavorito(Favorito nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("insertarNuevoFavorito");
                datos.setearParametro("@idUser", nuevo.idUser);
                datos.setearParametro("@IdArticulo", nuevo.IdArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from favoritos where IdArticulo = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
