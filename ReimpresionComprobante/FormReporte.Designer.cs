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
            this.ctrl_BotonAceptar1 = new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonAceptar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrl_BotonCancelar1 = new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonCancelar();
            this.bgWorkerReporte = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // ctrl_BotonAceptar1
            // 
            this.ctrl_BotonAceptar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonAceptar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ctrl_BotonAceptar1.FondoColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonAceptar1.Location = new System.Drawing.Point(216, 95);
            this.ctrl_BotonAceptar1.Name = "ctrl_BotonAceptar1";
            this.ctrl_BotonAceptar1.Size = new System.Drawing.Size(75, 75);
            this.ctrl_BotonAceptar1.TabIndex = 0;
            this.ctrl_BotonAceptar1.ControlClicked += new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonAceptar.ControlClickedHandler(this.ctrl_BotonAceptar1_ControlClicked);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 176);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(343, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Seleccione Aceptar para generar el reporte o Cancelar para salir.";
            // 
            // ctrl_BotonCancelar1
            // 
            this.ctrl_BotonCancelar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonCancelar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ctrl_BotonCancelar1.FondoColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ctrl_BotonCancelar1.Location = new System.Drawing.Point(297, 95);
            this.ctrl_BotonCancelar1.Name = "ctrl_BotonCancelar1";
            this.ctrl_BotonCancelar1.Size = new System.Drawing.Size(75, 75);
            this.ctrl_BotonCancelar1.TabIndex = 11;
            this.ctrl_BotonCancelar1.ControlClicked += new ReimpresionComprobante.PresentationLayer.Controls.Ctrl_BotonCancelar.ControlClickedHandler(this.ctrl_BotonCancelar1_ControlClicked);
            // 
            // bgWorkerReporte
            // 
            this.bgWorkerReporte.WorkerReportsProgress = true;
            this.bgWorkerReporte.WorkerSupportsCancellation = true;
            this.bgWorkerReporte.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerReporte_DoWork);
            this.bgWorkerReporte.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerReporte_ProgressChanged);
            this.bgWorkerReporte.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerReporte_RunWorkerCompleted);
            // 
            // FormReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(384, 211);
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
            this.Text = "Reporte";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PresentationLayer.Controls.Ctrl_BotonAceptar ctrl_BotonAceptar1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private PresentationLayer.Controls.Ctrl_BotonCancelar ctrl_BotonCancelar1;
        private System.ComponentModel.BackgroundWorker bgWorkerReporte;
    }
}

