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
        private int secuencia = 0;
        public frmAltasCambios()
        {
            InitializeComponent();
            txtTipo_mov1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtSec1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtF_descuento1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtProyecto1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtNombre_em1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtTipo_rel1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtNumdesc1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtTotdesc1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtImp_unit1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtFolio_.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtNumdesc_.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtTotdesc_.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtImp_unit_.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);

        }

        private void rellenarDatosGenerales(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 3)
            {
                if (string.IsNullOrWhiteSpace(txtImp_unit1.Text)) {
                    txtProyecto1.Text = txtProyecto.Text;
                    txtRfc1.Text = txtRfc.Text;
                    txtNombre_em1.Text = txtNombre_em.Text;
                    txtNumdesc1.Text = "1";
                    txtTotdesc1.Text = txtPlazo.Text;
                    txtImp_unit1.Text = txtImp_unit.Text;
                }
                else
                {
                    MessageBox.Show("No es posible copiar valores en este registro", "No se puede copiar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            
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
                txtF_primdesc.Text = Convert.ToString(resultado["f_primdesc"]).Replace(" 12:00:00 a. m.", ""); ;
                txtPlazo.Text = Convert.ToString(resultado["plazo"]);
                txtImp_unit.Text = Convert.ToString(resultado["imp_unit"]);
                txtImporte.Text = Convert.ToString(resultado["importe"]);
                txtDireccion.Text = Convert.ToString(resultado["direccion"]);
                txtF_solicitud.Text = Convert.ToString(resultado["f_solicitud"]).Replace(" 12:00:00 a. m.", ""); ;
                txtF_emischeq.Text = Convert.ToString(resultado["f_emischeq"]).Replace(" 12:00:00 a. m.", ""); ;

                string query = string.Format("select * from datos.d_ecqdep where folio = {0} order by sec desc limit 1", txtFolio.Text);
                List<Dictionary<string, object>> result = globales.consulta(query);

                Dictionary<string, object> diccionario = result[0];

                txtTipo_mov1.Text = Convert.ToString(diccionario["tipo_mov"]);
                txtSec1.Text = Convert.ToString(diccionario["sec"]);
                txtF_descuento1.Text = Convert.ToString(diccionario["f_descuento"]).Replace(" 12:00:00 a. m.","");
                txtProyecto1.Text = Convert.ToString(diccionario["proyecto"]);
                txtRfc1.Text = Convert.ToString(diccionario["rfc"]);
                txtNombre_em1.Text = Convert.ToString(diccionario["nombre_em"]);
                txtTipo_rel1.Text = Convert.ToString(diccionario["tipo_rel"]);

                txtNumdesc1.Text = Convert.ToString(diccionario["numdesc"]);
                txtTotdesc1.Text = Convert.ToString(diccionario["totdesc"]);
                txtImp_unit1.Text = Convert.ToString(diccionario["imp_unit"]);
                txtFolio_.Text = Convert.ToString(diccionario["folio_"]);
                txtNumdesc_.Text = Convert.ToString(diccionario["numdesc_"]);
                txtTotdesc_.Text = Convert.ToString(diccionario["totdesc_"]);
                txtImp_unit_.Text = Convert.ToString(diccionario["imp_unit_"]);


            }
            catch(Exception e) {
                MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            limpiarSolicitudesDependencias();
            limpiaredoCuenta();
            if (e.KeyChar == 8) {
                return;
            }
            frmFolios.ShowDialog();
        }

        private void limpiaredoCuenta()
        {
            txtFolio.Text = "";
            txtSecretaria.Text = "";
            txtRfc.Text = "";
            txtNombre_em.Text = "";
            txtProyecto.Text = "";
            txtTipo_pago.Text = "";
            txtF_primdesc.Text = "";
            txtPlazo.Text = "";
            txtImp_unit.Text = "";
            txtImporte.Text = "";
            txtDireccion.Text = "";
            txtF_solicitud.Text = "";
            txtF_emischeq.Text = "";
            txtUbic_pagare.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Parte de validaciones... favor de ingresarlas más tarde...
                string ubicacionPagare = txtUbic_pagare.Text;
                string query = string.Format("update datos.p_edocta set ubic_pagare = '{0}' where folio = {1}",ubicacionPagare,txtFolio.Text);
                if (globales.consulta(query, true))
                {
                    string[] arreglo = txtF_descuento1.Text.Split('/');
                    string fechaDescuento = string.Format("{0}-{1}-{2}", arreglo[2], arreglo[1], arreglo[0]);
                    query = string.Format("insert into datos.d_ecqdep(folio,tipo_mov,sec,tipo_rel,f_descuento,numdesc,totdesc,imp_unit,rfc,nombre_em,proyecto) values({0},'{1}','{2}','{3}','{4}',{5},{6},{7},'{8}','{9}','{10}')",
                                        txtFolio.Text, txtTipo_mov1.Text, txtSec1.Text, txtTipo_rel1.Text, fechaDescuento, txtNumdesc1.Text, txtTotdesc1.Text, txtImp_unit1.Text, txtRfc1.Text, txtNombre_em1.Text, txtProyecto1.Text);
                    globales.consulta(query, true);
                    MessageBox.Show("Registro guardado exitosamente!!","Registro guardado",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    limpiaredoCuenta();
                    limpiarSolicitudesDependencias();
                }
                else {
                    MessageBox.Show("Error al modificar altas y cambios, favor de contactar a los de sistemas..","Error de registro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show("Error, contactar a los de sistemas","Error en sistemas",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtTipo_mov1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8) {
                string query = string.Format("select count(folio) as cantidad from datos.d_ecqdep where folio = {0}",txtFolio.Text);
                var aux = globales.consulta(query);
                secuencia =(int) aux[0]["cantidad"];
                secuencia++;
                limpiarSolicitudesDependencias();
                txtSec1.Text = Convert.ToString(secuencia);
            }
        }

        private void limpiarSolicitudesDependencias() {
            txtTipo_mov1.Text = "";
            txtSec1.Text = "";
            txtF_descuento1.Text = "";
            txtProyecto1.Text = "";
            txtRfc1.Text = "";
            txtNombre_em1.Text = "";
            txtTipo_rel1.Text = "";
            txtNumdesc1.Text = "";
            txtTotdesc1.Text = "";
            txtImp_unit1.Text = "";

            txtFolio_.Text = "";
            txtNumdesc_.Text = "";
            txtTotdesc_.Text = "";
            txtImp_unit_.Text = "";
        }

        private void panel6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void frmAltasCambios_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
