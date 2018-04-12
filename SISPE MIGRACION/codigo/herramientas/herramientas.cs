using SISPE_MIGRACION.codigo.herramientas.forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas
{
    class herramientas
    {
        public static DateTime sacarFechaHabil(int dias, string fechaEspecifica = "")
        {
            DateTime tiempo = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fechaEspecifica))
            {
                string[] aux = fechaEspecifica.Split('/');
                tiempo = new DateTime(Convert.ToInt32(aux[2]), Convert.ToInt32(aux[1]), Convert.ToInt32(aux[0]));
            }
            int contador = dias;
            for (int x = 1; x <= contador; x++)
            {
                DayOfWeek nombreDia = tiempo.DayOfWeek;
                string nombre = nombreDia.ToString();
                if (nombre == "Saturday" || nombre == "Sunday")
                {
                    contador++;
                }
                tiempo = tiempo.AddDays(1);
            }


            return tiempo;
        }

        internal static void reportes(string nombreReporte, string tablaDataSet, object[] objeto, bool imprimir = false)
        {
            frmReporte reporte = new frmReporte(nombreReporte,tablaDataSet);
            reporte.cargarDatos(tablaDataSet, objeto, imprimir);
            reporte.ShowDialog();
        }

        

      

        public static dynamic SeleccionaTasaInteres()
        {
            object tasaDeInteres = null;
           
                frmTasaDeInteresescs tasa = new frmTasaDeInteresescs();
                tasa.ShowDialog();
                tasaDeInteres = tasa.resultado;
            
            return tasaDeInteres;
        }
       
        public static void imprimirDocumento(){
            

            }

       
    }
}
