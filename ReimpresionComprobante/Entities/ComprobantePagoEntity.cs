using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimpresionComprobante.Entities
{
    public class ComprobantePagoEntity
    {
        public int IDComprobante { get; set; }
        public string Serie { get; set; }
        public int Folio { get; set; }

        public DateTime Fecha { get; set; }

        public string FormaDePago { get; set; }

        public string Moneda { get; set; }
        public decimal TipoDeCambio { get; set; }

        public decimal Total { get; set; }

        List<DocumentosRelacionadosEntity>  DocumentosRelacionados { get; set; }

        
    }
}
