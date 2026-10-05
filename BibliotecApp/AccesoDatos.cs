using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace appclinica
{
    public class AccesoDatos
    {
        // Cadena de conexión fija para este proyecto
        // (Modifica los valores de Server, Database, Uid y Pwd según tu entorno de MySQL)
        private string cadenaConexion = "Server=localhost;Database=clinicadb;Uid=root;Pwd=Lamar2022$;Port=3306;";

        // Constructor vacío por defecto
        public AccesoDatos()
        {
        }

        #region Métodos de Ejecución de Comandos (INSERT, UPDATE, DELETE)

        /// <summary>
        /// Ejecuta un comando SQL que no requiere parámetros.
        /// </summary>
        public int EjecutarComando(string query)
        {
            return EjecutarComando(query, null);
        }

        /// <summary>
        /// Ejecuta un comando SQL preparado con parámetros para mayor seguridad contra SQL Injection.
        /// Retorna el número de registros afectados.
        /// </summary>
        public int EjecutarComando(string query, Dictionary<string, object> parametros)
        {
            int filasAfectadas = 0;

            // El bloque using asegura la apertura, uso y cierre automático de la conexión
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    // Si hay parámetros, los agregamos de forma segura
                    if (parametros != null)
                    {
                        foreach (var param in parametros)
                        {
                            comando.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    try
                    {
                        conexion.Open();
                        filasAfectadas = comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al ejecutar el comando: " + ex.Message);
                    }
                }
            }

            return filasAfectadas;
        }

        #endregion

        #region Métodos de Ejecución de Consultas (SELECT)

        /// <summary>
        /// Ejecuta una consulta SELECT sin parámetros (sin WHERE).
        /// </summary>
        public ArrayList EjecutarConsulta(string query)
        {
            return EjecutarConsulta(query, null);
        }

        /// <summary>
        /// Ejecuta una consulta SELECT con parámetros preparados para el WHERE.
        /// Retorna un ArrayList "madre" que contiene diccionarios clave-valor por cada fila,
        /// facilitando su conversión en otra clase hacia tus objetos de negocio.
        /// </summary>
        public ArrayList EjecutarConsulta(string query, Dictionary<string, object> parametros)
        {
            ArrayList resultadoGeneral = new ArrayList();

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    if (parametros != null)
                    {
                        foreach (var param in parametros)
                        {
                            comando.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    try
                    {
                        conexion.Open();
                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                // Cada fila se representa como un diccionario (Columna -> Valor)
                                Dictionary<string, object> fila = new Dictionary<string, object>();

                                for (int i = 0; i < lector.FieldCount; i++)
                                {
                                    string nombreColumna = lector.GetName(i);
                                    object valorColumna = lector.GetValue(i);

                                    // Manejo seguro de nulos en la base de datos
                                    fila[nombreColumna] = (valorColumna == DBNull.Value) ? null : valorColumna;
                                }

                                resultadoGeneral.Add(fila);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al ejecutar la consulta: " + ex.Message);
                    }
                }
            }

            return resultadoGeneral;
        }

        #endregion
    }
}