using System;

namespace ReimpresionComprobante.Entities
{
    public class ComprobantePagoEntity
    {
        public string LugarExpedicion { get; set; }
        public string MetodoPago { get; set; }
        public string TipoComprobante { get; set; }
        public decimal Total { get; set; }
        public Nullable<Decimal> TipoCambio { get; set; }
        public string Moneda { get; set; }
        public Nullable<Decimal> Descuento { get; set; }
        public decimal Subtotal { get; set; }
        public string CondicionesPago { get; set; }
        public string FormaPago { get; set; }
        public DateTime fecha { get; set; }
        public Nullable<Int32> Folio { get; set; }
        public string Serie { get; set; }
        public string NumRegIDTribReceptor { get; set; }
        public string UsoCFDI { get; set; }
        public string Certificado { get; set; }
        public string Sello { get; set; }
        public string NoCertificado { get; set; }////////
        public string Version { get; set; }////////
        public int IDPago { get; set; }///////
        public string RecidenciaFiscalReceptor { get; set; }////////

        public string Confirmacion { get; set; }
        public int ID_SerieFolio { get; set; }
        public int ID_Contribuyente { get; set; }
        public string ID_Cliente { get; set; }
        public Nullable<Int32> IDTimbreDigital { get; set; }
        public int IDXml { get; set; }
        public bool EstaTimbrado { get; set; }
        public ComprobanteReceptorEntity receptorComprobante { get; set; }



    }
}
