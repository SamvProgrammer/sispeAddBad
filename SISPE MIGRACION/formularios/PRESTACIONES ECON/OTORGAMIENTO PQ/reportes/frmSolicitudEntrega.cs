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
    public partial class frmSolicitudEntrega : Form
    {
        public frmSolicitudEntrega()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

          
            DateTime f1 = fecha1.Value;
            DateTime f2 = fecha2.Value;

            string t1 = string.Format("{0}-{1}-{2}",f1.Year,f1.Month,f1.Day);
            string t2 = string.Format("{0}-{1}-{2}", f2.Year, f2.Month, f2.Day);

            filtrar(t1,t2);


        }

        private void filtrar(string t1, string t2)
        {
            string query = string.Format("select * from datos.p_quirog where f_emischeq >= '{0}' and f_emischeq <= '{1}' order by f_emischeq asc", t1, t2);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 =new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                double folio = 0;
                string nombre_em = string.Empty;
                string fecha_solicitud = string.Empty;
                string f_recib = "_____";
                double importe = 0;
                double plazo = 0;
                string proyecto = string.Empty;
                string fecha_emision = string.Empty;
                try
                {
                    folio = Convert.ToDouble(item["folio"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    fecha_solicitud = Convert.ToString(item["f_solicitud"]);
                    fecha_emision = Convert.ToString(item["f_emischeq"]);
                    importe = Convert.ToDouble(item["importe"]);
                    plazo = Convert.ToDouble(item["plazo"]);
                    proyecto = Convert.ToString(item["proyecto"]);
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
                object[] tt1 = { folio, nombre_em, fecha_solicitud, fecha_emision, f_recib, importe, plazo, proyecto };
                aux2[contador] = tt1;
                contador++;
            }

            object[] parametros = { "fecha1","fecha2" };
            object[] valor = { fecha1.Text,fecha2.Text };
            object[][] enviarParametros= new object[2][];
          
            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporteSolicitudEntrega", "p_quirog",aux2,"",false, enviarParametros);
          //  globales.reportes("reporteSolicitudEntrega", "p_quirog",aux2);
        }

        private void frmSolicitudEntrega_Load(object sender, EventArgs e)
        {

        }
    }
}
