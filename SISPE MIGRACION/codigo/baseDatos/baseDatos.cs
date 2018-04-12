using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.baseDatos
{
    class baseDatos
    {
        private static string cadenaConexion = string.Empty;
        private static NpgsqlConnection conexion;
        public static bool realizarConexion(string cadena) {
            cadenaConexion = cadena;
            bool conexionRealizada = false;
            try
            {
                conexion = new NpgsqlConnection(cadenaConexion);
                conexion.Open();
                conexionRealizada = true;
                conexion.Close();
            }
            catch(Exception e) {
                byte[] arreglo = System.Text.Encoding.Default.GetBytes(e.Message);
                string mensaje = System.Text.Encoding.ASCII.GetString(arreglo);
                MessageBox.Show(mensaje, "Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            return conexionRealizada;
        }

        public static dynamic consulta(string query, bool tipoSelect = false) {
            
            var consulta = new List<Dictionary<string, object>>();
            try
            {
                conexion.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query,conexion);
                if (!tipoSelect)
                {
                    System.Data.Common.DbDataReader datos = cmd.ExecuteReader();

                    while (datos.Read())
                    {
                        Dictionary<string, object> objeto = new Dictionary<string, object>();
                        for (int x = 0; x < datos.FieldCount; x++)
                        {
                            objeto.Add(datos.GetName(x), datos.GetValue(x));
                        }
                        consulta.Add(objeto);
                    }
                }
                else {
                    try {
                        cmd.ExecuteNonQuery();
                        conexion.Close();
                        return true;
                    }
                    catch (Exception e) {
                        MessageBox.Show(e.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        conexion.Close();
                        return false;
                    }
                }
                conexion.Close();
            }
            catch(Exception e) {
                MessageBox.Show(e.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                conexion.Close();
            }
            return consulta;
        }
    }
}
