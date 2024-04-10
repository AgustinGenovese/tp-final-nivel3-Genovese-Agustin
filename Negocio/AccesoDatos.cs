using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        // Propiedad de solo lectura para acceder al lector desde fuera de la clase
        {
            get { return lector; }
        }


        public AccesoDatos()
        // Constructor de la clase AccesoDatos
        {
            conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        // Establece la consulta SQL que se ejecutará y configura el tipo de comando como texto
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void setearParametro(string nombreParametro, object valor)
        // Añade parámetros a la colección de parámetros del comando
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombreParametro;
            parametro.Value = valor;
            comando.Parameters.Add(parametro);
        }

        public void setearProcedimiento(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        public void ejecutarLectura()
        // Ejecuta una lectura de datos y asigna el resultado al lector
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ejecutarAccion()
        // Ejecuta un comando que no devuelve datos
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ejecutarAccionScalar()
        // Ejecuta un comando que no devuelve datos
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return int.Parse(comando.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void cerrarConexion()
        // Cierra el lector de datos si está abierto y cierra la conexión a la base de datos
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }
    }
}
