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
    public partial class validarSituacionLaboral : Form
    {
        public validarSituacionLaboral()
        {
            InitializeComponent();
        }

        private void validarSituacionLaboral_Load(object sender, EventArgs e)
        {
            DateTime fecha1 = DateTime.Now;
            fecha1 = new DateTime(fecha1.Year, fecha1.Month, 15);
            fecha1 = fecha1.AddDays(15);
            fecha1 = new DateTime(fecha1.Year,fecha1.Month,15);

            DateTime fecha2 = fecha1;
            fecha2 = fecha2.AddDays(-30);
            if (fecha2.Month == 2)
            {
                fecha2 = new DateTime(fecha2.Year, fecha2.Month,28);
            }
            else {
                fecha2 = new DateTime(fecha2.Year, fecha2.Month, 30);
            }

            fe1.Value = fecha1;
            fe2.Value = fecha2;
        }
    }
}
