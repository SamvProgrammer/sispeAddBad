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

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    public partial class frmMovimientos : Form
    {
        public frmMovimientos()
        {
            InitializeComponent();
        }
      

        private void datos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmMovimientos_Load_1(object sender, EventArgs e)
        {
            string query = "select tipo_mov,tipo,movimiento from catalogos.movimientos";
            var elemento = baseDatos.consulta(query);

            foreach (var item in elemento)
            {
                string tipo_mov = item["tipo_mov"];
                string tipo = item["tipo"];
                string movimiento = item["movimiento"];

                datos.Rows.Add(tipo_mov, tipo, movimiento);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmMovimientos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void frmMovimientos_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
