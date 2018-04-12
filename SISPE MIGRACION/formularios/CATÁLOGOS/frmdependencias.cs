using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
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
    public partial class frmdependencias : Form
    {
        internal enviarDatos enviar;
        private List<Dictionary<string, object>> resultado;
        private string proyecto = string.Empty;
        public frmdependencias()
        {
            InitializeComponent();
        }

        private void frmdependencias_Load(object sender, EventArgs e)
        {
            string query = "select cuenta,descripcion,proy from catalogos.dependencias";
            resultado = baseDatos.consulta(query);

            foreach (Dictionary<string,object> item in resultado)
            {
                string descripcion = Convert.ToString(item["descripcion"]);
                string proy = Convert.ToString(item["proy"]);
                datos.Rows.Add(proy, descripcion);
            }

        }

        private void datos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> valor =  null;
            foreach (Dictionary<string, object> item in resultado)
            {
                if (item["proy"].Equals(proyecto))
                {
                    valor = item;
                    break;
                }
            }
            limpiar();
            Close();
            enviar(valor);
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            limpiar();
            Close();
        }

        private void limpiar()
        {
            txtBusqueda.Text = "";
        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            string query = string.Format("select * from catalogos.dependencias where descripcion like '{0}%' or proy like '{0}%' LIMIT 30",txtBusqueda.Text);
            var elemento = baseDatos.consulta(query);
            datos.Rows.Clear();
            foreach (var item in elemento)
            {
                string descripcion = item["descripcion"];
                string proy = item["proy"];
                datos.Rows.Add(proy, descripcion);
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
            if (e.KeyChar == 13)
                btnseleccionar_Click(null,null);
        }

        private void datos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnseleccionar_Click(null,null);
        }

        private void datos_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            proyecto = Convert.ToString(datos.Rows[e.Cell.RowIndex].Cells[0].Value);
        }
    }
}
