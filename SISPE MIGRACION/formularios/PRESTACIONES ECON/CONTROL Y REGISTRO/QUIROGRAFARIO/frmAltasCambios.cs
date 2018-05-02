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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO
{
    delegate void rellenar(Dictionary<string,object> resultado);
    public partial class frmAltasCambios : Form
    {
        private frmCatalogoP_quirog frmFolios;
        public frmAltasCambios()
        {
            InitializeComponent();
        }

        private void frmAltasCambios_Load(object sender, EventArgs e)
        {
            frmFolios = new frmCatalogoP_quirog();
            frmFolios.tablaConsultar = "p_edocta";
            frmFolios.enviar2 = rellenarCamposFolio;
        }

        public void rellenarCamposFolio(Dictionary<string,object>resultado) {
            try
            {
                txtFolio.Text = Convert.ToString(resultado["folio"]);
                txtSecretaria.Text = Convert.ToString(resultado["secretaria"]);
                txtRfc.Text = Convert.ToString (resultado["rfc"]);
                txtNombre_em.Text = Convert.ToString(resultado["nombre_em"]);
                txtTipo_pago.Text = Convert.ToString(resultado["tipo_pago"]);
                txtProyecto.Text = Convert.ToString(resultado["proyecto"]);
                txtF_primdesc.Text = Convert.ToString(resultado["f_primdesc"]);
                txtPlazo.Text = Convert.ToString(resultado["plazo"]);
                txtImp_unit.Text = Convert.ToString(resultado["imp_unit"]);
                txtImporte.Text = Convert.ToString(resultado["importe"]);
                txtDireccion.Text = Convert.ToString(resultado["direccion"]);
                txtF_solicitud.Text = Convert.ToString(resultado["f_solicitud"]);
                txtF_emischeq.Text = Convert.ToString(resultado["f_emischeq"]);

            }
            catch(Exception e) {
                MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar == 8) {
                //Sección de eliominark
            }
            frmFolios.ShowDialog();
        }
    }
}
