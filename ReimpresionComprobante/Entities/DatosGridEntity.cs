﻿using System;

namespace ReimpresionComprobante.Entities
{
    public class DatosGridEntity
    {
        public int ID { get; set; }
        public string Serie { get; set; }
        public int Folio { get; set; }
        public DateTime FechaEmision { get; set; }
        public string ID_Cliente { get; set; }
        public string Cliente { get; set; }
        public string Moneda { get; set; }
        public decimal Total { get; set; }
        public int IDPago { get; set; }
    }
}
