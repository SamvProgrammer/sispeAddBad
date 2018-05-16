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
    public partial class p_caja : Form
    {
        public p_caja()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
        }

        private void p_caja_Load(object sender, EventArgs e)
        {
            deshabilitar();
        }

        private void deshabilitar(){
            deshabilitarElemento(txtFolio);
            deshabilitarElemento(txtF_descuento);
            deshabilitarElemento(txtTotal);

            deshabilitarElemento(txtRfc);
            deshabilitarElemento(txtNombre_em);
            deshabilitarElemento(txtSecretaria);
            deshabilitarElemento(txtdescripcion);

            deshabilitarElemento(txtDescuentos);
            deshabilitarElemento(txtImp_unit);
            deshabilitarElemento(txtDelDescuento);
            deshabilitarElemento(txtNumDesc);
            deshabilitarElemento(txtPlazo);
            deshabilitarElemento(txtImp_unitCap);
            deshabilitarElemento(txtImp_unitIntereses);
        }

        private void deshabilitarElemento(Control x) {
            x.Enabled = false;
        }
    }
}
