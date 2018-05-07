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
    public partial class frmActualizarRelLaboral : Form
    {
        public frmActualizarRelLaboral()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime datos = fe1.Value;
                int dia = datos.Day;
                if (dia > 15) {
                    MessageBox.Show("Fecha pertenece a segunda quincena.","Fecha quincena",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }

                Cursor = Cursors.WaitCursor;

                MessageBox.Show("Se seleccionara el último tipo de relación laboral de aportaciones al fondo..", "Seleccionando", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string query = string.Format("select rfc,tipo_rel,folio from datos.d_ecqdep");
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (Dictionary<string,object> item in resultado) {
                    string rfc = Convert.ToString(item["rfc"]);
                    string folio = Convert.ToString(item["folio"]);
                    query = string.Format("select tipo_rel from datos.empleados where rfc='{0}'",rfc);
                    resultado = globales.consulta(query);
                    if (resultado.Count > 0)
                    {
                        string tipo_relacion = Convert.ToString(resultado[0]["tipo_rel"]);
                        if (!string.IsNullOrWhiteSpace(tipo_relacion)) {
                            query = string.Format("update datos.d_ecqdep set tipo_rel = '{0}' where folio = '{1}'",tipo_relacion,folio);
                            globales.consulta(query,true);
                        }
                    }
                }
                MessageBox.Show("Se actualizó Tipo de Relación Laboral de los FOLIOS a descontar desde el "+fe1.Text, "Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch {
                MessageBox.Show("Error en el proceso de actualizar","Error en el proceso de actualizar",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
            Close();
        }
    }
}
