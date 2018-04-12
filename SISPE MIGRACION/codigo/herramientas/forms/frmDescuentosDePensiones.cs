using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    public partial class frmDescuentosDePensiones : Form
    {
        public bool esAceptar = true;
        internal cambiarDatos cambiar;
        public frmDescuentosDePensiones()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            esAceptar = false;
            Close();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            esAceptar = true;
            Close();
        }

        private void frmDescuentosDePensiones_Load(object sender, EventArgs e)
        {
            
        }

        private void PER_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void PER_TextChanged(object sender, EventArgs e)
        {
            cambiar(((TextBox)PER).Text);
        }
    }
}
