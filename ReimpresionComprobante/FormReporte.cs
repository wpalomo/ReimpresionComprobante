using ReimpresionComprobante.BusinessLayer;
using ReimpresionComprobante.DataAccessLayer;
using ReimpresionComprobante.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
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
        private List<DatosGridEntity> list;
        private ReimpresionComprobantes reimpre = null;
        private string printerName = string.Empty;

        private InmobiliariaEntity inmobiliariaSeleccionada = new InmobiliariaEntity();

        public FormReporte(string nombreUsuario)
        {
            InitializeComponent();            
            Acceso.Usuario = nombreUsuario;
            Configuraciones.CadenaConexionODBC = Properties.Settings.Default.ConnectionStringODBC;
            Configuraciones.CadenaConexionSQLServer = Properties.Settings.Default.ConnectionStringCFD;
            Configuraciones.Asunto = Properties.Settings.Default.AsuntoCobro;
            Configuraciones.Cuerpo = Properties.Settings.Default.CuerpoCobro;
            reimpre = new ReimpresionComprobantes(Configuraciones.CadenaConexionODBC, Acceso.Usuario, Configuraciones.CadenaConexionSQLServer);
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

                comboBoxInmobiliaria.DataSource = reimpre.GetInmobiliarias();
                comboBoxInmobiliaria.ValueMember = "ID";
                comboBoxInmobiliaria.DisplayMember = "NombreComercial";
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
                textBoxCliente.Clear();

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
                
                list = reimpre.GetDatosGrid(elFiltro);

                SetGridSource(list);
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

        private void SetGridSource(List<DatosGridEntity> laLista)
        {
            try
            {
                var source = new BindingSource
                {
                    DataSource = laLista
                };
                dataGridViewComprobantes.DataSource = source;

                dataGridViewComprobantes.Columns["ID"].Width = 50;//ID
                dataGridViewComprobantes.Columns["ID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dataGridViewComprobantes.Columns["ID"].Visible = false;
                dataGridViewComprobantes.Columns["Serie"].Width = 50;//Serie
                dataGridViewComprobantes.Columns["Serie"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["Serie"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["Folio"].Width = 50;//Folio
                dataGridViewComprobantes.Columns["Folio"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["Folio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["FechaEmision"].Width = 75;//Fecha
                dataGridViewComprobantes.Columns["FechaEmision"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["FechaEmision"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["ID_Cliente"].Visible = false;//ID_Cliente
                dataGridViewComprobantes.Columns["Cliente"].Width = 150;//Cliente
                dataGridViewComprobantes.Columns["Cliente"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["Cliente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewComprobantes.Columns["Moneda"].Width = 75;//Monea
                dataGridViewComprobantes.Columns["Moneda"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["Moneda"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["Total"].Width = 75;//Total
                dataGridViewComprobantes.Columns["Total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewComprobantes.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewComprobantes.Columns["IDPago"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + Environment.NewLine + ex.Message, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            catch { } 
            return false;
        }

        private void textBoxCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (list != null)
                {
                    if(list.Count > 0)
                    {
                        if (sender is TextBox textBox)
                        {
                            string theText = textBox.Text;
                            if(theText.Length > 0)
                            {
                                List<DatosGridEntity> listaLocal = list.Where(s => s.Cliente.Contains(theText)).ToList();
                                SetGridSource(listaLocal);
                            }
                            else
                            {
                                SetGridSource(list);
                            }
                        }
                    }                                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + Environment.NewLine + ex.Message, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewComprobantes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int idCompro = (int)dataGridViewComprobantes.Rows[e.RowIndex].Cells[0].Value;
            }
            catch
            {

            }
        }

        private void dataGridViewComprobantes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {                
                int idCompro = (int)dataGridViewComprobantes.SelectedRows[0].Cells["ID"].Value;
            }
            catch
            {

            }
        }

        private void buttonPDF_Click(object sender, EventArgs e)
        {
            var cursorActual = Cursor.Current;
            try
            {
                
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (checkBoxImpresora.Checked)
                        {
                            Configuraciones.EnviarImprimir = true;
                        }
                        else
                        {
                            Configuraciones.EnviarImprimir = false;
                        }
                        int rowindex = dataGridViewComprobantes.CurrentCell.RowIndex;
                        int idCompro = Convert.ToInt32(dataGridViewComprobantes.CurrentRow.Cells["ID"].Value.ToString());                        
                        string elxml = reimpre.GetXmlComprobante(idCompro, inmobiliariaSeleccionada.ID);
                        if (!string.IsNullOrEmpty(elxml))
                        {
                            Cursor.Current = cursorActual;
                            MessageBox.Show("Error: " + Environment.NewLine + elxml, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                           if(checkBoxCorreo.Checked)
                           {
                                string idcliente = dataGridViewComprobantes.CurrentRow.Cells["ID_Cliente"].Value.ToString();
                                string elcorreo = reimpre.EnviarCorreo(idCompro, inmobiliariaSeleccionada.ID, idcliente);
                                if (string.IsNullOrEmpty(elcorreo))
                                {
                                    //Cursor.Current = cursorActual;
                                    MessageBox.Show("Exito: " + Environment.NewLine + "Su correo fue enviado de forma exitosa", "Saari", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    CrearLogError(elcorreo);
                                }
                           }                            
                        }
                    }
                    else
                    {
                        Cursor.Current = cursorActual;
                        MessageBox.Show("Error: " + Environment.NewLine + "No hay comprobantes en la lista, debe realizar una busqueda", "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error: " + Environment.NewLine + "No hay comprobantes en la lista, debe realizar una busqueda", "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + Environment.NewLine + ex.Message, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = cursorActual;
            }
        }

        private void buttonCorreo_Click(object sender, EventArgs e)
        {
            var cursorActual = Cursor.Current;
            try
            {
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (checkBoxImpresora.Checked)
                        {
                            Configuraciones.EnviarImprimir = true;
                        }
                        else
                        {
                            Configuraciones.EnviarImprimir = false;
                        }

                        int idCompro = Convert.ToInt32(dataGridViewComprobantes.CurrentRow.Cells["ID"].Value.ToString());
                        
                        string idcliente = dataGridViewComprobantes.CurrentRow.Cells["ID_Cliente"].Value.ToString();
                        string elcorreo = reimpre.EnviarCorreo(idCompro, inmobiliariaSeleccionada.ID, idcliente);
                        if (string.IsNullOrEmpty(elcorreo))
                        {
                            Cursor.Current = cursorActual;
                            MessageBox.Show("Exito: " + Environment.NewLine + "Su correo fue enviado de forma exitosa", "Saari", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //log
                            Cursor.Current = cursorActual;
                            CrearLogError(elcorreo);
                        }
                    }
                    else
                    {
                        Cursor.Current = cursorActual;
                        MessageBox.Show("Error: " + Environment.NewLine + "No hay comprobantes en la lista, debe realizar una busqueda", "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Cursor.Current = cursorActual;
                    MessageBox.Show("Error: " + Environment.NewLine + "No hay comprobantes en la lista, debe realizar una busqueda", "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = cursorActual;
                MessageBox.Show("Error: " + Environment.NewLine + ex.Message, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = cursorActual;
            }
        }


        private void CrearLogError(string mensajecorreo)
        {
            string serie = dataGridViewComprobantes.CurrentRow.Cells["Serie"].Value.ToString();
            string folio = dataGridViewComprobantes.CurrentRow.Cells["Folio"].Value.ToString();
            string cliente = dataGridViewComprobantes.CurrentRow.Cells["ID_Cliente"].Value.ToString();
            int idcompro = Convert.ToInt32(dataGridViewComprobantes.CurrentRow.Cells["ID"].Value.ToString());

            string fecha = DateTime.Now.ToString("ddMMyyyy");
            string directorio = reimpre.GetRutaLog(idcompro);

            Directory.CreateDirectory(directorio);
            string nombreLog = "ErrorEnvioComprobantePago_" + serie.Trim() + "_" + folio.Trim() + "_" + cliente.Trim() + "_" + fecha + ".txt";
            var LogPath = directorio + @"\" + nombreLog;
            string logmessage = mensajecorreo + " " + " en fecha: " + DateTime.Now.ToString();

            if (!File.Exists(LogPath))
            {
                File.Create(LogPath).Dispose();
                using (StreamWriter tw = new StreamWriter(LogPath))
                {
                    tw.WriteLine(logmessage);
                    tw.Close();
                }
            }
            else if (File.Exists(LogPath))
            {
                using (StreamWriter tw = new StreamWriter(LogPath, true))
                {
                    tw.WriteLine(logmessage);
                    tw.Close();
                }
            }
            MessageBox.Show("Error: " + Environment.NewLine + "No se pudo enviar el correo, consulte el Log en:" + Environment.NewLine + LogPath, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void checkBoxImpresora_CheckedChanged(object sender, EventArgs e)
        {
            PrinterSettings printSet = new PrinterSettings();
            printerName = printSet.PrinterName;
            int copies = 1;
            if (checkBoxImpresora.Checked == true)
            {
                try
                {
                    PrintDialog printDialog = new PrintDialog();
                    DialogResult result = printDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Configuraciones.EnviarImprimir = true;
                        copies = printDialog.PrinterSettings.Copies;
                        printerName = printDialog.PrinterSettings.PrinterName;
                        Configuraciones.PrintName = printerName;
                        Configuraciones.Copies = copies;
                    }
                    else
                    {
                        checkBoxImpresora.Checked = false;
                        Configuraciones.EnviarImprimir = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido el siguiente error:\n - " + ex.Message, "Saari-Cobranza", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
