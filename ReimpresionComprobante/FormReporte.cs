using ReimpresionComprobante.BusinessLayer;
using ReimpresionComprobante.DataAccessLayer;
using ReimpresionComprobante.Entities;
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
        private CobranzaDAL cobranzaDAL = null;

        private InmobiliariaEntity inmobiliariaSeleccionada = new InmobiliariaEntity();

        public FormReporte(string nombreUsuario)
        {
            InitializeComponent();            
            Acceso.Usuario = nombreUsuario;
            Configuraciones.CadenaConexionODBC = Properties.Settings.Default.ConnectionStringODBC;
            Configuraciones.CadenaConexionSQLServer = Properties.Settings.Default.ConnectionStringCFD;
            Configuraciones.Asunto = Properties.Settings.Default.AsuntoCobro;
            Configuraciones.Cuerpo = Properties.Settings.Default.CuerpoCobro;
            inmoDAL = new SaariDAL(Configuraciones.CadenaConexionODBC, Acceso.Usuario, Configuraciones.CadenaConexionSQLServer);
            cobranzaDAL = new CobranzaDAL(Configuraciones.CadenaConexionSQLServer, Acceso.Usuario);
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
            //if(string.IsNullOrEmpty(Result))
            //{
            //    MessageBox.Show("¡Reporte generado exitosamente!", "Reporte de Recibos Electrónicos de Pagos - Saari", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    progressBar1.Value = 0;
            //}
            //else
            //{
            //    MessageBox.Show("Ocurrió un problema, no se pudo generar el reporte: " + Environment.NewLine + Result, "Reporte de Recibos Electrónicos de Pagos - Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            FiltrosEntity elFiltro = null;
            var cursorActual = Cursor.Current;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                elFiltro = new FiltrosEntity();

                var lainmo = inmobiliariaSeleccionada.ID;
                var nominmo = inmobiliariaSeleccionada.RazonSocial;

                elFiltro.Inmobiliaria = lainmo.ToString();
                elFiltro.NombreInmobiliaria = nominmo;
                elFiltro.FechaDel = dateInicio.Value.Date;
                elFiltro.FechaAl = dateFin.Value.Date;
                elFiltro.Serie = textBoxSerie.Text;
                if(textBoxSerieDel.Text.Length > 0)
                {
                    if(IsNumeric(textBoxSerieDel.Text))
                    {
                        elFiltro.SerieDel = Convert.ToInt32(textBoxSerieDel.Text);
                    }
                }
                if(textBoxSerieAl.Text.Length > 0)
                {
                    if(IsNumeric(textBoxSerieAl.Text))
                    {
                        elFiltro.SerieAl = Convert.ToInt32(textBoxSerieAl.Text);
                    }
                }
                elFiltro.Cliente = textBoxCliente.Text;

                var source = new BindingSource();
                List<DatosGridEntity> list; 

                list = cobranzaDAL.getDatosGrid(elFiltro);

                source.DataSource = list;
                dataGridView1.DataSource = source;

                dataGridView1.Columns[0].Width = 50;//ID
                dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[1].Width = 50;//Serie
                dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[2].Width = 50;//Folio
                dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[3].Width = 75;//Fecha
                dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[4].Width = 150;//Cliente
                dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[5].Width = 75;//Monea
                dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[6].Width = 75;//Total
                dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + Environment.NewLine + ex.Message, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = cursorActual;
            }
            //bgWorkerReporte.RunWorkerAsync();
        }

        private void comboBoxInmobiliaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {         
                inmobiliariaSeleccionada = comboBoxInmobiliaria.SelectedItem as InmobiliariaEntity;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + Environment.NewLine + ex.Message, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Decimal.Parse(Expression as string);
                else
                    Decimal.Parse(Expression.ToString());
                return true;
            }
            catch { } // descartar errores y retornar falso
            return false;
        }


    }
}
