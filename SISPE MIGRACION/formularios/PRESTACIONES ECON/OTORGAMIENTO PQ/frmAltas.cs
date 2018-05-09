using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.codigo.baseDatos.repositorios;
using SISPE_MIGRACION.codigo.herramientas.forms;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ
{
    delegate void enviarDatos(Dictionary<string, object> datos, bool externo = false);
    delegate void enviarDatos2(Dictionary<string, object> quirografario, List<Dictionary<string, object>> avales, bool externo = false);
    delegate void cambiarDatos(string texto);
    public partial class frmAltas : Form
    {
        private Dictionary<string, string> modalidades;
        private frmEmpleados frmEmpleados;
        private frmdependencias frmdependencias;
        private p_quirog quiro;
        private double Ant_A = 0;
        private double Ant_M = 0;
        private double Ant_Q = 0;
        private double meses_corres = 0;
        private int plazo = 0;
        private string tipo_pago = string.Empty;
        private Dictionary<string, object> auxiliar;
        private string fechaSolicitud = string.Empty;
        private double t_interes = 0;
        private string aceptado = string.Empty;
        private double Secuen = 0.00;
        private string carta = string.Empty;
        private string v_fecha = string.Empty;
        private string b_fecha = string.Empty;
        private bool guardar = false;
        private string cve_categ = string.Empty;
        private bool boolDeducciones = false;
        private string txtFecha = string.Empty;

        //Parte de las deducciones como variables globales

        private double PER;
        private double DED;
        private string D;
        private double DED1;
        private double PER2;

        private double PER3 = 0.00;
        private double PER4 = 0.00;
        private double PER5 = 0.00;
        private double PER6 = 0.00;

        private double DED3 = 0.00;
        private double DED4 = 0.00;
        private double DED5 = 0.00;
        private double DED6 = 0.00;
        private double DED7 = 0.00;
        private double DED8 = 0.00;
        private double DED9 = 0.00;
        private double DED10 = 0.00;


        private double RES = 0.00;
        private double RES1 = 0.00;

        double TOPE = 0.00;
        double RESL = 0.00;
        double RES3 = 0.00;
        double Por = 0.00;
        double RESD = 0.00;
        double RES2 = 0.00;
        double IMP = 0.00;
        double IM = 0.00;

        private string f_primdesc = string.Empty;
        public frmAltas()
        {
            InitializeComponent();
            modalidades = new Dictionary<string, string>();
            modalidades.Add("B", "BASE");
            modalidades.Add("C", "CONFIANZA");
            modalidades.Add("J", "JUBILADOS");
            modalidades.Add("M", "MANDOS MEDIOS");
            modalidades.Add("P", "PENSIONADOS");
            modalidades.Add("T", "PENSIONISTAS");
            //=============================  Inicialziación de eventos para la tecla ENTER -> TAB ===================
            this.txtProyecto.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtrfc2.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtSecretaria.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtAntQ.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtSueldoBase.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtTelefono.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtExtencion.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtDomicilio.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtNue.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtnap2.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtmeses_corres.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtOtros_desc.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtPorc.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtplazo.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtdesc.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtFolio.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtAdscripcion.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);

            this.txtRfc1.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtAnti1.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtdomicilio1.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);

            this.txtrfc2.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtantg2.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtdomicilio2.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);

            //==================================================================================================
        }

        private void cambiarTab(object sender, PreviewKeyDownEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) {
                SendKeys.Send("{TAB}");//Cuando se presiona la tecla enter, este le manda señal a la tecla TAB para que active el evento de traspaso...
            }
        }

        private void ALTAS_Load(object sender, EventArgs e)
        {

            txtTrl.Text = modalidades.First().Key;
            lblmod.Text = modalidades.First().Value;

            frmdependencias = new frmdependencias();
            frmdependencias.enviar = rellenarCamposSecretarias;

            txtAntiguedad.Text = "A M Q";
            DateTime fecha = globales.sacarFechaHabil(45);
            txtFecha = string.Format("{0}/{1}/{2}", fecha.Day, fecha.Month, fecha.Year);
            b_fecha = txtFecha;
            txtEmisionCheque.Text = txtFecha;
            fechaSolicitud = string.Format("{0}/{1}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (btnsalir.Text.Contains("Cancel"))
            {
                DialogResult dialogo = MessageBox.Show("¿Seguro que desea cancelar la operación?", "Cancelar operación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogo == DialogResult.No) return;
                limpiarTodosCampos();

                btnsalir.Text = "Salir";
                txtFolio.Text = "AUTOGENERADO";
                btnNuevo.Enabled = true;
                btnNuevo.Visible = true;

                btnModifica.Enabled = true;
                btnModifica.Visible = true;

                btnGuardar.Enabled = false;
                btnGuardar.Visible = false;

                btnCalculo.Enabled = false;
                btnImprimir.Enabled = true;
                txtEmisionCheque.Text = txtFecha;
            }
            else
            {
                Close();
            }

        }

        private void limpiarTodosCampos()
        {
            limpiarCamposRFC();
            desactivarControlesBasicos();
            limpiarSecretariaCampos();
            limpiarLiquidoCampos();
            txtAntQ.Text = "0";
            limpiarAvales();
            txtTelefono.Text = "";
            txtExtencion.Text = "";
            txtdesc.Text = "0.00";


            Ant_Q = 0;
            Ant_M = 0;
            Ant_A = 0;
            carta = " ";
            aceptado = " ";
            Secuen = 0;
        }

        private void limpiarAvales()
        {
            txtrfc2.Text = "";
            txtproy2.Text = "";
            txtnap2.Text = "";
            txtnombre2.Text = "";
            txtdomicilio2.Text = "";
            txtnue2.Text = "";
            txtantg2.Text = "";

            txtRfc1.Text = "";
            txtProyect1.Text = "";
            txtNap1.Text = "";
            txtNombre1.Text = "";
            txtdomicilio1.Text = "";
            txtNue1.Text = "";
            txtAnti1.Text = "";

            desactivarControl(txtRfc1);
            desactivarControl(txtrfc2);
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        public void rellenarCamposdeRFC(Dictionary<string, object> datos, bool externo = false)
        {

            string rfc = Convert.ToString(datos["rfc"]);

            //Verifica que el susuario que se ingreso con su RFC no se encuentre en la tabla de P_QUIROG.....
            //Si este se encuentra verifica que no se haya realizado algún movimiento en los último 120 días...

            MessageBox.Show("Se verificara el RFC en FOLIOs anteriores....", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor = Cursors.WaitCursor;
            string query = string.Format("select *, (select CAST(now() AS DATE) - CAST('120 days' AS INTERVAL)) as limite from datos.P_QUIROG " +
                                         "where F_solicitud >= (select CAST(now() AS DATE) - CAST('120 days' AS INTERVAL)) " +
                                         "and RFC like '%{0}%'", rfc);
            List<Dictionary<string, object>> resultado = baseDatos.consulta(query);
            Cursor = Cursors.Default;
            if (resultado.Count > 0)
            {
                string limite = Convert.ToString(resultado[0]["limite"]);
                MessageBox.Show("Este RFC ya fue utilizado en un préstamo después del " + limite, "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            this.txtRfc.Text = rfc;
            this.txtnombre_em.Text = Convert.ToString(datos["nombre_em"]);
            this.txtProyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtSueldoBase.Text = Convert.ToString(datos["sueldo_base"]);
            this.txtNap.Text = Convert.ToString(datos["nap"]);
            this.txtDomicilio.Text = Convert.ToString(datos["direccion"]);
            this.txtNue.Text = Convert.ToString(datos["nue"]);
            this.cve_categ = Convert.ToString(datos["cve_categ"]);

        }

        public void rellenarCamposSecretarias(Dictionary<string, object> datos, bool externo = false)
        {
            if (!externo)
            {

                /*
                    Se agrega líneas para pedir los importes de percepciones
                    y reducciones del trabajador.
                */
                try
                {
                    int cate = Convert.ToInt32(this.cve_categ.Substring(2, 2));
                    if (cate > 16)
                    {
                        MessageBox.Show("La categoría es mayor a 16", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch
                {
                    MessageBox.Show("El empleado no contiene categoría..Favor de verificar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                PER = Convert.ToDouble(txtSueldoBase.Text);
                DED = 0.00;
                D = "N";
                DED1 = DED;
                PER2 = PER;

                PER3 = 0.00;
                PER4 = 0.00;
                PER5 = 0.00;
                PER6 = 0.00;

                DED3 = 0.00;
                DED4 = 0.00;
                DED5 = 0.00;
                DED6 = 0.00;
                DED7 = 0.00;
                DED8 = 0.00;
                DED9 = 0.00;
                DED10 = 0.00;

                frmDescuentosDePensiones descuentos = new frmDescuentosDePensiones();
                descuentos.cambiar = cambiarTxtSueldoBase;
                descuentos.PER.Text = Convert.ToString(PER);
                descuentos.DED3.Text = Convert.ToString(DED3);
                descuentos.DED4.Text = Convert.ToString(DED4);
                descuentos.DED5.Text = Convert.ToString(DED5);
                descuentos.DED6.Text = Convert.ToString(DED6);
                descuentos.ShowDialog();

                DED3 = Convert.ToDouble(descuentos.DED3.Text);
                DED4 = Convert.ToDouble(descuentos.DED4.Text);
                DED5 = Convert.ToDouble(descuentos.DED5.Text);
                DED6 = Convert.ToDouble(descuentos.DED6.Text);

                if (descuentos.esAceptar)
                {
                    if (descuentos.ROY2.Checked)
                    {
                        DED1 = DED1 / 2;
                        DED3 = DED3 / 2;
                        DED4 = DED4 / 2;
                        DED5 = DED5 / 2;
                        DED6 = DED6 / 2;
                        DED7 = DED7 / 2;
                        DED8 = DED8 / 2;
                        DED9 = DED9 / 2;
                        DED10 = DED10 / 2;

                        PER2 = PER2 / 2;
                        PER3 = PER3 / 2;
                        PER4 = PER4 / 2;
                        PER5 = PER5 / 2;
                        PER6 = PER6 / 2;
                    }

                    DED1 = DED1 + DED3 + DED4 + DED5 + DED6 + DED7 + DED8 + DED9 + DED10;
                    PER2 = PER;
                    D = "S";
                }
                else
                {
                    if (descuentos.ROY2.Checked)
                    {
                        DED1 = DED1 + DED3 + DED4 + DED5 + DED6 + DED7 + DED8 + DED9 + DED10;
                        DED1 = DED1 / 2;
                    }

                    DED1 = DED1 + DED3 + DED4 + DED5 + DED6 + DED7 + DED8 + DED9 + DED10;
                    PER2 = PER;
                }

                //************************fin de percepciones y reducciones del trabajador******
            }

            int movimientos = Convert.ToInt32(this.txtmeses_corres.Text);
            if (movimientos != 0)
            {
                DialogResult d = MessageBox.Show("Los mov. serás recalculados", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.No)
                    return;
            }

            this.auxiliar = datos;
            string secretaria = Convert.ToString(datos["proy"]);
            string descripcionProyecto = Convert.ToString(datos["descripcion"]);
            txtSecretaria.Text = secretaria;
            txtAdscripcion.Text = descripcionProyecto;

            if (secretaria != "J" && secretaria != "P" && secretaria != "T")
            {
                txtSueldo_m.Text = (Convert.ToDouble(txtSueldoBase.Text) * 2).ToString();
                int Qtotales = Convert.ToInt32(txtAntQ.Text);
                int AA = Convert.ToInt32((Qtotales) / 24);
                int QAux = Qtotales - (AA * 24);
                int AM = Convert.ToInt32((QAux / 2));
                int AQ = QAux - (AM * 2);

                this.Ant_A = AA;
                this.Ant_M = AM;
                this.Ant_Q = AQ;

                this.tipo_pago = "Q";
                if (Convert.ToDouble(txtAntQ.Text) >= 12 && Convert.ToDouble(txtAntQ.Text) < 24)
                {
                    this.meses_corres = 3;
                    this.plazo = 24;
                }
                else if (Convert.ToDouble(txtAntQ.Text) >= 24 && Convert.ToDouble(txtAntQ.Text) < 120)
                {
                    this.meses_corres = 4;
                    this.plazo = 36;

                }
                else if (Convert.ToDouble(txtAntQ.Text) >= 120 && Convert.ToDouble(txtAntQ.Text) < 240)
                {
                    this.meses_corres = 5;
                    this.plazo = 36;
                }
                else if (Convert.ToDouble(txtAntQ.Text) >= 240)
                {
                    this.meses_corres = 6;
                    this.plazo = 48;

                }
                else
                {
                    this.meses_corres = 0;
                    this.plazo = 0;
                }

            }
            else
            {
                this.Ant_A = 0;
                this.Ant_M = 0;
                this.Ant_Q = 0;
                txtSueldo_m.Text = txtSueldoBase.Text;
                this.tipo_pago = "M";
                if (secretaria == "J")
                {
                    this.meses_corres = 6;
                    this.plazo = 24;
                }
                else
                {
                    this.meses_corres = 3;
                    this.plazo = 12;
                }
            }

            txtAntiguedad.Text = string.Format("{0}A {1}M {2}Q", Convert.ToString(this.Ant_A), Convert.ToString(this.Ant_M), Convert.ToString(this.Ant_Q));
            txtmeses_corres.Text = Convert.ToString(this.meses_corres);
            txtplazo.Text = Convert.ToString(this.plazo);
            txtTipoPago.Text = this.tipo_pago;

        }

        private void txtRfc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (txtRfc.ReadOnly)
            {
                return;
            }

            if (!globales.alfaNumerico(e.KeyChar))
                return;

            if (e.KeyChar == 8)
            {
                limpiarCamposRFC();
                return;
            }


            frmEmpleados frmEmpleados = new frmEmpleados();
            frmEmpleados.enviar = rellenarCamposdeRFC;
            frmEmpleados.ShowDialog();
            this.ActiveControl = txtSecretaria;
        }

        private void limpiarCamposRFC()
        {
            this.txtRfc.Text = "";
            this.txtnombre_em.Text = "";
            this.txtProyecto.Text = "";
            this.txtSueldoBase.Text = "0.00";
            this.txtNap.Text = "";
            this.txtDomicilio.Text = "";
            this.txtNue.Text = "";
        }



        private void txtRfc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSecretaria_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = true;
            if (txtSecretaria.ReadOnly) return;
            if (!globales.alfaNumerico(e.KeyChar))
                return;

            if (e.KeyChar == 8)
            {
                limpiarSecretariaCampos();
                return;
            }
            frmdependencias.ShowDialog();
            e.Handled = true;
            this.ActiveControl = txtAntQ;
        }

        private void limpiarSecretariaCampos()
        {
            txtSecretaria.Text = "";
            txtAdscripcion.Text = "";
            txtAntiguedad.Text = "";
            txtmeses_corres.Text = "0";
            txtplazo.Text = "";
            txtTipoPago.Text = "";
            txtSueldo_m.Text = "0.00";
            txtTrl.Text = "B";

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);

        }

        private void txtAntQ_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);

            if (e.KeyChar == '.')
                e.Handled = true;
        }

        private void txtliquido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);

        }

        private void txtliquido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSecretaria_TextChanged(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void txtAntQ_Leave(object sender, EventArgs e)
        {
            if (txtAntQ.ReadOnly) return;

            int valor = (string.IsNullOrWhiteSpace(txtAntQ.Text) ? 0 : Convert.ToInt32(txtAntQ.Text));
            if (valor < 12)
            {
                MessageBox.Show("No debe ser menor a 12 quincenas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAntQ.Text = "0";
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAntQ.Text))
            {
                txtAntQ.Text = "0";

            }

            if (!string.IsNullOrWhiteSpace(txtSecretaria.Text))
            {
                rellenarCamposSecretarias(auxiliar, true);

            }

        }

        private void txtliquido_Leave(object sender, EventArgs e)
        {

        }

        private void limpiarLiquidoCampos()
        {
            txtF_primerdesc.Text = "";
            txtliquido.Text = "0.00";
            txtFondo_g.Text = "0.00";
            txtOtros_desc.Text = "0.00";
            txtintereses.Text = "0.00";
            txtImporte.Text = "0.00";
            txtImpUnit.Text = "0.00";
            txtultpago.Text = "";
            txtF_primerdesc.Text = "";
            lblmod.Text = "Base";
            txtPorc.Text = "0.00";

        }

        private void txtRfc1_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = true;
            if (txtRfc1.ReadOnly) return;
            if (!globales.alfaNumerico(e.KeyChar)) return;

            if (e.KeyChar == 8)
            {
                txtRfc1.Text = "";
                txtProyect1.Text = "";
                txtNap1.Text = "";
                txtNombre1.Text = "";
                txtdomicilio1.Text = "";
                txtNue1.Text = "";
                txtAnti1.Text = "";
                return;
            }
            frmEmpleados = new frmEmpleados();
            frmEmpleados.enviar = rellenarAval1;
            frmEmpleados.ShowDialog();
        }

        private void txtrfc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (txtrfc2.ReadOnly) return;
            if (!globales.alfaNumerico(e.KeyChar)) return;

            if (e.KeyChar == 8)
            {
                txtrfc2.Text = "";
                txtproy2.Text = "";
                txtnap2.Text = "";
                txtnombre2.Text = "";
                txtdomicilio2.Text = "";
                txtnue2.Text = "";
                txtantg2.Text = "";
                return;
            }

            this.frmEmpleados = new frmEmpleados();
            frmEmpleados.enviar = rellenarAval2;
            this.frmEmpleados.ShowDialog();
        }

        public void rellenarAval1(Dictionary<string, object> datos, bool externo = false)
        {

            if (Convert.ToString(datos["rfc"]) == txtrfc2.Text)
            {
                MessageBox.Show("Aval repetido, porfavor ingresar otro aval", "Error aval", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            txtRfc1.Text = Convert.ToString(datos["rfc"]);
            txtProyect1.Text = Convert.ToString(datos["proyecto"]);
            txtNap1.Text = Convert.ToString(datos["nap"]);
            txtNombre1.Text = Convert.ToString(datos["nombre_em"]);
            txtdomicilio1.Text = Convert.ToString(datos["direccion"]);
            txtNue1.Text = Convert.ToString(datos["nue"]);
            txtAnti1.Text = Convert.ToString(datos["antig_q"]);
        }
        public void rellenarAval2(Dictionary<string, object> datos, bool externo = false)
        {
            if (Convert.ToString(datos["rfc"]) == txtRfc1.Text)
            {
                MessageBox.Show("Aval repetido, porfavor ingresar otro aval", "Error aval", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            txtrfc2.Text = Convert.ToString(datos["rfc"]);
            txtproy2.Text = Convert.ToString(datos["proyecto"]);
            txtnap2.Text = Convert.ToString(datos["nap"]);
            txtnombre2.Text = Convert.ToString(datos["nombre_em"]);
            txtdomicilio2.Text = Convert.ToString(datos["direccion"]);
            txtnue2.Text = Convert.ToString(datos["nue"]);
            txtantg2.Text = Convert.ToString(datos["antig_q"]);
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtRfc.Text))
            {
                MessageBox.Show("Se debe insertar un RFC para continuar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRfc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSecretaria.Text))
            {
                MessageBox.Show("Se debe insertar secretaria", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSecretaria.Focus();
                return;
            }


            if (string.IsNullOrWhiteSpace(txtRfc1.Text) && string.IsNullOrWhiteSpace(txtrfc2.Text))
            {
                DialogResult dialogo = MessageBox.Show("La operación se efectuara sin un aval\n¿Desea agregar algún aval?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogo == DialogResult.Yes)
                {
                    frmEmpleados = new frmEmpleados();
                    frmEmpleados.enviar = rellenarAval1;
                    frmEmpleados.ShowDialog();
                    return;
                }
            }

            p_quirog obj = verificarObjeto();

            if (!guardar)
            {
                if (modificar(obj))
                {
                    MessageBox.Show("Registro actualizado exitosamente!!", "Registro actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult resultado = MessageBox.Show("¿Desea imprimir la presente solicitud?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.No)
                    {
                        MessageBox.Show("Puede impirmir más adelante!!", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;
                        imprimir(obj);

                    }
                    MessageBox.Show("Proceso terminado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarTodosCampos();
                    btnNuevo.Enabled = true;
                    btnsalir.Text = "SALIR";
                    btnModifica.Enabled = true;
                    btnModifica.Visible = true;
                    btnGuardar.Enabled = false;
                    btnGuardar.Visible = false;
                    btnImprimir.Enabled = true;
                    btnCalculo.Enabled = false;
                }
                else
                    MessageBox.Show("Error al actualizar el registro, contactar al equipo de sistemas!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (guardar)
            {
                if (insertarRegistro(obj))
                {
                    MessageBox.Show("Registro guardado exitosamente!!", "Registro guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult resultado = MessageBox.Show("¿Desea imprimir la presente solicitud?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.No)
                    {
                        MessageBox.Show("Puede impirmir más adelante!!", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;
                        imprimir(obj);

                    }
                    MessageBox.Show("Proceso terminado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarTodosCampos();
                    btnNuevo.Enabled = true;
                    btnsalir.Text = "SALIR";
                    btnModifica.Enabled = true;
                    btnModifica.Visible = true;
                    btnGuardar.Enabled = false;
                    btnGuardar.Visible = false;
                    btnImprimir.Enabled = true;
                    btnCalculo.Enabled = false;
                }
                else
                    MessageBox.Show("Error al guardar el registor, contactar al equipo de sistemas!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
        }


        private bool modificar(p_quirog obj) {
            bool registro = false;

            try
            {
                obj.folio = Convert.ToInt32(txtFolio.Text);
                obj.f_emischeq = Regex.Replace(obj.f_emischeq, @"\s+", "");
                obj.f_primdesc = Regex.Replace(obj.f_primdesc, @"\s+", "");
                obj.f_ultmode = Regex.Replace(obj.f_ultmode, @"\s+", "");
                string query = "update  datos.p_quirog set rfc = '{0}',nombre_em = '{1}',proyecto = '{2}',secretaria = '{3}',antig_q = {4},sueldo_base = {5},descripcion = '{6}',telefono = '{7}',extension = '{8}',direccion='{9}',nue = '{10}',nap = {11}," +
                   "sueldo_m = {12},ant_a = {13},ant_m = {14},ant_q = {15},meses_corres = {16},otros_desc = {17},trel = '{18}',porc = {19},plazo = {20},tipo_pago = '{21}',f_emischeq = {22},f_primdesc = {23},f_ultimode = {24},imp_unit = {25},importe = {26},interes = {27},fondo_g = {28},liquido = {29},carta = '{30}',f_solicitud = '{31}',secuen = {32},aceptado = '{33}' where folio = {34} ";

                query = string.Format(query, obj.rfc, obj.nombre_em, obj.proyecto, obj.secretaria, obj.antig_q, obj.sueldo_base, obj.descripcion, obj.telefono, obj.extencion, obj.direccion, obj.nue, obj.nap,
                   obj.sueldo_m, obj.ant_a, obj.ant_m, obj.ant_q, obj.meses_corres, obj.otros_desc, obj.trel, obj.porc, obj.plazo, obj.tipo_pago, obj.f_emischeq, obj.f_primdesc, obj.f_ultmode, obj.imp_unit, obj.importe, obj.interes, obj.fondo_g, obj.liquido,
                   obj.carta, obj.f_solicitud, obj.secuen, obj.aceptado, obj.folio);


                obj.lista = new List<d_quirog>();
                registro = true;

            }
            catch {
                registro = false;
            }

            return registro;
        }

        private bool insertarRegistro(p_quirog obj)
        {
            bool registro = false;

            try
            {
                string query = "select max(folio) as maximo from datos.p_quirog";
                var resultado = globales.consulta(query);
                int maximo = Convert.ToInt32(resultado[0]["maximo"]) + 1;
                obj.folio = maximo;

                query = "insert into datos.p_quirog(folio,rfc,nombre_em,proyecto,secretaria,antig_q,sueldo_base,descripcion,telefono,extension,direccion,nue,nap," +
                    "sueldo_m,ant_a,ant_m,ant_q,meses_corres,otros_desc,trel,porc,plazo,tipo_pago,f_emischeq,f_primdesc,f_ultimode,imp_unit,importe,interes,fondo_g,liquido,carta,f_solicitud,secuen,aceptado) values({0},'{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}','{9}'," +
                    "'{10}','{11}',{12},{13},{14},{15},{16},{17},{18},'{19}',{20},{21},'{22}',{23},{24},{25},{26},{27},{28},{29},{30},'{31}','{32}',{33},'{34}')";
                query = string.Format(query, obj.folio, obj.rfc, obj.nombre_em, obj.proyecto, obj.secretaria, obj.antig_q, obj.sueldo_base, obj.descripcion, obj.telefono, obj.extencion, obj.direccion, obj.nue, obj.nap,
                    obj.sueldo_m, obj.ant_a, obj.ant_m, obj.ant_q, obj.meses_corres, obj.otros_desc, obj.trel, obj.porc, obj.plazo, obj.tipo_pago, obj.f_emischeq, obj.f_primdesc, obj.f_ultmode, obj.imp_unit, obj.importe, obj.interes, obj.fondo_g, obj.liquido,
                    obj.carta, obj.f_solicitud, obj.secuen, obj.aceptado);

                obj.lista = new List<d_quirog>();
                if (globales.consulta(query, true))
                {
                    registro = true;
                    //Parte que se agregar para las insercciones de la tabla de deducciones....
                    string status = (this.boolDeducciones) ? "S" : "N";
                    query = string.Format("insert into datos.d_perded values({0},'{1}',{2},'{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24})"
                        , obj.folio, status, 1, obj.rfc, obj.nombre_em, this.PER2, this.DED1, this.RES, this.RES1, this.IMP, this.IM, this.RES3, this.RES2, this.PER3, this.PER4, this.PER5, this.PER6, this.DED3, this.DED4, this.DED5, this.DED6, this.DED7, this.DED8, this.DED9, this.DED10);

                    globales.consulta(query, true);

                    if (!string.IsNullOrWhiteSpace(txtRfc1.Text))
                    {
                        d_quirog detalleQuirog = new d_quirog();
                        detalleQuirog.folio = obj.folio;
                        detalleQuirog.rfc = txtRfc1.Text;
                        detalleQuirog.nombre_em = txtNombre1.Text;
                        detalleQuirog.direccion = txtdomicilio1.Text;
                        detalleQuirog.proyecto = txtProyect1.Text;
                        detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtNap1.Text)) ? 0 : Convert.ToDouble(txtNap1.Text);
                        detalleQuirog.nue = txtNue1.Text;
                        detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtAnti1.Text)) ? 0 : Convert.ToInt32(txtAnti1.Text);
                        query = "insert into datos.D_QUIROG values({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'')";
                        query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig);
                        globales.consulta(query, true);
                        registro = true;
                        obj.lista.Add(detalleQuirog);
                    }

                    if (!string.IsNullOrWhiteSpace(txtrfc2.Text))
                    {
                        d_quirog detalleQuirog = new d_quirog();
                        detalleQuirog.folio = obj.folio;
                        detalleQuirog.rfc = txtrfc2.Text;
                        detalleQuirog.nombre_em = txtnombre2.Text;
                        detalleQuirog.direccion = txtdomicilio2.Text;
                        detalleQuirog.proyecto = txtproy2.Text;
                        detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtnap2.Text)) ? 0 : Convert.ToDouble(txtnap2.Text);
                        detalleQuirog.nue = txtnue2.Text;
                        detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtantg2.Text)) ? 0 : Convert.ToInt32(txtantg2.Text);
                        query = "insert into datos.D_QUIROG values({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'')";
                        query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig);
                        globales.consulta(query, true);
                        registro = true;
                        obj.lista.Add(detalleQuirog);
                    }
                }
                else
                {
                    registro = false;
                }

            }
            catch
            {
                registro = false;
            }


            return registro;
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            limpiarTodosCampos();
            activarControlesBasicos();
            txtEmisionCheque.Text = txtFecha;

            btnNuevo.Enabled = false;
            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;

            btnCalculo.Enabled = true;


            btnModifica.Enabled = false;
            btnModifica.Visible = false;
            btnImprimir.Enabled = false;

            btnsalir.Text = "Cancelar";
            txtFolio.Text = "AUTOGENERADO";

            frmEmpleados = new frmEmpleados();
            frmEmpleados.enviar = rellenarCamposdeRFC;
            frmEmpleados.ShowDialog();
            this.ActiveControl = txtProyecto;
            guardar = true;
        }

        private void activarControlesBasicos()
        {
            activarControl(txtRfc);
            activarControl(txtSecretaria);
            activarControl(txtAntQ);
            activarControl(txtTelefono);
            activarControl(txtExtencion);
            activarControl(txtRfc1);
            activarControl(txtrfc2);
            activarControl(txtdomicilio1);
            activarControl(txtdomicilio2);
            activarControl(txtantg2);
            activarControl(txtAnti1);
            activarControl(txtSueldoBase);
            activarControl(txtmeses_corres);
            activarControl(txtPorc);
            activarControl(txtplazo);
            activarControl(txtProyecto);
            activarControl(txtDomicilio);
        }
        private void desactivarControlesBasicos()
        {
            desactivarControl(txtFolio);
            desactivarControl(txtRfc);
            desactivarControl(txtSecretaria);
            desactivarControl(txtAntQ);
            desactivarControl(txtTelefono);
            desactivarControl(txtExtencion);
            desactivarControl(txtRfc1);
            desactivarControl(txtrfc2);
            desactivarControl(txtAnti1);
            desactivarControl(txtantg2);
            desactivarControl(txtdomicilio1);
            desactivarControl(txtdomicilio2);

            desactivarControl(txtSueldoBase);
            desactivarControl(txtmeses_corres);
            desactivarControl(txtPorc);
            desactivarControl(txtplazo);
            desactivarControl(txtProyecto);
        }

        public void desactivarControl(TextBox control)
        {
            control.ReadOnly = true;
            control.Cursor = Cursors.No;
        }
        public void activarControl(TextBox control)
        {
            control.ReadOnly = false;
            control.Cursor = Cursors.IBeam;
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            activarControlesBasicos();


            btnNuevo.Enabled = false;
            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;


            btnModifica.Enabled = false;
            btnModifica.Visible = false;
            btnImprimir.Enabled = false;
            btnsalir.Text = "Cancelar";

            txtFolio.ReadOnly = false;
            txtFolio.Cursor = Cursors.IBeam;

            btnModifica.Enabled = true;

            btnCalculo.Enabled = true;

            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar = rellenarModificarFolios;
            p_quirog.tablaConsultar = "p_quirog";
            p_quirog.ShowDialog();
            this.ActiveControl = txtFolio;
            guardar = false;
        }
        private void rellenarModificarFolios(Dictionary<string, object> quirografario, List<Dictionary<string, object>> avales, bool externo = false)
        {
            txtFolio.Text = Convert.ToString(quirografario["folio"]);
            txtRfc.Text = Convert.ToString(quirografario["rfc"]);
            txtnombre_em.Text = Convert.ToString(quirografario["nombre_em"]);
            txtProyecto.Text = Convert.ToString(quirografario["proyecto"]);
            txtSecretaria.Text = Convert.ToString(quirografario["secretaria"]);
            txtAntQ.Text = Convert.ToString(quirografario["antig_q"]);
            txtSueldoBase.Text = Convert.ToString(quirografario["sueldo_base"]);
            txtAdscripcion.Text = Convert.ToString(quirografario["descripcion"]);
            txtTelefono.Text = Convert.ToString(quirografario["telefono"]);
            txtExtencion.Text = Convert.ToString(quirografario["extension"]);
            txtDomicilio.Text = Convert.ToString(quirografario["direccion"]);
            txtNue.Text = Convert.ToString(quirografario["nue"]);
            txtNap.Text = Convert.ToString(quirografario["nap"]);
            txtSueldo_m.Text = Convert.ToString(quirografario["sueldo_m"]);
            txtAntiguedad.Text = Convert.ToString(quirografario["ant_a"]) + " A" + Convert.ToString(quirografario["ant_m"]) + " M" + Convert.ToString(quirografario["ant_q"]) + " Q";
            txtmeses_corres.Text = Convert.ToString(quirografario["meses_corres"]);
            txtOtros_desc.Text = Convert.ToString(quirografario["otros_desc"]);
            txtPorc.Text = Convert.ToString(quirografario["porc"]);
            txtplazo.Text = Convert.ToString(quirografario["plazo"]);
            txtTipoPago.Text = Convert.ToString(quirografario["tipo_pago"]);
            txtTrl.Text = Convert.ToString(quirografario["trel"]);
            txtEmisionCheque.Text = Convert.ToString(quirografario["f_emischeq"]).Replace("12:00:00 a. m.", "");
            txtF_primerdesc.Text = Convert.ToString(quirografario["f_primdesc"]).Replace("12:00:00 a. m.", ""); ;
            txtultpago.Text = Convert.ToString(quirografario["f_ultimode"]).Replace("12:00:00 a. m.", ""); ;
            txtImpUnit.Text = Convert.ToString(quirografario["imp_unit"]);
            txtImporte.Text = Convert.ToString(quirografario["importe"]);
            txtintereses.Text = Convert.ToString(quirografario["interes"]);
            txtFondo_g.Text = Convert.ToString(quirografario["fondo_g"]);
            txtOtros_desc.Text = Convert.ToString(quirografario["otros_desc"]);
            txtliquido.Text = Convert.ToString(quirografario["liquido"]);

            if (avales.Count == 1)
            {
                txtRfc1.Text = Convert.ToString(avales[0]["rfc"]);
                txtProyect1.Text = Convert.ToString(avales[0]["proyecto"]);
                txtNap1.Text = Convert.ToString(avales[0]["nap"]);
                txtNombre1.Text = Convert.ToString(avales[0]["nombre_em"]);
                txtdomicilio1.Text = Convert.ToString(avales[0]["direccion"]);
                txtNue1.Text = Convert.ToString(avales[0]["nue"]);
                txtAnti1.Text = Convert.ToString(avales[0]["antig"]);
            }
            else if (avales.Count == 2)
            {
                txtRfc1.Text = Convert.ToString(avales[0]["rfc"]);
                txtProyect1.Text = Convert.ToString(avales[0]["proyecto"]);
                txtNap1.Text = Convert.ToString(avales[0]["nap"]);
                txtNombre1.Text = Convert.ToString(avales[0]["nombre_em"]);
                txtdomicilio1.Text = Convert.ToString(avales[0]["direccion"]);
                txtNue1.Text = Convert.ToString(avales[0]["nue"]);
                txtAnti1.Text = Convert.ToString(avales[0]["antig"]);

                txtrfc2.Text = Convert.ToString(avales[0]["rfc"]);
                txtproy2.Text = Convert.ToString(avales[0]["proyecto"]);
                txtnap2.Text = Convert.ToString(avales[0]["nap"]);
                txtnombre2.Text = Convert.ToString(avales[0]["nombre_em"]);
                txtdomicilio2.Text = Convert.ToString(avales[0]["direccion"]);
                txtnue2.Text = Convert.ToString(avales[0]["nue"]);
                txtantg2.Text = Convert.ToString(avales[0]["antig"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calculoLiquido();
        }

        private void calculoLiquido()
        {
            List<Dictionary<string, object>> resultado = (List<Dictionary<string, object>>)globales.seleccionaTasaDeInteres();
            Dictionary<string, object> objeto = resultado[0];
            txtTrl.Text = Convert.ToString(objeto["trel"]);
            lblmod.Text = modalidades[txtTrl.Text];
            t_interes = Convert.ToDouble(objeto["tasa"]);
            t_interes = (t_interes / 24) / 100;
            string mensaje = string.Format("Se aplico tasa del {0}", fechaSolicitud);
            MessageBox.Show(mensaje, "Aplicación de tasas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult dialogo;

            double auxSueldoM = Convert.ToDouble(txtSueldo_m.Text);
            txtImpUnit.Text = Convert.ToString((auxSueldoM * meses_corres) / plazo);

            TOPE = 0.00;
            RES = 0.00;
            RESL = 0.00;
            RES3 = 0.00;
            RES1 = 0.00;
            Por = 0.00;
            RESD = 0.00;
            RES2 = 0.00;
            string RO = string.Empty;
            string NOM = string.Empty;
            IMP = 0.00;
            IM = 0.00;

            if (!this.D.Equals("N"))
            {
                TOPE = PER2 / 2;
                RES = DED1 + Convert.ToDouble(txtImpUnit.Text);
                RESL = Convert.ToDouble(txtImpUnit.Text);
                RES3 = RES / PER2;
                RES3 = RES3 * 100;
                RES1 = DED1 + Convert.ToDouble(txtImpUnit.Text);
                Por = RES3;

                if (TOPE < RES)
                {
                regresar1:
                    string cadena = string.Format("Este RFC se excede con un {0}%\n¿Desea ajustar?", RES3);
                    dialogo = MessageBox.Show(cadena, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogo == DialogResult.Yes)
                    {
                        txtImpUnit.Text = Convert.ToString(TOPE - DED1);
                        RESD = Convert.ToDouble(txtImpUnit.Text);
                        RES1 = DED1 + Convert.ToDouble(txtImpUnit.Text);
                        RES2 = RES1 / PER2;
                        RES2 = RES2 * 100;
                        Por = RES2;
                    }
                    else
                    {
                        dialogo = MessageBox.Show("¿Esta seguro de continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogo == DialogResult.Yes)
                        {
                            RES2 = RES / PER2;
                            RES2 = RES2 * 100;
                            Por = RES;
                        }
                        else
                        {
                            goto regresar1;
                        }
                    }

                }
                else
                {
                    RES2 = RES / PER2;
                    RES2 = RES2 * 100;
                    Por = RES2;
                }
            }
            else
            {
                TOPE = PER2 / 2;
                RES = DED + Convert.ToDouble(txtImpUnit.Text);
                RES3 = RES / PER2;
                RES3 = RES3 * 100;
                Por = RES3;
                RES1 = DED + Convert.ToDouble(txtImpUnit.Text);
                if (TOPE < RES)
                {
                regresa2:
                    string cadena = string.Format("Este RFC se excede con un {0}%\n¿Desea ajustar?", RES3);
                    dialogo = MessageBox.Show(cadena, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogo == DialogResult.Yes)
                    {
                        txtImpUnit.Text = Convert.ToString(TOPE - DED);
                        RES1 = DED + Convert.ToDouble(txtImpUnit.Text);
                        RES2 = RES1 / PER2;
                        RES2 = RES2 * 100;
                        Por = RES2;
                    }
                    else
                    {
                        dialogo = MessageBox.Show("¿Esta seguro de continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogo == DialogResult.Yes)
                        {
                            RES2 = RES1 / PER2;
                            RES2 = RES2 * 100;
                            Por = RES2;
                        }
                        else
                        {
                            goto regresa2;
                        }
                    }
                }
                else
                {
                    RES2 = RES1 / PER2;
                    RES2 = RES2 * 100;
                    Por = RES2;
                }
            }

            //**********************termina el calculoi de txtimpUnit***********
            txtImporte.Text = Convert.ToString(Convert.ToDouble(txtImpUnit.Text) * plazo);
            //Agrega información a la base
            RO = txtRfc.Text;
            NOM = txtnombre_em.Text;
            IMP = Convert.ToDouble(txtImporte.Text);
            IM = Convert.ToDouble(txtImpUnit.Text);
            txtPorc.Text = Convert.ToString(Por);
            double aux1 = Convert.ToDouble(txtImporte.Text);
            if (txtTipoPago.Text == "Q")
                txtintereses.Text = ((aux1 * ((plazo / 2) + 1)) * t_interes).ToString("#.##");
            else
                txtintereses.Text = ((aux1 * ((plazo) + 1)) * t_interes).ToString("#.##");

            int auxAnti_q = Convert.ToInt32(txtAntQ.Text);
            if (auxAnti_q < 240 && txtSecretaria.Text != "J" && txtSecretaria.Text != "T")
            {
                txtFondo_g.Text = (Convert.ToDouble(txtImporte.Text) * 0.02).ToString("#.##");
            }
            else
            {
                txtFondo_g.Text = "0";
            }

            aceptado = "S";
            Secuen = 1;

            txtliquido.Text = Convert.ToString(Convert.ToDouble(txtImporte.Text) - Convert.ToDouble(txtintereses.Text) - Convert.ToDouble(txtFondo_g.Text) - Convert.ToDouble(txtOtros_desc.Text));
            dialogo = MessageBox.Show("¿Se acepta el credito?", "Crédito", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            this.boolDeducciones = false;
            if (dialogo == DialogResult.Yes)
            {
                this.boolDeducciones = true;
                dialogo = MessageBox.Show("¿Se modifico el plazo?", "Crédito", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                carta = (DialogResult.Yes == dialogo) ? "S" : "N";
            }

            //************ cálculo del primer descuento en relación al tipo de pago *************


            v_fecha = txtEmisionCheque.Text;
            DateTime auxF = globales.sacarFechaHabil(35, v_fecha);
            if (txtTipoPago.Text == "Q")
            {
                auxF = new DateTime(auxF.Year, auxF.Month, 15);
                if (auxF.Month == 2)
                {
                    auxF = new DateTime(auxF.Year, auxF.Month, 28);
                }
                else
                {
                    auxF = new DateTime(auxF.Year, auxF.Month, 30);
                }
            }

            f_primdesc = string.Format("{0}/{1}/{2}", auxF.Day, auxF.Month, auxF.Year);
            txtF_primerdesc.Text = f_primdesc;
        }

        private void txtEmisionCheque_Leave(object sender, EventArgs e)
        {
            calculoLiquido();
        }

        private void frmAltas_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Desea salir del módulo?", "Cerrando módulo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (txtFolio.ReadOnly)
            {
                return;
            }

            if (!globales.alfaNumerico(e.KeyChar))
                return;


            limpiarTodosCampos();
            activarControlesBasicos();
            txtEmisionCheque.Text = "";
            frmCatalogoP_quirog P_quirog = new frmCatalogoP_quirog();
            P_quirog.enviar = rellenarModificarFolios;
            P_quirog.ShowDialog();
            this.ActiveControl = txtRfc;
        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {

        }

        public void cambiarTxtSueldoBase(string texto)
        {
            txtSueldoBase.Text = texto;
        }

        private void imprimir(p_quirog obj, int checador = 4)
        {
            //Se necesita para sacar los meses de acuerdo al DataTime.............
            string[] meses = {
                "Enero",
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Septiembre",
                "Octubre",
                "Noviembre",
                "Diciembre"
            };

            #region plazos del quirografario.....
            //Empieza el arreglo para sacar los plazoz del quirografario junto con su importe...
            string[] ar_fecharsve = new string[48];
            string[] ar_fechasca = new string[48];
            string fev = obj.f_primdesc;
            if (obj.tipo_pago == Convert.ToChar("Q"))
            {
                int band = 15;
                for (int x = 0; x < obj.plazo; x++)
                {
                    fev = fev.Replace("'", "");
                    ar_fecharsve[x] = fev;
                    ar_fechasca[x] = obj.imp_unit.ToString("#.##");
                    if (band == 15)
                    {
                        string[] split = fev.Split('-');
                        if (split[1] == "2")
                        {
                            fev = string.Format("{0}-02-28", split[0]);
                        }
                        else
                        {
                            fev = string.Format("{0}-{1}-30", split[0], split[1]);
                        }
                        band = 30;
                    }
                    else
                    {
                        string[] split = fev.Split('-');
                        int año = Convert.ToInt32(split[0]);
                        int mes = Convert.ToInt32(split[1]);
                        DateTime fechaAux = new DateTime(año, mes, 1);
                        fechaAux = fechaAux.AddDays(35);
                        fev = string.Format("{0}-{1}-15", fechaAux.Year, fechaAux.Month);
                        band = 15;
                    }
                }

            }


            #endregion


            #region Asignación de los reportes.........

            string fecha = string.Format("{0} de {1} del {2}", DateTime.Now.Day, meses[DateTime.Now.Month], DateTime.Now.Year);
            string t1 = string.Format("OAXACA DE JUÁREZ, OAX., {0} de {1} del {2}", DateTime.Now.Day, meses[DateTime.Now.Month], DateTime.Now.Year);
            string terminosYCondiciones = string.Format("Debo(emos) y Pagaré(mos) incondicionalmente por este pagaré mercantil, en esta plaza ( o en cualquier otro lugar a elección del" +
                " acreedor), a la orden de la \"OFICINA DE PENSIONES DEL ESTADO DE OAXACA\", él día: {0} la cantidad de ${1} {2} 00/100 M.N), valor recibido a mi entera sastifacción.", ar_fecharsve[Convert.ToInt32(obj.plazo) - 1], obj.importe, globales.numerosLetras(Convert.ToInt32(obj.importe)));


            object quir = null;
            object quir2 = null;

            string query = string.Format("select * from datos.d_perded where folio = {0}", obj.folio);

            List<Dictionary<string, object>> resultado = globales.consulta(query);

            string percepciones = string.Empty;
            string deducciones = string.Empty;

            string reduc1 = string.Empty; ;
            string porcentaje1 = string.Empty; ;

            string reduc2 = string.Empty; ;
            string porcentaje2 = string.Empty;

            if (resultado.Count != 0) {
                 percepciones = Convert.ToDouble((resultado[0]["percepciones"])).ToString("#.##");
                 deducciones = Convert.ToDouble(resultado[0]["deducciones"]).ToString("#.##");

                 reduc1 = Convert.ToDouble(resultado[0]["deduc_rec1"]).ToString("#.##");
                 porcentaje1 = Convert.ToDouble(resultado[0]["porcentaje1"]).ToString("#.##");

                 reduc2 = Convert.ToDouble(resultado[0]["deduc_rec2"]).ToString("#.##");
                 porcentaje2 = Convert.ToDouble(resultado[0]["porcentaje2"]).ToString("#.##");
            }

            percepciones = (string.IsNullOrWhiteSpace(percepciones)) ? "0" : percepciones;
            deducciones = (string.IsNullOrWhiteSpace(deducciones)) ? "0" : deducciones;

            reduc1 = (string.IsNullOrWhiteSpace(reduc1)) ? "0" : reduc1;
            porcentaje1 = (string.IsNullOrWhiteSpace(porcentaje1)) ? "0" : porcentaje1;

            reduc2 = (string.IsNullOrWhiteSpace(reduc2)) ? "0" : reduc2;
            porcentaje2 = (string.IsNullOrWhiteSpace(porcentaje2)) ? "0" : porcentaje2;

            obj.f_emischeq = obj.f_emischeq.Replace("'", "");

            if (obj.lista.Count == 1)
            {
                object[] quiro = {obj.folio, string.Format("OAXACA DE JUÁREZ, OAX., {0}", fecha), obj.nombre_em, obj.rfc, obj.direccion, obj.proyecto, obj.descripcion, obj.telefono,obj.importe.ToString("#.##"),obj.plazo, obj.tipo_pago,
                                  obj.interes,obj.fondo_g,obj.liquido.ToString("#.##"),percepciones,deducciones,reduc1,porcentaje1,reduc2,porcentaje2,obj.sueldo_m,string.Format("{0}A {1}M {2}Q", obj.ant_a, obj.ant_m, obj.ant_q),obj.nue,obj.nap,obj.f_emischeq,obj.lista[0].nombre_em,
                                  obj.lista[0].direccion,obj.lista[0].rfc, obj.lista[0].proyecto,obj.lista[0].antig,obj.lista[0].nue,obj.lista[0].nap};
                object[] quiro2 = { obj.folio, obj.plazo, t1, obj.importe.ToString("#.##"), terminosYCondiciones, obj.nombre_em, obj.rfc, obj.proyecto, obj.nap, obj.direccion ,obj.lista[0].nombre_em,obj.lista[0].rfc,obj.lista[0].proyecto,obj.lista[0].nap,obj.lista[0].direccion,"","","","","", ar_fecharsve[0], ar_fecharsve[1] , ar_fecharsve[2] , ar_fecharsve[3] , ar_fecharsve[4] , ar_fecharsve[5],
                                    ar_fecharsve[6],ar_fecharsve[7],ar_fecharsve[8],ar_fecharsve[9],ar_fecharsve[10],ar_fecharsve[11],ar_fecharsve[12],ar_fecharsve[13],ar_fecharsve[14],ar_fecharsve[15],ar_fecharsve[16],ar_fecharsve[17],ar_fecharsve[18],ar_fecharsve[19],ar_fecharsve[20],ar_fecharsve[21],ar_fecharsve[22],
                                    ar_fecharsve[23],ar_fecharsve[24],ar_fecharsve[25],ar_fecharsve[26],ar_fecharsve[27],ar_fecharsve[28],ar_fecharsve[29],ar_fecharsve[30],ar_fecharsve[31],ar_fecharsve[32],ar_fecharsve[33],ar_fecharsve[34],ar_fecharsve[35],ar_fecharsve[36],ar_fecharsve[37],ar_fecharsve[38],ar_fecharsve[39],
                                    ar_fecharsve[40],ar_fecharsve[41],ar_fecharsve[42],ar_fecharsve[43],ar_fecharsve[44],ar_fecharsve[45],ar_fecharsve[46],ar_fecharsve[47],ar_fechasca[0],ar_fechasca[1],ar_fechasca[2],ar_fechasca[3],ar_fechasca[4],ar_fechasca[5],ar_fechasca[6],ar_fechasca[7],ar_fechasca[8],ar_fechasca[9],ar_fechasca[10],
                                    ar_fechasca[11],ar_fechasca[12],ar_fechasca[13],ar_fechasca[14],ar_fechasca[15],ar_fechasca[16],ar_fechasca[17],ar_fechasca[18],ar_fechasca[19],ar_fechasca[20],ar_fechasca[21],ar_fechasca[22],ar_fechasca[23],ar_fechasca[24],ar_fechasca[25],ar_fechasca[26],ar_fechasca[27],ar_fechasca[28],ar_fechasca[29],ar_fechasca[30],
                                    ar_fechasca[31],ar_fechasca[32],ar_fechasca[33],ar_fechasca[34],ar_fechasca[35],ar_fechasca[36],ar_fechasca[37],ar_fechasca[38],ar_fechasca[39],ar_fechasca[40],ar_fechasca[41],ar_fechasca[42],ar_fechasca[43],ar_fechasca[44],ar_fechasca[45],ar_fechasca[46],ar_fechasca[47]
                                    };
                quir = quiro;
                quir2 = quiro2;
            }
            else if (obj.lista.Count == 2)
            {
                object[] quiro = {obj.folio, string.Format("OAXACA DE JUÁREZ, OAX., {0}", fecha), obj.nombre_em, obj.rfc, obj.direccion, obj.proyecto, obj.descripcion, obj.telefono,obj.importe.ToString("#.##"),obj.plazo, obj.tipo_pago,
                                  obj.interes,obj.fondo_g,obj.liquido.ToString("#.##"),percepciones,deducciones,reduc1,porcentaje1,reduc2,porcentaje2,obj.sueldo_m,string.Format("{0}A {1}M {2}Q", obj.ant_a, obj.ant_m, obj.ant_q),obj.nue,obj.nap,obj.f_emischeq,obj.lista[0].nombre_em,obj.lista[0].direccion, obj.lista[0].rfc,
                                  obj.lista[0].proyecto,obj.lista[0].antig,obj.lista[0].nue,obj.lista[0].nap,obj.lista[1].nombre_em,obj.lista[1].direccion,obj.lista[1].rfc,obj.lista[1].proyecto,obj.lista[1].antig,obj.lista[1].nue,obj.lista[1].nap};
                object[] quiro2 = { obj.folio, obj.plazo, t1, obj.importe.ToString("#.##"), terminosYCondiciones, obj.nombre_em, obj.rfc, obj.proyecto, obj.nap, obj.direccion ,obj.lista[0].nombre_em,obj.lista[0].rfc,obj.lista[0].proyecto,obj.lista[0].nap,obj.lista[0].direccion,obj.lista[1].nombre_em,obj.lista[1].rfc,obj.lista[1].proyecto,obj.lista[1].nap,obj.lista[1].direccion, ar_fecharsve[0], ar_fecharsve[1] , ar_fecharsve[2] , ar_fecharsve[3] , ar_fecharsve[4] , ar_fecharsve[5],
                                    ar_fecharsve[6],ar_fecharsve[7],ar_fecharsve[8],ar_fecharsve[9],ar_fecharsve[10],ar_fecharsve[11],ar_fecharsve[12],ar_fecharsve[13],ar_fecharsve[14],ar_fecharsve[15],ar_fecharsve[16],ar_fecharsve[17],ar_fecharsve[18],ar_fecharsve[19],ar_fecharsve[20],ar_fecharsve[21],ar_fecharsve[22],
                                    ar_fecharsve[23],ar_fecharsve[24],ar_fecharsve[25],ar_fecharsve[26],ar_fecharsve[27],ar_fecharsve[28],ar_fecharsve[29],ar_fecharsve[30],ar_fecharsve[31],ar_fecharsve[32],ar_fecharsve[33],ar_fecharsve[34],ar_fecharsve[35],ar_fecharsve[36],ar_fecharsve[37],ar_fecharsve[38],ar_fecharsve[39],
                                    ar_fecharsve[40],ar_fecharsve[41],ar_fecharsve[42],ar_fecharsve[43],ar_fecharsve[44],ar_fecharsve[45],ar_fecharsve[46],ar_fecharsve[47],ar_fechasca[0],ar_fechasca[1],ar_fechasca[2],ar_fechasca[3],ar_fechasca[4],ar_fechasca[5],ar_fechasca[6],ar_fechasca[7],ar_fechasca[8],ar_fechasca[9],ar_fechasca[10],
                                    ar_fechasca[11],ar_fechasca[12],ar_fechasca[13],ar_fechasca[14],ar_fechasca[15],ar_fechasca[16],ar_fechasca[17],ar_fechasca[18],ar_fechasca[19],ar_fechasca[20],ar_fechasca[21],ar_fechasca[22],ar_fechasca[23],ar_fechasca[24],ar_fechasca[25],ar_fechasca[26],ar_fechasca[27],ar_fechasca[28],ar_fechasca[29],ar_fechasca[30],
                                    ar_fechasca[31],ar_fechasca[32],ar_fechasca[33],ar_fechasca[34],ar_fechasca[35],ar_fechasca[36],ar_fechasca[37],ar_fechasca[38],ar_fechasca[39],ar_fechasca[40],ar_fechasca[41],ar_fechasca[42],ar_fechasca[43],ar_fechasca[44],ar_fechasca[45],ar_fechasca[46],ar_fechasca[47]
                                    };
                quir = quiro;
                quir2 = quiro2;
            }
            else
            {
                object[] quiro = {obj.folio, string.Format("OAXACA DE JUÁREZ, OAX., {0}", fecha), obj.nombre_em, obj.rfc, obj.direccion, obj.proyecto, obj.descripcion, obj.telefono,obj.importe.ToString("#.##"),obj.plazo, obj.tipo_pago,
                                  obj.interes,obj.fondo_g,obj.liquido.ToString("#.##"),percepciones,deducciones,reduc1,porcentaje1,reduc2,porcentaje2,obj.sueldo_m,string.Format("{0}A {1}M {2}Q", obj.ant_a, obj.ant_m, obj.ant_q),obj.nue,obj.nap,obj.f_emischeq};

                object[] quiro2 = { obj.folio, obj.plazo, t1, obj.importe.ToString("#.##"), terminosYCondiciones, obj.nombre_em, obj.rfc, obj.proyecto, obj.nap, obj.direccion ,"","","","","","","","","","", ar_fecharsve[0], ar_fecharsve[1] , ar_fecharsve[2] , ar_fecharsve[3] , ar_fecharsve[4] , ar_fecharsve[5],
                                    ar_fecharsve[6],ar_fecharsve[7],ar_fecharsve[8],ar_fecharsve[9],ar_fecharsve[10],ar_fecharsve[11],ar_fecharsve[12],ar_fecharsve[13],ar_fecharsve[14],ar_fecharsve[15],ar_fecharsve[16],ar_fecharsve[17],ar_fecharsve[18],ar_fecharsve[19],ar_fecharsve[20],ar_fecharsve[21],ar_fecharsve[22],
                                    ar_fecharsve[23],ar_fecharsve[24],ar_fecharsve[25],ar_fecharsve[26],ar_fecharsve[27],ar_fecharsve[28],ar_fecharsve[29],ar_fecharsve[30],ar_fecharsve[31],ar_fecharsve[32],ar_fecharsve[33],ar_fecharsve[34],ar_fecharsve[35],ar_fecharsve[36],ar_fecharsve[37],ar_fecharsve[38],ar_fecharsve[39],
                                    ar_fecharsve[40],ar_fecharsve[41],ar_fecharsve[42],ar_fecharsve[43],ar_fecharsve[44],ar_fecharsve[45],ar_fecharsve[46],ar_fecharsve[47],ar_fechasca[0],ar_fechasca[1],ar_fechasca[2],ar_fechasca[3],ar_fechasca[4],ar_fechasca[5],ar_fechasca[6],ar_fechasca[7],ar_fechasca[8],ar_fechasca[9],ar_fechasca[10],
                                    ar_fechasca[11],ar_fechasca[12],ar_fechasca[13],ar_fechasca[14],ar_fechasca[15],ar_fechasca[16],ar_fechasca[17],ar_fechasca[18],ar_fechasca[19],ar_fechasca[20],ar_fechasca[21],ar_fechasca[22],ar_fechasca[23],ar_fechasca[24],ar_fechasca[25],ar_fechasca[26],ar_fechasca[27],ar_fechasca[28],ar_fechasca[29],ar_fechasca[30],
                                    ar_fechasca[31],ar_fechasca[32],ar_fechasca[33],ar_fechasca[34],ar_fechasca[35],ar_fechasca[36],ar_fechasca[37],ar_fechasca[38],ar_fechasca[39],ar_fechasca[40],ar_fechasca[41],ar_fechasca[42],ar_fechasca[43],ar_fechasca[44],ar_fechasca[45],ar_fechasca[46],ar_fechasca[47]
                                    };
                quir = quiro;
                quir2 = quiro2;
            }



            object[] objeto = { quir };
            object[] objeto2 = { quir2 };

            if (obj.porc > 50)
            {
                if (MessageBox.Show(string.Format("Esta solicitud excede con {0}% ¿Desea imprimir?", ""), "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
            }

            if (checador == 4)
            {
                globales.reportes("p_quirogSolicitud", "p_quirog_solicitud", objeto, "Se imprimira solicitud de QUIROGRAFARIO");
                globales.reportes("reportePagareQuiro", "pagare_quirog", objeto2, "Se imprimira el pagare de QUIROGRAFARIO");
            }

            if (checador == 1)
            {
                globales.reportes("p_quirogSolicitud", "p_quirog_solicitud", objeto, "Se imprimira solicitud de QUIROGRAFARIO");
            }
            if (checador == 3)
            {
                globales.reportes("reportePagareQuiro", "pagare_quirog", objeto2, "Se imprimira el pagare de QUIROGRAFARIO");
            }


            MessageBox.Show("Termino el proceso de impresión de Quirografarío", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            #endregion


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("hola desde todo", new Font("Arial", 40, FontStyle.Bold), Brushes.Black, 150, 125);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar = rellenarModificarFolios;
            p_quirog.ShowDialog();

            frmImpresionQuirografario imp = new frmImpresionQuirografario();
            imp.ShowDialog();
            int checador = imp.checador;
            tiposDeImpresion(checador);
        }
        private p_quirog verificarObjeto()
        {
            p_quirog obj = new p_quirog();
            obj.rfc = txtRfc.Text;
            obj.nombre_em = txtnombre_em.Text;
            obj.proyecto = txtProyecto.Text;
            obj.secretaria = txtSecretaria.Text;
            obj.antig_q = (string.IsNullOrWhiteSpace(txtAntQ.Text)) ? 0 : Convert.ToInt32(txtAntQ.Text);
            obj.sueldo_base = (string.IsNullOrWhiteSpace(txtSueldoBase.Text)) ? 0 : Convert.ToDouble(txtSueldoBase.Text);
            obj.descripcion = txtAdscripcion.Text;
            obj.telefono = txtTelefono.Text;
            obj.extencion = txtExtencion.Text;
            obj.direccion = txtDomicilio.Text;
            obj.nue = txtNue.Text;
            obj.nap = (string.IsNullOrWhiteSpace(txtNap.Text)) ? 0 : Convert.ToDouble(txtNap.Text);
            obj.sueldo_m = (string.IsNullOrWhiteSpace(txtSueldo_m.Text)) ? 0 : Convert.ToDouble(txtSueldo_m.Text);
            obj.ant_q = Convert.ToInt32(Ant_Q);
            obj.ant_m = Convert.ToInt32(Ant_M);
            obj.ant_a = Convert.ToInt32(Ant_A);
            obj.meses_corres = (string.IsNullOrWhiteSpace(txtmeses_corres.Text)) ? 0 : Convert.ToDouble(txtmeses_corres.Text);
            obj.otros_desc = (string.IsNullOrWhiteSpace(txtdesc.Text)) ? 0 : Convert.ToDouble(txtdesc.Text);
            obj.porc = (string.IsNullOrWhiteSpace(txtPorc.Text)) ? 0 : Convert.ToDouble(txtPorc.Text);
            obj.plazo = (string.IsNullOrWhiteSpace(txtplazo.Text)) ? 0 : Convert.ToDouble(txtplazo.Text);
            obj.tipo_pago = Convert.ToChar(txtTipoPago.Text);
            obj.trel = Convert.ToChar((string.IsNullOrWhiteSpace(txtTrl.Text)?" ": txtTrl.Text));
            obj.f_emischeq = (string.IsNullOrWhiteSpace(txtEmisionCheque.Text)) ? "null" : txtEmisionCheque.Text;
            obj.f_primdesc = (string.IsNullOrWhiteSpace(txtF_primerdesc.Text)) ? "null" : txtF_primerdesc.Text;
            obj.f_ultmode = string.IsNullOrWhiteSpace(txtultpago.Text) ? "null" : txtultpago.Text;
            obj.imp_unit = (string.IsNullOrWhiteSpace(txtImpUnit.Text)) ? 0 : Convert.ToDouble(txtImpUnit.Text);
            obj.importe = (string.IsNullOrWhiteSpace(txtImporte.Text)) ? 0 : Convert.ToDouble(txtImporte.Text);
            obj.interes = (string.IsNullOrWhiteSpace(txtintereses.Text)) ? 0 : Convert.ToDouble(txtintereses.Text);
            obj.fondo_g = (string.IsNullOrWhiteSpace(txtFondo_g.Text)) ? 0 : Convert.ToDouble(txtFondo_g.Text);
            obj.liquido = (string.IsNullOrWhiteSpace(txtliquido.Text)) ? 0 : Convert.ToDouble(txtliquido.Text);
            obj.carta = (string.IsNullOrWhiteSpace(this.carta)) ? Convert.ToChar(" ") : Convert.ToChar(this.carta);
            obj.f_solicitud = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            obj.aceptado = (string.IsNullOrWhiteSpace(this.aceptado)) ? Convert.ToChar(" ") : Convert.ToChar(this.aceptado);
            obj.secuen = this.Secuen;

            if (obj.f_emischeq != "null")
            {
                string[] aux2 = obj.f_emischeq.Split('/');
                obj.f_emischeq = string.Format("'{0}-{1}-{2}'", aux2[2], aux2[1], aux2[0]);
            }
            if (obj.f_primdesc != "null")
            {
                string[] aux2 = obj.f_primdesc.Split('/');
                obj.f_primdesc = string.Format("'{0}-{1}-{2}'", aux2[2], aux2[1], aux2[0]);
            }

            if (obj.f_ultmode != "null")
            {
                string[] aux2 = obj.f_ultmode.Split('/');
                obj.f_ultmode = string.Format("'{0}-{1}-{2}'", aux2[2], aux2[1], aux2[0]);
            }
            return obj;
        }
        private void tiposDeImpresion(int checador)
        {
            p_quirog obj = verificarObjeto();
            obj.folio = Convert.ToInt32(txtFolio.Text);
            obj.lista = new List<d_quirog>();

            if (!string.IsNullOrWhiteSpace(txtRfc1.Text))
            {
                d_quirog detalleQuirog = new d_quirog();
                detalleQuirog.folio = obj.folio;
                detalleQuirog.rfc = txtRfc1.Text;
                detalleQuirog.nombre_em = txtNombre1.Text;
                detalleQuirog.direccion = txtdomicilio1.Text;
                detalleQuirog.proyecto = txtProyect1.Text;
                detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtNap1.Text)) ? 0 : Convert.ToDouble(txtNap1.Text);
                detalleQuirog.nue = txtNue1.Text;
                detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtAnti1.Text)) ? 0 : Convert.ToInt32(txtAnti1.Text);
                obj.lista.Add(detalleQuirog);
            }

            if (!string.IsNullOrWhiteSpace(txtrfc2.Text))
            {
                d_quirog detalleQuirog = new d_quirog();
                detalleQuirog.folio = obj.folio;
                detalleQuirog.rfc = txtrfc2.Text;
                detalleQuirog.nombre_em = txtnombre2.Text;
                detalleQuirog.direccion = txtdomicilio2.Text;
                detalleQuirog.proyecto = txtproy2.Text;
                detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtnap2.Text)) ? 0 : Convert.ToDouble(txtnap2.Text);
                detalleQuirog.nue = txtnue2.Text;
                detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtantg2.Text)) ? 0 : Convert.ToInt32(txtantg2.Text);
                obj.lista.Add(detalleQuirog);
            }

            imprimir(obj, checador);
        }

        private void txtSueldoBase_Leave(object sender, EventArgs e)
        {
            if (this.txtSueldoBase.ReadOnly) return;

            if (!string.IsNullOrWhiteSpace(txtSecretaria.Text)) {
                rellenarCamposSecretarias(auxiliar);
            }
        }

        private void txtSueldoBase_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
