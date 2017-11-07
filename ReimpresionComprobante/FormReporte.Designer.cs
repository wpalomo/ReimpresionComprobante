namespace ReimpresionComprobante
{
    partial class FormReporte
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReporte));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxInmobiliaria = new System.Windows.Forms.ComboBox();
            this.dateInicio = new System.Windows.Forms.DateTimePicker();
            this.dateFin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSerie = new System.Windows.Forms.TextBox();
            this.textBoxSerieDel = new System.Windows.Forms.TextBox();
            this.textBoxSerieAl = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxCliente = new System.Windows.Forms.TextBox();
            this.checkBoxImpresora = new System.Windows.Forms.CheckBox();
            this.checkBoxCorreo = new System.Windows.Forms.CheckBox();
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.buttonPDF = new System.Windows.Forms.Button();
            this.buttonCorreo = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.dataGridViewComprobantes = new System.Windows.Forms.DataGridView();
            this.bgWorkerReporte = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComprobantes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 466);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(334, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Inmobiliaria";
            // 
            // comboBoxInmobiliaria
            // 
            this.comboBoxInmobiliaria.FormattingEnabled = true;
            this.comboBoxInmobiliaria.Location = new System.Drawing.Point(77, 9);
            this.comboBoxInmobiliaria.Name = "comboBoxInmobiliaria";
            this.comboBoxInmobiliaria.Size = new System.Drawing.Size(522, 21);
            this.comboBoxInmobiliaria.TabIndex = 12;
            this.comboBoxInmobiliaria.SelectedIndexChanged += new System.EventHandler(this.comboBoxInmobiliaria_SelectedIndexChanged);
            // 
            // dateInicio
            // 
            this.dateInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateInicio.Location = new System.Drawing.Point(48, 22);
            this.dateInicio.Name = "dateInicio";
            this.dateInicio.Size = new System.Drawing.Size(153, 20);
            this.dateInicio.TabIndex = 15;
            // 
            // dateFin
            // 
            this.dateFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFin.Location = new System.Drawing.Point(48, 50);
            this.dateFin.Name = "dateFin";
            this.dateFin.Size = new System.Drawing.Size(153, 20);
            this.dateFin.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Del:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Al:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Serie";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(109, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Del";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(198, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Al";
            // 
            // textBoxSerie
            // 
            this.textBoxSerie.Location = new System.Drawing.Point(38, 28);
            this.textBoxSerie.Name = "textBoxSerie";
            this.textBoxSerie.Size = new System.Drawing.Size(66, 20);
            this.textBoxSerie.TabIndex = 22;
            // 
            // textBoxSerieDel
            // 
            this.textBoxSerieDel.Location = new System.Drawing.Point(133, 28);
            this.textBoxSerieDel.Name = "textBoxSerieDel";
            this.textBoxSerieDel.Size = new System.Drawing.Size(60, 20);
            this.textBoxSerieDel.TabIndex = 23;
            // 
            // textBoxSerieAl
            // 
            this.textBoxSerieAl.Location = new System.Drawing.Point(217, 28);
            this.textBoxSerieAl.Name = "textBoxSerieAl";
            this.textBoxSerieAl.Size = new System.Drawing.Size(60, 20);
            this.textBoxSerieAl.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Filtrar por Cliente";
            // 
            // textBoxCliente
            // 
            this.textBoxCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxCliente.Location = new System.Drawing.Point(116, 127);
            this.textBoxCliente.Name = "textBoxCliente";
            this.textBoxCliente.Size = new System.Drawing.Size(481, 20);
            this.textBoxCliente.TabIndex = 26;
            this.textBoxCliente.TextChanged += new System.EventHandler(this.textBoxCliente_TextChanged);
            // 
            // checkBoxImpresora
            // 
            this.checkBoxImpresora.AutoSize = true;
            this.checkBoxImpresora.Location = new System.Drawing.Point(15, 420);
            this.checkBoxImpresora.Name = "checkBoxImpresora";
            this.checkBoxImpresora.Size = new System.Drawing.Size(113, 17);
            this.checkBoxImpresora.TabIndex = 28;
            this.checkBoxImpresora.Text = "Enviar a impresora";
            this.checkBoxImpresora.UseVisualStyleBackColor = true;
            this.checkBoxImpresora.CheckedChanged += new System.EventHandler(this.checkBoxImpresora_CheckedChanged);
            // 
            // checkBoxCorreo
            // 
            this.checkBoxCorreo.AutoSize = true;
            this.checkBoxCorreo.Location = new System.Drawing.Point(157, 420);
            this.checkBoxCorreo.Name = "checkBoxCorreo";
            this.checkBoxCorreo.Size = new System.Drawing.Size(107, 17);
            this.checkBoxCorreo.TabIndex = 29;
            this.checkBoxCorreo.Text = "Enviar por correo";
            this.checkBoxCorreo.UseVisualStyleBackColor = true;
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.buttonBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBuscar.BackgroundImage")));
            this.buttonBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonBuscar.Location = new System.Drawing.Point(524, 48);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(75, 70);
            this.buttonBuscar.TabIndex = 30;
            this.buttonBuscar.UseVisualStyleBackColor = false;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // buttonPDF
            // 
            this.buttonPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.buttonPDF.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPDF.BackgroundImage")));
            this.buttonPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonPDF.Location = new System.Drawing.Point(355, 420);
            this.buttonPDF.Name = "buttonPDF";
            this.buttonPDF.Size = new System.Drawing.Size(75, 70);
            this.buttonPDF.TabIndex = 31;
            this.buttonPDF.UseVisualStyleBackColor = false;
            this.buttonPDF.Click += new System.EventHandler(this.buttonPDF_Click);
            // 
            // buttonCorreo
            // 
            this.buttonCorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.buttonCorreo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCorreo.BackgroundImage")));
            this.buttonCorreo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCorreo.Location = new System.Drawing.Point(439, 420);
            this.buttonCorreo.Name = "buttonCorreo";
            this.buttonCorreo.Size = new System.Drawing.Size(75, 70);
            this.buttonCorreo.TabIndex = 32;
            this.buttonCorreo.UseVisualStyleBackColor = false;
            this.buttonCorreo.Click += new System.EventHandler(this.buttonCorreo_Click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.buttonCancelar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCancelar.BackgroundImage")));
            this.buttonCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCancelar.Location = new System.Drawing.Point(520, 420);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 70);
            this.buttonCancelar.TabIndex = 33;
            this.buttonCancelar.UseVisualStyleBackColor = false;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // dataGridViewComprobantes
            // 
            this.dataGridViewComprobantes.AllowUserToAddRows = false;
            this.dataGridViewComprobantes.AllowUserToDeleteRows = false;
            this.dataGridViewComprobantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComprobantes.Location = new System.Drawing.Point(12, 156);
            this.dataGridViewComprobantes.Name = "dataGridViewComprobantes";
            this.dataGridViewComprobantes.ReadOnly = true;
            this.dataGridViewComprobantes.Size = new System.Drawing.Size(587, 252);
            this.dataGridViewComprobantes.TabIndex = 34;
            this.dataGridViewComprobantes.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewComprobantes_CellMouseDoubleClick);
            this.dataGridViewComprobantes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewComprobantes_KeyDown);
            // 
            // bgWorkerReporte
            // 
            this.bgWorkerReporte.WorkerReportsProgress = true;
            this.bgWorkerReporte.WorkerSupportsCancellation = true;
            this.bgWorkerReporte.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerReporte_DoWork);
            this.bgWorkerReporte.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerReporte_ProgressChanged);
            this.bgWorkerReporte.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerReporte_RunWorkerCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateInicio);
            this.groupBox1.Controls.Add(this.dateFin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 82);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rango por Fecha";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxSerieAl);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxSerie);
            this.groupBox2.Controls.Add(this.textBoxSerieDel);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(233, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 82);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rango por Serie";
            // 
            // FormReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(614, 502);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewComprobantes);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonCorreo);
            this.Controls.Add(this.buttonPDF);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.checkBoxCorreo);
            this.Controls.Add(this.checkBoxImpresora);
            this.Controls.Add(this.textBoxCliente);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBoxInmobiliaria);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reimpresión de Comprobantes de Pago";
            this.Load += new System.EventHandler(this.FormReporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComprobantes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxInmobiliaria;
        private System.Windows.Forms.DateTimePicker dateInicio;
        private System.Windows.Forms.DateTimePicker dateFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSerie;
        private System.Windows.Forms.TextBox textBoxSerieDel;
        private System.Windows.Forms.TextBox textBoxSerieAl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxCliente;
        private System.Windows.Forms.CheckBox checkBoxImpresora;
        private System.Windows.Forms.CheckBox checkBoxCorreo;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.Button buttonPDF;
        private System.Windows.Forms.Button buttonCorreo;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.DataGridView dataGridViewComprobantes;
        private System.ComponentModel.BackgroundWorker bgWorkerReporte;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

