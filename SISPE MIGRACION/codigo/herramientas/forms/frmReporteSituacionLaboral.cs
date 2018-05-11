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
    public partial class frmReporteSituacionLaboral : Form
    {
        private string texto = string.Empty;
        public frmReporteSituacionLaboral(string texto)
        {
            InitializeComponent();
            this.texto = texto;
        }

        private void frmReporteSituacionLaboral_Load(object sender, EventArgs e)
        {
            try
            {
                txtArea.Text = texto;
            }
            catch {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("¿Deseas imprimir el reporte?","Iprimiendo reporte",MessageBoxButtons.YesNo,MessageBoxIcon.Information)) {
                printDialog1.Document = printDocument1;

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Font drawFont = new Font("Arial", 6);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            PointF drawPoint = new PointF(5.0F, 5.0F);
            e.Graphics.DrawString(texto, drawFont, drawBrush,drawPoint);
        }
    }
}
