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
    public partial class frmProyecto : Form

    {
        private DataGridViewRow elemento1;
        private string cprocve = string.Empty;
        public frmProyecto()
        {
            InitializeComponent();
        }

        private void frmProyecto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmProyecto_FormClosing(object sender, FormClosingEventArgs e)
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

        private void frmProyecto_Load(object sender, EventArgs e)

        {
            string query = "select cprocve,cprodes from catalogos.proyecto";
            var elemento = baseDatos.consulta(query);

            foreach (var item in elemento)
            {
                string cprocve = Convert.ToString(item["cprocve"]);
                string cprodes = Convert.ToString(item["cprodes"]);
                datos02.Rows.Add(cprocve, cprodes);
                btnnuevo.Enabled = true;
                btnguardar.Enabled = true;
                btnmodifica.Enabled = true;
                txtdes.Visible = false;
                txtproye.Visible = false;
                lbldes.Visible = false;
                lblproy.Visible = false;
                btnok.Visible = false;

            }
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            txtproye.Enabled = true;
            txtdes.Enabled = true;
            lblproy.Enabled = true;
            lbldes.Enabled = true;
            btnmodifica.Visible = false;
            btnnuevo.Visible = false;
            btnok.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtproye.Text))
            {
                MessageBox.Show("Esta vacio proyecto");
                txtproye.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtdes.Text))
            {
                MessageBox.Show("Esta vacio descripción");
                txtdes.Focus();
                return;
            }
            string query = string.Format("insert into catalogos.proyecto(cprocve,cprodes) values ('{0}','{1}')", txtproye.Text, txtdes.Text);
            if (baseDatos.consulta(query, true))
            {

                MessageBox.Show("Registro Guardado Coreectamente");

                datos02.Rows.Add(txtproye.Text, txtdes.Text);
                //   this.Controls.OfType<TextBox>().ToList(txtdes,txtproye).ForEach(o => o.Text = "");
                txtdes.Clear();
                txtproye.Clear();
                btnmodifica.Enabled = true;
                btnnuevo.Enabled = true;
                txtproye.Enabled = false;
                txtdes.Enabled = false;
                lblproy.Enabled = false;
                lbldes.Enabled = false;

            }

            else
            {
                MessageBox.Show("Error contacte a sistemas");
                //  this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
                txtdes.Clear();
                txtproye.Clear();
                btnmodifica.Visible = false;
                btnnuevo.Enabled = true;
                txtproye.Enabled = false;
                txtdes.Enabled = false;
                lblproy.Enabled = false;
                lbldes.Enabled = false;

            }

        }

        private void btnmodifica_Click(object sender, EventArgs e)
        {
            string cprodes = Convert.ToString(elemento1.Cells[1].Value);
            txtdes.Text = cprodes;
            txtproye.Text = cprocve;

            btnnuevo.Visible = false;
            btnguardar.Visible = false;
            btnmodifica.Visible = false;
            txtdes.Visible = true;
            txtproye.Visible = true;
            lblproy.Visible = true;
            lbldes.Visible = true;
            btnok.Visible = true;




        }

        private void datos02_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var mod = datos02.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string actualiza = "update catalogos.proyecto set "; 
                switch (e.ColumnIndex)
                {
                    case 1:
                        actualiza += " cprodes = '{0}'";
                        break;
                    

                }
                actualiza += " where cprocve = '{1}'";
                string proye = string.Format(actualiza, mod, datos02.Rows[e.RowIndex].Cells[0].Value);
                if (baseDatos.consulta(proye, true))
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

        private void datos02_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                elemento1 = datos02.Rows[e.RowIndex];
                txtdes.Enabled = true;
                txtproye.Enabled = true;

            }
            catch
            {
                MessageBox.Show("Error en los datos del dataGrid.. contacte a sistemas para dar solución");
            }

        }

        private void datos02_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtproye.Text))
            {
                MessageBox.Show("Esta vacio proyecto");
                txtproye.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtdes.Text))
            {
                MessageBox.Show("Esta vacio descripción");
                txtdes.Focus();
                return;
            }
            string cambio = string.Format("update catalogos.proyecto set cprocve = '{0}', cprodes = '{1}' where cprocve = '{2}'", txtproye.Text, txtdes.Text, cprocve);
            if (baseDatos.consulta(cambio, true))
            {

                MessageBox.Show("Registro Guardado Coreectamente");
                txtdes.Enabled = true;
                txtproye.Enabled = true;
                txtdes.Clear();
                txtproye.Clear();
                btnmodifica.Visible = true;
                btnguardar.Visible = true;


            }
            else
            {
                MessageBox.Show("Error contacte a sistemas");
                txtdes.Enabled = true;
                txtproye.Enabled = true;
                txtdes.Clear();
                txtproye.Clear();
                btnmodifica.Visible = true;
                btnguardar.Visible = true;


            }
            datos02.Rows.Clear();
            frmProyecto_Load(null, null);

        }
    }







}
