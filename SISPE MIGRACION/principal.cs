using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION
{
    static class principal
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string host = SISPE_MIGRACION.Properties.Resources.servidor;
            string usuario = SISPE_MIGRACION.Properties.Resources.usuario;
            string password = SISPE_MIGRACION.Properties.Resources.password;
            string database = SISPE_MIGRACION.Properties.Resources.baseDatos;
            
            string queryConexion = string.Format("Host={0};Username={1};Password={2};Database={3}",host,usuario,password,database);          

            if (baseDatos.realizarConexion(queryConexion))
            {
                Application.Run(new login());///
            }
        }
    }
}
