using ReimpresionComprobante.BusinessLayer;
using ReimpresionComprobante.DataAccessLayer;
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
        private string NombreUsuario { get; set; }
        private string Result = string.Empty;
        private SaariDAL inmoDAL = null;

        public FormReporte(string nombreUsuario)
        {
            InitializeComponent();            
            Acceso.Usuario = nombreUsuario;
            Configuraciones.CadenaConexionODBC = Properties.Settings.Default.ConnectionStringODBC;
            Configuraciones.CadenaConexionSQLServer = Properties.Settings.Default.ConnectionStringCFD;
            //Configuraciones.RutaRepositorio = Properties.Settings.Default.RutaRepositorio;
            //Configuraciones.RutaFormatoPagoParcial = Properties.Settings.Default.FormatoPagoParcial;
            Configuraciones.Asunto = Properties.Settings.Default.AsuntoCobro;
            Configuraciones.Cuerpo = Properties.Settings.Default.CuerpoCobro;
            inmoDAL = new SaariDAL(Configuraciones.CadenaConexionODBC, Acceso.Usuario, Configuraciones.CadenaConexionSQLServer);
        }
        

        private void bgWorkerReporte_DoWork(object sender, DoWorkEventArgs e)
        {
            //ComprobantesDePagoEmitidos comprobantesEmitidos = new ComprobantesDePagoEmitidos();
            //Result = comprobantesEmitidos.generarReporteComprobantesDePagosEmitidos(DateTime.Now.AddMonths(-1), DateTime.Now, "ARRXX", true, bgWorkerReporte);            
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

        private void FormReporte_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                int ultimoDia = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, ultimoDia);
                dateInicio.Value = fechaInicio;
                dateFin.Value = fechaFin;

                comboBoxInmobiliaria.DataSource = inmoDAL.getInmobiliarias();
                comboBoxInmobiliaria.ValueMember = "ID";
                comboBoxInmobiliaria.DisplayMember = "RazonSocial";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + Environment.NewLine + ex.Message, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
