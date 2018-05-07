using SISPE_MIGRACION.codigo.baseDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    public partial class frmCatemplea : Form
    {

        private List<Dictionary<string, object>> resultado;
        private Dictionary<string, object> persona;
        public frmCatemplea()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void txtbuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {                    
                listBusqueda.Items.Clear();
                if (string.IsNullOrWhiteSpace(txtbuscar.Text))
                    return;
                string busqueda = txtbuscar.Text;
                string query = string.Format("select * from datos.empleados where rfc like '{0}%' OR nombre_em LIKE  '{0}%' limit 30;",busqueda,busqueda);
                resultado = baseDatos.consulta(query);
                resultado.ForEach(o => listBusqueda.Items.Add(o["rfc"]+" | "+o["nombre_em"]));
            }
            catch {

            }
        }

        private void listBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                limpiarControles();
                activarControles(false);
                ListBox lista = sender as ListBox;
                int seleccionado = lista.SelectedIndex;
                persona = resultado[seleccionado];
                txtrfc.Text = persona["rfc"].ToString();
                txtnombre.Text = persona["nombre_em"].ToString();
                txtstatus.Text = persona["status"].ToString();
                rdMasculino.Checked = (((!string.IsNullOrWhiteSpace(persona["sexo"] as string)) && persona["sexo"].ToString() == "H"));
                rdFemenino.Checked = (((!string.IsNullOrWhiteSpace(persona["sexo"] as string)) && persona["sexo"].ToString() == "F"));
                txtdirec.Text = persona["direccion"].ToString();
                txtNap.Text = Convert.ToString( persona["nap"]);
                txtDependencia.Text = persona["depe"].ToString();
                txtClaveCategoria.Text = persona["cve_categ"].ToString();
                fechaNacimiento.Text = persona["fecha_nac"].ToString();
                fechaIngresos.Text = persona["fecha_ing"].ToString();
                txtProyectos.Text = persona["proyecto"].ToString();
                txtSueldoBase.Text = persona["sueldo_base"].ToString();
                txtRelacionLaboral.Text = persona["tipo_rel"].ToString();
                btnEliminar.Visible = true;
                btnEliminar.Enabled = true;
                btnModificar.Visible = true;
                btnModificar.Enabled = true;
                btnNuevo.Enabled = true;
                btnNuevo.Text = "NUEVO EMPLEADO";
                btnModificar.Text = "MODIFICAR";
                txtbuscar.Text = "";
                lista.Items.Clear();
            }
        }

        private void frmCatemplea_Load(object sender, EventArgs e)
        {
            activarControles(false);
            btnModificar.Visible = false;
            btnEliminar.Visible = false;
            btnCancelar.Visible = false;
           
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void activarControles(bool activar)
        {
            txtrfc.Enabled = activar;
            rdFemenino.Enabled = activar;
            rdMasculino.Enabled = activar;
            txtnombre.Enabled = activar;
            fechaIngresos.Enabled = activar;
            fechaNacimiento.Enabled = activar;
            txtstatus.Enabled = activar;
            txtdirec.Enabled = activar;
            txtNap.Enabled = activar;
            txtDependencia.Enabled = activar;
            txtProyectos.Enabled = activar;
            txtClaveCategoria.Enabled = activar;
            txtSueldoBase.Enabled = activar;
            txtRelacionLaboral.Enabled = activar;
        }

        private void limpiarControles() {
            txtrfc.Text = "";
            rdFemenino.Checked = false;
            rdMasculino.Checked = true;
            txtnombre.Text = "";
            fechaIngresos.Text = "";
            fechaNacimiento.Text = "";
            txtstatus.Text = "";
            txtdirec.Text = "";
            txtNap.Text = "";
            txtDependencia.Text = "";
            txtProyectos.Text = "";
            txtClaveCategoria.Text = "";
            txtSueldoBase.Text = "";
            txtRelacionLaboral.Text = "";
        }


        private void btnNuevoclick(object sender, EventArgs e)
        {
            if (btnNuevo.Text.Contains("NUEVO")) {
                activarControles(true);
                limpiarControles();
                btnNuevo.Text = "AGREGAR EMPLEADO";
                btnModificar.Visible = false;
                btnEliminar.Visible = false;
                return;
            }

            if(validaciones()){

                string p1 = txtrfc.Text;
                string p2 = txtnombre.Text;
                string p3 = (rdMasculino.Checked) ? "H" : "F";
                string p4 = fechaNacimiento.Value.Year + "-" + fechaNacimiento.Value.Month + "-" + fechaNacimiento.Value.Day;
                string p5 = txtstatus.Text;
                string p6 = txtdirec.Text;
                string p7 = (string.IsNullOrWhiteSpace(txtNap.Text)) ? "0" : txtNap.Text;
                string p8 = fechaIngresos.Value.Year+"-"+ fechaIngresos.Value.Month+"-"+ fechaIngresos.Value.Day;
                string p9 = txtDependencia.Text;
                string p10 = txtProyectos.Text;
                string p11 = txtClaveCategoria.Text;
                string p12 = (string.IsNullOrWhiteSpace(txtSueldoBase.Text)) ? "0" : txtSueldoBase.Text;
                string p13 = txtRelacionLaboral.Text;

                string query = string.Format("insert into datos.empleados(rfc,nombre_em,sexo,fecha_nac,status,direccion," +
                                         "nap,fecha_ing,depe,proyecto,cve_categ,sueldo_base,tipo_rel) VALUES" +
                                         "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}'," +
                                         " {6}, '{7}', '{8}', '{9}', '{10}', {11}, '{12}')", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
                if (baseDatos.consulta(query, true))
                {
                    MessageBox.Show("Registro ingresado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else {
                    MessageBox.Show("Error en el ingreso de datos, favor de contactar a sistemas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                limpiarControles();
                activarControles(false);
                btnNuevo.Text = "NUEVO EMPLEADO";

            }



        }

        private bool validaciones()
        {

            if (string.IsNullOrWhiteSpace(txtrfc.Text))
            {
                MessageBox.Show("favor de ingresar el rfc", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtrfc.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                MessageBox.Show("favor de ingresar el nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtnombre.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(fechaNacimiento.Text))
            {
                MessageBox.Show("favor de ingresar la fecha de nacimiento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!rdFemenino.Checked && !rdMasculino.Checked)
            {
                MessageBox.Show("favor seleccionar sexo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rdMasculino.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtdirec.Text))
            {
                MessageBox.Show("favor de ingresar la dirección", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtdirec.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(fechaIngresos.Text))
            {
                MessageBox.Show("favor de ingresar fecha de ingresos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                fechaIngresos.Focus();
                return false;
            }
            else {
                return true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (btnModificar.Text.Contains("MODIFICAR")) {
                btnModificar.Text = "ACTUALIZAR";
                activarControles(true);
                txtrfc.Enabled = false;
                btnNuevo.Enabled = false;
                btnEliminar.Enabled = false;
                btnCancelar.Visible = true;
                return;
            }

            if (validaciones()) {
                string p1 = txtrfc.Text;
                string p2 = txtnombre.Text;
                string p3 = (rdMasculino.Checked) ? "H" : "F";
                string p4 = fechaNacimiento.Value.Year + "-" + fechaNacimiento.Value.Month + "-" + fechaNacimiento.Value.Day;
                string p5 = txtstatus.Text;
                string p6 = txtdirec.Text;
                string p7 = (string.IsNullOrWhiteSpace(txtNap.Text)) ? "0" : txtNap.Text;
                string p8 = fechaIngresos.Value.Year + "-" + fechaIngresos.Value.Month + "-" + fechaIngresos.Value.Day;
                string p9 = txtDependencia.Text;
                string p10 = txtProyectos.Text;
                string p11 = txtClaveCategoria.Text;
                string p12 = (string.IsNullOrWhiteSpace(txtSueldoBase.Text)) ? "0" : txtSueldoBase.Text;
                string p13 = txtRelacionLaboral.Text;
                string identificador = persona["id"].ToString();

                string query = string.Format("update datos.empleados set nombre_em = '{0}',  sexo = '{1}', "+
                                             "fecha_nac = '{2}',  status = '{3}',  direccion = '{4}', "+
                                             "nap = {5},  fecha_ing = '{6}',  depe = '{7}',  proyecto = '{8}', "+
                                             "cve_categ = '{9}',  sueldo_base = {10},  tipo_rel = '{11}' "+
                                             "where id = {12}",p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13,identificador);


                if (baseDatos.consulta(query, true))
                {
                    MessageBox.Show("Registro actualizado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnModificar.Visible = false;
                    btnEliminar.Visible = false;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error en la actualización de datos, favor de contactar a sistemas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                limpiarControles();
                activarControles(false);
                btnModificar.Text = "MODIFICAR";
                btnCancelar.Visible = false;


            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea eliminar este registro?","Eliminando registro",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (resultado == DialogResult.Yes) {
                string id = persona["id"].ToString();
                string query = string.Format("delete from datos.empleados where id = {0}",id);
                if (baseDatos.consulta(query,true)) {
                    MessageBox.Show("Registro eliminado correctamente","Registro eliminado",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    limpiarControles();
                    activarControles(false);
                    btnEliminar.Visible = false;
                    btnModificar.Visible = false;
                    btnNuevo.Enabled = true;
                    btnNuevo.Visible = true;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnCancelar.Visible = false;
            btnNuevo.Enabled = true;
            btnEliminar.Enabled = true;
            activarControles(false);
            btnModificar.Text = "MODIFICAR";
        }

        private void frmCatemplea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }
    }
}
