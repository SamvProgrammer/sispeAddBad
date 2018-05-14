using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA;
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

        public void rellenarConsulta(Dictionary<string, object> datos)
        {

            datosgb.Rows.Clear();

            Cursor = Cursors.WaitCursor;



            this.txtrfc.Text = Convert.ToString(datos["rfc"]);
            this.txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            this.txtproyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtfolio.Text = Convert.ToString(datos["folio"]);
            this.txtdirec.Text = Convert.ToString(datos["direccion"]);
            this.txtcheque.Text = Convert.ToString(datos["f_emischeq"]).Replace("12:00:00 a. m.", "");
            this.txtpago.Text = Convert.ToString(datos["tipo_pago"]);
            this.txtimporte.Text = Convert.ToString(datos["importe"]);
            this.txtubicacion.Text = Convert.ToString(datos["ubic_pagare"]);
            this.txttotal.Text = Convert.ToString(datos["importe"]);
            this.txtsecretaria.Text = Convert.ToString(datos["secretaria"]);
            this.txtpagocuenta.Text = Convert.ToString(datos["f_primdesc"]).Replace("12:00:00 a. m.", "");
            this.txtfechasolicitud.Text = Convert.ToString(datos["f_solicitud"]).Replace("12:00:00 a. m.", "");


            //el código para llenar el dagrid...
            string aux = Convert.ToString(datos["folio"]);
            string query = string.Format("select f_descuento,numdesc,totdesc,importe,rfc,cuenta,proyecto,tipo_rel from datos.descuentos where  folio = {0} order by numdesc", aux);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            resultado = baseDatos.consulta(query);

            foreach (Dictionary<string, object> item in resultado)
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

        public void adicional()
        {



        }

        private void frmconsulta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {

                adicionalconsulta forma = new adicionalconsulta();
                forma.Show();

                string folio = txtfolio.Text;
                string prestamo = string.Format("select sum(importe) as total from datos.descuentos where folio ={0}", folio);
                List<Dictionary<string, object>> resultado = globales.consulta(prestamo);
                string total = Convert.ToString(resultado[0]["total"]);
                forma.txtacumulado.Text = total;

                string totalpres = string.Format("select importe as topresta from datos.p_quirog where folio= {0}", folio);
                List<Dictionary<string, object>> resultado1 = globales.consulta(totalpres);
                string topresta = Convert.ToString(resultado1[0]["topresta"]);
                forma.txttotal.Text = topresta;

                int operacion = (Convert.ToInt32(topresta) - Convert.ToInt32(total));

                forma.txtsaldo.Text = Convert.ToString(operacion);
            }
        }
    }
}
