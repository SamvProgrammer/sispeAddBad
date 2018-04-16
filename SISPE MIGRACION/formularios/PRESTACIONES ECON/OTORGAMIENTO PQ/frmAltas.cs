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
        }

        private void ALTAS_Load(object sender, EventArgs e)
        {

            txtTrl.Text = modalidades.First().Key;
            lblmod.Text = modalidades.First().Value;

            frmdependencias = new frmdependencias();
            frmdependencias.enviar = rellenarCamposSecretarias;

            txtAntiguedad.Text = "A M Q";
            DateTime fecha = globales.sacarFechaHabil(45);
            string txtFecha = string.Format("{0}/{1}/{2}", fecha.Day, fecha.Month, fecha.Year);
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
                btnNuevo.Enabled = true;
                btnNuevo.Visible = true;

                btnModifica.Enabled = true;
                btnModifica.Visible = true;

                btnGuardar.Enabled = false;
                btnGuardar.Visible = false;

                btnCalculo.Enabled = false;

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
            limpiarCamposRFC();
            limpiarSecretariaCampos();
            limpiarLiquidoCampos();
            txtAntQ.Text = "0";
            limpiarAvales();
            txtTelefono.Text = "";
            txtExtencion.Text = "";
            txtdesc.Text = "0.00";
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
        }

        public void rellenarCamposSecretarias(Dictionary<string, object> datos, bool externo = false)
        {
            if (!externo)
            {

                /*
                    Se agrega líneas para pedir los importes de percepciones
                    y reducciones del trabajador.
                */

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

            this.auxiliar = datos;
            string secretaria = Convert.ToString(datos["proy"]);
            string descripcionProyecto = Convert.ToString(datos["descripcion"]);
            txtSecretaria.Text = secretaria;
            txtAdscripcion.Text = descripcionProyecto;

            if (secretaria != "J" && secretaria != "P" && secretaria != "T")
            {
                txtSueldo_m.Text = (Convert.ToDouble(txtSueldoBase.Text) * 2).ToString();
                double Qtotales = Convert.ToDouble(txtAntQ.Text);
                double AA = (Qtotales) / 24;
                double QAux = Qtotales - (AA * 24);
                double AM = (QAux / 2);
                double AQ = QAux - (AM * 2);

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

            e.Handled = true;
            frmdependencias.ShowDialog();
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
            globales.reportes("reportePagareQuiro","",null,null);
            return;

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
            obj.trel = Convert.ToChar(txtTrl.Text);
            obj.f_emischeq = (string.IsNullOrWhiteSpace(txtEmisionCheque.Text)) ? "null" : txtEmisionCheque.Text;
            obj.f_primdesc = (string.IsNullOrWhiteSpace(txtF_primerdesc.Text)) ? "null" : txtF_primerdesc.Text;
            obj.f_ultmode = string.IsNullOrWhiteSpace(txtultpago.Text) ? "null" : txtultpago.Text;
            obj.imp_unit = (string.IsNullOrWhiteSpace(txtImpUnit.Text)) ? 0 : Convert.ToDouble(txtImpUnit.Text);
            obj.importe = (string.IsNullOrWhiteSpace(txtImporte.Text)) ? 0 : Convert.ToDouble(txtImporte.Text);
            obj.interes = (string.IsNullOrWhiteSpace(txtintereses.Text)) ? 0 : Convert.ToDouble(txtintereses.Text);
            obj.fondo_g = (string.IsNullOrWhiteSpace(txtFondo_g.Text)) ? 0 : Convert.ToDouble(txtFondo_g.Text);
            obj.liquido = (string.IsNullOrWhiteSpace(txtliquido.Text)) ? 0 : Convert.ToDouble(txtliquido.Text);
            obj.carta = (string.IsNullOrWhiteSpace(this.carta)) ? Convert.ToChar("") : Convert.ToChar(this.carta);
            obj.f_solicitud = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

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

            if (!guardar)
            {
                MessageBox.Show("Se modificara");
            }
            else if (guardar)
            {
                if (insertarRegistro(obj))
                {
                    MessageBox.Show("Registro guardado exitosamente!!", "Registro guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult resultado = MessageBox.Show("¿Desea impirmir la presente solicitud?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.No)
                    {
                        MessageBox.Show("Puede impirmir más adelante!!", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    this.Cursor = Cursors.WaitCursor;
                    imprimir(obj);
                }
                else
                    MessageBox.Show("Error al guardar el registor, contactar al equipo de sistemas!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
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
                    "sueldo_m,ant_a,ant_m,ant_q,meses_corres,otros_desc,trel,porc,plazo,tipo_pago,f_emischeq,f_primdesc,f_ultimode,imp_unit,importe,interes,fondo_g,liquido,carta,f_solicitud) values({0},'{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}','{9}'," +
                    "'{10}','{11}',{12},{13},{14},{15},{16},{17},{18},'{19}',{20},{21},'{22}',{23},{24},{25},{26},{27},{28},{29},{30},'{31}','{32}')";
                query = string.Format(query, obj.folio, obj.rfc, obj.nombre_em, obj.proyecto, obj.secretaria, obj.antig_q, obj.sueldo_base, obj.descripcion, obj.telefono, obj.extencion, obj.direccion, obj.nue, obj.nap,
                    obj.sueldo_m, obj.ant_a, obj.ant_m, obj.ant_q, obj.meses_corres, obj.otros_desc, obj.trel, obj.porc, obj.plazo, obj.tipo_pago, obj.f_emischeq, obj.f_primdesc, obj.f_ultmode, obj.imp_unit, obj.importe, obj.interes, obj.fondo_g, obj.liquido,
                    obj.carta, obj.f_solicitud);

                obj.lista = new List<d_quirog>();
                if (globales.consulta(query, true))
                {
                    registro = true;
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

            activarControlesBasicos();


            btnNuevo.Enabled = false;
            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;

            btnCalculo.Enabled = true;


            btnModifica.Enabled = false;
            btnModifica.Visible = false;
            btnsalir.Text = "Cancelar";

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
            activarControl(txtdesc);
            activarControl(txtRfc1);
            activarControl(txtrfc2);
            activarControl(txtSueldoBase);
            activarControl(txtmeses_corres);
            activarControl(txtdesc);
            activarControl(txtPorc);
            activarControl(txtplazo);
            activarControl(txtProyecto);
        }
        private void desactivarControlesBasicos()
        {
            desactivarControl(txtRfc);
            desactivarControl(txtSecretaria);
            desactivarControl(txtAntQ);
            desactivarControl(txtTelefono);
            desactivarControl(txtExtencion);
            desactivarControl(txtdesc);
            desactivarControl(txtRfc1);
            desactivarControl(txtrfc2);

            desactivarControl(txtSueldoBase);
            desactivarControl(txtmeses_corres);
            desactivarControl(txtdesc);
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
            btnsalir.Text = "Cancelar";

            txtFolio.ReadOnly = false;
            txtFolio.Cursor = Cursors.IBeam;

            btnModifica.Enabled = true;

            btnCalculo.Enabled = true;

            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar = rellenarModificarFolios;
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
            txtEmisionCheque.Text = Convert.ToString(quirografario["f_emischeq"]);
            txtF_primerdesc.Text = Convert.ToString(quirografario["f_primdesc"]);
            txtultpago.Text = Convert.ToString(quirografario["f_ultimode"]);
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

            double TOPE = 0.00;
            double RES = 0.00;
            double RESL = 0.00;
            double RES3 = 0.00;
            double RES1 = 0.00;
            double Por = 0.00;
            double RESD = 0.00;
            double RES2 = 0.00;
            string RO = string.Empty;
            string NOM = string.Empty;
            double IMP = 0.00;
            double IM = 0.00;

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
                txtintereses.Text = Convert.ToString((aux1 * ((plazo / 2) + 1)) * t_interes);
            else
                txtintereses.Text = Convert.ToString((aux1 * ((plazo) + 1)) * t_interes);

            int auxAnti_q = Convert.ToInt32(txtAntQ.Text);
            if (auxAnti_q < 240 && txtSecretaria.Text != "J" && txtSecretaria.Text != "T")
            {
                txtFondo_g.Text = Convert.ToString(Convert.ToDouble(txtImporte.Text) * 0.02);
            }
            else
            {
                txtFondo_g.Text = "0";
            }

            string reto = "S";
            string ret = "N";

            aceptado = "S";
            Secuen = 1;

            txtliquido.Text = Convert.ToString(Convert.ToDouble(txtImporte.Text) - Convert.ToDouble(txtintereses.Text) - Convert.ToDouble(txtFondo_g.Text) - Convert.ToDouble(txtOtros_desc.Text));
            dialogo = MessageBox.Show("¿Se acepta el credito?", "Crédito", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogo == DialogResult.Yes)
            {
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

        private void imprimir(p_quirog obj)
        {
            string[] meses = {
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



            string fecha = string.Format("{0} de {1} del {2}", DateTime.Now.Day, meses[DateTime.Now.Month], DateTime.Now.Year);
            //Parte de los reportes....................

            object quir = null;
            if (obj.lista.Count == 1)
            {
                object[] quiro = {
                    obj.folio, string.Format("OAXACA DE JUAREZ, OAX., {0}", fecha), obj.nombre_em, obj.rfc, obj.direccion, obj.proyecto, obj.descripcion, obj.telefono,obj.importe,obj.plazo, obj.tipo_pago,
               obj.interes,obj.fondo_g,obj.importe,"","","","","","",obj.sueldo_m,string.Format("{0}A {1}M {2}Q", obj.ant_a, obj.ant_m, obj.ant_q),obj.nue,obj.nap,obj.f_emischeq,
                  (obj.lista[0].nombre_em == null) ? "" : obj.lista[0].nombre_em,
            (obj.lista[0].direccion == null) ? "" : obj.lista[0].direccion,
            (obj.lista[0].rfc == null) ? "" : obj.lista[0].rfc,
            (obj.lista[0].proyecto == null) ? "" : obj.lista[0].proyecto,
            (obj.lista[0].antig == null) ? "" : Convert.ToString(obj.lista[0].antig),
            (obj.lista[0].nue == null) ? "" : obj.lista[0].nue,
            (obj.lista[0].nap == null) ? "" : Convert.ToString(obj.lista[0].nap)};
                quir = quiro;
            }
            else if (obj.lista.Count == 2)
            {
                object[] quiro = {
                    obj.folio, string.Format("OAXACA DE JUAREZ, OAX., {0}", fecha), obj.nombre_em, obj.rfc, obj.direccion, obj.proyecto, obj.descripcion, obj.telefono,obj.importe,obj.plazo, obj.tipo_pago,
               obj.interes,obj.fondo_g,obj.importe,"","","","","","",obj.sueldo_m,string.Format("{0}A {1}M {2}Q", obj.ant_a, obj.ant_m, obj.ant_q),obj.nue,obj.nap,obj.f_emischeq,
                  (obj.lista[0].nombre_em == null) ? "" : obj.lista[0].nombre_em,
            (obj.lista[0].direccion == null) ? "" : obj.lista[0].direccion,
            (obj.lista[0].rfc == null) ? "" : obj.lista[0].rfc,
            (obj.lista[0].proyecto == null) ? "" : obj.lista[0].proyecto,
            (obj.lista[0].antig == null) ? "" : Convert.ToString(obj.lista[0].antig),
            (obj.lista[0].nue == null) ? "" : obj.lista[0].nue,
            (obj.lista[0].nap == null) ? "" : Convert.ToString(obj.lista[0].nap),
                (obj.lista[1].nombre_em == null) ? "" : obj.lista[1].nombre_em,
            (obj.lista[1].direccion == null) ? "" : obj.lista[1].direccion,
            (obj.lista[1].rfc == null) ? "" : obj.lista[1].rfc,
            (obj.lista[1].proyecto == null) ? "" : obj.lista[1].proyecto,
            (obj.lista[1].antig == null) ? "" : Convert.ToString(obj.lista[1].antig),
            (obj.lista[1].nue == null) ? "" : obj.lista[1].nue,
            (obj.lista[1].nap == null) ? "" : Convert.ToString(obj.lista[1].nap)};
                quir = quiro;
            }
            else
            {
                object[] quiro = {
                    obj.folio, string.Format("OAXACA DE JUAREZ, OAX., {0}", fecha), obj.nombre_em, obj.rfc, obj.direccion, obj.proyecto, obj.descripcion, obj.telefono,obj.importe,obj.plazo, obj.tipo_pago,
               obj.interes,obj.fondo_g,obj.importe,"","","","","","",obj.sueldo_m,string.Format("{0}A {1}M {2}Q", obj.ant_a, obj.ant_m, obj.ant_q),obj.nue,obj.nap,obj.f_emischeq};
                quir = quiro;
            }



            object[] objeto = { quir };

            globales.reportes("p_quirogSolicitud", "p_quirog_solicitud", objeto, "Se imprimira solicitud de QUIROGRAFARIO", true);

            //globales.reportes("reportePagareQuiro", "", objeto, "Se imprimira el pagaré único", true);


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("hola desde todo", new Font("Arial", 40, FontStyle.Bold), Brushes.Black, 150, 125);
        }
    }
}
