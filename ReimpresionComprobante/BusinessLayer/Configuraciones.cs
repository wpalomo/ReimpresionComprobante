using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimpresionComprobante.BusinessLayer
{
    public static class Configuraciones
    {
        /// <summary>
        /// Cadena de conexión a la base de datos ODBC
        /// </summary>
        public static string CadenaConexionODBC { get; set; }
        /// <summary>
        /// Cadena de conexión a la base de datos de SQL Server
        /// </summary>
        public static string CadenaConexionSQLServer { get; set; }
        /// <summary>
        /// Ruta en la cual se van a guardar los reportes
        /// </summary>
        public static string RutaRepositorio { get; set; }
        /// <summary>
        /// Ruta del formato de recibo de pago parcial
        /// </summary>
        public static string RutaFormatoPagoParcial { get; set; }
        /// <summary>
        /// Ruta del formato de recibo de pago parcial
        /// </summary>
        public static string RutaFormatoPagoTotal { get; set; }
        /// <summary>
        /// Asunto que se enviará en el correo electrónico cuando una factura sea pagada
        /// </summary>        
        public static string Asunto { get; set; }
        /// <summary>
        /// Cuerpo del correo electrónico que se enviará cuando una factura sea pagada
        /// </summary>
        public static string Cuerpo { get; set; }
        /// <summary>
        /// Habilita la administración de periodos de pago para determinar si el pago se aplicará en un periodo abierto
        /// </summary>
        public static bool AdministrarPeriodos { get; set; }
        /// <summary>
        /// Ruta del directorio donde se guardaran el registro de log cuando ocurra un problema al registrar un pago con Administracion de Periodos habilitada.
        /// </summary>
        public static string RutaLogPeriodos { get; set; }
        /// <summary>
        /// Habilita aplicar mostrar solo facturas de un solo cliente una vez que se selecciona una factura del listado de facturas pendientes de pago.
        /// </summary>
        public static bool FiltrarPagosPorCliente { get; set; }
        /// <summary>
        /// Importe minimo de saldo a favor a almacenar
        /// </summary>
        public static decimal MinSdoAFavor { get; set; }
        /// <summary>
        /// Dias a considerar para el cálculo anual
        /// </summary>
        public static int DiasCalculoAnual { get; set; }

        public static string printName { get; set; }
        public static int copies { get; set; }
        public static bool enviarImprimir { get; set; }
    }
}
