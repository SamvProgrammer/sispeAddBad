using SISPE_MIGRACION.formularios.CATÁLOGOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.PAGO_DE_MARCHA
{
    public partial class frmpagomarcha : Form
    {
        private frmCatalogoP_quirog frmCatalogoP_quirog;
        private bool guardar = false;
        private List<Dictionary<string, object>> resultado;
        public frmpagomarcha()
        {
            InitializeComponent();
        }

        private void frmpagomarcha_Load(object sender, EventArgs e)
        {
            txtfolio.Enabled = false;
            btnGuarda.Visible = false;
            btnguardar2.Visible = false;

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Seguro que desea cancelar la operación?", "Cancelar operación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogo == DialogResult.No) return;

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnnueo.Visible = false;
            btnGuarda.Visible = true;
            btnguardar2.Visible = false; 
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar2 = llenacampos;
            p_quirog.tablaConsultar = "p_marcha";
            p_quirog.ShowDialog();
            btnnueo.Visible = false;
            btnmodi.Visible = false;
        }


        private void limpiaTodosCampos()
        {
            txtdependencia.Clear();
            txtdescuento.Clear();
            txtfecharec.Clear();
            txtfecobro.Clear();
            txtfolio.Clear();
            txtimporteletra.Clear();
            txtliquido.Clear();
            txtmeses.Clear();
            txtmonto.Clear();
            txtmuerto.Clear();
            txtnombre.Clear();
            txtnumcheq.Clear();
            txtparentesco.Clear();
            txtrfc.Clear();
            txtsueldo.Clear();
            txtsuertudo.Clear();
            txtvida.Clear();
            txtconcepto.Clear();
            

            
        }

        private void visible ()
        {
            btnnueo.Visible = true;
            btnmodi.Visible = true;
            btnGuarda.Visible = false;
            btnguardar2.Visible = false;
        }


        private void actualiza()
        {
            try
            {

                string llena = "UPDATE datos.p_marcha SET rfc = '{0}', sueldo_base = '{1}', descripcion = '{2}', f_recibo = '{3}', n_cheque = '{4}', nombre_em = '{5}', depe = '{6}', f_acaec = '{7}', meses = '{8}', pers_cobro = '{9}', parentesco = '{10}', f_cobro = '{11}', monto = '{12}', descuentos = '{13}', liquido = '{14}' , concepto_desc='{15}' , liq_letra='{16}' WHERE folio = '{17}' ";
                string query= string.Format(llena, txtrfc.Text, txtsueldo.Text, txtvida.Text, txtfecharec.Text, txtnumcheq.Text, txtnombre.Text, txtdependencia.Text, txtmuerto.Text, txtmeses.Text, txtsuertudo.Text, txtparentesco.Text, txtfecobro.Text, txtmonto.Text, txtdescuento.Text, txtliquido.Text, txtconcepto.Text,txtimporteletra, txtfolio.Text );
                globales.consulta(query,true);
                MessageBox.Show("Registros modificados");
            }
            catch
            {
                
            }

            limpiaTodosCampos();
            activabotones();

        }

        private void activabotones()
        {
            btnnueo.Visible = true;
            btnmodi.Visible = true;
            btnGuarda.Visible = true;
            btnsalir.Visible = true;

        }


        public void llenacampos(Dictionary<string, object> campo)
        {


            this.txtfolio.Text = Convert.ToString(campo["folio"]);
            this.txtrfc.Text = Convert.ToString(campo["rfc"]);
            this.txtsueldo.Text = Convert.ToString(campo["sueldo_base"]);
            this.txtvida.Text = Convert.ToString(campo["descripcion"]);
            this.txtfecharec.Text = Convert.ToString(campo["f_recibo"]).Replace("12:00:00 a. m.", ""); 
            this.txtnumcheq.Text = Convert.ToString(campo["n_cheque"]);
            this.txtnombre.Text = Convert.ToString(campo["nombre_em"]);
            this.txtdependencia.Text = Convert.ToString(campo["depe"]);
            this.txtmuerto.Text = Convert.ToString(campo["f_acaec"]).Replace("12:00:00 a. m.", ""); 
            this.txtmeses.Text = Convert.ToString(campo["meses"]);
            this.txtsuertudo.Text = Convert.ToString(campo["pers_cobro"]);
            this.txtparentesco.Text = Convert.ToString(campo["parentesco"]);
            this.txtfecobro.Text = Convert.ToString(campo["f_cobro"]).Replace("12:00:00 a. m.", "");
            this.txtmonto.Text = Convert.ToString(campo["monto"]);
            this.txtdescuento.Text = Convert.ToString(campo["descuentos"]);
            this.txtconcepto.Text = Convert.ToString(campo["concepto_desc"]);
            this.txtliquido.Text = Convert.ToString(campo["liquido"]);
            this.txtimporteletra.Text = Convert.ToString(campo["monto"]);
           
          
            
            string numero;
            numero = txtimporteletra.Text;
            this.txtimporteletra.Text = (globales.convertirNumerosLetras(numero, true));


        }

        private void nuevo()
        {
            try
            {
                string maximo = "SELECT MAX(folio+1) as tmp FROM datos.p_marcha";

                List<Dictionary<string,object>> resultado = globales.consulta(maximo);

                string folio = Convert.ToString(resultado[0]["tmp"]);
                String CONVERSION = globales.convertirNumerosLetras(this.txtmonto.Text, true);
                string nuevo = "INSERT INTO datos.p_marcha (rfc , sueldo_base , descripcion , f_recibo , n_cheque , nombre_em , depe  , f_acaec , meses , pers_cobro , parentesco , f_cobro , monto , descuentos , liquido,concepto_desc , liq_letra ,folio) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')";
                string query = string.Format(nuevo, txtrfc.Text, txtsueldo.Text, txtvida.Text, txtfecharec.Text, txtnumcheq.Text, txtnombre.Text, txtdependencia.Text, txtmuerto.Text, txtmeses.Text, txtsuertudo.Text, txtparentesco.Text, txtfecobro.Text, txtmonto.Text, txtdescuento.Text, txtliquido.Text, txtconcepto.Text,CONVERSION,folio);
                globales.consulta(query, true);
                MessageBox.Show("Nuevo Folio Insertado");
                MessageBox.Show("EL FOLIO GENERADO ES:"+folio);
                btnguardar2.Visible = false;
                btnmodi.Visible = true;
              
            }
            catch
            {

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            btnGuarda.Visible = false;
            btnguardar2.Visible = true;
            btnmodi.Visible = false;
            txtimporteletra.Enabled = false;
            DialogResult dialogo = MessageBox.Show("¿Seguro que deseas ingresar un nuevo folio?", "Cancelar operación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogo == DialogResult.No) return;

            limpiaTodosCampos();
            txtfolio.Text = "AUTOGENERADO";



            //   this.ActiveControl = txtfolio;

        }

        private void btnsalir_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmpagomarcha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                DialogResult dialogo = MessageBox.Show("¿Deseas regresar al Menú?", "Cancelar operación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogo == DialogResult.No) return;
                Close();

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtdependencia.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtconcepto.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtconcepto.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtdescuento.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtdescuento.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtfecharec.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtfecharec.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtfecobro.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtfecobro.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtimporteletra.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtimporteletra.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtliquido.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtliquido.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtmeses.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtmeses.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtmonto.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtmonto.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtmuerto.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtmuerto.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtnombre.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtnumcheq.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtnumcheq.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtparentesco.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtparentesco.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtrfc.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtrfc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtsueldo.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtsueldo.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtsuertudo.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtsuertudo.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtvida.Text))
            {
                MessageBox.Show("Esta vacio un campo");
                txtvida.Focus();
                return;
            }

            actualiza();
            btnGuarda.Visible = false;

            txtimporteletra.Enabled = false;
            visible();


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            nuevo();
            limpiaTodosCampos();
            txtimporteletra.Enabled = true;
            visible();


        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar2 = imprimirReporte;
            p_quirog.tablaConsultar = "p_marcha";
            p_quirog.ShowDialog();
        }
        private void imprimirReporte(Dictionary<string,object> resultado)
        {
            object[][] parametros = new object[2][];

            object[] cabeceras = { "cantidad", "imporletra", "nombre" , "rfc" ,"tipo", "meses" , "sueldo" ,"menos","liquido", "facaec" };
            object[] valores = {Convert.ToString(resultado["monto"]), Convert.ToString(resultado["liq_letra"]), Convert.ToString(resultado["nombre_em"]), Convert.ToString(resultado["rfc"]), Convert.ToString(resultado["descripcion"]), Convert.ToString(resultado["meses"]), Convert.ToString(resultado["sueldo_base"]),  Convert.ToString(resultado["descuentos"]), Convert.ToString(resultado["liquido"]), Convert.ToString(resultado["f_acaec"]).Replace("12:00:00 a. m.", "") };

            parametros[0] = cabeceras;
            parametros[1] = valores;

            globales.reportes("ReporteMarcha", "p_marcha",new object[] { },"",false,parametros);
        }
    }

   
    
}
