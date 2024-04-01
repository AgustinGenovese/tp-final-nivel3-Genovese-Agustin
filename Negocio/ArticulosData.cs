using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using Negocio;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace negocio
{
    public class ArticulosData
    {
        public List<Articulo> listarConSP() //Procedimiento Almacenado
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedListar");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo
                    {
                        Id = (int)datos.Lector["Id"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Descripcion = (string)datos.Lector["Descripcion"],

                        Categoria = new Categoria
                        {
                            id = (int)datos.Lector["IdCategoria"],
                            descripcion = (string)datos.Lector["CategoriaDescripcion"]
                        },

                        Marca = new Marca
                        {
                            id = (int)datos.Lector["IdMarca"],
                            descripcion = (string)datos.Lector["MarcaDescripcion"]
                        },

                        Imagen = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl")) ? null : (string)datos.Lector["ImagenUrl"],
                        Precio = (decimal)datos.Lector["Precio"]
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

        public void agregarConSP(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storedAltaArticulo");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@idMarca", nuevo.Marca.id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.id);
                datos.setearParametro("@ImagenUrl", nuevo.Imagen);
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

        public Articulo listarPorId(string id)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            Articulo seleccionado = new Articulo();

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, A.IdMarca, A.IdCategoria, C.Descripcion AS CategoriaDescripcion, M.Descripcion AS MarcaDescripcion From ARTICULOS A, CATEGORIAS C, MARCAS M Where A.IdMarca = M.Id And A.IdCategoria = C.Id ";
                comando.CommandText += " and A.Id = " +  id;
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    seleccionado.Id = (int)lector["Id"];
                    seleccionado.Codigo = (string)lector["Codigo"];
                    seleccionado.Nombre = (string)lector["Nombre"];
                    seleccionado.Descripcion = (string)lector["Descripcion"];

                    seleccionado.Categoria = new Categoria();
                    seleccionado.Categoria.id = (int)lector["IdCategoria"];
                    seleccionado.Categoria.descripcion = (string)lector["CategoriaDescripcion"];

                    seleccionado.Marca = new Marca();
                    seleccionado.Marca.id = (int)lector["IdMarca"];
                    seleccionado.Marca.descripcion = (string)lector["MarcaDescripcion"];

                    seleccionado.Imagen = lector.IsDBNull(lector.GetOrdinal("ImagenUrl")) ? null : (string)lector["ImagenUrl"];
                    seleccionado.Precio = (decimal)lector["Precio"];
                }
                conexion.Close();
                return seleccionado;
            }
            catch (Exception ex)
            {
                throw ex;
            }         
        }

        public void modificarConSP(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarArticulo");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@idMarca", nuevo.Marca.id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.id);
                datos.setearParametro("@ImagenUrl", nuevo.Imagen);
                datos.setearParametro("@id", nuevo.Id);
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

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from articulos where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, A.IdMarca, A.IdCategoria, C.Descripcion AS CategoriaDescripcion, M.Descripcion AS MarcaDescripcion From ARTICULOS A, CATEGORIAS C, MARCAS M Where A.IdMarca = M.Id And A.IdCategoria = C.Id And ";
                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Precio < " + filtro;
                            break;
                        default:
                            consulta += "Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Codigo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Codigo like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Codigo like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Codigo like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "M.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "C.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "C.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "C.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.descripcion = (string)datos.Lector["CategoriaDescripcion"];

                    aux.Marca = new Marca();
                    aux.Marca.id = (int)datos.Lector["IdMarca"];
                    aux.Marca.descripcion = (string)datos.Lector["MarcaDescripcion"];

                    aux.Imagen = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl")) ? null : (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
