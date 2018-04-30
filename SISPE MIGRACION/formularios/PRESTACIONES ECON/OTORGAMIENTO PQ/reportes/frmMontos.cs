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
    public partial class frmMontos : Form
    {
        public frmMontos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fec1 = fe1.Value;
            DateTime fec2 = fe2.Value;

            string variable = (radioImporte.Checked) ? "importe" : "liquido";

            string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);
            string c2 = string.Format("{0}-{1}-{2}", fec2.Year, fec2.Month, fec2.Day);


            filtrar(c1, c2, variable);

        }
        private void filtrar(string c1, string c2, string variable)
        {
            string query = string.Format("SELECT folio,rfc,nombre_em,tipo_rel,proyecto, {0} from datos.p_quirog where f_emischeq >= '{1}' AND f_emischeq <= '{2}' order by folio", variable, c1, c2);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            double suma = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                double folio = 0;
                string nombre_em = string.Empty;
                string tipo_rel = string.Empty;
                string proyecto = string.Empty;
                double temporal1 = 0;
                string f_emischeq = string.Empty;
                string rfc = string.Empty;
                try
                {
                    folio = Convert.ToDouble(item["folio"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    tipo_rel = Convert.ToString(item["tipo_rel"]);
                    proyecto = Convert.ToString(item["proyecto"]);
                    temporal1 = (variable == "importe") ? Convert.ToDouble(item["importe"]) : Convert.ToDouble(item["liquido"]);
                    rfc = Convert.ToString(item["rfc"]);
                }
                catch
                {

                }
              //  folio,rfc,nombre_em,tipo_rel,proyecto,
                object[] tt1 = { folio, rfc, nombre_em, tipo_rel, proyecto, temporal1 };
                aux2[contador] = tt1;
                contador++;
                suma +=temporal1;
            }
            object[] parametros = { "fec1", "fec2","suma" };
            object[] valor = { fe1.Text, fe2.Text , suma.ToString() };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;


          

            globales.reportes("reportMonto", "p_montos", aux2, "", false, enviarParametros);


        }
    }
}
