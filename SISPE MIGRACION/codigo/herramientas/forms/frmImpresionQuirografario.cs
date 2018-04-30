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
    public partial class frmImpresionQuirografario : Form
    {
        internal int checador = 0;
        public frmImpresionQuirografario()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (r1.Checked)
                checador = 1;

            if (r2.Checked)
                checador = 2;

            if (r3.Checked)
                checador = 3;

            if (r4.Checked)
                checador = 4;

            if (r5.Checked)
                checador = 5;

            Close();
        }

        private void frmImpresionQuirografario_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
