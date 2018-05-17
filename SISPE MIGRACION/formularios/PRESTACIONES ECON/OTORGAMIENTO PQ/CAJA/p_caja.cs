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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.CAJA
{
    public partial class p_caja : Form
    {
        private bool esInsertar = false;
        private double numdesc = 0;
        private double descuentos = 0;
        private double delDescuento = 0;
        private double imp_unit_cap = 0;
        private double imp_unit = 0;
        private string imp_unit_capl = string.Empty;
        private string fum = string.Empty;
        private string hum = string.Empty;
        private string folioImprimir = string.Empty;
        private string fechaImprimir = string.Empty;
        private string imp_unit_intl = string.Empty;
        private double imp_unit_int = 0;

        private double total = 0;

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
            txtTotal.Enabled = false;
            txtdescripcion.Enabled = false;
            txtNombre_em.Enabled = false;
            txtRfc.Enabled = false;

            txtF_descuento.Value = DateTime.Now;
            btnGuardar.Enabled = false;
        }

        private void deshabilitar(bool habilitar = false){
            deshabilitarElemento(txtFolio, habilitar);
            deshabilitarElemento(txtF_descuento, habilitar);

            
            deshabilitarElemento(txtSecretaria, habilitar);

            deshabilitarElemento(txtDescuentos, habilitar);
            deshabilitarElemento(txtImp_unit, habilitar);
            deshabilitarElemento(txtDelDescuento, habilitar);
            deshabilitarElemento(txtPlazo, habilitar);
            deshabilitarElemento(txtImp_unitCap, habilitar);
            deshabilitarElemento(txtImp_unitIntereses, habilitar);
        }

        private void deshabilitarElemento(Control x,bool aux) {
            x.Enabled = aux;
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            try
            {
                llamarForma();
                this.ActiveControl = txtF_descuento;
                btnGuardar.Enabled = true;
                btnModifica.Enabled = false;
                btnNuevo.Enabled = false;
                this.esInsertar = true;
            }
            catch {

            }
        }

        public void rellenarcampos(Dictionary<string, object> datos) {
            limpiarCampos(true);

            string rfc = Convert.ToString(datos["rfc"]);
            string secretaria = Convert.ToString(datos["secretaria"]);
            string descripcion = Convert.ToString(datos["descripcion"]);
            string nombre = Convert.ToString(datos["nombre_em"]);
            string folio = Convert.ToString(datos["folio"]);
            string plazo = Convert.ToString(datos["plazo"]);
            string imp_unit = Convert.ToString(datos["imp_unit"]);

            txtFolio.Text = folio;
            txtRfc.Text = rfc;
            txtSecretaria.Text = secretaria;
            txtdescripcion.Text = descripcion;
            txtNombre_em.Text = nombre;
            txtPlazo.Text = plazo;
            txtImp_unit.Text = imp_unit;
        }

        private void limpiarCampos(bool limpiar)
        {
            deshabilitar(limpiar);
            txtFolio.Text = "";
            txtF_descuento.Text = "";
            txtTotal.Text = "";
            txtRfc.Text = "";
            txtNombre_em.Text = "";
            txtSecretaria.Text = "";
            txtdescripcion.Text = "";

            txtDescuentos.Text = "";
            txtImp_unit.Text = "";
            txtDelDescuento.Text = "";
            txtNumDesc.Text = "";
            txtPlazo.Text = "";

            txtImp_unitCap.Text = "";
            txtLetra1.Text = "";
            txtLetra2.Text = "";

            txtImp_unitIntereses.Text = "";
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar == 8) {
                limpiarCampos(false);
                txtFolio.Enabled = true;
                btnGuardar.Enabled = false;
                btnModifica.Enabled = true;
                this.ActiveControl = txtFolio;
                return;
            }
            llamarForma();
            btnGuardar.Enabled = true;
            btnModifica.Enabled = false;
            this.ActiveControl = txtF_descuento;
        }

        private void llamarForma()
        {
            frmCatalogoP_quirog cuenta = new frmCatalogoP_quirog();
            cuenta.enviar2 = rellenarcampos;
            cuenta.enviarBool = true;
            cuenta.tablaConsultar = "p_edocta";
            cuenta.ShowDialog();
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            try
            {
                btnGuardar.Enabled = true;
                this.esInsertar = false;

                this.ActiveControl = txtF_descuento;
                btnGuardar.Enabled = true;
                btnModifica.Enabled = false;
                btnNuevo.Enabled = false;
            }
            catch {

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.esInsertar) {
                guardar();
            }
        }


//        ;*** PAGOS POR CAJA***
//;PROCESO QUE PERMITE EN LA EDICION DE RECIBOS QUIROGRAFARIOS
//;O HIPOTECARIOS CALCULAR EL TOTAL Y EL IMPORTE EN LETRAS
//; ADEMAS DEL PAGO FINAL DEL RECIBO.
        private void letr_recib(string nombre) {
            switch (nombre) {
                case "txtPlazo":
                    //this.numdesc = Convert.ToDouble(txtNumDesc.Text);
                    this.delDescuento = Convert.ToDouble(txtDelDescuento.Text);
                    this.descuentos = Convert.ToDouble(txtDescuentos.Text);
                    this.imp_unit = Convert.ToDouble(txtImp_unit.Text);
                    if (this.numdesc != (this.delDescuento + (this.descuentos - 1))) {
                        this.numdesc = this.delDescuento + (this.descuentos - 1);
                        this.imp_unit_cap = this.imp_unit * this.descuentos;
                        this.imp_unit_capl = globales.convertirNumerosLetras(this.imp_unit_cap.ToString(), true);
                        this.fum = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        this.hum = string.Format("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);

                        txtImp_unitCap.Text = this.imp_unit_cap.ToString();
                        txtNumDesc.Text = this.numdesc.ToString();
                        txtLetra1.Text = this.imp_unit_capl;
                        txtImp_unitIntereses.Text = "0.00";
                    }
                    break;
                case "txtImp_unitIntereses":
                    this.imp_unit_int = (string.IsNullOrWhiteSpace(txtImp_unitCap.Text)?0: Convert.ToDouble(txtImp_unitIntereses.Text));
                    //this.total = Convert.ToDouble(txtTotal.Text);
                    this.imp_unit_intl = globales.convertirNumerosLetras(this.imp_unit_int.ToString(),true);
                    this.imp_unit_capl = globales.convertirNumerosLetras(txtImp_unitCap.Text,true);
                    this.total = this.imp_unit_cap + this.imp_unit_int;


                    txtTotal.Text = total.ToString();
                    break;
            }
        }


        private void guardar() {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescuentos.Text)) {
                    MessageBox.Show("Se debe ingresar la cantidad de pagos","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    txtDescuentos.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtImp_unit.Text)) {
                    MessageBox.Show("Se debe ingresar el importe unitario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtImp_unit.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDelDescuento.Text)) {
                    MessageBox.Show("Se debe ingresar el primer pago", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDelDescuento.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumDesc.Text)) {
                    MessageBox.Show("Se debe ingresar el número de descuento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNumDesc.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPlazo.Text)) {
                    MessageBox.Show("Se debe ingresar los plazos del prestamo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPlazo.Focus();
                    return;

                }

                if (string.IsNullOrWhiteSpace(txtImp_unitCap.Text)) {
                    MessageBox.Show("Se debe ingresar el pago capital", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtImp_unitCap.Focus();
                    return;
                }

           
            }
            catch {

            }
        }

        private void eventoEntrar(object sender, EventArgs e)
        {
            string nombre = ((TextBox)sender).Name;
            letr_recib(nombre);
        }

        private void txtImp_unitCap_Leave(object sender, EventArgs e)
        {
            letr_recib("txtImp_unitIntereses");
        }
    }
}
