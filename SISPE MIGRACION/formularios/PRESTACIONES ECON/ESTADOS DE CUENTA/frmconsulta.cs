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
        private frmCatalogoP_quirog frmCatalogoP_quirog;
        private bool guardar = false;
        private List<Dictionary<string, object>> resultado;
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
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar2 = rellenarConsulta;
            p_quirog.tablaConsultar = "p_edocta";
            p_quirog.ShowDialog();
            this.ActiveControl = txtfolio;
          //  guardar = false;
        }

        private void rellenarConsulta(Dictionary<string, object> datos)
        {


            Cursor = Cursors.WaitCursor;



            this.txtrfc.Text = Convert.ToString(datos["rfc"]);
            this.txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            this.txtproyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtfolio.Text = Convert.ToString(datos["folio"]);
            this.txtdirec.Text = Convert.ToString(datos["direccion"]);
            this.txtcheque.Text = Convert.ToString(datos["f_emischeq"]).Replace("12:00:00 a. m.", ""); ;
            this.txtpago.Text = Convert.ToString(datos["tipo_pago"]);
            this.txtimporte.Text = Convert.ToString(datos["importe"]);
            this.txtubicacion.Text = Convert.ToString(datos["ubic_pagar"]);
            this.txttotal.Text = Convert.ToString(datos["importe"]);
            this.txtsecretaria.Text = Convert.ToString(datos["secretaria"]);
            this.txtpagocuenta.Text = Convert.ToString(datos["f_primdesc"]).Replace("12:00:00 a. m.", "");
            this.txtfechasolicitud.Text = Convert.ToString(datos["f_solicitud"]).Replace("12:00:00 a. m.", "");


            //el código para llenar el dagrid...
            string aux = Convert.ToString(datos["folio"]);
            string query = string.Format("select f_descuento,numdesc,totdesc,importe,rfc,cuenta,proyecto,tipo_rel from datos.descuentos where  folio = {0} order by numdesc", aux);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            resultado = baseDatos.consulta(query);

            foreach (Dictionary<string, object> item in resultado )
            {
                string f_descuento = Convert.ToString(item["f_descuento"]).Replace("12:00:00 a. m.", "");
                string numdesc = Convert.ToString(item["numdesc"]);
                string totdesc = Convert.ToString(item["totdesc"]);
                string importe = Convert.ToString(item["importe"]);
                string rfc = Convert.ToString(item["rfc"]);
                string cuenta = Convert.ToString(item["cuenta"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string tipo_rel = Convert.ToString(item["tipo_rel"]);

                datosgb.Rows.Add(f_descuento, numdesc, totdesc, importe, rfc, cuenta, proyecto, tipo_rel);

            }


            Cursor = Cursors.Default;
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Seguro que desea cancelar la operación?", "Cancelar operación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogo == DialogResult.No) return;
         
            Close();
        }

        private void frmconsulta_Load(object sender, EventArgs e)
        {
         

        }
    }
}
