using System;

namespace ReimpresionComprobante.Entities
{
    public class FiltrosEntity
    {
        public string Inmobiliaria { get; set; }
        public string NombreInmobiliaria { get; set; }
        public DateTime FechaDel { get; set; }
        public DateTime FechaAl { get; set; }
        public string Serie { get; set; }
        public int SerieDel { get; set; }
        public int SerieAl { get; set; }
        public string Cliente { get; set; }
    }
}
