using System;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes
{
    partial class frmPagares
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
            this.radioDes = new System.Windows.Forms.RadioButton();
            this.radioCentral = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fe2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.fe1 = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.radioDes);
            this.panel1.Controls.Add(this.radioCentral);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.fe2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.fe1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 301);
            this.panel1.TabIndex = 2;
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
            // radioDes
            // 
            this.radioDes.AutoSize = true;
            this.radioDes.Location = new System.Drawing.Point(265, 209);
            this.radioDes.Name = "radioDes";
            this.radioDes.Size = new System.Drawing.Size(101, 17);
            this.radioDes.TabIndex = 10;
            this.radioDes.Text = "Descentralizado";
            this.radioDes.UseVisualStyleBackColor = true;
            // 
            // radioCentral
            // 
            this.radioCentral.AutoSize = true;
            this.radioCentral.Checked = true;
            this.radioCentral.Location = new System.Drawing.Point(97, 209);
            this.radioCentral.Name = "radioCentral";
            this.radioCentral.Size = new System.Drawing.Size(83, 17);
            this.radioCentral.TabIndex = 9;
            this.radioCentral.TabStop = true;
            this.radioCentral.Text = "Centralizado";
            this.radioCentral.UseVisualStyleBackColor = true;
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
            // fe2
            // 
            this.fe2.Location = new System.Drawing.Point(97, 159);
            this.fe2.Name = "fe2";
            this.fe2.Size = new System.Drawing.Size(277, 20);
            this.fe2.TabIndex = 6;
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
            // fe1
            // 
            this.fe1.Location = new System.Drawing.Point(97, 101);
            this.fe1.Name = "fe1";
            this.fe1.Size = new System.Drawing.Size(277, 20);
            this.fe1.TabIndex = 4;
            // 
            // frmPagares
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 301);
            this.Controls.Add(this.panel1);
            this.Name = "frmPagares";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPagares";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private void frmPagares_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker fe2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker fe1;
        private System.Windows.Forms.RadioButton radioDes;
        private System.Windows.Forms.RadioButton radioCentral;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}