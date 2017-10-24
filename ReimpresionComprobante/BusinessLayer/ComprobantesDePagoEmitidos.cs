using FastReport;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using ReimpresionComprobante.DataAccessLayer;
using ReimpresionComprobante.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimpresionComprobante.BusinessLayer
{
    public class ComprobantesDePagoEmitidos
    {
        public ComprobantesDePagoEmitidos()
        {

        }
        public string generarReporteComprobantesDePagosEmitidos(DateTime fechaInicio, DateTime fechaFin, string idInmobiliaria, bool esPdf, BackgroundWorker worker)
        {
            string rutaFormato = @"C:\Formatos_FR\ComprobantesPagoEmitidos.frx";
            string rutaReporte = @"C:\SaariDB\ComprobantesDePagoEmitidos\";
            try
            {
                if (File.Exists(rutaFormato))
                {
                    ComprobantePagoDAL PagosDAL = new ComprobantePagoDAL();
                    worker.ReportProgress(10);
                    List<ComprobantePagoEntity> listaComprobantes = PagosDAL.getComprobantesDePago(worker);
                    if (listaComprobantes.Count > 0)
                    {
                        Report report = new Report();
                        report.Load(rutaFormato);
                        report.RegisterData(listaComprobantes, "Pago");
                        DataBand bandaComprobantes = new DataBand();
                        bandaComprobantes = report.FindObject("DataRecibo") as DataBand;
                        bandaComprobantes.DataSource = report.GetDataSource("Pago");
                        report.Prepare();
                        worker.ReportProgress(85);
                        string filename = string.Empty;

                        if (esPdf)
                        {
                            filename = rutaReporte + @"ComprobantesDePagosEmitidos_" + DateTime.Now.ToShortDateString().Replace('/', '_') + DateTime.Now.ToLongTimeString().Replace(':', '_') + ".pdf";
                            PDFExport export = new PDFExport();
                            report.Export(export, filename);
                        }
                        else
                        {
                            filename = rutaReporte + @"ComprobantesDePagosEmitidos_" + DateTime.Now.ToShortDateString().Replace('/', '_') + DateTime.Now.ToLongTimeString().Replace(':', '_') + ".xlsx";
                            Excel2007Export export = new Excel2007Export();
                            report.Export(export, filename);
                        }
                        worker.ReportProgress(90);
                        report.Dispose();
                        worker.ReportProgress(100);
                        try
                        {
                            System.Diagnostics.Process.Start(filename);
                        }
                        catch
                        {
                            return "Existión un problema al intentar abrir el reporte.";
                        }
                        return string.Empty;
                    }
                    else
                        return "No se encontraron registros con los parámetros indicados.";
                }
                else
                    return "No se encontró el formato: " + rutaFormato;

            }
            catch (Exception ex)
            {
                return "Error general al generar el reporte de comprobantes de pago emitidos: " + Environment.NewLine + ex.Message;
            }
        }

    }
}
