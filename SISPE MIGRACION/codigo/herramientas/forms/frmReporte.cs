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
    public partial class frmReporte : Form
    {
        private bool cargando;
        private bool imprimir;

        public frmReporte(string nombreReporte, string tablaDataSet)
        {
            InitializeComponent();
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.p_quirogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tablas = new SISPE_MIGRACION.reportes.tablas();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);

            ((System.ComponentModel.ISupportInitialize)(this.p_quirogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablas)).BeginInit();
            this.SuspendLayout();
            // 
            // p_quirogBindingSource
            // 
            this.p_quirogBindingSource.DataMember = tablaDataSet;
            this.p_quirogBindingSource.DataSource = this.tablas;
            // 
            // tablas
            // 
            this.tablas.DataSetName = "tablas";
            this.tablas.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.p_quirogBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = string.Format("SISPE_MIGRACION.reportes.{0}.rdlc", nombreReporte);
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(768, 499);
            this.reportViewer1.TabIndex = 0;

            // 
            // frmReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 499);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReporte";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmReporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.p_quirogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablas)).EndInit();
            this.ResumeLayout(false);
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            if (imprimir) {
                MessageBox.Show("Se va a imprimri la solicitud quirografario", "Solicitud Quirografario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reportViewer1.PrintDialog();
                Close();
            }
        }

        private void frmReporte_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();

        }

        internal void cargarDatos(string tablaNombre, object[] objeto, bool imprimir = false) {

            DataTable tabla = tablas.Tables[tablaNombre];

            

            
            foreach (var item in objeto) {
                tabla.Rows.Add((object[])item);
            }
            
            this.reportViewer1.RefreshReport();
            this.imprimir = imprimir;
        }




     
    }
}
