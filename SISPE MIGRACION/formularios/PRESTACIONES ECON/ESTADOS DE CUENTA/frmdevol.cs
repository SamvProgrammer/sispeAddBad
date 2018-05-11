using SISPE_MIGRACION.formularios.CATÁLOGOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA
{
    public partial class frmdevol : Form
    {
        public frmdevol()
        {
            InitializeComponent();
        }

        private void frmdevol_Load(object sender, EventArgs e)
        {
            
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
        //    p_quirog.enviar2 = rellenarConsulta;
            p_quirog.tablaConsultar = "p_edocta";
            p_quirog.ShowDialog();
           
        }
    }
}
