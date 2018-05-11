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

namespace SISPE_MIGRACION.formularios
{
    public partial class login : Form
    {
        private const string formulario = "login";
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
        #if DEBUG
            txtUsuario.Text = "administrador";
            txtPassword.Text = "bu1ld3r";
        #endif
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string metodo = "btnIngresar_Click";
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MessageBox.Show("Favor de ingresar el usuario.","Ingresar usuario",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Favor de ingresar la contraseña.", "Ingresar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                string usuario = txtUsuario.Text;
                string password = txtPassword.Text;
                string query = string.Format("Select usuario,password from catalogos.usuarios where usuario = '{0}' and password = '{1}'", usuario, password);
                var respuesta = baseDatos.consulta(query);
                if (respuesta.Count > 0)
                {                    
                    new menuPrincipal().Show();
                    Hide();
                }
                else
                    MessageBox.Show("Usuario y/o contraseña invalida","Error ingresar sistema",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

                
            }
            catch {
                MessageBox.Show(string.Format("Error formulario {0} método {1}",formulario,metodo));
            }
            this.Cursor = Cursors.Default;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) btnIngresar_Click(null, null);
        }
    }
}
