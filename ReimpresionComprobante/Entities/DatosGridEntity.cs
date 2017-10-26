using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimpresionComprobante.Entities
{
    public class DatosGridEntity
    {
        public int ID { get; set; }
        public string Serie { get; set; }
        public int Folio { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Cliente { get; set; }
        public string Moneda { get; set; }
        public decimal Total { get; set; }
    }
}
