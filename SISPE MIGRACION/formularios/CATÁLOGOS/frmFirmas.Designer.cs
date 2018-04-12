namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    partial class frmFirmas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFirmas));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnok = new System.Windows.Forms.Button();
            this.lblobsev = new System.Windows.Forms.Label();
            this.lblinic = new System.Windows.Forms.Label();
            this.txtobserv = new System.Windows.Forms.TextBox();
            this.txtinic = new System.Windows.Forms.TextBox();
            this.lbldesc = new System.Windows.Forms.Label();
            this.lblnomb = new System.Windows.Forms.Label();
            this.lblclave = new System.Windows.Forms.Label();
            this.txtdesc = new System.Windows.Forms.TextBox();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.txtclave = new System.Windows.Forms.TextBox();
            this.btnsalir = new System.Windows.Forms.Button();
            this.btnguarda = new System.Windows.Forms.Button();
            this.btnmodifica = new System.Windows.Forms.Button();
            this.btnnuevo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dbfirmas = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbfirmas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1437, 686);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1437, 686);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.btnsalir);
            this.groupBox2.Controls.Add(this.btnguarda);
            this.groupBox2.Controls.Add(this.btnmodifica);
            this.groupBox2.Controls.Add(this.btnnuevo);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1259, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 686);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OPCIONES";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnok);
            this.groupBox3.Controls.Add(this.lblobsev);
            this.groupBox3.Controls.Add(this.lblinic);
            this.groupBox3.Controls.Add(this.txtobserv);
            this.groupBox3.Controls.Add(this.txtinic);
            this.groupBox3.Controls.Add(this.lbldesc);
            this.groupBox3.Controls.Add(this.lblnomb);
            this.groupBox3.Controls.Add(this.lblclave);
            this.groupBox3.Controls.Add(this.txtdesc);
            this.groupBox3.Controls.Add(this.txtnombre);
            this.groupBox3.Controls.Add(this.txtclave);
            this.groupBox3.Location = new System.Drawing.Point(3, 348);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(175, 332);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "NUEVO REGISTRO";
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(50, 303);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(75, 23);
            this.btnok.TabIndex = 10;
            this.btnok.Text = "OK";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // lblobsev
            // 
            this.lblobsev.AutoSize = true;
            this.lblobsev.Enabled = false;
            this.lblobsev.Location = new System.Drawing.Point(47, 246);
            this.lblobsev.Name = "lblobsev";
            this.lblobsev.Size = new System.Drawing.Size(91, 13);
            this.lblobsev.TabIndex = 9;
            this.lblobsev.Text = "Observaciones";
            // 
            // lblinic
            // 
            this.lblinic.AutoSize = true;
            this.lblinic.Enabled = false;
            this.lblinic.Location = new System.Drawing.Point(59, 188);
            this.lblinic.Name = "lblinic";
            this.lblinic.Size = new System.Drawing.Size(54, 13);
            this.lblinic.TabIndex = 8;
            this.lblinic.Text = "Iniciales";
            // 
            // txtobserv
            // 
            this.txtobserv.Enabled = false;
            this.txtobserv.Location = new System.Drawing.Point(0, 262);
            this.txtobserv.Name = "txtobserv";
            this.txtobserv.Size = new System.Drawing.Size(175, 20);
            this.txtobserv.TabIndex = 7;
            // 
            // txtinic
            // 
            this.txtinic.Enabled = false;
            this.txtinic.Location = new System.Drawing.Point(0, 204);
            this.txtinic.Name = "txtinic";
            this.txtinic.Size = new System.Drawing.Size(175, 20);
            this.txtinic.TabIndex = 6;
            // 
            // lbldesc
            // 
            this.lbldesc.AutoSize = true;
            this.lbldesc.Enabled = false;
            this.lbldesc.Location = new System.Drawing.Point(47, 131);
            this.lbldesc.Name = "lbldesc";
            this.lbldesc.Size = new System.Drawing.Size(74, 13);
            this.lbldesc.TabIndex = 5;
            this.lbldesc.Text = "Descripción";
            // 
            // lblnomb
            // 
            this.lblnomb.AutoSize = true;
            this.lblnomb.Enabled = false;
            this.lblnomb.Location = new System.Drawing.Point(59, 73);
            this.lblnomb.Name = "lblnomb";
            this.lblnomb.Size = new System.Drawing.Size(50, 13);
            this.lblnomb.TabIndex = 4;
            this.lblnomb.Text = "Nombre";
            // 
            // lblclave
            // 
            this.lblclave.AutoSize = true;
            this.lblclave.Enabled = false;
            this.lblclave.Location = new System.Drawing.Point(59, 23);
            this.lblclave.Name = "lblclave";
            this.lblclave.Size = new System.Drawing.Size(39, 13);
            this.lblclave.TabIndex = 3;
            this.lblclave.Text = "Clave";
            // 
            // txtdesc
            // 
            this.txtdesc.Enabled = false;
            this.txtdesc.Location = new System.Drawing.Point(0, 147);
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Size = new System.Drawing.Size(175, 20);
            this.txtdesc.TabIndex = 2;
            // 
            // txtnombre
            // 
            this.txtnombre.Enabled = false;
            this.txtnombre.Location = new System.Drawing.Point(0, 89);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(175, 20);
            this.txtnombre.TabIndex = 1;
            // 
            // txtclave
            // 
            this.txtclave.Enabled = false;
            this.txtclave.Location = new System.Drawing.Point(0, 39);
            this.txtclave.Name = "txtclave";
            this.txtclave.Size = new System.Drawing.Size(175, 20);
            this.txtclave.TabIndex = 0;
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
            // btnguarda
            // 
            this.btnguarda.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnguarda.Image = ((System.Drawing.Image)(resources.GetObject("btnguarda.Image")));
            this.btnguarda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnguarda.Location = new System.Drawing.Point(3, 227);
            this.btnguarda.Name = "btnguarda";
            this.btnguarda.Size = new System.Drawing.Size(172, 39);
            this.btnguarda.TabIndex = 6;
            this.btnguarda.Text = "GUARDAR CAMBIOS ";
            this.btnguarda.UseVisualStyleBackColor = false;
            this.btnguarda.Click += new System.EventHandler(this.btnguarda_Click);
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
            this.btnnuevo.Text = "AGREGAR";
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
            this.groupBox1.Controls.Add(this.dbfirmas);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1253, 680);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DESGLOSE DE FIRMAS";
            // 
            // dbfirmas
            // 
            this.dbfirmas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dbfirmas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dbfirmas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbfirmas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dbfirmas.Location = new System.Drawing.Point(3, 19);
            this.dbfirmas.MultiSelect = false;
            this.dbfirmas.Name = "dbfirmas";
            this.dbfirmas.Size = new System.Drawing.Size(1244, 670);
            this.dbfirmas.TabIndex = 0;
            this.dbfirmas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfirmas_CellContentClick);
            this.dbfirmas.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfirmas_CellValueChanged);
            this.dbfirmas.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfirmas_RowEnter);
            this.dbfirmas.RowErrorTextChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.dbfirmas_RowErrorTextChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Clave";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nombre";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Descripción P/ Firma";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Iniciales";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Observaciones";
            this.Column5.Name = "Column5";
            // 
            // frmFirmas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1437, 686);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmFirmas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Firmas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFirmas_FormClosing);
            this.Load += new System.EventHandler(this.frmFirmas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFirmas_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbfirmas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dbfirmas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbldesc;
        private System.Windows.Forms.Label lblnomb;
        private System.Windows.Forms.Label lblclave;
        private System.Windows.Forms.TextBox txtdesc;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.Button btnsalir;
        private System.Windows.Forms.Button btnguarda;
        private System.Windows.Forms.Button btnmodifica;
        private System.Windows.Forms.Button btnnuevo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblobsev;
        private System.Windows.Forms.Label lblinic;
        private System.Windows.Forms.TextBox txtobserv;
        private System.Windows.Forms.TextBox txtinic;
        private System.Windows.Forms.TextBox txtclave;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}