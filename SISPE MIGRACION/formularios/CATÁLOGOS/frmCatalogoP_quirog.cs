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
    public partial class frmCatalogoP_quirog : Form
    {
        internal enviarDatos2 enviar;
        private int numeroMaximo = 0;
        private List<Dictionary<string, object>> resultado;
        private string folio = string.Empty;
        public frmCatalogoP_quirog()
        {
            InitializeComponent();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
            if (!string.IsNullOrWhiteSpace(txtBusqueda.Text)) {
                if (txtBusqueda.Text.ElementAt(0) >= '0' && txtBusqueda.Text.ElementAt(0) <= '9')
                {
                    if ((e.KeyChar <= '9' && e.KeyChar >= '0' || e.KeyChar == 8))
                    {
                        e.Handled = false;
                    }
                    else {
                        e.Handled = true;
                    }
                    txtBusqueda.MaxLength = numeroMaximo;
                }
                else
                {
                    txtBusqueda.MaxLength = 13;
                }
            }
            
        }

        private void frmCatalogoP_quirog_Load(object sender, EventArgs e)
        {
            string query = string.Format("select MAX(FOLIO) from datos.p_quirog");

            List<Dictionary<string, object>> resultado2 = globales.consulta(query);
            string maximo = Convert.ToString(resultado2[0]["max"]);
            numeroMaximo = maximo.Length;

            string query2 = string.Format("select * from datos.p_quirog order by folio asc limit 100");

            resultado = globales.consulta(query2);
            resultado.ForEach(o => datos.Rows.Add(o["folio"], o["rfc"], o["nombre_em"]));
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format("select * from datos.p_quirog");
            if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                query += " where ";
                if (txtBusqueda.Text.ElementAt(0) >= '0' && txtBusqueda.Text.ElementAt(0) <= '9')
                {
                    string cadenaAux = txtBusqueda.Text;
                    string cadenaAux2 = cadenaAux;
                    if (txtBusqueda.Text.Length != numeroMaximo)
                    {
                        for (int x = txtBusqueda.Text.Length; x <= numeroMaximo; x++)
                        {
                            query += string.Format("  folio >= {0} AND folio <= {1}  OR", cadenaAux,cadenaAux2);
                            cadenaAux += "0";
                            cadenaAux2 += "9";
                        }
                        query = query.Substring(0, query.Length - 2);
                    }
                    else {
                        query += string.Format("  FOLIO = {0} ", txtBusqueda.Text);
                    }
                    
                }
                else
                {
                    query += string.Format("  RFC LIKE '{0}%' OR NOMBRE_EM LIKE '{1}%'", txtBusqueda.Text, txtBusqueda.Text);
                }
            }
            query += " order by FOLIO DESC limit 100";

            resultado = globales.consulta(query);
            datos.Rows.Clear();
            resultado.ForEach(o => datos.Rows.Add(o["folio"],o["rfc"], o["nombre_em"]));
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            Close();
            Dictionary<string, object> aux = null;
            foreach (var item in resultado) {
                if (Convert.ToString(item["folio"]) == this.folio) {
                    aux = item;
                    break;
                }
            }

            string query = string.Format("select * from datos.D_QUIROG where FOLIO = '{0}'",this.folio);
            List<Dictionary<string, object>> aux2 = null;
            aux2 = globales.consulta(query);

            enviar(aux,aux2);
        }

        private void datos_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            folio = Convert.ToString(datos.Rows[e.Cell.RowIndex].Cells[0].Value);
        }
    }
}
