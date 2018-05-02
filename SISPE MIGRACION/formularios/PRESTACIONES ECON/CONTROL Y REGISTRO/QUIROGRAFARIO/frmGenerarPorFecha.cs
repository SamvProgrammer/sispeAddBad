using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO
{
    public partial class frmGenerarPorFecha : Form
    {
        public frmGenerarPorFecha()
        {
            InitializeComponent();
        }

        private void frmGenerarPorFecha_Load(object sender, EventArgs e)
        {
            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
            fecha = fecha.AddDays(30);
            string fechaTexto = string.Format("{0}/{1}/{2}", "15", fecha.Month, fecha.Year);
            txtFecha.Text = fechaTexto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblMensaje.Visible = true;
                lblMensaje.Text = "GENERANDO ESTADOS DE CUENTA";
                MessageBox.Show("Se va a generar el estado de cuenta y solicitudes de descuento", "Estados de cuenta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string[] arreglo = txtFecha.Text.Split('/');
                string fechaTexto = string.Format("{0}-{1}-{2}", arreglo[2], arreglo[1], arreglo[0]);
                string query = string.Format("select * from datos.p_quirog where f_primdesc = '{0}'", fechaTexto);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (Dictionary<string, object> item in resultado)
                {

                    query = string.Format("select * from datos.p_edocta where folio = {0}", item["folio"]);
                    resultado = globales.consulta(query);
                    if (resultado.Count != 0) continue;

                    string f_solicitud = string.Empty;
                    string f_emischeq = string.Empty;
                    string f_primdesc = string.Empty;

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(item["f_solicitud"])))
                    {
                        arreglo = Convert.ToString(item["f_solicitud"]).Replace(" 12:00:00 a. m.", "").Split('/');
                        f_solicitud = string.Format("{0}-{1}-{2}", arreglo[2], arreglo[1], arreglo[0]);
                    }

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(item["f_emischeq"])))
                    {
                        arreglo = Convert.ToString(item["f_emischeq"]).Replace(" 12:00:00 a. m.", "").Split('/');
                        f_emischeq = string.Format("{0}-{1}-{2}", arreglo[2], arreglo[1], arreglo[0]);
                    }

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(item["f_primdesc"])))
                    {
                        arreglo = Convert.ToString(item["f_primdesc"]).Replace(" 12:00:00 a. m.", "").Split('/');
                        f_primdesc = string.Format("{0}-{1}-{2}", arreglo[2], arreglo[1], arreglo[0]);
                    }

                    query = string.Format("insert into datos.p_edocta values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}',{12},{13},{14},'')",
                        item["folio"], item["rfc"], item["nombre_em"], item["direccion"], item["proyecto"], item["secretaria"], item["descripcion"], f_solicitud, f_emischeq, f_primdesc, item["antig_q"], item["tipo_pago"], item["plazo"], item["imp_unit"], item["importe"]);
                    globales.consulta(query, true);
                    query = string.Format("insert into datos.d_ecqdep(folio,tipo_mov,sec,tipo_rel,f_descuento,numdesc,totdesc,imp_unit,rfc,nombre_em,proyecto) values({0},'{1}','{2}','{3}','{4}',{5},{6},{7},'{8}','{9}','{10}')",
                                        item["folio"], "AN", "1", item["tipo_rel"], f_primdesc, 1, item["plazo"], item["imp_unit"], item["rfc"], item["nombre_em"], item["proyecto"]);
                    globales.consulta(query, true);
                }
                lblMensaje.Visible = false;
                this.Cursor = Cursors.Default;
                MessageBox.Show("Proceso terminado","Proceso terminado",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Close();
            }
            catch
            {
                Cursor = Cursors.Default;
                lblMensaje.Visible = false;
                MessageBox.Show("Error en el sistema, porfavor contactar al departamento de sistemas","Error en el sistema",MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}