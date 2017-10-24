using ReimpresionComprobante.BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReimpresionComprobante
{
    public partial class FormReporte : Form
    {
        private string Result = string.Empty;
        public FormReporte()
        {
            InitializeComponent();
        }
        

        private void bgWorkerReporte_DoWork(object sender, DoWorkEventArgs e)
        {
            ComprobantesDePagoEmitidos comprobantesEmitidos = new ComprobantesDePagoEmitidos();
            Result = comprobantesEmitidos.generarReporteComprobantesDePagosEmitidos(DateTime.Now.AddMonths(-1), DateTime.Now, "ARRXX", true, bgWorkerReporte);            
        }

        private void bgWorkerReporte_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void bgWorkerReporte_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(string.IsNullOrEmpty(Result))
            {
                MessageBox.Show("¡Reporte generado exitosamente!", "Reporte de Recibos Electrónicos de Pagos - Saari", MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressBar1.Value = 0;
            }
            else
            {
                MessageBox.Show("Ocurrió un problema, no se pudo generar el reporte: " + Environment.NewLine + Result, "Reporte de Recibos Electrónicos de Pagos - Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrl_BotonAceptar1_ControlClicked()
        {
            bgWorkerReporte.RunWorkerAsync();
        }

        private void ctrl_BotonCancelar1_ControlClicked()
        {
            this.Close();
        }
    }
}
