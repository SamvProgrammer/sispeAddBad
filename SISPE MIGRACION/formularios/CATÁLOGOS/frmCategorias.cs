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
    public partial class frmCategorias : Form
    {
        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            string query = "select* from catalogos.categorias";
            var elemento = baseDatos.consulta(query);

            foreach (var item in elemento)
            {
                string ccatcve = item["ccatcve"];
                string ccatdes = item["ccatdes"];
                decimal ccatsue = item["ccatsue"];
                    datos02.Rows.Add(ccatcve,ccatdes,ccatsue);
            }

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCategorias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==Keys.F2)
            {
                Close();
            }
        }

        private void frmCategorias_FormClosing(object sender, FormClosingEventArgs e)
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
