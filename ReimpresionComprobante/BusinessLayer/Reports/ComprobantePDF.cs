using System;
using System.IO;
using System.Diagnostics;
using FastReport;
using FastReport.Export.Pdf;
using FastReport.Export.OoXML;
using System.Data;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using ReimpresionComprobante.DataAccessLayer;
using ReimpresionComprobante.Entities;
using System.Windows.Forms;

namespace ReimpresionComprobante.BusinessLayer.Reports
{
    public class ComprobantePDF
    {

        private string nombreArchivo = string.Empty;
        private string PathRutaFormato = string.Empty;
        Directorio csDirectorio = new Directorio();
        private bool esRetimbrado = false;
        Report ReporteCFDIV33 = new Report();
        private CobranzaDAL CobranzaDAL = new CobranzaDAL(Configuraciones.CadenaConexionSQLServer, Acceso.Usuario);
        public ComprobantePDF()
        {
            try
            {
                this.PathRutaFormato = Properties.Settings.Default.FormatoComprobantePago;
            }
            catch
            {
                PathRutaFormato = string.Empty;
            }
        }

        public string GenerarComprobante(Comprobante ComprobanteV33, int IDContribuyente, string rutaFile, bool Esretimbrado, int idComprobante, bool imprimir, bool abrir)
        {
            esRetimbrado = Esretimbrado;
            string MessageError = string.Empty;

            try
            {
                #region EMISOR Y RECEPTOR
                string nombreFile = string.Empty;
                string cantidadLetra = string.Empty;
                Report report = new Report();
                report.Load(PathRutaFormato);


                DataTable dtDatos = new DataTable();
                DataColumn dcNombreEmisor = new DataColumn("NombreEmisor", typeof(string));
                DataColumn dcEmisorRFC = new DataColumn("EmisorRFC", typeof(string));
                DataColumn dcNombreReceptor = new DataColumn("NombreReceptor", typeof(string));
                DataColumn dcReceptorRFC = new DataColumn("ReceptorRFC", typeof(string));
                DataColumn dcFechaPago = new DataColumn("Fecha", typeof(DateTime));
                DataColumn dcFormaPago = new DataColumn("FormaPago", typeof(string));
                DataColumn dcTotalPago = new DataColumn("TotalPago", typeof(decimal));
                DataColumn dcTipoCambio = new DataColumn("TC", typeof(decimal));
                DataColumn dcMoneda = new DataColumn("Moneda", typeof(string));
                DataColumn dcVersionCfdi = new DataColumn("versionCFDI", typeof(string));
                DataColumn dcSerie = new DataColumn("Serie", typeof(string));
                DataColumn dcFolio = new DataColumn("Folio", typeof(string));
                DataColumn dcLugarExpedicion = new DataColumn("LugarExpedicion", typeof(string));
                DataColumn dcMetodoPago = new DataColumn("MetodoPago", typeof(string));
                DataColumn cdCantidadLetra = new DataColumn("CantidadLetra", typeof(string));
                DataColumn cdEmisorRegimen = new DataColumn("EmisorRegimen", typeof(string));
                DataColumn cdTipoComprobante = new DataColumn("TipoComprobante", typeof(string));
                dtDatos.Columns.Add(dcNombreEmisor);
                dtDatos.Columns.Add(dcEmisorRFC);
                dtDatos.Columns.Add(dcNombreReceptor);
                dtDatos.Columns.Add(dcReceptorRFC);
                dtDatos.Columns.Add(dcFechaPago);
                dtDatos.Columns.Add(dcFormaPago);
                dtDatos.Columns.Add(dcTotalPago);
                dtDatos.Columns.Add(dcTipoCambio);
                dtDatos.Columns.Add(dcMoneda);
                dtDatos.Columns.Add(dcVersionCfdi);
                dtDatos.Columns.Add(dcSerie);
                dtDatos.Columns.Add(dcFolio);
                dtDatos.Columns.Add(dcLugarExpedicion);
                dtDatos.Columns.Add(dcMetodoPago);
                dtDatos.Columns.Add(cdCantidadLetra);
                dtDatos.Columns.Add(cdEmisorRegimen);
                dtDatos.Columns.Add(cdTipoComprobante);
                dtDatos.AcceptChanges();

                ComprobantePagoEntity comprobantePago = new ComprobantePagoEntity();
                comprobantePago = CobranzaDAL.GetDatosComprobante(idComprobante);
                if (comprobantePago.TipoCambio <= 0)
                    comprobantePago.TipoCambio = 1;

                try
                {

                    int entereos = 0;
                    decimal decimales = 0;
                    entereos = Convert.ToInt32(Math.Truncate(comprobantePago.Total));
                    decimales = comprobantePago.Total - entereos;
                    decimales = decimales * 100;
                    int d = Convert.ToInt32(decimales);
                    string moneda = string.Empty;
                    if (comprobantePago.Moneda == "MXN")
                        moneda = "PESOS";
                    else if (comprobantePago.Moneda == "USD")
                        moneda = "DOLARES";
                    else
                        moneda = "PESOS";
                    cantidadLetra = NumToLetra.NumeroALetras(Convert.ToString(entereos), Convert.ToString(d), moneda);
                }
                catch { }

                string RegimenFiscal = CobranzaDAL.GetRegimenFiscal(ComprobanteV33.Emisor.RegimenFiscal);
                if (!string.IsNullOrEmpty(RegimenFiscal))
                    ComprobanteV33.Emisor.RegimenFiscal = ComprobanteV33.Emisor.RegimenFiscal + " - " + RegimenFiscal;

                string formaPago = CobranzaDAL.GetFormaPago(idComprobante);
                if (!string.IsNullOrEmpty(formaPago))
                    ComprobanteV33.FormaPago = formaPago;

                string metodoPago = "Pago en parcialidades o diferido";//CobranzaDAL.getMetodoPago("PPD");
                if (!string.IsNullOrEmpty(metodoPago))
                    ComprobanteV33.MetodoPago = "PPD -" + metodoPago;

                string Tipocomprobante = "P - Pago";//CobranzaDAL.getTipoComprobante(ComprobanteV33.TipoDeComprobante);
                if (!string.IsNullOrEmpty(Tipocomprobante))
                    ComprobanteV33.TipoDeComprobante = Tipocomprobante;

                dtDatos.Rows.Add(ComprobanteV33.Emisor.Nombre, ComprobanteV33.Emisor.Rfc, ComprobanteV33.Receptor.Nombre, ComprobanteV33.Receptor.Rfc, ComprobanteV33.Fecha,
ComprobanteV33.FormaPago, comprobantePago.Total, comprobantePago.TipoCambio, comprobantePago.Moneda, ComprobanteV33.Version, ComprobanteV33.Serie, ComprobanteV33.Folio,
ComprobanteV33.LugarExpedicion, ComprobanteV33.MetodoPago, cantidadLetra, ComprobanteV33.Emisor.RegimenFiscal, ComprobanteV33.TipoDeComprobante);
                report.RegisterData(dtDatos, "Datos");
                #endregion

                #region CFDI RELACIONADOS
                try
                {
                    //if (ComprobanteV33.CfdiRelacionados != null)
                    //{
                    DataTable dtCFDisRelacionados = new DataTable();
                    DataColumn dcUUI = new DataColumn("UUID", typeof(string));
                    DataColumn dcTipoRelacion = new DataColumn("TipoRelacion", typeof(string));
                    dtCFDisRelacionados.Columns.Add(dcUUI);
                    dtCFDisRelacionados.Columns.Add(dcTipoRelacion);
                    dtCFDisRelacionados.AcceptChanges();
                    DataBand bandacfdisRelacionados = report.FindObject("Data2") as DataBand;
                    DataBand bandacfdisRelacionadosEncabezado = report.FindObject("Data1") as DataBand;

                    if (ComprobanteV33.CfdiRelacionados != null)
                    {
                        foreach (ComprobanteCfdiRelacionadosCfdiRelacionado cfdiRelacionado in ComprobanteV33.CfdiRelacionados.CfdiRelacionado)
                        {
                            dtCFDisRelacionados.Rows.Add(cfdiRelacionado.UUID, ComprobanteV33.CfdiRelacionados.TipoRelacion);
                        }
                        bandacfdisRelacionados.Visible = true;
                        bandacfdisRelacionadosEncabezado.Visible = true;
                    }
                    report.RegisterData(dtCFDisRelacionados, "cfdiRelacionado");
                    bandacfdisRelacionados.DataSource = report.GetDataSource("cfdiRelacionado");
                    //  }
                }
                catch
                {
                    return MessageError = "Error al intentar leer los datos de los CFDIs Relacionados.";
                }
                #endregion

                #region CONCEPTOS CFDIs


                DataTable dtConceptosCFDIs = new DataTable();
                DataColumn dcClave = new DataColumn("ClaveProdServ", typeof(string));
                DataColumn dcCantidad = new DataColumn("Cantidad", typeof(int));
                DataColumn dcDescripcion = new DataColumn("Descripcion", typeof(string));
                DataColumn dcValorUnitario = new DataColumn("ValorUnitario", typeof(decimal));
                DataColumn dcImporte = new DataColumn("Importe", typeof(decimal));
                dtConceptosCFDIs.Columns.Add(dcClave);
                dtConceptosCFDIs.Columns.Add(dcCantidad);
                dtConceptosCFDIs.Columns.Add(dcDescripcion);
                dtConceptosCFDIs.Columns.Add(dcValorUnitario);
                dtConceptosCFDIs.Columns.Add(dcImporte);

                foreach (ComprobanteConcepto ConceptosCFDI in ComprobanteV33.Conceptos)
                {
                    dtConceptosCFDIs.Rows.Add(ConceptosCFDI.ClaveProdServ, ConceptosCFDI.Cantidad, ConceptosCFDI.Descripcion, ConceptosCFDI.ValorUnitario, ConceptosCFDI.Importe);
                }
                report.RegisterData(dtConceptosCFDIs, "ConceptosCFDIs");
                DataBand bandaConceptos = report.FindObject("Data4") as DataBand;
                bandaConceptos.DataSource = report.GetDataSource("ConceptosCFDIs");


                #endregion

                #region DOC RELACIONADOS
                DataTable dtDocRelacionados = new DataTable();
                DataColumn dcDRUUID = new DataColumn("DRUUID", typeof(string));
                DataColumn dcDRSerie = new DataColumn("DRSerie", typeof(string));
                DataColumn dcDRFolio = new DataColumn("DRFolio", typeof(string));
                DataColumn dcDRMetodoPago = new DataColumn("DRMetodoPago", typeof(string));
                DataColumn dcDRTotal = new DataColumn("DRMoneda", typeof(string));
                DataColumn dcDRSaldoAnterior = new DataColumn("DRSaldoAnterior", typeof(decimal));
                DataColumn dcDRMontoPagado = new DataColumn("DRMontoPagado", typeof(decimal));
                DataColumn dcDRSaldoPendiente = new DataColumn("DRSaldoPendiente", typeof(decimal));
                DataColumn dcDRNumParcialidad = new DataColumn("DRNumParcialidad", typeof(int));
                dtDocRelacionados.Columns.Add(dcDRUUID);
                dtDocRelacionados.Columns.Add(dcDRSerie);
                dtDocRelacionados.Columns.Add(dcDRFolio);
                dtDocRelacionados.Columns.Add(dcDRMetodoPago);
                dtDocRelacionados.Columns.Add(dcDRTotal);
                dtDocRelacionados.Columns.Add(dcDRSaldoAnterior);
                dtDocRelacionados.Columns.Add(dcDRMontoPagado);
                dtDocRelacionados.Columns.Add(dcDRSaldoPendiente);
                dtDocRelacionados.Columns.Add(dcDRNumParcialidad);
                foreach (PagosPagoDoctoRelacionado docRelacionado in ComprobanteV33.Complemento[0].Pago.Pago[0].DoctoRelacionado)
                {
                    dtDocRelacionados.Rows.Add(docRelacionado.IdDocumento, docRelacionado.Serie, docRelacionado.Folio, docRelacionado.MetodoDePagoDR, docRelacionado.MonedaDR, docRelacionado.ImpSaldoAnt, docRelacionado.ImpPagado
                        , docRelacionado.ImpSaldoInsoluto, docRelacionado.NumParcialidad);
                }
                report.RegisterData(dtDocRelacionados, "DocRelacionados");
                DataBand bandaDocsRelacionados = report.FindObject("Data6") as DataBand;
                bandaDocsRelacionados.DataSource = report.GetDataSource("DocRelacionados");
                #endregion

                #region CEDULA Y LOGO 
                try
                {
                    PictureObject pic = report.FindObject("picLogo") as PictureObject;
                    if (pic != null)
                    {
                        //if (rfcEmpresa != string.Empty)
                        //{
                        string rutaLogo = CobranzaDAL.GetRutaLogoContribuyente(IDContribuyente);//csEmpresa.getEmpresaByRFC(rfcEmpresa);
                        if (rutaLogo != null)
                        {
                            if (csDirectorio.existFile(rutaLogo) == true)
                                pic.Image = Image.FromFile(rutaLogo);
                        }
                        // }
                    }
                }
                catch
                { }

                try
                {
                    PictureObject pic = report.FindObject("picCedula") as PictureObject;
                    if (pic != null)
                    {
                        //if (rfcEmpresa != string.Empty)
                        //{
                        string rutaLogo = CobranzaDAL.GetRutaCedulaContribuyente(IDContribuyente);//csEmpresa.getEmpresaByRFC(rfcEmpresa);
                        if (rutaLogo != null)
                        {
                            if (csDirectorio.existFile(rutaLogo) == true)
                                pic.Image = Image.FromFile(rutaLogo);
                        }
                        // }
                    }
                }
                catch
                { }

                #endregion

                #region TIMBRE DIGITAL
                DataTable dtTimbreFiscal = new DataTable();
                DataColumn dcCadenaOriginal = new DataColumn("CadenaOriginal", typeof(string));
                DataColumn dcSello = new DataColumn("Sello", typeof(string));
                DataColumn dcSelloSAT = new DataColumn("SelloSAT", typeof(string));
                DataColumn dcFechaTimbre = new DataColumn("FechaTimbre", typeof(DateTime));
                DataColumn dcVersionTimbre = new DataColumn("VersionTimbre", typeof(string));
                DataColumn dcCerSat = new DataColumn("CerSat", typeof(string));
                DataColumn dcUUID = new DataColumn("UUID", typeof(string));
                dtTimbreFiscal.Columns.Add(dcCadenaOriginal);
                dtTimbreFiscal.Columns.Add(dcSello);
                dtTimbreFiscal.Columns.Add(dcSelloSAT);
                dtTimbreFiscal.Columns.Add(dcFechaTimbre);
                dtTimbreFiscal.Columns.Add(dcVersionTimbre);
                dtTimbreFiscal.Columns.Add(dcCerSat);
                dtTimbreFiscal.Columns.Add(dcUUID);
                dtTimbreFiscal.AcceptChanges();

                var cadenaO = string.Empty;
                var SelloCFD = string.Empty;
                var SelloSAT = string.Empty;
                DateTime FechaTimbrado = new DateTime();
                var version = string.Empty;
                var NoCertificadoSAT = string.Empty;
                var UUID = string.Empty;
                //Cadena Original

                //SelloCFD
                string cfdSello = string.Empty;
                try
                {
                    SelloCFD = ComprobanteV33.Complemento[0].Any[0].GetAttribute("SelloCFD");
                    int n = SelloCFD.Length - 8;
                    cfdSello = SelloCFD.Substring(n, 8);
                }
                catch
                {
                    SelloCFD = string.Empty;
                }
                //SelloSAT
                try
                {
                    SelloSAT = ComprobanteV33.Complemento[0].Any[0].GetAttribute("SelloSAT");
                }
                catch
                {
                    SelloSAT = string.Empty;
                }
                //FechaTimbrado
                try
                {
                    FechaTimbrado = Convert.ToDateTime(ComprobanteV33.Complemento[0].Any[0].GetAttribute("FechaTimbrado"));
                }
                catch
                {
                    FechaTimbrado = DateTime.Now;
                }
                //Version
                try
                {
                    version = ComprobanteV33.Complemento[0].Any[0].GetAttribute("Version");
                }
                catch
                {
                    version = string.Empty;
                }
                //Nocertificado
                try
                {
                    NoCertificadoSAT = ComprobanteV33.Complemento[0].Any[0].GetAttribute("NoCertificadoSAT");
                }
                catch
                {
                    NoCertificadoSAT = string.Empty;
                }
                //UUID
                try
                {
                    UUID = ComprobanteV33.Complemento[0].Any[0].GetAttribute("UUID");
                }
                catch
                {
                    UUID = string.Empty;
                }
                try
                {
                    cadenaO = CobranzaDAL.GetCadenaOriginal(UUID);// Hace falta cambiar este dato y asignar la cadena real
                }
                catch
                {
                    cadenaO = string.Empty;
                }

                dtTimbreFiscal.Rows.Add(cadenaO, SelloCFD, SelloSAT, FechaTimbrado, version, NoCertificadoSAT, UUID);
                report.RegisterData(dtTimbreFiscal, "Timbre");
                #endregion


                #region  CODIGO DE BARRAS BIDIMENCIONAL
                genCodigoBarrasBidimensional(ComprobanteV33.Emisor.Rfc, ComprobanteV33.Receptor.Rfc, Convert.ToString(ComprobanteV33.Total), UUID, cfdSello);
                try
                {
                    PictureObject picCBB = report.FindObject("PicCBB") as PictureObject;
                    if (picCBB != null)
                    {
                        string pathImagen = Application.StartupPath + Properties.Settings.Default.cbbqrcodeImage;
                        if (File.Exists(pathImagen))
                            picCBB.Image = Image.FromFile(pathImagen);
                        else
                        {
                            pathImagen = Application.StartupPath + "/" + Properties.Settings.Default.cbbqrcodeImage;

                            if (File.Exists(pathImagen))
                                picCBB.Image = Image.FromFile(pathImagen);
                            else
                                MessageBox.Show("El código de barras bidimensional no pudo ser agregado al CFDi.", "PmtLocla", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch
                { }
                #endregion

                try
                {
                    string pathXml = rutaFile;// CobranzaDAL.getRutaDirectorio(IDContribuyente) + @"\CRP\";
                    nombreArchivo = ComprobanteV33.Emisor.Nombre + "_" + ComprobanteV33.Serie + "_" + ComprobanteV33.Folio + ".pdf";
                    MessageError = Exportar(report, true, pathXml, nombreArchivo, abrir);
                    return MessageError;
                }
                catch
                { }
                return MessageError;
            }
            catch (Exception ex)
            {
                return "Error al tratar de crear reporte : " + ex;
            }
        }

        private string Exportar(Report reporte, bool esPdf, string PathSave, string nombreArchivo, bool abrir)
        {
            try
            {

                string rutaGuardar = Path.GetDirectoryName(PathSave) + @"\";
                Directory.CreateDirectory(rutaGuardar);

                reporte.Prepare();
                string filename = string.Empty;
                if (esPdf)
                {
                    filename = rutaGuardar + nombreArchivo;
                    PDFExport export = new PDFExport();
                    reporte.Export(export, filename);
                }
                else
                {

                    filename = rutaGuardar + nombreArchivo; 
                    Excel2007Export export = new Excel2007Export();
                    reporte.Export(export, filename);
                }
                nombreArchivo = filename;
                if (Configuraciones.EnviarImprimir)
                {
                    impresionDirecta(reporte);
                }                    
                reporte.Dispose();

                if(abrir)
                {
                    if (File.Exists(filename))
                    {
                        Process.Start(filename);
                        //SendMail mail = new SendMail();
                        //mail.enviarMail("CTE3","Administrador",filename);
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Error al exportar el reporte: " + Environment.NewLine + ex.Message;
            }
        }

        public void genCodigoBarrasBidimensional(string rfc_emisor, string rfc_receptor, string total_comprobante, string uuid, string digitosSelloDigital)
        {
            try
            {
                //CFDI
                string url = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx";
                string url2 = "https://prodretencionverificacion.clouda.sat.gob.mx/";
                decimal totalComprobante = Convert.ToDecimal(total_comprobante);
                total_comprobante = totalComprobante.ToString("0000000000.000000");
                String dato = url + "&id=" + uuid + @"?re=" + rfc_emisor + @"&rr=" + rfc_receptor + @"&tt=" + total_comprobante + @"&fe" + digitosSelloDigital;

                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                QrCode qrCode = qrEncoder.Encode(dato);
                GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
                string imagepath = Application.StartupPath + Properties.Settings.Default.cbbqrcodeImage;
                if (!File.Exists(imagepath)) { imagepath = Application.StartupPath + Properties.Settings.Default.cbbqrcodeImage; }
                using (var stream = new FileStream(imagepath, FileMode.Create))
                    renderer.WriteToStream(qrCode.Matrix, System.Drawing.Imaging.ImageFormat.Png, stream);
            }
            catch { }
        }

        public string impresionDirecta(Report reporte)
        {
            System.Drawing.Printing.PrinterSettings printSet = new System.Drawing.Printing.PrinterSettings();
            // string printerName = printSet.PrinterName;
            ReporteCFDIV33 = reporte;
            string result = string.Empty;
            try
            {
                ReporteCFDIV33.Prepare();
                ReporteCFDIV33.PrintSettings.ShowDialog = false;
                ReporteCFDIV33.PrintSettings.Copies = Configuraciones.Copies; ;
                ReporteCFDIV33.PrintSettings.Printer = Configuraciones.PrintName;
                ReporteCFDIV33.Print();
                ReporteCFDIV33.Dispose();
            }
            catch (Exception ex)
            {
                result = "Error:" + ex.Message;
            }

            return result;
        }
    }

}
