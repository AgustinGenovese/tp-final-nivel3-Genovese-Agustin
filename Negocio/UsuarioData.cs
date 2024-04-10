using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using Microsoft.Win32;

namespace Negocio
{
    public class UsuarioData
    {
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select id, email, pass, nombre, apellido, urlImagenPerfil, admin from USERS where email = @email and pass = @pass");
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@pass", usuario.Pass);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["id"];
                    usuario.Admin = (bool)datos.Lector["admin"];

                    if (!(datos.Lector["email"] is DBNull))
                        usuario.Email = (string)datos.Lector["email"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["apellido"];

                    if (!(datos.Lector["urlImagenPerfil"] is DBNull))
                        usuario.ImagenPerfil = (string)datos.Lector["urlImagenPerfil"];

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int Registrarse(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("insertarNuevo");
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@pass", nuevo.Pass);
                datos.setearParametro("@admin", nuevo.Admin);
                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public Usuario listarPorId(string id)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            Usuario seleccionado = new Usuario();

            try
            {
                conexion.ConnectionString = ConfigurationManager.AppSettings["cadenaConexion"];
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select id, email, pass, nombre, apellido, urlImagenPerfil, admin from USERS where ";
                comando.CommandText += "Id = " + id;
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    seleccionado.Id = (int)lector["id"];
                    seleccionado.Admin = (bool)lector["admin"];

                    if (!(lector["email"] is DBNull))
                        seleccionado.Email = (string)lector["email"];

                    if (!(lector["nombre"] is DBNull))
                        seleccionado.Nombre = (string)lector["nombre"];

                    if (!(lector["apellido"] is DBNull))
                        seleccionado.Apellido = (string)lector["apellido"];

                    if (!(lector["urlImagenPerfil"] is DBNull))
                        seleccionado.ImagenPerfil = (string)lector["urlImagenPerfil"];   
                }

                conexion.Close();
                return seleccionado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void actualizar(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update USERS set urlImagenPerfil = @imagen, nombre = @nombre, apellido = @apellido Where Id = @id");
                datos.setearParametro("@imagen", user.ImagenPerfil != null ? user.ImagenPerfil : (Object)DBNull.Value);
                datos.setearParametro("@nombre", user.Nombre);
                datos.setearParametro("@apellido", user.Apellido);
                datos.setearParametro("@id", user.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}


