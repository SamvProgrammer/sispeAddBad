using SISPE_MIGRACION.codigo.herramientas.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ
{
    
    public partial class frmTasas : Form
    {
        private List<string> relacionesLaborales;
        public frmTasas()
        {
            InitializeComponent();
        }

        private void frmTasas_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from datos.c_tasai";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                txtTrel.Text = Convert.ToString(resultado[0]["trel"]);
                txtTasas_q.Text = Convert.ToString(resultado[0]["tasa_q"]);
                resultado.ForEach(o => cmbDescripcion.Items.Add(o["descripcion"]));
                cmbDescripcion.SelectedIndex = 0;
                cmbDescripcion.Focus();
                relacionesLaborales = new List<string>();
                resultado.ForEach(o => relacionesLaborales.Add(Convert.ToString(o["trel"])));

            }
            catch {

            }
        }

        private void cmbDescripcion_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format("select * from datos.c_tasai where descripcion = '{0}'",cmbDescripcion.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            txtTrel.Text = Convert.ToString(resultado[0]["trel"]);
            txtTasas_q.Text = Convert.ToString(resultado[0]["tasa_q"]);
            datos.Rows.Clear();
             query = string.Format("select * from catalogos.tasa where trel = '{0}' and t_prestamo = 'Q' order by id desc",txtTrel.Text);
             resultado = globales.consulta(query);
            resultado.ForEach(o => datos.Rows.Add(o["fmodif"], o["tasa"], o["fum"], o["hum"], o["uuqm"],cmbDescripcion.Text));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAgregarTasaInteres tasa = new frmAgregarTasaInteres();
            tasa.ShowDialog();
            string txt1Fecha = tasa.txtFecha;
            string txt1Interes = tasa.txtInteres;
            DateTime auxFechaActual = DateTime.Now;
            string fechaActual = string.Format("{0}-{1}-{2}", auxFechaActual.Year, auxFechaActual.Month, auxFechaActual.Day);
            string horaActual = string.Format("{0}:{1}",auxFechaActual.Hour,auxFechaActual.Minute);
            if (tasa.aceptar) {
                DialogResult resultado = MessageBox.Show("¿Desea que todos las relaciones laborales sea afectado con la nueva tasa de interes?","Tasa de intereses",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                string query = string.Empty;
                if (resultado == DialogResult.Yes)
                {
                    foreach (string item in this.relacionesLaborales) {
                        query = string.Format("insert into catalogos.tasa(trel,fmodif,tasa,uuqm,fum,hum,t_prestamo) values ('{0}','{1}',{2},'{3}','{4}','{5}','{6}')", item, txt1Fecha, txt1Interes, "T", fechaActual, horaActual, "Q");
                        if (!globales.consulta(query, true))
                            MessageBox.Show("Error en el sistema, contacte a los de sistemas", "Error en la consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    MessageBox.Show("Proceso terminado, todas las relaciones laborales afectadas por la tasa de interes actual","Proceso terminado",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else {
                    query = string.Format("insert into catalogos.tasa(trel,fmodif,tasa,uuqm,fum,hum,t_prestamo) values ('{0}','{1}',{2},'{3}','{4}','{5}','{6}')",txtTrel.Text,txt1Fecha,txt1Interes,"T",fechaActual,horaActual,"Q");
                    if (!globales.consulta(query, true)) MessageBox.Show("Error en el sistema, contacte a los de sistemas", "Error en la consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        MessageBox.Show(string.Format("Proceso terminado, la relación laboral {0} afectado por la tasa de interes actual",this.cmbDescripcion.Text),"Proceso terminado",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                cmbDescripcion_TextChanged(null,null);
            }
        }

    }
}
