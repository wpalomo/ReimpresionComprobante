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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ctrl_BotonCancelar1 = new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonCancelar();
            this.ctrl_BotonAceptar1 = new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonAceptar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 452);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(336, 23);
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
            this.label6.Location = new System.Drawing.Point(233, 68);
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(116, 116);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(481, 20);
            this.textBox1.TabIndex = 26;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 151);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(585, 229);
            this.dataGridView1.TabIndex = 27;
            // 
            // ctrl_BotonCancelar1
            // 
            this.ctrl_BotonCancelar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonCancelar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ctrl_BotonCancelar1.FondoColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonCancelar1.Location = new System.Drawing.Point(524, 400);
            this.ctrl_BotonCancelar1.Name = "ctrl_BotonCancelar1";
            this.ctrl_BotonCancelar1.Size = new System.Drawing.Size(75, 75);
            this.ctrl_BotonCancelar1.TabIndex = 11;
            this.ctrl_BotonCancelar1.ControlClicked += new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonCancelar.ControlClickedHandler(this.ctrl_BotonCancelar1_ControlClicked);
            // 
            // ctrl_BotonAceptar1
            // 
            this.ctrl_BotonAceptar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonAceptar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ctrl_BotonAceptar1.FondoColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonAceptar1.Location = new System.Drawing.Point(524, 34);
            this.ctrl_BotonAceptar1.Name = "ctrl_BotonAceptar1";
            this.ctrl_BotonAceptar1.Size = new System.Drawing.Size(75, 75);
            this.ctrl_BotonAceptar1.TabIndex = 0;
            this.ctrl_BotonAceptar1.ControlClicked += new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonAceptar.ControlClickedHandler(this.ctrl_BotonAceptar1_ControlClicked);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(30, 401);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(113, 17);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Enviar a impresora";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(162, 400);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(107, 17);
            this.checkBox2.TabIndex = 29;
            this.checkBox2.Text = "Enviar por correo";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // FormReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(614, 516);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

