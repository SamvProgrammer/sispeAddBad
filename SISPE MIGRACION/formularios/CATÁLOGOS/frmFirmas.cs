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
    public partial class frmFirmas : Form
    {
        private DataGridViewRow elemento;

        public frmFirmas()
        {
            InitializeComponent();
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            btnnuevo.Visible = false;
            btnguarda.Visible = false;
            lblclave.Enabled = true;
            txtclave.Enabled = true;
            txtdesc.Enabled = true;
            txtinic.Enabled = true;
            txtnombre.Enabled = true;
            txtobserv.Enabled = true;
            lbldesc.Enabled = true;
            lblinic.Enabled = true;
            lblnomb.Enabled = true;
            lblobsev.Enabled = true;
            btnmodifica.Enabled = false;
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmFirmas_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Desea cerrar el módulo?",
           "Cerrar el módulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogo == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void frmFirmas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void frmFirmas_Load(object sender, EventArgs e)
        {
            string firmas = "select * from catalogos.firmas ";
            var elemento = baseDatos.consulta(firmas);
            foreach (var item in elemento)
            {
                string clave = Convert.ToString(item["clave"]);
                string nombre = Convert.ToString(item["nombre"]);
                string cargo = Convert.ToString(item["cargo"]);
                string iniresp = Convert.ToString(item["iniresp"]);
                string observ = Convert.ToString(item["observ"]);

                dbfirmas.Rows.Add(clave, nombre, cargo, iniresp, observ);
            }

        }

        private void dbfirmas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnguarda_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtclave.Text))
            {
                MessageBox.Show("Esta vacio clave");
                txtclave.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                MessageBox.Show("Esta vacio nombre");
                txtnombre.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtinic.Text))
            {
                MessageBox.Show("Esta vacio iniciales");
                txtinic.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtobserv.Text))
            {
                MessageBox.Show("Esta vacio observaciones");
                txtobserv.Focus();
                return;
            }


            string firma = string.Format("insert into catalogos.firmas (clave,nombre,cargo,iniresp,observ) values  = '{0}','{1}' ,'{2}','{3}','{4}')", txtclave.Text, txtnombre.Text, txtdesc.Text, txtinic.Text, txtobserv.Text);
            if (baseDatos.consulta(firma, true))
            {

                MessageBox.Show("Registro Guardado Coreectamente");
                dbfirmas.Rows.Clear();
                txtclave.Clear();
                txtdesc.Clear();
                txtinic.Clear();
                txtnombre.Clear();
                txtobserv.Clear();
                btnnuevo.Enabled = true;
                btnmodifica.Enabled = true;
                btnguarda.Visible = true;
                frmFirmas_Load(null, null);

            }
            else
            {
                MessageBox.Show("Error contacte a sistemas");
                txtclave.Clear();
                txtdesc.Clear();
                txtinic.Clear();
                txtnombre.Clear();
                txtobserv.Clear();
                btnnuevo.Enabled = true;
                btnmodifica.Enabled = true;
                frmFirmas_Load(null, null);
            }
        }

        private void btnmodifica_Click(object sender, EventArgs e)

        {
            string clave = Convert.ToString(elemento.Cells[0].Value);
            string nombre = Convert.ToString(elemento.Cells[1].Value);
            string descripcion = Convert.ToString(elemento.Cells[2].Value);
            string iniciales = Convert.ToString(elemento.Cells[3].Value);
            string observaciones = Convert.ToString(elemento.Cells[4].Value);
            txtclave.Text = clave;
            txtnombre.Text = nombre;
            txtdesc.Text = descripcion;
            txtinic.Text = iniciales;
            txtobserv.Text = observaciones;
            btnnuevo.Visible = false;
            btnguarda.Visible = false;
            btnnuevo.Visible = false;

        }

        private void dbfirmas_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                elemento = dbfirmas.Rows[e.RowIndex];
                txtclave.Enabled = true;
                txtdesc.Enabled = true;
                txtinic.Enabled = true;
                txtobserv.Enabled = true;
                txtnombre.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Error en los datos del dataGrid.. contacte a sistemas para dar solución");
            }
        }

        private void dbfirmas_RowErrorTextChanged(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dbfirmas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var aux = dbfirmas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string query = "update catalogos.firmas set ";
                switch (e.ColumnIndex)
                {
                    case 1:
                        query += "nombre = '{0}'";
                        break;
                    case 2:
                        query += "cargo = '{0}'";
                        break;
                    case 3:
                        query += "iniresp = '{0}'";
                        break;
                    case 4:
                        query += "observ = '{0}'";
                        break;

                }
                query += " where clave = '{1}'";
                string firma = string.Format(query, aux, dbfirmas.Rows[e.RowIndex].Cells[0].Value);
                if (baseDatos.consulta(firma, true))
                {
                    MessageBox.Show("Registro modificado");
                }
                else
                {
                    MessageBox.Show("Error en la actualización");
                }
            }
            catch
            {

            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtclave.Text))
            {
                MessageBox.Show("Esta vacio clave");
                txtclave.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                MessageBox.Show("Esta vacio nombre");
                txtnombre.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtinic.Text))
            {
                MessageBox.Show("Esta vacio iniciales");
                txtinic.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtobserv.Text))
            {
                MessageBox.Show("Esta vacio observaciones");
                txtobserv.Focus();
                return;
            }


            string firma = string.Format("update catalogos.firmas set nombre = '{0}', cargo = '{1}', iniresp = '{2}',observ = '{3}' where clave = '{4}'", txtnombre.Text, txtdesc.Text, txtinic.Text, txtobserv.Text, txtclave.Text);

            if (baseDatos.consulta(firma, true))
            {

                MessageBox.Show("Registro Guardado Coreectamente");

                txtclave.Clear();
                txtdesc.Clear();
                txtinic.Clear();
                txtnombre.Clear();
                txtobserv.Clear();
                btnnuevo.Visible = true;
                btnmodifica.Visible = true;
                dbfirmas.Rows.Clear();
                frmFirmas_Load(null, null);

            }
            else
            {
                MessageBox.Show("Error contacte a sistemas");
                txtclave.Clear();
                txtdesc.Clear();
                txtinic.Clear();
                txtnombre.Clear();
                txtobserv.Clear();
                btnnuevo.Visible = true;
                btnmodifica.Visible = true;
                frmFirmas_Load(null, null);
            }
        }

    }
    }

