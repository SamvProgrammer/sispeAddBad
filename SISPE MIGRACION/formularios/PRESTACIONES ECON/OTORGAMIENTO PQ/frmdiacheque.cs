using SISPE_MIGRACION.codigo.baseDatos;
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
    public partial class frmdiacheque : Form
    {
        private DataGridViewRow elemento3;
        public frmdiacheque()
        {
            InitializeComponent();
        }

        private void frmprogcheque_Load(object sender, EventArgs e)
        {
            string cheques = ("SELECT fecha, inhabil, CASE WHEN to_char(fecha, 'd') = '1' then cast('Domingo' as char(10)) WHEN to_char(fecha,'d') = '2' then cast('Lunes' as char(10)) WHEN to_char(fecha,'d') = '3' then cast('Martes' as char(10)) WHEN to_char(fecha,'d') = '4'then cast('Miercoles' as char(10)) WHEN to_char(fecha,'d') = '5'then cast('Jueves' as char(10)) WHEN to_char(fecha,'d') = '6'then cast('Viernes' as char(10)) WHEN to_char(fecha,'d') = '7' then cast('Sabado' as char(10))END AS dia, programados FROM catalogos.progpq order by fecha desc limit 1000");
            var elemento3 = baseDatos.consulta(cheques);
            foreach (var item in elemento3)
            {
                string fecha = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.", ""); ; ;
                string inhabil = Convert.ToString(item["inhabil"]);
                string dia = Convert.ToString(item["dia"]);
                string programados = Convert.ToString(item["programados"]);

                gridcheques.Rows.Add(fecha, inhabil, dia, programados);



            }

                

           

        }

        private void frmdiacheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void frmdiacheque_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Desea cerrar el módulo?",
       "Cerrar el módulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogo == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void gridcheques_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var aux = gridcheques.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string query = "update catalogos.progpq set ";
                switch (e.ColumnIndex)
                {
                    case 1:
                        query += " inhabil = '{0}'";
                        break;
                    case 3:
                        query += " programados = '{0}'";
                        break;
                   
                }
                query += " where fecha = '{1}'";
                string dia = string.Format(query, aux, gridcheques.Rows[e.RowIndex].Cells[0].Value);
                if (baseDatos.consulta(dia, true))
                {
                    MessageBox.Show("Registro modificado");
                }
                else
                {
                    MessageBox.Show("Error en la actualización");
                }
            }
            catch
            {

            }
        }
    }
}
