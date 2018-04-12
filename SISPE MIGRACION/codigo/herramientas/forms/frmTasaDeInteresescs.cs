using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    public partial class frmTasaDeInteresescs : Form
    {
        private string tasa_ { get; set; }
        private bool esQuirog { get; set; }
        public List<Dictionary<string, object>> resultado;
        public string tasa {
            get {
                return tasa_;
            }
        }

        public frmTasaDeInteresescs(bool esQuirog = true)
        {
            InitializeComponent();
            this.esQuirog = esQuirog;
        }

        private void frmTasaDeInteresescs_Load(object sender, EventArgs e)
        {
            //Se obtiene los tipos de relación de catalogos.tasas de la DB
            Dictionary<string, string> tipoRelacion = new Dictionary<string, string>();
            tipoRelacion.Add("B","BASE");
            tipoRelacion.Add("C", "CONFIANZA");
            tipoRelacion.Add("J", "JUBILADO");
            tipoRelacion.Add("M", "MANDO MEDIO");
            tipoRelacion.Add("P", "PENSIONADO");
            tipoRelacion.Add("I", "PENSIONISTA");

            foreach (var item in tipoRelacion) {
                RadioButton elementos = new RadioButton();
                elementos.Text = item.Value;
                elementos.Name = item.Key;
                groupRadio.Controls.Add(elementos);
            }
            RadioButton c =(RadioButton) groupRadio.Controls[0];
            c.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control item in  groupRadio.Controls) {
                RadioButton control = (RadioButton)item;
                if (control.Checked) {
                    tasa_ = control.Name;
                    break;
                }
            }
            string query = string.Format("select * from catalogos.tasa where t_prestamo = '{0}' and trel = '{1}' "+
                "order by fmodif desc limit 1;",((this.esQuirog)?"Q":"H"),tasa);

            resultado = globales.consulta(query);
            
            Close();
        }
        
    }
}
