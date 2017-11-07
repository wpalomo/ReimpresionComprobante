using System;

namespace ReimpresionComprobante.Entities
{
    public class ReciboEntity
    {
        public int IDHistRec { get; set; }
        public int ID_CFD { get; set; }
        public string Serie { get; set; }
        public int Folio { get; set; }
        public string IDCliente { get; set; }
        public string RazonSocialCliente { get; set; }
        public string NombreComercialCliente { get; set; }
        public string IdentificadorCliente { get; set; }
        public string IDInmueble { get; set; }
        public string NombreInmueble { get; set; }
        public string MonedaEmision { get; set; }
        public string MonedaPago { get; set; }
        public decimal TipoCambioPago { get; set; }
        public decimal TotalFactura { get; set; }
        public decimal SaldoFactura { get; set; }
        public decimal ctdpag { get; set; }//Este campo es para el comprobante del pago
        public decimal TipoCambioEmision { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimientoPago { get; set; }
        public string SerieFolio { get { return Folio > 0 ? Serie + " " + Folio : string.Empty; } }
        public string Periodo { get; set; }
        public string TipoDocumento { get; set; }
        public string DescripcionTipoDocumento
        {
            get
            {
                return (TipoDocumento == "R" || TipoDocumento == "X") ? "Factura" : "Cargo periódico";
            }
        }
        public decimal CobroAAplicar { get; set; }
        public string Comentarios { get; set; }
        public decimal Moratorios { get; set; }
        public string IDContrato { get; set; }
        public string IDEmpresa { get; set; }
        public decimal CobroAAplicarPesos
        {
            get
            {
                if (MonedaEmision == "P")
                    return CobroAAplicar;
                else
                    return CobroAAplicar * TipoCambioEmision;
            }
        }
        public decimal Importe { get; set; }
        public decimal ImporteIVA { get; set; }
        public decimal IVAaCobrar { get; set; }
        public string NombreArrendatario { get; set; }
        public string Concepto { get; set; }
        public int NumeroEdificio { get; set; }
        public string TasaIVA { get; set; }
        public string PersonalidadArrendadora { get; set; }
        public string PersonalidadCliente { get; set; }
        public string Campo1 { get; set; }
        public string Campo2 { get; set; }
        public string Campo3 { get; set; }
        public string Campo4 { get; set; }
        public string Campo5 { get; set; }
        public string Campo7 { get; set; }
        public string Campo9 { get; set; }
        public string Campo16 { get; set; }
        public bool EsPagoParcial { get; set; }
        public ReciboEntity PagoParcial { get; set; }
        public int NumeroPagoParcial { get; set; }
        public string Fecha { get; set; }
        public DateTime FechaPago { get; set; }
        public bool SeSaldoFactura { get; set; }
        public decimal IVAMoratorios { get; set; }
        public int NumeroRecibo { get; set; }
        public string Estatus { get; set; }
        public DateTime CampoDate1 { get; set; }
        public DateTime CampoDate2 { get; set; }
        public int DebRec { get; set; }
        public decimal SaldoAFavorPreviamenteGenerado { get; set; }
        public int IdPagoCobranza { get; set; }
        public string TipoRecibo { get; set; }
        public decimal PagoTotal { get; set; }
        public string ObservacionesPago { get; set; }
        //public string DescripcionMonedaEmision { get; set; }
    }
}
