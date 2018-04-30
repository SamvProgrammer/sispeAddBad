using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    public partial class frmImprimirReporteTasas : Form
    {
        public frmImprimirReporteTasas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string aux1 = string.Format("{0}-{1}-{2}",fecha1.Value.Year, fecha1.Value.Month, fecha1.Value.Day);
                string aux2 = string.Format("{0}-{1}-{2}", fecha2.Value.Year, fecha2.Value.Month, fecha2.Value.Day);
                string query = string.Format("select t1.trel,t2.descripcion,t1.tasa,t1.fmodif from catalogos.tasa t1 inner join datos.c_tasai t2 on t1.trel = t2.trel where fmodif >= '{0}' and fmodif <= '{1}'",aux1,aux2);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                object[] objeto = new object[resultado.Count];
                int contador = 0;
                foreach (Dictionary<string,object> item in resultado) {
                    object[] objeto2 = {item["trel"],item["descripcion"],(Convert.ToDouble(item["tasa"])/24/100).ToString("#.##"),item["fmodif"],item["tasa"] };
                    objeto[contador] = objeto2;
                    contador++;
                }
                 aux1 = string.Format("{0}/{1}/{2}", fecha1.Value.Day, fecha1.Value.Month, fecha1.Value.Year);
                 aux2 = string.Format("{0}/{1}/{2}", fecha2.Value.Day, fecha2.Value.Month, fecha2.Value.Year);
                string fechaActual = string.Format("{0}/{1}/{2}",DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                string titulo = string.Format("Reporte de Tasa de Interes \n Prestamos Quirogragarios\nMovimientos del  {0} al {1}",aux1,aux2);
                object[] parametros = { "fechaActual", "titulo" };
                object[] valor = { fechaActual, titulo };
                object[][] enviarParametros = new object[2][];
                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;
                globales.reportes("reporteTasasDeInteresQ", "tasaInteres",objeto,"Imprimiendo reporte de tasa de interes",false,enviarParametros);

            }
            catch {
                MessageBox.Show("Contacte al área de sistemas, error en capturar fechas...");
            }
        }

        private void frmImprimirReporteTasas_Load(object sender, EventArgs e)
        {

        }
    }
}
