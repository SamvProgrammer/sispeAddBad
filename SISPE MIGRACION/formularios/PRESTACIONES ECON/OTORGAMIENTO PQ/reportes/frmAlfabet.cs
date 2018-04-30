using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes
{
    public partial class frmAlfabet : Form
    {
        public frmAlfabet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fec1 = fecha1.Value;
            DateTime fec2 = fecha2.Value;

            string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);
            string c2 = string.Format("{0}-{1}-{2}", fec2.Year, fec2.Month, fec2.Day);

            filtrar(c1, c2);


        }

        private void filtrar(string c1, string c2)
        {
            string query = string.Format("select folio, nombre_em , f_solicitud , f_emischeq ,proyecto , importe from datos.p_quirog where f_emischeq >= '{0}' and f_emischeq <= '{1}' order by nombre_em asc", c1, c2);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                double folio = 0;
                string nombre_em = string.Empty;
                string fecha_solicitud = string.Empty;
                string fecha_emision = string.Empty;
                string proyecto = string.Empty;
                double importe = 0;

                try
                {
                    folio = Convert.ToDouble(item["folio"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    fecha_solicitud = Convert.ToString(item["f_solicitud"]);
                    fecha_emision = Convert.ToString(item["f_emischeq"]);
                    proyecto = Convert.ToString(item["proyecto"]);
                    importe = Convert.ToDouble(item["importe"]);

                }
                catch
                {

                }
                fecha_solicitud = fecha_solicitud.Replace(" 12:00:00 a. m.", "");
                fecha_emision = fecha_emision.Replace(" 12:00:00 a. m.", "");

                string[] aux = fecha_solicitud.Split('/');
                fecha_solicitud = string.Format("{0}-{1}-{2}", aux[2], aux[1], aux[0]);

                aux = fecha_emision.Split('/');
                fecha_emision = string.Format("{0}-{1}-{2}", aux[2], aux[1], aux[0]);
                object[] tt1 = { folio, nombre_em, fecha_solicitud, " ", fecha_emision, importe, " ", proyecto};
                aux2[contador] = tt1;
                contador++;
            }
            object[] parametros = { "fech1", "fech2" };
            object[] valor = { fecha1.Text, fecha2.Text };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporteAlfa", "p_quirog", aux2, "", false, enviarParametros);
        }
    }
}

       

