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
            this.bgWorkerReporte = new System.ComponentModel.BackgroundWorker();
            this.comboBoxInmobiliaria = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.buttonPDF = new System.Windows.Forms.Button();
            this.buttonCorreo = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ctrl_BotonCancelar1 = new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonCancelar();
            this.ctrl_BotonAceptar1 = new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonAceptar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 452);
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
            // bgWorkerReporte
            // 
            this.bgWorkerReporte.WorkerReportsProgress = true;
            this.bgWorkerReporte.WorkerSupportsCancellation = true;
            this.bgWorkerReporte.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerReporte_DoWork);
            this.bgWorkerReporte.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerReporte_ProgressChanged);
            this.bgWorkerReporte.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerReporte_RunWorkerCompleted);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Rango por Fecha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Rango por Serie";
            // 
            // dateInicio
            // 
            this.dateInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateInicio.Location = new System.Drawing.Point(48, 62);
            this.dateInicio.Name = "dateInicio";
            this.dateInicio.Size = new System.Drawing.Size(168, 20);
            this.dateInicio.TabIndex = 15;
            // 
            // dateFin
            // 
            this.dateFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFin.Location = new System.Drawing.Point(48, 90);
            this.dateFin.Name = "dateFin";
            this.dateFin.Size = new System.Drawing.Size(168, 20);
            this.dateFin.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Del:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Al:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(233, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Serie";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(341, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Del";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(436, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Al";
            // 
            // textBoxSerie
            // 
            this.textBoxSerie.Location = new System.Drawing.Point(270, 61);
            this.textBoxSerie.Name = "textBoxSerie";
            this.textBoxSerie.Size = new System.Drawing.Size(66, 20);
            this.textBoxSerie.TabIndex = 22;
            // 
            // textBoxSerieDel
            // 
            this.textBoxSerieDel.Location = new System.Drawing.Point(370, 61);
            this.textBoxSerieDel.Name = "textBoxSerieDel";
            this.textBoxSerieDel.Size = new System.Drawing.Size(60, 20);
            this.textBoxSerieDel.TabIndex = 23;
            // 
            // textBoxSerieAl
            // 
            this.textBoxSerieAl.Location = new System.Drawing.Point(458, 61);
            this.textBoxSerieAl.Name = "textBoxSerieAl";
            this.textBoxSerieAl.Size = new System.Drawing.Size(60, 20);
            this.textBoxSerieAl.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Filtrar por Cliente";
            // 
            // textBoxCliente
            // 
            this.textBoxCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxCliente.Location = new System.Drawing.Point(116, 116);
            this.textBoxCliente.Name = "textBoxCliente";
            this.textBoxCliente.Size = new System.Drawing.Size(481, 20);
            this.textBoxCliente.TabIndex = 26;
            this.textBoxCliente.TextChanged += new System.EventHandler(this.textBoxCliente_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 406);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(113, 17);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Enviar a impresora";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(157, 406);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(107, 17);
            this.checkBox2.TabIndex = 29;
            this.checkBox2.Text = "Enviar por correo";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBuscar.BackgroundImage")));
            this.buttonBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonBuscar.Location = new System.Drawing.Point(524, 41);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(75, 70);
            this.buttonBuscar.TabIndex = 30;
            this.buttonBuscar.UseVisualStyleBackColor = true;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // buttonPDF
            // 
            this.buttonPDF.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPDF.BackgroundImage")));
            this.buttonPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonPDF.Location = new System.Drawing.Point(355, 406);
            this.buttonPDF.Name = "buttonPDF";
            this.buttonPDF.Size = new System.Drawing.Size(75, 70);
            this.buttonPDF.TabIndex = 31;
            this.buttonPDF.UseVisualStyleBackColor = true;
            // 
            // buttonCorreo
            // 
            this.buttonCorreo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCorreo.BackgroundImage")));
            this.buttonCorreo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCorreo.Location = new System.Drawing.Point(439, 406);
            this.buttonCorreo.Name = "buttonCorreo";
            this.buttonCorreo.Size = new System.Drawing.Size(75, 70);
            this.buttonCorreo.TabIndex = 32;
            this.buttonCorreo.UseVisualStyleBackColor = true;
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCancelar.BackgroundImage")));
            this.buttonCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCancelar.Location = new System.Drawing.Point(522, 406);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 70);
            this.buttonCancelar.TabIndex = 33;
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 142);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(590, 252);
            this.dataGridView1.TabIndex = 34;
            // 
            // ctrl_BotonCancelar1
            // 
            this.ctrl_BotonCancelar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonCancelar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ctrl_BotonCancelar1.FondoColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonCancelar1.Location = new System.Drawing.Point(539, 400);
            this.ctrl_BotonCancelar1.Name = "ctrl_BotonCancelar1";
            this.ctrl_BotonCancelar1.Size = new System.Drawing.Size(75, 75);
            this.ctrl_BotonCancelar1.TabIndex = 11;
            this.ctrl_BotonCancelar1.Visible = false;
            this.ctrl_BotonCancelar1.ControlClicked += new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonCancelar.ControlClickedHandler(this.ctrl_BotonCancelar1_ControlClicked);
            // 
            // ctrl_BotonAceptar1
            // 
            this.ctrl_BotonAceptar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonAceptar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ctrl_BotonAceptar1.FondoColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonAceptar1.Location = new System.Drawing.Point(539, 429);
            this.ctrl_BotonAceptar1.Name = "ctrl_BotonAceptar1";
            this.ctrl_BotonAceptar1.Size = new System.Drawing.Size(75, 75);
            this.ctrl_BotonAceptar1.TabIndex = 0;
            this.ctrl_BotonAceptar1.Visible = false;
            this.ctrl_BotonAceptar1.ControlClicked += new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonAceptar.ControlClickedHandler(this.ctrl_BotonAceptar1_ControlClicked);
            // 
            // FormReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(614, 485);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonCorreo);
            this.Controls.Add(this.buttonPDF);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBoxCliente);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxSerieAl);
            this.Controls.Add(this.textBoxSerieDel);
            this.Controls.Add(this.textBoxSerie);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateFin);
            this.Controls.Add(this.dateInicio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxInmobiliaria);
            this.Controls.Add(this.ctrl_BotonCancelar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ctrl_BotonAceptar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reimpresión de Comprobantes de Pago";
            this.Load += new System.EventHandler(this.FormReporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PresentationLayer.Controls.Ctrl_BotonAceptar ctrl_BotonAceptar1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private PresentationLayer.Controls.Ctrl_BotonCancelar ctrl_BotonCancelar1;
        private System.ComponentModel.BackgroundWorker bgWorkerReporte;
        private System.Windows.Forms.ComboBox comboBoxInmobiliaria;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.Button buttonPDF;
        private System.Windows.Forms.Button buttonCorreo;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

