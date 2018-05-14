using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.CAJA
{
    public partial class frmOpcionesPagoCaja : Form
    {
        public frmOpcionesPagoCaja()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rdEdicion.Checked)
            {
                if (rdQuiro.Checked) {
                    new p_caja().ShowDialog();
                }
            }
            else {
                if (rdQuiro.Checked) {

                }
            }
        }
    }
}
