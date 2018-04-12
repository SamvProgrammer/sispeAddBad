namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    partial class frmDescuentosDePensiones
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ROY2 = new System.Windows.Forms.RadioButton();
            this.ROY1 = new System.Windows.Forms.RadioButton();
            this.DED6 = new System.Windows.Forms.TextBox();
            this.DED5 = new System.Windows.Forms.TextBox();
            this.DED4 = new System.Windows.Forms.TextBox();
            this.DED3 = new System.Windows.Forms.TextBox();
            this.PER = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.DED6);
            this.panel1.Controls.Add(this.DED5);
            this.panel1.Controls.Add(this.DED4);
            this.panel1.Controls.Add(this.DED3);
            this.panel1.Controls.Add(this.PER);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 273);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnSalir);
            this.panel2.Controls.Add(this.btnAceptar);
            this.panel2.Location = new System.Drawing.Point(25, 218);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 41);
            this.panel2.TabIndex = 12;
            // 
            // btnSalir
            // 
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(363, 3);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 31);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "&Cancelar";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(282, 3);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 31);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ROY2);
            this.groupBox1.Controls.Add(this.ROY1);
            this.groupBox1.Location = new System.Drawing.Point(25, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccionar";
            // 
            // ROY2
            // 
            this.ROY2.AutoSize = true;
            this.ROY2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ROY2.Location = new System.Drawing.Point(28, 57);
            this.ROY2.Name = "ROY2";
            this.ROY2.Size = new System.Drawing.Size(84, 20);
            this.ROY2.TabIndex = 1;
            this.ROY2.Text = "Mensual";
            this.ROY2.UseVisualStyleBackColor = true;
            // 
            // ROY1
            // 
            this.ROY1.AutoSize = true;
            this.ROY1.Checked = true;
            this.ROY1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ROY1.Location = new System.Drawing.Point(28, 27);
            this.ROY1.Name = "ROY1";
            this.ROY1.Size = new System.Drawing.Size(95, 20);
            this.ROY1.TabIndex = 0;
            this.ROY1.TabStop = true;
            this.ROY1.Text = "Quincenal";
            this.ROY1.UseVisualStyleBackColor = true;
            // 
            // DED6
            // 
            this.DED6.Location = new System.Drawing.Point(284, 191);
            this.DED6.Name = "DED6";
            this.DED6.Size = new System.Drawing.Size(186, 20);
            this.DED6.TabIndex = 10;
            // 
            // DED5
            // 
            this.DED5.Location = new System.Drawing.Point(284, 135);
            this.DED5.Name = "DED5";
            this.DED5.Size = new System.Drawing.Size(186, 20);
            this.DED5.TabIndex = 9;
            // 
            // DED4
            // 
            this.DED4.Location = new System.Drawing.Point(284, 78);
            this.DED4.Name = "DED4";
            this.DED4.Size = new System.Drawing.Size(186, 20);
            this.DED4.TabIndex = 8;
            // 
            // DED3
            // 
            this.DED3.Location = new System.Drawing.Point(284, 26);
            this.DED3.Name = "DED3";
            this.DED3.Size = new System.Drawing.Size(186, 20);
            this.DED3.TabIndex = 7;
            // 
            // PER
            // 
            this.PER.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PER.Location = new System.Drawing.Point(25, 35);
            this.PER.Name = "PER";
            this.PER.Size = new System.Drawing.Size(216, 20);
            this.PER.TabIndex = 6;
            this.PER.TextChanged += new System.EventHandler(this.PER_TextChanged);
            this.PER.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PER_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(281, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Deduc4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(281, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Deduc3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(281, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Deduc2";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(281, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Deduc1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sueldo base:";
            // 
            // frmDescuentosDePensiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 273);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDescuentosDePensiones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Descuentos de pensiones";
            this.Load += new System.EventHandler(this.frmDescuentosDePensiones_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.TextBox DED6;
        internal System.Windows.Forms.TextBox DED5;
        internal System.Windows.Forms.TextBox DED4;
        internal System.Windows.Forms.TextBox DED3;
        internal System.Windows.Forms.TextBox PER;
        internal System.Windows.Forms.RadioButton ROY2;
        internal System.Windows.Forms.RadioButton ROY1;
    }
}