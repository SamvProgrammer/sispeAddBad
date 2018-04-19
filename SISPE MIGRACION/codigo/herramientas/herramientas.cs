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

        internal static void reportes(string nombreReporte, string tablaDataSet, object[] objeto,string mensaje, bool imprimir = false)
        {
            frmReporte reporte = new frmReporte(nombreReporte,tablaDataSet);
            reporte.cargarDatos(tablaDataSet, objeto,mensaje, imprimir);
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
        public static string numerosALetras(int number)
        {
            if (number == 0)
                return "Cero";

            if (number < 0)
                return "Menos " + numerosALetras(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += numerosALetras(number / 1000000) + " Millones ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += numerosALetras(number / 1000) + " Mil ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += numerosALetras(number / 100) + " Cien ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "Y ";

                var unitsMap = new[] { "cero", "Uno", "Dos", "Tres", "Cuatro", "Cinco", "Seis", "Siete", "Ocho", "Nueve", "Diez", "Once", "Doce", "Trece", "Catorce", "Quince", "Dieciséis", "diecisiete", "dieciocho", "diecinueve" };
                var tensMap = new[] { "Cero", "Diez", "Veinte", "Treinta", "Cuarenta", "Cincuenta", "Sesenta", "Setenta", "Ochenta", "Noventa" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }


    }
}
