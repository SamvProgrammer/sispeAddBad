using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.UTILERIAS
{
    public partial class frmGenerarMesCheque : Form
    {
        private string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        public frmGenerarMesCheque()
        {
            InitializeComponent();
        }

        private void frmGenerarMesCheque_Load(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Now;
            cmbAño.Items.Add(hoy.Year);
            cmbAño.Items.Add(hoy.Year+1);

            cmbAño.SelectedIndex = 0;

            for (int x = 1; x < meses.Length; x++) {
                cmbMes.Items.Add(meses[x]);
            }
            cmbMes.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Deseas generar el mes siguiente?","Meses",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.No) {
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCantidad.Text)) {
                MessageBox.Show("Se debe ingresar la cantidad de emisión de cheques", "Cantidad de cheques",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                txtCantidad.Focus();
                return;
            }

            //Consulta para verificar fechas ingresadas previamente

            string tmpQuery = string.Format("select count(fecha) as cantidad from catalogos.progpq where to_char(fecha,'MM') like '%{0}%' and to_char(fecha,'yyyy') = '{1}'",cmbMes.SelectedIndex+1,cmbAño.Text);
            List<Dictionary<string, object>> tmpResultado = globales.consulta(tmpQuery);
            int cantidad = Convert.ToInt32(tmpResultado[0]["cantidad"]);

            if (cantidad > 0) {
                MessageBox.Show("Ya se encuentra el mes generado","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            DateTime fecha = new DateTime(Convert.ToInt32(cmbAño.Text),cmbMes.SelectedIndex+1,1);
            int contadorDia = 1;
            while (true) {
                cantidad = (string.IsNullOrWhiteSpace(txtCantidad.Text))?0:Convert.ToInt32(txtCantidad.Text);
                string fechaInsertar = string.Format("{0}-{1}-{2}",cmbAño.Text,cmbMes.SelectedIndex+1,contadorDia++);
                DayOfWeek nombreDia = fecha.DayOfWeek;
                string nombre = nombreDia.ToString();
                string enable = "";
                if (nombre == "Saturday" || nombre == "Sunday")
                    enable = "*";
                string query = string.Format("insert into catalogos.progpq(fecha,inhabil,programados,utilizados) values('{0}','{1}',{2},0)",fechaInsertar,enable,0);
                if (cmbMes.SelectedIndex != 11)
                {
                    if (cmbMes.SelectedIndex + 1 != fecha.Month)
                    {
                        break; //Termina la inserción del mes...
                    }
                }
                else {
                    if (fecha.Month != 12) {
                        break;//Rompe el ciclo...
                    }
                }
                fecha = fecha.AddDays(1);
                globales.consulta(query,true);
            }
            MessageBox.Show("Proceso terminado...", "Proceso terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Cursor = Cursors.Default;
            this.Close();
        }
    }
}
