using SISPE_MIGRACION.codigo.baseDatos;
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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.Edo_cuenta
{
    public partial class frmconsulta : Form
    {
        private frmEmpleados frmEmpleados;
        public frmconsulta()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEmpleados = new frmEmpleados();
            frmEmpleados.enviar = rellenarCamposdeRFC;
            frmEmpleados.ShowDialog();
        }

        public void rellenarCamposdeRFC(Dictionary<string, object> datos, bool externo = false)
        {

            string rfc = Convert.ToString(datos["rfc"]);

            //Verifica que el susuario que se ingreso con su RFC no se encuentre en la tabla de P_QUIROG.....
            //Si este se encuentra verifica que no se haya realizado algún movimiento en los último 120 días...

            MessageBox.Show("Se ha seleccionado un RFC para consulta", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor = Cursors.WaitCursor;
            string query = string.Format("select *, (select CAST(now() AS DATE) ) as limite from datos.P_QUIROG " +
                                         "where F_solicitud >= (select CAST(now() AS DATE) - CAST('120 days' AS INTERVAL)) " +
                                         "and RFC like '%{0}%'", rfc);
            List<Dictionary<string, object>> resultado = baseDatos.consulta(query);
            Cursor = Cursors.Default;
            if (resultado.Count > 0)
          
            this.txtrfc.Text = rfc;
            this.txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            this.txtproyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtfolio.Text = Convert.ToString(datos["folio"]);
            this.txtdirec.Text = Convert.ToString(datos["direccion"]);
            this.txtcheque.Text = Convert.ToString(datos["f_emischeq"]);
            this.txtpagocuenta.Text = Convert.ToString(datos["tipo_pago"]);
            this.txtimporte.Text = Convert.ToString(datos["importe"]);
            this.txtubicacion.Text = Convert.ToString(datos["nue"]);
            this.txttotal.Text = Convert.ToString(datos["liquido"]);
            this.txtsecretaria.Text = Convert.ToString(datos["secretaria"]);
            this.txtpagocuenta.Text = Convert.ToString(datos["f_primdesc"]);
            this.txtfechasolicitud.Text = Convert.ToString(datos["f_solicitud"]);

        }

    }
}
