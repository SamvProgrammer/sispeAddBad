namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    partial class frmProyecto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProyecto));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnok = new System.Windows.Forms.Button();
            this.lbldes = new System.Windows.Forms.Label();
            this.lblproy = new System.Windows.Forms.Label();
            this.txtdes = new System.Windows.Forms.TextBox();
            this.txtproye = new System.Windows.Forms.TextBox();
            this.btnsalir = new System.Windows.Forms.Button();
            this.btnguardar = new System.Windows.Forms.Button();
            this.btnmodifica = new System.Windows.Forms.Button();
            this.btnnuevo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.datos02 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datos02)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 700);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(916, 700);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.btnsalir);
            this.groupBox2.Controls.Add(this.btnguardar);
            this.groupBox2.Controls.Add(this.btnmodifica);
            this.groupBox2.Controls.Add(this.btnnuevo);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(738, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 700);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OPCIONES";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnok);
            this.groupBox3.Controls.Add(this.lbldes);
            this.groupBox3.Controls.Add(this.lblproy);
            this.groupBox3.Controls.Add(this.txtdes);
            this.groupBox3.Controls.Add(this.txtproye);
            this.groupBox3.Location = new System.Drawing.Point(3, 342);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(175, 249);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "NUEVO REGISTRO";
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(51, 177);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(75, 23);
            this.btnok.TabIndex = 5;
            this.btnok.Text = "ACEPTAR";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // lbldes
            // 
            this.lbldes.AutoSize = true;
            this.lbldes.Enabled = false;
            this.lbldes.Location = new System.Drawing.Point(48, 96);
            this.lbldes.Name = "lbldes";
            this.lbldes.Size = new System.Drawing.Size(74, 13);
            this.lbldes.TabIndex = 4;
            this.lbldes.Text = "Descripción";
            // 
            // lblproy
            // 
            this.lblproy.AutoSize = true;
            this.lblproy.Enabled = false;
            this.lblproy.Location = new System.Drawing.Point(59, 34);
            this.lblproy.Name = "lblproy";
            this.lblproy.Size = new System.Drawing.Size(57, 13);
            this.lblproy.TabIndex = 3;
            this.lblproy.Text = "Proyecto";
            // 
            // txtdes
            // 
            this.txtdes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtdes.Enabled = false;
            this.txtdes.Location = new System.Drawing.Point(0, 112);
            this.txtdes.Name = "txtdes";
            this.txtdes.Size = new System.Drawing.Size(175, 20);
            this.txtdes.TabIndex = 1;
            // 
            // txtproye
            // 
            this.txtproye.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtproye.Enabled = false;
            this.txtproye.Location = new System.Drawing.Point(0, 50);
            this.txtproye.Name = "txtproye";
            this.txtproye.Size = new System.Drawing.Size(175, 20);
            this.txtproye.TabIndex = 0;
            // 
            // btnsalir
            // 
            this.btnsalir.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsalir.Location = new System.Drawing.Point(3, 287);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(172, 39);
            this.btnsalir.TabIndex = 7;
            this.btnsalir.Text = "SALIR ";
            this.btnsalir.UseVisualStyleBackColor = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnguardar.Image = ((System.Drawing.Image)(resources.GetObject("btnguardar.Image")));
            this.btnguardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnguardar.Location = new System.Drawing.Point(3, 227);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(172, 39);
            this.btnguardar.TabIndex = 6;
            this.btnguardar.Text = "GUARDAR CAMBIOS ";
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnmodifica
            // 
            this.btnmodifica.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnmodifica.Image = ((System.Drawing.Image)(resources.GetObject("btnmodifica.Image")));
            this.btnmodifica.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnmodifica.Location = new System.Drawing.Point(3, 168);
            this.btnmodifica.Name = "btnmodifica";
            this.btnmodifica.Size = new System.Drawing.Size(172, 39);
            this.btnmodifica.TabIndex = 5;
            this.btnmodifica.Text = "MODIFICAR ";
            this.btnmodifica.UseVisualStyleBackColor = false;
            this.btnmodifica.Click += new System.EventHandler(this.btnmodifica_Click);
            // 
            // btnnuevo
            // 
            this.btnnuevo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnnuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnnuevo.Image")));
            this.btnnuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnnuevo.Location = new System.Drawing.Point(3, 110);
            this.btnnuevo.Name = "btnnuevo";
            this.btnnuevo.Size = new System.Drawing.Size(172, 39);
            this.btnnuevo.TabIndex = 4;
            this.btnnuevo.Text = "NUEVO";
            this.btnnuevo.UseVisualStyleBackColor = false;
            this.btnnuevo.Click += new System.EventHandler(this.btnnuevo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(172, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.datos02);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(731, 700);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DESGLOSE DE PROYECTO";
            // 
            // datos02
            // 
            this.datos02.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datos02.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.datos02.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datos02.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.datos02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datos02.Location = new System.Drawing.Point(3, 16);
            this.datos02.Name = "datos02";
            this.datos02.Size = new System.Drawing.Size(725, 681);
            this.datos02.TabIndex = 0;
            this.datos02.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datos02_CellContentClick);
            this.datos02.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datos02_CellValueChanged);
            this.datos02.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.datos02_RowEnter);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Proyecto";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Descripción";
            this.Column2.Name = "Column2";
            // 
            // frmProyecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 700);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmProyecto";
            this.Text = "Proyecto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProyecto_FormClosing);
            this.Load += new System.EventHandler(this.frmProyecto_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmProyecto_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datos02)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView datos02;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnsalir;
        private System.Windows.Forms.Button btnguardar;
        private System.Windows.Forms.Button btnmodifica;
        private System.Windows.Forms.Button btnnuevo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbldes;
        private System.Windows.Forms.Label lblproy;
        private System.Windows.Forms.TextBox txtdes;
        private System.Windows.Forms.TextBox txtproye;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}