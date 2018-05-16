namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes
{
    partial class frmliquidos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioLiquido = new System.Windows.Forms.RadioButton();
            this.radioImporte = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fechas2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.fechas1 = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.radioLiquido);
            this.panel1.Controls.Add(this.radioImporte);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.fechas2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.fechas1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 311);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SISPE_MIGRACION.Properties.Resources.logo_pensiones;
            this.pictureBox1.Location = new System.Drawing.Point(85, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(299, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // radioLiquido
            // 
            this.radioLiquido.AutoSize = true;
            this.radioLiquido.Location = new System.Drawing.Point(265, 209);
            this.radioLiquido.Name = "radioLiquido";
            this.radioLiquido.Size = new System.Drawing.Size(59, 17);
            this.radioLiquido.TabIndex = 10;
            this.radioLiquido.Text = "Liquido";
            this.radioLiquido.UseVisualStyleBackColor = true;
            // 
            // radioImporte
            // 
            this.radioImporte.AutoSize = true;
            this.radioImporte.Checked = true;
            this.radioImporte.Location = new System.Drawing.Point(97, 209);
            this.radioImporte.Name = "radioImporte";
            this.radioImporte.Size = new System.Drawing.Size(60, 17);
            this.radioImporte.TabIndex = 9;
            this.radioImporte.TabStop = true;
            this.radioImporte.Text = "Importe";
            this.radioImporte.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "&Generar reporte";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Fecha final:";
            // 
            // fechas2
            // 
            this.fechas2.Location = new System.Drawing.Point(97, 159);
            this.fechas2.Name = "fechas2";
            this.fechas2.Size = new System.Drawing.Size(277, 20);
            this.fechas2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fecha inicio:";
            // 
            // fechas1
            // 
            this.fechas1.Location = new System.Drawing.Point(97, 101);
            this.fechas1.Name = "fechas1";
            this.fechas1.Size = new System.Drawing.Size(277, 20);
            this.fechas1.TabIndex = 4;
            // 
            // frmliquidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 311);
            this.Controls.Add(this.panel1);
            this.Name = "frmliquidos";
            this.Text = "Liquidos";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioLiquido;
        private System.Windows.Forms.RadioButton radioImporte;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker fechas2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker fechas1;
    }
}