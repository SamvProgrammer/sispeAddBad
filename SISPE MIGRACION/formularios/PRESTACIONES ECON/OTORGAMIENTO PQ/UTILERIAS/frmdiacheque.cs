using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.UTILERIAS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ
{
    public partial class frmdiacheque : Form
    {

        private string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        public frmdiacheque()
        {
            InitializeComponent();
        }

        private void frmprogcheque_Load(object sender, EventArgs e)
        {
            string cheques = ("SELECT fecha, inhabil, CASE WHEN to_char(fecha, 'd') = '1' then cast('Domingo' as char(10)) WHEN to_char(fecha,'d') = '2' then cast('Lunes' as char(10)) WHEN to_char(fecha,'d') = '3' then cast('Martes' as char(10)) WHEN to_char(fecha,'d') = '4'then cast('Miercoles' as char(10)) WHEN to_char(fecha,'d') = '5'then cast('Jueves' as char(10)) WHEN to_char(fecha,'d') = '6'then cast('Viernes' as char(10)) WHEN to_char(fecha,'d') = '7' then cast('Sabado' as char(10))END AS dia, programados FROM catalogos.progpq order by fecha desc limit 100");
            var elemento3 = baseDatos.consulta(cheques);
            cmbAño.Items.Clear();
            cmbMes.Items.Clear();
            foreach (var item in elemento3)
            {
                string fecha = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.", "");
                bool inhabil = Convert.ToString(item["inhabil"]) != "*" ? false : true;
                string dia = Convert.ToString(item["dia"]);
                string programados = Convert.ToString(item["programados"]);
                gridcheques.Rows.Add(fecha, inhabil, dia, programados);

            }


            //Consulta para sacar la fecha más pequeña hasta la fecha actual... combo box
            List<Dictionary<string, object>> resultado = globales.consulta("select fecha from catalogos.progpq order by fecha asc limit 1");
            string año = Convert.ToString(resultado[0]["fecha"]).Replace(" 12:00:00 a. m.", "").Split('/')[2];
            int auxAño = Convert.ToInt32(año);
            for (int x = DateTime.Now.Year; x >= auxAño; x--)
            {
                cmbAño.Items.Add(x);
            }
            for (int x = 1; x < this.meses.Length; x++)
            {
                cmbMes.Items.Add(this.meses[x]);
            }
        }

        private void frmdiacheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void frmdiacheque_FormClosing(object sender, FormClosingEventArgs e)
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

        private void gridcheques_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var aux = gridcheques.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string query = "update catalogos.progpq set ";
                switch (e.ColumnIndex)
                {
                    case 1:
                        query += " inhabil = '{0}'";
                        aux = (Convert.ToBoolean(aux)) ? "*" : "";
                        break;
                    case 3:
                        query += " programados = '{0}'";
                        break;

                }
                query += " where fecha = '{1}'";
                string dia = string.Format(query, aux, gridcheques.Rows[e.RowIndex].Cells[0].Value);
                baseDatos.consulta(dia, true);
            }
            catch
            {

            }
        }

        private void Boxmeses_Enter(object sender, EventArgs e)
        {

        }

        private void gridcheques_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {

        }

        private void gridcheques_Click(object sender, EventArgs e)
        {

        }

        private void cmbAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }
        private void filtrar()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string query = string.Empty;
                gridcheques.Rows.Clear();
                if (!string.IsNullOrWhiteSpace(cmbAño.Text) && string.IsNullOrWhiteSpace(cmbMes.Text))
                {
                    query = string.Format("select *,CASE WHEN to_char(fecha, 'd') = '1' then cast('Domingo' as char(10)) WHEN to_char(fecha,'d') = '2' then cast('Lunes' as char(10)) WHEN to_char(fecha,'d') = '3' then cast('Martes' as char(10)) WHEN to_char(fecha,'d') = '4'then cast('Miercoles' as char(10)) WHEN to_char(fecha,'d') = '5'then cast('Jueves' as char(10)) WHEN to_char(fecha,'d') = '6'then cast('Viernes' as char(10)) WHEN to_char(fecha,'d') = '7' then cast('Sabado' as char(10))END AS dia from catalogos.progpq where fecha >= '{0}-01-01' AND fecha <= '{0}-12-31'", cmbAño.Text);
                }
                else if (string.IsNullOrWhiteSpace(cmbAño.Text) && !string.IsNullOrWhiteSpace(cmbMes.Text))
                {
                    for (int x = 0; x < cmbAño.Items.Count; x++)
                    {
                        query = "select *,CASE WHEN to_char(fecha, 'd') = '1' then cast('Domingo' as char(10)) WHEN to_char(fecha,'d') = '2' then cast('Lunes' as char(10)) WHEN to_char(fecha,'d') = '3' then cast('Martes' as char(10)) WHEN to_char(fecha,'d') = '4'then cast('Miercoles' as char(10)) WHEN to_char(fecha,'d') = '5'then cast('Jueves' as char(10)) WHEN to_char(fecha,'d') = '6'then cast('Viernes' as char(10)) WHEN to_char(fecha,'d') = '7' then cast('Sabado' as char(10))END AS dia from catalogos.progpq ";
                        string item = Convert.ToString(cmbAño.Items[x]);
                        if (cmbMes.SelectedIndex != 11)
                        {
                            query += string.Format("where fecha >= '{0}-{1}-01' AND fecha < '{0}-{2}-01'", item, cmbMes.SelectedIndex + 1, cmbMes.SelectedIndex + 2);
                        }
                        else
                        {
                            string año2 = (Convert.ToInt32(item) + 1).ToString();
                            query += string.Format("where fecha >= '{0}-12-01' AND fecha < '{1}-01-01'", item, año2);
                        }
                        List<Dictionary<string, object>> tmp = globales.consulta(query);
                        foreach (var item2 in tmp)
                        {
                            string fecha = Convert.ToString(item2["fecha"]).Replace(" 12:00:00 a. m.", "");
                            bool inhabil = Convert.ToString(item2["inhabil"]) != "*" ? false : true;
                            string dia = Convert.ToString(item2["dia"]);
                            string programados = Convert.ToString(item2["programados"]);
                            gridcheques.Rows.Add(fecha, inhabil, dia, programados);
                        }
                    }
                    this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    query = "select *,CASE WHEN to_char(fecha, 'd') = '1' then cast('Domingo' as char(10)) WHEN to_char(fecha,'d') = '2' then cast('Lunes' as char(10)) WHEN to_char(fecha,'d') = '3' then cast('Martes' as char(10)) WHEN to_char(fecha,'d') = '4'then cast('Miercoles' as char(10)) WHEN to_char(fecha,'d') = '5'then cast('Jueves' as char(10)) WHEN to_char(fecha,'d') = '6'then cast('Viernes' as char(10)) WHEN to_char(fecha,'d') = '7' then cast('Sabado' as char(10))END AS dia from catalogos.progpq ";
                    if (cmbMes.SelectedIndex != 11)
                    {
                        query += string.Format(" where fecha >= '{0}-{1}-01' AND fecha < '{0}-{2}-01';", cmbAño.Text, cmbMes.SelectedIndex + 1, cmbMes.SelectedIndex + 2);
                    }
                    else
                    {
                        string año2 = (Convert.ToInt32(cmbAño.Text) + 1).ToString();
                        query += string.Format(" where fecha >= '{0}-12-01' AND fecha < '{1}-01-01';", cmbAño.Text, año2);
                    }

                }
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (var item in resultado)
                {
                    string fecha = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.", "");
                    bool inhabil = Convert.ToString(item["inhabil"]) != "*" ? false : true;
                    string dia = Convert.ToString(item["dia"]);
                    string programados = Convert.ToString(item["programados"]);
                    gridcheques.Rows.Add(fecha, inhabil, dia, programados);

                }
            }
            catch
            {
                MessageBox.Show("Error en el sistema, contactar a los de informatica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            new frmGenerarMesCheque().ShowDialog();
            gridcheques.Rows.Clear();
            frmprogcheque_Load(null,null);
            cmbMes.SelectedIndex = -1;
            cmbAño.SelectedIndex = -1;
        }
    }
}
