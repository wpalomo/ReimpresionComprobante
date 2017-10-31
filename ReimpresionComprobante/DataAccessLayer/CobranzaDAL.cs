using ReimpresionComprobante.BusinessLayer;
using ReimpresionComprobante.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReimpresionComprobante.DataAccessLayer
{
    public class CobranzaDAL
    {
        private string cadenaDeConexion = string.Empty;
        private string usuario = string.Empty;
        public static string mensajeError = "";
        public static bool error = false;
        public static string user = string.Empty;
        private SaariDAL inmoDAL = null;


        public CobranzaDAL(string cadenaDeConexion, string usuario)
        {
            this.cadenaDeConexion = cadenaDeConexion;
            this.usuario = usuario;
            user = this.usuario;
            inmoDAL = new SaariDAL(Configuraciones.CadenaConexionODBC, user, cadenaDeConexion);
        }

        //public void insertLog(string tipoAccion, string nombreTabla, int idRelacionado, string descripcion, bool esError)
        //{
        //    SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //    try
        //    {
        //        string sql = "INSERT INTO Cobranza.Log VALUES(@TipoAccion, @Tabla, @IDRelacionado, @Descripcion, @EsError, @Usuario, @FechaHora)";
        //        SqlCommand comando = new SqlCommand(sql, conexion);
        //        comando.Parameters.Add("@TipoAccion", SqlDbType.NVarChar).Value = tipoAccion;
        //        comando.Parameters.Add("@Tabla", SqlDbType.NVarChar).Value = nombreTabla;
        //        comando.Parameters.Add("@IDRelacionado", SqlDbType.Int).Value = idRelacionado > 0 ? idRelacionado : (object)DBNull.Value;
        //        comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar).Value = descripcion;
        //        comando.Parameters.Add("@Usuario", SqlDbType.NVarChar).Value = usuario;
        //        comando.Parameters.Add("@FechaHora", SqlDbType.DateTime).Value = DateTime.Now;
        //        comando.Parameters.Add("@EsError", SqlDbType.Bit).Value = esError;
        //        conexion.Open();
        //        comando.ExecuteNonQuery();
        //        conexion.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        conexion.Close();                
        //    }
        //}

        //        public decimal getSaldoAFavor(string idCliente)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = @"SELECT Cobranza.SaldoAFavor.IDSaldo, Cobranza.SaldoAFavor.Importe AS Favor, ISNULL(SUM(Cobranza.PagoConSaldoAFavor.Total), 0) AS Pagado
        //                FROM Cobranza.SaldoAFavor 
        //                LEFT JOIN Cobranza.PagoConSaldoAFavor ON Cobranza.PagoConSaldoAFavor.IDSaldo = Cobranza.SaldoAFavor.IDSaldo 
        //                WHERE Cobranza.SaldoAFavor.IDCliente = @IDCliente
        //                GROUP BY Cobranza.PagoConSaldoAFavor.IDSaldo, Cobranza.SaldoAFavor.IDSaldo, Cobranza.SaldoAFavor.Importe";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDCliente", SqlDbType.NVarChar).Value = idCliente;
        //                decimal favor = 0, pagado = 0;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    favor += Convert.ToDecimal(reader["Favor"]);
        //                    pagado += Convert.ToDecimal(reader["Pagado"]);
        //                }
        //                reader.Close();
        //                conexion.Close();
        //                decimal saldo = favor - pagado;
        //                return saldo;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();                
        //                return 0;
        //            }
        //        }


        //        [Obsolete(@"Hubo un cambio en la base de datos, no es necesario conocer el ID del cliente en el saldo aplicado ya que cada saldo aplicado se relaciona con un saldo a favor", true)]
        //        public decimal getTotalPagosConSaldoAFavor(string idCliente)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = @"SELECT ISNULL(SUM(Cobranza.PagoConSaldoAFavor.Total), 0) AS Total FROM Cobranza.SaldoAFavor 
        //JOIN Cobranza.PagoConSaldoAFavor ON Cobranza.PagoConSaldoAFavor.IDSaldo = Cobranza.SaldoAFavor.IDSaldo
        //WHERE Cobranza.SaldoAFavor.IDCliente = @IDCliente";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDCliente", SqlDbType.NVarChar).Value = idCliente;
        //                conexion.Open();
        //                decimal total = Convert.ToDecimal(comando.ExecuteScalar());
        //                conexion.Close();
        //                return total;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                return 0;
        //            }
        //        }

        //        public int insertPago(PagoEntity pago)
        //        {
        //            SaariDAL inmoDAL = new SaariDAL(Configuraciones.CadenaConexionODBC, Acceso.Usuario, Configuraciones.CadenaConexionSQLServer);
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = "INSERT INTO Cobranza.Pago VALUES (@IDCliente, @Fecha, @Moneda, @TipoDeCambio, @Medio, @BancoID, @BancoNombre, @InstrumentoID, @Total, @IVATotalCobrado); SELECT IDPago FROM Cobranza.Pago WHERE IDPago = SCOPE_IDENTITY()";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDCliente", SqlDbType.NVarChar).Value = pago.IDCliente;
        //                comando.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = pago.FechaPago.Date;
        //                comando.Parameters.Add("@Moneda", SqlDbType.NVarChar).Value = pago.MonedaPago;
        //                comando.Parameters.Add("@TipoDeCambio", SqlDbType.Decimal).Value = pago.TipoCambioPago;
        //                comando.Parameters.Add("@Medio", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(pago.MedioDePago) ? (object)DBNull.Value : pago.MedioDePago;
        //                comando.Parameters.Add("@BancoID", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(pago.BancoReceptorID) ? (object)DBNull.Value : pago.BancoReceptorID;
        //                comando.Parameters.Add("@BancoNombre", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(pago.BancoReceptorNombre) ? (object)DBNull.Value : pago.BancoReceptorNombre;
        //                comando.Parameters.Add("@InstrumentoID", SqlDbType.NVarChar).Value = pago.InstrumentoDePagoID <= 0 ? (object)DBNull.Value : pago.InstrumentoDePagoID;
        //                comando.Parameters.Add("@Total", SqlDbType.Decimal).Value = pago.TotalDelPago;
        //                comando.Parameters.Add("@IVATotalCobrado", SqlDbType.Decimal).Value = pago.IVACobrado;
        //                conexion.Open();
        //                int idPago = Convert.ToInt32(comando.ExecuteScalar());
        //                conexion.Close();
        //                if (idPago > 0)
        //                    pago.seCreoPago = true;
        //                insertLog("Insertar", "Pago", idPago, string.Format("Se insertó un pago por {0}, para el cliente con ID {1}. Se asignó el ID {2}", pago.TotalDelPago, pago.IDCliente, idPago), false);
        //                return idPago;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Insertar", "Pago", 0, string.Format("Error al insertar un pago por {0}, para el cliente con ID {1}. Excepción: {2}", pago.TotalDelPago, pago.IDCliente, ex.Message), true);
        //                return 0;
        //            }
        //        }

        //        public bool insertReciboPagado(ReciboPagadoEntity recPag)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = "INSERT INTO Cobranza.ReciboPagado VALUES (@IDPago, @IDHistRec, @TotalPagado, @Comentarios)";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDPago", SqlDbType.Int).Value = recPag.IDPago;
        //                comando.Parameters.Add("@IDHistRec", SqlDbType.Int).Value = recPag.IDHistRec;
        //                comando.Parameters.Add("@TotalPagado", SqlDbType.Decimal).Value = recPag.TotalPagado;
        //                comando.Parameters.Add("@Comentarios", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(recPag.Comentarios) ? (object)DBNull.Value : recPag.Comentarios;
        //                conexion.Open();
        //                comando.ExecuteNonQuery();
        //                conexion.Close();
        //                insertLog("Insertar", "ReciboPagado", recPag.IDHistRec, string.Format("Se insertó un pago por {0} para el recibo con ID {1}", recPag.TotalPagado, recPag.IDHistRec), false);
        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Insertar", "ReciboPagado", 0, string.Format("Error al insertar un pago para el recibo con ID {0}. Excepción: {1}", recPag.IDHistRec, ex.Message), true);
        //                return false;
        //            }
        //        }

        //        public bool insertReciboPagado(SaldoAFavorAplicadoEntity saldoAplicado)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = "INSERT INTO Cobranza.PagoConSaldoAFavor VALUES (@IDSaldo, @IDHistRec, @Total, @Fecha, @Comentarios, @IDPagoCob)";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDSaldo", SqlDbType.Int).Value = saldoAplicado.IDSaldo;
        //                comando.Parameters.Add("@IDHistRec", SqlDbType.Int).Value = saldoAplicado.IDHistRec;
        //                comando.Parameters.Add("@Total", SqlDbType.Decimal).Value = saldoAplicado.TotalAplicado;
        //                comando.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = saldoAplicado.FechaPago.Date;
        //                comando.Parameters.Add("@Comentarios", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(saldoAplicado.Comentarios) ? (object)DBNull.Value : saldoAplicado.Comentarios;
        //                comando.Parameters.Add("@IDPagoCob", SqlDbType.Int).Value = saldoAplicado.IDPagoCob;
        //                conexion.Open();
        //                comando.ExecuteNonQuery();
        //                conexion.Close();
        //                insertLog("Insertar", "PagoConSaldoAFavor", saldoAplicado.IDHistRec, string.Format("Se insertó un pago con saldo por {0} para el recibo con ID {1}", saldoAplicado.TotalAplicado, saldoAplicado.IDHistRec), false);
        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Insertar", "PagoConSaldoAFavor", 0, string.Format("Error al insertar un pago con saldo para el recibo con ID {0}. Excepción: {1}", saldoAplicado.IDHistRec, ex.Message), true);
        //                return false;
        //            }
        //        }

        //        public bool insertSaldoAFavor(SaldoAFavorEntity saldo)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = "INSERT INTO Cobranza.SaldoAFavor VALUES (@IDPago, @IDCliente, @Importe)";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDPago", SqlDbType.Int).Value = saldo.IDPago;
        //                comando.Parameters.Add("@IDCliente", SqlDbType.NVarChar).Value = saldo.IDCliente;
        //                comando.Parameters.Add("@Importe", SqlDbType.Decimal).Value = saldo.Total;
        //                conexion.Open();
        //                comando.ExecuteNonQuery();
        //                conexion.Close();
        //                insertLog("Insertar", "SaldoAFavor", 0, string.Format("Se insertó un saldo a favor de {0} del pago con ID {1}", saldo.Total, saldo.IDPago), false);
        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Insertar", "SaldoAFavor", 0, string.Format("Error al insertar un saldo a favor de {0} del pago con ID {1}. Excepción: {2}", saldo.Total, saldo.IDPago, ex.Message), true);
        //                return false;
        //            }
        //        }

        //        public bool getSaldoAFavor(string idCliente, List<DetalleSaldoEntity> listaDetalle)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = @"SELECT Cobranza.SaldoAFavor.IDPago, IDSaldo, Fecha, Importe FROM Cobranza.Pago 
        //INNER JOIN Cobranza.SaldoAFavor ON Cobranza.SaldoAFavor.IDPago = Cobranza.Pago.IDPago
        //WHERE Cobranza.SaldoAFavor.IDCliente = @IDCte 
        //ORDER BY Fecha";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDCte", SqlDbType.NVarChar).Value = idCliente;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    DetalleSaldoEntity detalle = new DetalleSaldoEntity();
        //                    detalle.IDPago = (int)reader["IDPago"];
        //                    detalle.IDSaldo = Convert.ToInt32(reader["IDSaldo"]);
        //                    detalle.EsAFavor = true;
        //                    detalle.Fecha = Convert.ToDateTime(reader["Fecha"]);
        //                    detalle.SaldoAFavor = Convert.ToDecimal(reader["Importe"]);
        //                    listaDetalle.Add(detalle);
        //                }
        //                reader.Close();
        //                conexion.Close();
        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "Pago", 0, string.Format("Error al consultar los saldos a favor del cliente con ID {0}. Excepción: {1}", idCliente, ex.Message), true);
        //                return false;
        //            }
        //        }

        //        public bool getPagosConSaldo(string idCliente, List<DetalleSaldoEntity> listaDetalle)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = @"SELECT IDHistRec, Fecha, SUM(Total) AS Total FROM Cobranza.SaldoAFavor
        //INNER JOIN Cobranza.PagoConSaldoAFavor ON Cobranza.PagoConSaldoAFavor.IDSaldo = Cobranza.SaldoAFavor.IDSaldo
        //WHERE IDCliente = @IDCte
        //GROUP BY IDHistRec, Fecha";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDCte", SqlDbType.NVarChar).Value = idCliente;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    DetalleSaldoEntity detalle = new DetalleSaldoEntity();
        //                    detalle.EsAFavor = false;
        //                    detalle.Fecha = Convert.ToDateTime(reader["Fecha"]);
        //                    detalle.SaldoAplicado = Convert.ToDecimal(reader["Total"]);
        //                    detalle.IDHistRec = (int)reader["IDHistRec"];
        //                    listaDetalle.Add(detalle);
        //                }
        //                reader.Close();
        //                conexion.Close();
        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "SaldoAFavor", 0, string.Format("Error al consultar los pagos con saldos a favor del cliente con ID {0}. Excepción: {1}", idCliente, ex.Message), true);
        //                return false;
        //            }
        //        }


        //        public int insertComplementoPago(PagoEntity pago)
        //        {
        //            SaariDAL inmoDAL = new SaariDAL(Configuraciones.CadenaConexionODBC, Acceso.Usuario, Configuraciones.CadenaConexionSQLServer);
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {

        //                ComplementoPagoEntity ComplementoPago = new ComplementoPagoEntity();
        //                ComplementoPago.Version = "1.0";
        //                ComplementoPago.FechaPago = pago.FechaPago;
        //                ComplementoPago.FormaDePagoP = pago.FormaDePago;
        //                ComplementoPago.MonedaP = validarTipoMoneda(pago);
        //                ComplementoPago.TipoCambioP = pago.TipoCambioPago;
        //                ComplementoPago.Monto = pago.TotalDelPago;
        //                ComplementoPago.NumOperacion = pago.numeroOperacion;
        //                ComplementoPago.RFCEmisorCtaOrd = "";//RFC DEL BANCO
        //                ComplementoPago.NomBancoOrdExt = pago.nameBancoOrdenate;
        //                if (pago.nameBancoOrdenate == "***Seleccione un banco")
        //                {
        //                    pago.nombreBanco = String.Empty;
        //                    ComplementoPago.NomBancoOrdExt = String.Empty;
        //                }

        //                ComplementoPago.CtaOrdenante = pago.ctaOrdenante;
        //                ComplementoPago.RfcEmisorCtaBen = "";//RFC DEL BANCO

        //                InmobiliariaEntity laInmobiliaria = null;
        //                laInmobiliaria = inmoDAL.getInmobiliaria(pago.idInmobiliaria);
        //                ComplementoPago.RfcEmisorCtaBen = "";//laInmobiliaria.RFC;
        //                ComplementoPago.CtaBeneficiario = pago.ctaBeneficiario;
        //                ComplementoPago.ID_ComprobantePago = pago.ID_comrpobante;
        //                //Esta parte aun falta validarse correctamente y generar cadena, sello y tipoCad pago para cuando sea necesario.
        //                if (pago.FormaDePago == "03")
        //                {
        //                    ComplementoPago.TipoCadPago = "";
        //                    ComplementoPago.CadPago = "";
        //                    ComplementoPago.SelloPago = "";
        //                }
        //                string sql = @"INSERT INTO cfd.ComplementoPago VALUES(@version, @FechaPago, @FormaDePagoP, @MonedaP, @TipoCambioP, @Monto, @NumOperacion,@RfcEmisorCtaOrd, @nomBancoOrdExt, @CtaOrdenante, @RfcEmisorCtaBen, @CtaBeneficiario, @TipoCadPago, @CertPago, @CadPago, @SelloPago, @ID_ComprobantePago); SELECT ID_Complemento FROM cfd.ComplementoPago WHERE ID_Complemento = SCOPE_IDENTITY()";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@version", SqlDbType.NVarChar).Value = ComplementoPago.Version;
        //                comando.Parameters.Add("@FechaPago", SqlDbType.DateTime).Value = ComplementoPago.FechaPago.Date;
        //                comando.Parameters.Add("@FormaDePagoP", SqlDbType.NVarChar).Value = ComplementoPago.FormaDePagoP;
        //                comando.Parameters.Add("@MonedaP", SqlDbType.NVarChar).Value = ComplementoPago.MonedaP;
        //                comando.Parameters.Add("@TipoCambioP", SqlDbType.Decimal).Value = ComplementoPago.TipoCambioP;
        //                comando.Parameters.Add("@Monto", SqlDbType.Decimal).Value = ComplementoPago.Monto;
        //                comando.Parameters.Add("@NumOperacion", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.NumOperacion) ? (object)DBNull.Value : ComplementoPago.NumOperacion;
        //                comando.Parameters.Add("@RfcEmisorCtaOrd", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.RFCEmisorCtaOrd) ? (object)DBNull.Value : ComplementoPago.RFCEmisorCtaOrd;
        //                comando.Parameters.Add("@nomBancoOrdExt", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.NomBancoOrdExt) ? (object)DBNull.Value : ComplementoPago.NomBancoOrdExt;
        //                comando.Parameters.Add("@CtaOrdenante", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.CtaOrdenante) ? (object)DBNull.Value : ComplementoPago.CtaOrdenante;
        //                comando.Parameters.Add("@RfcEmisorCtaBen", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.RfcEmisorCtaBen) ? (object)DBNull.Value : ComplementoPago.RfcEmisorCtaBen;
        //                comando.Parameters.Add("@CtaBeneficiario", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.CtaBeneficiario) ? (object)DBNull.Value : ComplementoPago.CtaBeneficiario;
        //                comando.Parameters.Add("@TipoCadPago", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.TipoCadPago) ? (object)DBNull.Value : ComplementoPago.TipoCadPago;
        //                comando.Parameters.Add("@CertPago", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.CertPago) ? (object)DBNull.Value : ComplementoPago.CertPago;
        //                comando.Parameters.Add("@CadPago", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.CadPago) ? (object)DBNull.Value : ComplementoPago.CadPago;
        //                comando.Parameters.Add("@SelloPago", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(ComplementoPago.SelloPago) ? (object)DBNull.Value : ComplementoPago.SelloPago;
        //                //comando.Parameters.Add("@IdPago", SqlDbType.Int).Value = ComplementoPago.idPago;
        //                comando.Parameters.Add("@ID_ComprobantePago", SqlDbType.Int).Value = ComplementoPago.ID_ComprobantePago;
        //                conexion.Open();
        //                int idComplemento = Convert.ToInt32(comando.ExecuteScalar());
        //                conexion.Close();
        //                insertLog("Insertar", "cfd.ComplementoPago", idComplemento, string.Format("Se insertó el complemento con id de pago: {0} ", idComplemento), false);
        //                if (idComplemento > 0)
        //                    pago.seCreoComplemento = true;
        //                return idComplemento;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                var sinComplemento = 0;
        //                insertLog("Insertar", "cfd.ComplementoPago", 0, string.Format("Error al insertar el complemento con id de pago: {0}. Excepción: {1}", sinComplemento, ex.Message), true);
        //                return sinComplemento;
        //            }
        //        }

        //        private string validarTipoMoneda(PagoEntity pago)
        //        {
        //            string monedaPago = string.Empty;
        //            if (pago.MonedaPago == "P")
        //                monedaPago = "MXN";
        //            else if (pago.MonedaPago == "D")
        //                monedaPago = "USD";
        //            else
        //                monedaPago = "MXN";
        //            return monedaPago;
        //        }
        //        public int insertComprobantePago(PagoEntity pago)
        //        {
        //            SaariDAL inmoDAL = new SaariDAL(Configuraciones.CadenaConexionODBC, Acceso.Usuario, Configuraciones.CadenaConexionSQLServer);
        //            ComprobantePagoEntity comprobante = new ComprobantePagoEntity();

        //            ComprobanteCertificadoEntity elCertificado = null;
        //            ComprobanteReceptorEntity elReceptorEntity = null;
        //            var lugarExpedicion = "";
        //            var idcontribuyente = inmoDAL.getIDContribuyente(pago.idInmobiliaria);
        //            var idcontribuyentesql = getIDContribuyente(idcontribuyente);

        //            if (pago.RecibosPagados.Count > 0)
        //            {
        //                lugarExpedicion = inmoDAL.getLugarExpedicion(pago.RecibosPagados[0].IDEmpresa);
        //                elReceptorEntity = inmoDAL.getDatosReceptorComprobante(pago.RecibosPagados[0].IDCliente);
        //            }
        //            comprobante.LugarExpedicion = lugarExpedicion;

        //            comprobante.MetodoPago = "PPD";
        //            comprobante.TipoComprobante = "P";
        //            comprobante.Total = pago.TotalDelPago;
        //            comprobante.TipoCambio = pago.TipoCambioPago;

        //            comprobante.Moneda = validarTipoMoneda(pago);
        //            comprobante.Descuento = (decimal)0.00;
        //            comprobante.Subtotal = (decimal)0.00;
        //            comprobante.CondicionesPago = "";
        //            comprobante.FormaPago = pago.FormaDePago;
        //            comprobante.fecha = DateTime.Now.Date;
        //            var Folio = getFolioActual(pago.ID_SerieFolio);
        //            Folio++;
        //            comprobante.Folio = Folio;
        //            var Serie = getSerieActual(pago.ID_SerieFolio);
        //            comprobante.Serie = Serie;

        //            comprobante.RecidenciaFiscalReceptor = "";

        //            comprobante.NumRegIDTribReceptor = "";
        //            comprobante.UsoCFDI = "P01";

        //            elCertificado = getCertificadoComprobante(idcontribuyentesql);

        //            comprobante.Certificado = elCertificado.Certificado;
        //            comprobante.Sello = "";
        //            comprobante.IDPago = pago.PagoID;
        //            comprobante.NoCertificado = elCertificado.NoCertificado;
        //            comprobante.Version = "3.3";

        //            comprobante.Confirmacion = "";
        //            comprobante.ID_SerieFolio = pago.ID_SerieFolio;
        //            comprobante.ID_Cliente = pago.IDCliente;
        //            comprobante.ID_Contribuyente = Convert.ToInt32(idcontribuyentesql);
        //            comprobante.ID_Cliente = pago.IDCliente;
        //            comprobante.IDTimbreDigital = null;
        //            comprobante.EstaTimbrado = false;
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {

        //                string sql = @"INSERT INTO cfd.ComprobantePago VALUES (@Version, @Serie, @Folio, @Fecha, @Sello, @FormaPago, @NoCertificado, @Certificado, @CondicionesPago,@Subtotal,
        //                            @Descuento, @Moneda, @TipoCambio, @Total, @TipoComprobante, @MetodoPago, @LugarExpedicion,@Confirmacion, @ID_SerieFolio, @ID_Contribuyente, @ID_Cliente, 
        //                            @UsoCFDI, @IDPago, @IDTimbreDigital,@IDxml,@EstaTimbrado); SELECT ID_ComprobantePago FROM cfd.ComprobantePago WHERE ID_ComprobantePago = SCOPE_IDENTITY()";

        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@Version", SqlDbType.NVarChar).Value = comprobante.Version;
        //                comando.Parameters.Add("@Serie", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(comprobante.Serie) ? (object)DBNull.Value : comprobante.Serie;
        //                comando.Parameters.Add("@Folio", SqlDbType.Int).Value = comprobante.Folio > 0 ? comprobante.Folio : (object)DBNull.Value;
        //                comando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = comprobante.fecha.Date;
        //                comando.Parameters.Add("@Sello", SqlDbType.NVarChar).Value = comprobante.Sello;
        //                comando.Parameters.Add("@FormaPago", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(comprobante.FormaPago) ? (object)DBNull.Value : comprobante.FormaPago;
        //                comando.Parameters.Add("@NoCertificado", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(comprobante.NoCertificado) ? (object)DBNull.Value : comprobante.NoCertificado;
        //                comando.Parameters.Add("@Certificado", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(comprobante.Certificado) ? (object)DBNull.Value : comprobante.Certificado;
        //                comando.Parameters.Add("@CondicionesPago", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(comprobante.CondicionesPago) ? (object)DBNull.Value : comprobante.CondicionesPago;
        //                comando.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = comprobante.Subtotal;
        //                comando.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = comprobante.Descuento > 0 ? comprobante.Descuento : (object)DBNull.Value;
        //                comando.Parameters.Add("@Moneda", SqlDbType.NVarChar).Value = comprobante.Moneda;
        //                comando.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = comprobante.TipoCambio > 0 ? comprobante.TipoCambio : (object)DBNull.Value;
        //                comando.Parameters.Add("@Total", SqlDbType.Decimal).Value = comprobante.Total;
        //                comando.Parameters.Add("@TipoComprobante", SqlDbType.NVarChar).Value = comprobante.TipoComprobante;
        //                comando.Parameters.Add("@MetodoPago", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(comprobante.MetodoPago) ? (object)DBNull.Value : comprobante.MetodoPago;
        //                comando.Parameters.Add("@LugarExpedicion", SqlDbType.NVarChar).Value = comprobante.LugarExpedicion;
        //                comando.Parameters.Add("@Confirmacion", SqlDbType.NVarChar).Value = comprobante.Confirmacion;
        //                comando.Parameters.Add("@ID_SerieFolio", SqlDbType.Int).Value = comprobante.ID_SerieFolio;
        //                comando.Parameters.Add("@ID_Contribuyente", SqlDbType.Int).Value = comprobante.ID_Contribuyente;
        //                comando.Parameters.Add("@ID_Cliente", SqlDbType.NVarChar).Value = comprobante.ID_Cliente;
        //                comando.Parameters.Add("@UsoCFDI", SqlDbType.NVarChar).Value = comprobante.UsoCFDI;
        //                comando.Parameters.Add("@IDPago", SqlDbType.Int).Value = comprobante.IDPago;
        //                comando.Parameters.Add("@IDTimbreDigital", SqlDbType.Int).Value = (object)DBNull.Value;
        //                comando.Parameters.Add("@IDxml", SqlDbType.Int).Value = (object)DBNull.Value;
        //                comando.Parameters.Add("@EstaTimbrado", SqlDbType.Bit).Value = comprobante.EstaTimbrado;

        //                conexion.Open();
        //                int idComprobante = Convert.ToInt32(comando.ExecuteScalar());
        //                conexion.Close();
        //                insertLog("Insertar", "cfd.ComprobantePago", idComprobante, string.Format("Se insertó el comprobante con id de pago: {0} ", idComprobante), false);
        //                if (idComprobante > 0)
        //                    pago.seCreoComprobantePago = true;
        //                return idComprobante;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Insertar", "cfd.ComprobantePago", 0, string.Format("Error al insertar comprobante. Excepción: {0}", ex.Message), true);
        //                return 0;
        //            }
        //        }


        //        //      Metodos para el Comprobante
        //        public string getIDContribuyente(string idArrendadora)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            string idContribuyente = "0";
        //            try
        //            {
        //                string sql = @"SELECT ID_Contribuyente
        //  FROM Empresa.Contribuyente
        //  WHERE ID_Arrendadora = @idArrendadora";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@idArrendadora", SqlDbType.NVarChar).Value = idArrendadora;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    idContribuyente = reader["ID_Contribuyente"].ToString();
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "ID_Contribuyente", 0, string.Format("Error al consultar Empresa.Contribuyente con ID {0}. Excepción: {1}", idContribuyente, ex.Message), true);
        //            }
        //            return idContribuyente;
        //        }

        //        public string getClaveRegimen(int ID_Serie_Folio)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            string claveregimen = "";
        //            try
        //            {
        //                string sql = @"SELECT [ClaveRegimenFiscal]
        //  FROM [Empresa].[Serie_Folio]
        //  WHERE [ID_Serie_Folio] = @idserie";
        //                SqlCommand comando = new SqlCommand(sql, conexion);

        //                comando.Parameters.Add("@idserie", SqlDbType.Int).Value = ID_Serie_Folio;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    claveregimen = reader["ClaveRegimenFiscal"].ToString();
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "Serie_Folio", 0, string.Format("Error al consultar Empresa.Serie_Folio con ID {0}. Excepción: {1}", ID_Serie_Folio, ex.Message), true);
        //            }
        //            return claveregimen;
        //        }

        //        public ComprobanteCFDEntity getDatosCFD(int idCfd)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            ComprobanteCFDEntity datoscfd = new ComprobanteCFDEntity();
        //            try
        //            {
        //                string sql = @"SELECT Serie,Folio,Total,Subtotal,Moneda,UUID      
        //  FROM cfd.CFD
        //  WHERE ID_CFD = @idCfd";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@idCfd", SqlDbType.Int).Value = idCfd;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    datoscfd.serie = reader["Serie"].ToString();
        //                    if (reader["Folio"] != (object)DBNull.Value)
        //                    {
        //                        datoscfd.folio = Convert.ToInt32(reader["Folio"]);
        //                    }

        //                    if (reader["Total"] != (object)DBNull.Value)
        //                    {
        //                        datoscfd.total = Convert.ToDecimal(reader["Total"]);
        //                    }

        //                    if (reader["Subtotal"] != (object)DBNull.Value)
        //                    {
        //                        datoscfd.subtotal = Convert.ToDecimal(reader["Subtotal"]);
        //                    }
        //                    datoscfd.moneda = reader["Moneda"].ToString();
        //                    datoscfd.UUID = reader["UUID"].ToString();
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "cfd.CFD", 0, string.Format("Error al consultar cfd.CFD con ID {0}. Excepción: {1}", idCfd, ex.Message), true);
        //            }
        //            return datoscfd;
        //        }

        //        public ComprobantePagoEntity getComprobante(int ID_comrpobante)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            ComprobantePagoEntity elComprobante = null;
        //            try
        //            {
        //                string sql = @"SELECT [Version],[Serie],[Folio],[Fecha],[Sello],[FormaPago],[NoCertificado],[Certificado],[CondicionesPago],[Subtotal],[Descuento],[Moneda],
        //[TipoCambio],[Total],[TipoComprobante],[MetodoPago],[LugarExpedicion],[Confirmacion],[ID_SerieFolio],[ID_Contribuyente],[ID_Cliente],[UsoCFDI],[IDPago],[IDTimbreDigital]
        //FROM [cfd].[ComprobantePago]
        //WHERE [ID_ComprobantePago] = @idcomrobante";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@idcomrobante", SqlDbType.Int).Value = ID_comrpobante;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                elComprobante = new ComprobantePagoEntity();
        //                while (reader.Read())
        //                {
        //                    elComprobante.Version = reader["Version"].ToString();
        //                    elComprobante.Serie = reader["Serie"].ToString();
        //                    if (reader["Folio"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.Folio = Convert.ToInt32(reader["Folio"]);
        //                    }
        //                    if (reader["Fecha"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.fecha = Convert.ToDateTime(reader["Fecha"]);
        //                    }
        //                    elComprobante.Sello = reader["Sello"].ToString();
        //                    elComprobante.FormaPago = reader["FormaPago"].ToString();
        //                    elComprobante.NoCertificado = reader["NoCertificado"].ToString();
        //                    elComprobante.Certificado = reader["Certificado"].ToString();
        //                    elComprobante.CondicionesPago = reader["CondicionesPago"].ToString();
        //                    if (reader["Subtotal"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.Subtotal = Convert.ToDecimal(reader["Subtotal"]);
        //                    }
        //                    if (reader["Descuento"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.Descuento = Convert.ToDecimal(reader["Descuento"]);
        //                    }
        //                    elComprobante.Moneda = reader["Moneda"].ToString();
        //                    if (reader["TipoCambio"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.TipoCambio = Convert.ToDecimal(reader["TipoCambio"]);
        //                    }
        //                    if (reader["Total"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.Total = Convert.ToDecimal(reader["Total"]);
        //                    }
        //                    elComprobante.TipoComprobante = reader["TipoComprobante"].ToString();
        //                    elComprobante.MetodoPago = reader["MetodoPago"].ToString();
        //                    elComprobante.LugarExpedicion = reader["LugarExpedicion"].ToString();
        //                    elComprobante.Confirmacion = reader["Confirmacion"].ToString();
        //                    if (reader["ID_SerieFolio"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.ID_SerieFolio = Convert.ToInt32(reader["ID_SerieFolio"]);
        //                    }
        //                    if (reader["ID_Contribuyente"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.ID_Contribuyente = Convert.ToInt32(reader["ID_Contribuyente"]);
        //                    }
        //                    elComprobante.ID_Cliente = reader["ID_Cliente"].ToString();
        //                    elComprobante.UsoCFDI = reader["UsoCFDI"].ToString();
        //                    if (reader["IDPago"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.IDPago = Convert.ToInt32(reader["IDPago"]);
        //                    }
        //                    if (reader["IDTimbreDigital"] != (object)DBNull.Value)
        //                    {
        //                        elComprobante.IDTimbreDigital = Convert.ToInt32(reader["IDTimbreDigital"]);
        //                    }
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "cfd.ComplementoPago", 0, string.Format("Error al consultar cfd.ComplementoPago con ID {0}. Excepción: {1}", ID_comrpobante, ex.Message), true);
        //            }
        //            return elComprobante;
        //        }

        public ComprobantePagoEntity getDatosComprobante(int ID_comrpobante)
        {
            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
            ComprobantePagoEntity elComprobante = new ComprobantePagoEntity();
            try
            {
                string sql = @"SELECT cfd.ComprobantePago.Moneda as Moneda,cfd.ComprobantePago.TipoCambio as TipoDeCambio ,Cobranza.Pago.Total as Total from cfd.ComprobantePago
JOIN Cobranza.Pago ON Cobranza.Pago.IDPago = cfd.ComprobantePago.IDPago
where cfd.ComprobantePago.ID_ComprobantePago  = @idcomrobante";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@idcomrobante", SqlDbType.Int).Value = ID_comrpobante;
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    elComprobante.Moneda = reader["Moneda"].ToString();
                    elComprobante.TipoCambio = reader["TipoDeCambio"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TipoDeCambio"]);
                    elComprobante.Total = reader["Total"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Total"]);
                }
                reader.Close();
                conexion.Close();
            }
            catch
            {
                conexion.Close();                
            }
            return elComprobante;
        }
        //        public ComplementoPagoEntity getComplementoPago(int ID_comrpobante)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            ComplementoPagoEntity elComplemento = null;
        //            try
        //            {
        //                string sql = @"SELECT [version],[FechaPago],[FormaDePagoP],[MonedaP],[TipoCambioP],[Monto],[NumOperacion],[RfcEmisorCtaOrd],[NomBancoOrdExt],[CtaOrdenante],[RfcEmisorCtaBen],[CtaBeneficiario],[TipoCadPago],[CertPago],[CadPago],[SelloPago]
        //  FROM [cfd].[ComplementoPago]
        //  WHERE [ID_ComprobantePago] = @idcomrobante";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@idcomrobante", SqlDbType.Int).Value = ID_comrpobante;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                elComplemento = new ComplementoPagoEntity();
        //                while (reader.Read())
        //                {
        //                    elComplemento.Version = reader["version"].ToString();
        //                    if (reader["FechaPago"] != (object)DBNull.Value)
        //                    {
        //                        elComplemento.FechaPago = Convert.ToDateTime(reader["FechaPago"]);
        //                    }
        //                    elComplemento.FormaDePagoP = reader["FormaDePagoP"].ToString();
        //                    elComplemento.MonedaP = reader["MonedaP"].ToString();
        //                    if (reader["TipoCambioP"] != (object)DBNull.Value)
        //                    {
        //                        elComplemento.TipoCambioP = Convert.ToDecimal(reader["TipoCambioP"]);
        //                    }
        //                    if (reader["Monto"] != (object)DBNull.Value)
        //                    {
        //                        elComplemento.Monto = Convert.ToDecimal(reader["Monto"]);
        //                    }
        //                    elComplemento.NumOperacion = reader["NumOperacion"].ToString();
        //                    elComplemento.RFCEmisorCtaOrd = reader["RfcEmisorCtaOrd"].ToString();
        //                    elComplemento.NomBancoOrdExt = reader["NomBancoOrdExt"].ToString();
        //                    elComplemento.CtaOrdenante = reader["CtaOrdenante"].ToString();
        //                    elComplemento.RfcEmisorCtaBen = reader["RfcEmisorCtaBen"].ToString();
        //                    elComplemento.CtaBeneficiario = reader["CtaBeneficiario"].ToString();
        //                    elComplemento.TipoCadPago = reader["TipoCadPago"].ToString();
        //                    elComplemento.CertPago = reader["CertPago"].ToString();
        //                    elComplemento.CadPago = reader["CadPago"].ToString();
        //                    elComplemento.SelloPago = reader["SelloPago"].ToString();
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "cfd.ComplementoPago", 0, string.Format("Error al consultar cfd.ComplementoPago con ID {0}. Excepción: {1}", ID_comrpobante, ex.Message), true);
        //            }
        //            return elComplemento;
        //        }

        //        public ComprobanteCertificadoEntity getCertificadoComprobante(string ID_Contribuyente)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            ComprobanteCertificadoEntity elCertificado = new ComprobanteCertificadoEntity();
        //            try
        //            {
        //                string sql = @"SELECT [ID_Contribuyente],[NoCertificado],[Certificado]      
        //  FROM [Empresa].[SelloDigital]
        //  WHERE [ID_Contribuyente] = @IDContr";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDContr", SqlDbType.NVarChar).Value = ID_Contribuyente;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    elCertificado.NoCertificado = reader["NoCertificado"].ToString();
        //                    elCertificado.Certificado = reader["Certificado"].ToString();
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "[Empresa].[SelloDigital]", 0, string.Format("Error al consultar [Empresa].[SelloDigital] con ID {0}. Excepción: {1}", ID_Contribuyente, ex.Message), true);
        //            }
        //            return elCertificado;
        //        }

        //        public DataTable getTimbres(int idContribuyente, int idSerieFolio)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            DataTable dtResult = new DataTable();
        //            try
        //            {
        //                string sql = @"SELECT [ID_Serie_Folio] AS ID,[Descripcion] +' - '+[Serie] AS DESCRIPCION    
        //  FROM [Empresa].[Serie_Folio] WHERE [ID_Contribuyente]=" + idContribuyente + " AND [TipoUso] = 2";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                dtResult.Load(reader);
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "[Empresa].[Serie_Folio]", 0, string.Format("Error al consultar [Empresa].[Serie_Folio]. Excepción: {0}", ex.Message), true);
        //            }
        //            return dtResult;
        //        }

        //        public List<PendientesDeTimbrarEntity> getPendientesDeTimbrar(int idContribuyente)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            List<PendientesDeTimbrarEntity> ListPendientes = new List<PendientesDeTimbrarEntity>();
        //            try
        //            {
        //                string sql = @"SELECT ID_ComprobantePago,IDXMLComprobantePAgo, rutaXML,XmlContenido,XMLTimbrado,IDPago,ID_SerieFolio,ComprobantePago.ID_Contribuyente,RazonSocial,ID_Cliente FROM cfd.XMLComprobantePago
        //    JOIN cfd.ComprobantePago ON cfd.ComprobantePago.ID_XML = cfd.XMLComprobantePago.IDXMLComprobantePago
        //	JOIN Empresa.Contribuyente ON Empresa.Contribuyente.ID_Contribuyente = ComprobantePago.ID_Contribuyente
        //WHERE ComprobantePago.ID_Contribuyente = @idContribuyente AND XMLTimbrado = 0";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@idContribuyente", SqlDbType.Int).Value = idContribuyente;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    PendientesDeTimbrarEntity pendiente = new PendientesDeTimbrarEntity();
        //                    pendiente.idComprobante = (int)reader["ID_ComprobantePago"];
        //                    pendiente.idXML = (int)reader["IDXMLComprobantePAgo"];
        //                    pendiente.XMLContenido = (string)reader["XmlContenido"];
        //                    pendiente.EstaTimbrado = (bool)reader["XMLTimbrado"];
        //                    pendiente.RutaXML = (string)reader["rutaXML"];
        //                    pendiente.IDPago = (int)reader["IDPago"];
        //                    pendiente.ID_SerieFolio = (int)reader["ID_SerieFolio"];
        //                    pendiente.ID_Contribuyente = (int)reader["ID_Contribuyente"];
        //                    pendiente.RazonSocial = (string)reader["RazonSocial"];
        //                    pendiente.ID_Cliente = (string)reader["ID_Cliente"];
        //                    ListPendientes.Add(pendiente);
        //                }
        //                reader.Close();
        //                conexion.Close();
        //                return ListPendientes;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "cfd.TimbrePendiente", 0, string.Format("Error al consultar cfd.TimbrePendiente. Excepción: {0}", ex.Message), true);
        //                return null;
        //            }

        //        }
        //        public DataTable getTimbres()
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            DataTable dtResult = new DataTable();
        //            try
        //            {
        //                string sql = @"SELECT [ID_Serie_Folio] AS ID,[Descripcion] AS DESCRIPCION      
        //  FROM [Empresa].[Serie_Folio]";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                dtResult.Load(reader);
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "[Empresa].[Serie_Folio]", 0, string.Format("Error al consultar [Empresa].[Serie_Folio]. Excepción: {0}", ex.Message), true);
        //            }
        //            return dtResult;
        //        }

        //        public int getFolioActual(int ID_SerieFolio)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            int Serie = -1;
        //            try
        //            {
        //                string sql = @"SELECT [FolioActual]      
        //  FROM [Empresa].[Serie_Folio]
        //  WHERE [ID_Serie_Folio] = @IDSerie";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDSerie", SqlDbType.Int).Value = ID_SerieFolio;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    Serie = Convert.ToInt32(reader["FolioActual"].ToString());
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "[Empresa].[Serie_Folio]", 0, string.Format("Error al consultar [Empresa].[Serie_Folio]. Excepción: {0}", ex.Message), true);
        //            }
        //            return Serie;
        //        }

        //        public string getSerieActual(int ID_SerieFolio)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            string Serie = "";
        //            try
        //            {
        //                string sql = @"SELECT [Serie]      
        //  FROM [Empresa].[Serie_Folio]
        //  WHERE [ID_Serie_Folio] = @IDSerie";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDSerie", SqlDbType.Int).Value = ID_SerieFolio;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    Serie = reader["Serie"].ToString();
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "[Empresa].[Serie_Folio]", 0, string.Format("Error al consultar [Empresa].[Serie_Folio]. Excepción: {0}", ex.Message), true);
        //            }
        //            return Serie;
        //        }

        //        public ComprobanteDocumentoRelacionado GetComprobanteDocumentoRelacionado(int ID_CFD)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            ComprobanteDocumentoRelacionado elComprobanteDocumentoRelacionado = null;
        //            try
        //            {
        //                string sql = @"SELECT cfd.CFD.UUID, cfd.CFD.Serie, cfd.CFD.Folio, cfd.CFD.Moneda, cfd.CFD.TipoDeCambio, cfd.DocumentosRelacionados.NumParcialidad, cfd.DocumentosRelacionados.ImpSaldoAnt, cfd.DocumentosRelacionados.ImpPagado, cfd.DocumentosRelacionados.ImpSaldoInsoluto
        //FROM cfd.DocumentosRelacionados JOIN cfd.CFD ON cfd.CFD.ID_CFD = cfd.DocumentosRelacionados.ID_CFD
        //WHERE [cfd].[CFD].[ID_CFD] =  @idcfd";

        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@idcfd", SqlDbType.Int).Value = ID_CFD;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                elComprobanteDocumentoRelacionado = new ComprobanteDocumentoRelacionado();
        //                while (reader.Read())
        //                {
        //                    elComprobanteDocumentoRelacionado.UUID = reader["UUID"].ToString();
        //                    elComprobanteDocumentoRelacionado.Serie = reader["Serie"].ToString();
        //                    elComprobanteDocumentoRelacionado.Folio = reader["Folio"].ToString();
        //                    elComprobanteDocumentoRelacionado.Moneda = reader["Moneda"].ToString();
        //                    if (elComprobanteDocumentoRelacionado.Moneda == "DLS")
        //                        elComprobanteDocumentoRelacionado.Moneda = "USD";

        //                    if (reader["TipoDeCambio"] != (object)DBNull.Value)
        //                        elComprobanteDocumentoRelacionado.TipoDeCambio = decimal.Round(Convert.ToDecimal(reader["TipoDeCambio"]), 2);

        //                    if (reader["NumParcialidad"] != (object)DBNull.Value)
        //                        elComprobanteDocumentoRelacionado.NumParcialidad = Convert.ToInt32(reader["NumParcialidad"]);

        //                    if (reader["ImpSaldoAnt"] != (object)DBNull.Value)
        //                        elComprobanteDocumentoRelacionado.ImpSaldoAnt = Convert.ToDecimal(reader["ImpSaldoAnt"]);

        //                    if (reader["ImpPagado"] != (object)DBNull.Value)
        //                        elComprobanteDocumentoRelacionado.ImpPagado = Convert.ToDecimal(reader["ImpPagado"]);

        //                    if (reader["ImpSaldoInsoluto"] != (object)DBNull.Value)
        //                        elComprobanteDocumentoRelacionado.ImpSaldoInsoluto = Convert.ToDecimal(reader["ImpSaldoInsoluto"]);
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "cfd.ComplementoPago", 0, string.Format("Error al consultar Documento Relacionado con ID_CFD {0}. Excepción: {1}", ID_CFD, ex.Message), true);
        //            }
        //            return elComprobanteDocumentoRelacionado;
        //        }

        //        public ComprobanteConceptoCFDIEntity GetComprobanteConceptoCFDI(int ID_CFd)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            ComprobanteConceptoCFDIEntity elComprobanteConceptoCFDIEntity = null;
        //            try
        //            {
        //                string sql = @"SELECT TOP (1) [ClaveProdServ],[NoIdentificacion],[Cantidad],[ClaveUnidad],[Unidad],[Descripcion],[ValorUnitario],[Importe],[Descuento]    
        //  FROM [cfd].[ConceptoCFDI]
        //  WHERE [ID_CFd] = @idcfd";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@idcfd", SqlDbType.Int).Value = ID_CFd;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                elComprobanteConceptoCFDIEntity = new ComprobanteConceptoCFDIEntity();
        //                while (reader.Read())
        //                {
        //                    elComprobanteConceptoCFDIEntity.ClaveProdServ = reader["ClaveProdServ"].ToString();
        //                    elComprobanteConceptoCFDIEntity.NoIdentificacion = reader["NoIdentificacion"].ToString();
        //                    if (reader["Cantidad"] != (object)DBNull.Value)
        //                    {
        //                        elComprobanteConceptoCFDIEntity.Cantidad = Convert.ToDecimal(reader["Cantidad"]);
        //                    }
        //                    elComprobanteConceptoCFDIEntity.ClaveUnidad = reader["ClaveUnidad"].ToString();
        //                    elComprobanteConceptoCFDIEntity.Unidad = reader["Unidad"].ToString();
        //                    elComprobanteConceptoCFDIEntity.Descripcion = reader["Descripcion"].ToString();
        //                    if (reader["ValorUnitario"] != (object)DBNull.Value)
        //                    {
        //                        elComprobanteConceptoCFDIEntity.ValorUnitario = Convert.ToDecimal(reader["ValorUnitario"]);
        //                    }
        //                    if (reader["Importe"] != (object)DBNull.Value)
        //                    {
        //                        elComprobanteConceptoCFDIEntity.Importe = Convert.ToDecimal(reader["Importe"]);
        //                    }
        //                    if (reader["Descuento"] != (object)DBNull.Value)
        //                    {
        //                        elComprobanteConceptoCFDIEntity.Descuento = Convert.ToDecimal(reader["Descuento"]);
        //                    }
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "cfd.[ConceptoCFDI]", 0, string.Format("Error al consultar Documento Relacionado con ID_CFD {0}. Excepción: {1}", ID_CFd, ex.Message), true);
        //            }
        //            return elComprobanteConceptoCFDIEntity;
        //        }

        //        public ComprobanteCertificadoKey getComprobanteCertificadoKey(int ID_SerieFolio)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            ComprobanteCertificadoKey elComprobanteCertificadoKey = null;
        //            try
        //            {
        //                string sql = @"SELECT Empresa.SelloDigital.ID_SelloDigital, NoCertificado, Archivo_Key, Archivo_Cer, CAST(ClavePrivada as nvarchar(max)) AS Claveprivada
        //FROM Empresa.SelloDigital JOIN Empresa.Serie_Folio ON Empresa.Serie_Folio.ID_SelloDigital = Empresa.SelloDigital.ID_SelloDigital
        //WHERE Empresa.Serie_Folio.ID_Serie_Folio = @IDSerie";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IDSerie", SqlDbType.Int).Value = ID_SerieFolio;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                elComprobanteCertificadoKey = new ComprobanteCertificadoKey();
        //                while (reader.Read())
        //                {
        //                    elComprobanteCertificadoKey.NoCertificado = reader["NoCertificado"].ToString();
        //                    elComprobanteCertificadoKey.Archivo_Key = reader["Archivo_Key"].ToString();
        //                    elComprobanteCertificadoKey.Archivo_Cer = reader["Archivo_Cer"].ToString();
        //                    elComprobanteCertificadoKey.Archivo_Cer = elComprobanteCertificadoKey.Archivo_Cer;
        //                    elComprobanteCertificadoKey.Claveprivada = reader["Claveprivada"].ToString();
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "Empresa.SelloDigital JOIN Empresa.Serie_Folio", 0, string.Format("Error al consultar SelloDigital y Serie_Folio con ID_SerieFolio {0}. Excepción: {1}", ID_SerieFolio, ex.Message), true);
        //            }
        //            return elComprobanteCertificadoKey;
        //        }

        //        /// <summary>
        //        /// INSERTAR TIMBRE FISCAL DIGITAL EN BASE DE DATOS cfd.TimbreDigital 
        //        /// </summary>
        //        /// <param name="tfd"></param>
        //        /// <returns></returns>
        //        public int InsertarTimbreFiscalDigital(SaariCFD.BusinessLayer.Entities.TimbreDigitalV33 timbre)
        //        {
        //            //MsgError = string.Empty;

        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = @"INSERT INTO [cfd].[TimbreDigital]
        //               ([Version]
        //               ,[UUID]
        //               ,[FechaTimbrado]
        //               ,[RfcProvCertif]
        //               ,[SelloCFD]
        //               ,[SelloSAT]
        //               ,[NoCerSAT])                    
        //                VALUES(@ver, @uuid, @fechaTimb, @rfcProv, @selloCfd, @selloSat, @noCertSat) 
        //                SELECT SCOPE_IDENTITY()";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@ver", SqlDbType.NVarChar).Value = timbre.VersionTimbreSAT;
        //                comando.Parameters.Add("@uuid", SqlDbType.NVarChar).Value = timbre.UUID;
        //                comando.Parameters.Add("@fechaTimb", SqlDbType.DateTime).Value = timbre.FechaTimbrado;
        //                comando.Parameters.Add("@rfcProv", SqlDbType.NVarChar).Value = timbre.RfcProvCertif;
        //                comando.Parameters.Add("@selloCfd", SqlDbType.NVarChar).Value = timbre.SelloCFD;
        //                comando.Parameters.Add("@selloSat", SqlDbType.NVarChar).Value = timbre.SelloSAT;
        //                comando.Parameters.Add("@noCertSat", SqlDbType.NVarChar).Value = timbre.NoCertificadoSAT;

        //                conexion.Open();
        //                int id = Convert.ToInt32(comando.ExecuteScalar());
        //                conexion.Close();
        //                return id;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                return -1;
        //            }
        //        }
        //        public string updateXMLWithTimbre(int IdXML, XmlDocument XMLUpdate)
        //        {
        //            string sql = @"UPDATE [cfd].[XMLComprobantePago]
        //   SET [XmlContenido] = @XmlContenido,
        //       [XMLTimbrado] = @XMLTimbrado,
        //       [LastUpdateDate] = @LastUpdateDate
        // WHERE IDXMLComprobantePago=@IDXML";
        //            try
        //            {
        //                SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add(new SqlParameter("@XmlContenido", SqlDbType.Xml)
        //                {
        //                    Value = new System.Data.SqlTypes.SqlXml(new XmlTextReader(XMLUpdate.InnerXml, XmlNodeType.Document, null))
        //                });

        //                comando.Parameters.AddWithValue("@XMLTimbrado", true);
        //                comando.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now.Date);
        //                comando.Parameters.AddWithValue("@IDXML", IdXML);
        //                conexion.Open();
        //                int result = (int)comando.ExecuteNonQuery();
        //                conexion.Close();
        //                return "";
        //            }
        //            catch (Exception ex)
        //            {
        //                error = true;
        //                return mensajeError = "Ocurrio un problema al intentar actualizar el XML del Comprobante. Error: " + Environment.NewLine + ex.Message;

        //            }
        //        }

        //        public bool updateIDTimbreEnComprobante(int IDTimbreDigital, int ID_ComprobantePago, int idXML)
        //        {
        //            string sql = @"UPDATE [cfd].[ComprobantePago]
        //   SET IDTimbreDigital = @idtimbre,
        //EstaTimbrado = @EstaTimbrado,
        //ID_XML= @IdXML
        // WHERE [ID_ComprobantePago] = @idcomprobante";
        //            try
        //            {
        //                SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.AddWithValue("@idtimbre", IDTimbreDigital);
        //                comando.Parameters.AddWithValue("@EstaTimbrado", true);
        //                comando.Parameters.AddWithValue("@IdXML", idXML);
        //                comando.Parameters.AddWithValue("@idcomprobante", ID_ComprobantePago);
        //                conexion.Open();
        //                int result = (int)comando.ExecuteNonQuery();
        //                conexion.Close();
        //                return result > 0 ? true : false;
        //            }
        //            catch (Exception ex)
        //            {
        //                error = true;
        //                mensajeError = "Ocurrio un problema al intentar actualizar el ID de timbre en el Comprobante. Error: " + Environment.NewLine + ex.Message;
        //                return false; ;
        //            }
        //        }

        //        public int InsertarXMLComprobante(string ruta, XmlDocument xmlToSave, bool EstaTimbrado, int user, int tipo, bool esRetimbrado)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = "";
        //                if (esRetimbrado)
        //                {
        //                    sql = @"UPDATE [cfd].[XMLComprobantePago] SET [rutaXml]=@rutaXml,[XmlContenido]=@XmlContenido,[XMLTimbrado]=XMLTimbrado,[LastUpdateDate]=@Lastupdatedate,[ModifyByUser]=@Modifybyuser,
        //                        [Tipo]=@Tipo";
        //                }
        //                else
        //                {
        //                    sql = @"
        //INSERT INTO [cfd].[XMLComprobantePago]
        //           ([rutaXml]
        //           ,[XmlContenido]
        //           ,[XMLTimbrado]
        //           ,[LastUpdateDate]
        //           ,[ModifyByUser]
        //           ,[Tipo])
        //     VALUES
        //           (@rutaXml,@XmlContenido,@XMLTimbrado,@ModifyByUser,@LastUpdateDate,@Tipo)
        //                SELECT SCOPE_IDENTITY()";
        //                }
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@rutaXml", SqlDbType.NVarChar).Value = ruta;
        //                comando.Parameters.Add(new SqlParameter("@XmlContenido", SqlDbType.Xml)
        //                {
        //                    Value = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlToSave.InnerXml, XmlNodeType.Document, null))
        //                });
        //                comando.Parameters.Add("@XMLTimbrado", SqlDbType.Bit).Value = EstaTimbrado;
        //                comando.Parameters.Add("@Modifybyuser", SqlDbType.Int).Value = user;
        //                comando.Parameters.Add("@Lastupdatedate", SqlDbType.DateTime).Value = DateTime.Now;
        //                comando.Parameters.Add("@Tipo", SqlDbType.Int).Value = tipo;

        //                conexion.Open();
        //                int id = Convert.ToInt32(comando.ExecuteScalar());
        //                conexion.Close();
        //                return id;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                //MsgError = ex.Message;
        //                return -1;
        //            }
        //        }

        //        public int getIDUsuario(string usuario)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            int elUsuario = -1;
        //            try
        //            {
        //                string sql = @"SELECT UserID FROM Application.Users WHERE UserName  = @Usuario";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@Usuario", SqlDbType.NVarChar).Value = usuario;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    elUsuario = Convert.ToInt32(reader["UserID"].ToString());
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "Application.Users", 0, string.Format("Error al consultar Application.Users. Excepción: {0}", ex.Message), true);
        //            }
        //            return elUsuario;
        //        }

        //        public int InsertarCadenaOriginal(string CadOriginal, int IDComprobante, bool esRetimbrado)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {
        //                string sql = "";
        //                if (esRetimbrado)
        //                {
        //                    sql = @"UPDATE cfd.CadenaOriginalComprobante   SET CadenaOriginal = @cad WHERE IDComprobante = @Comprobante";
        //                }
        //                else
        //                {
        //                    sql = @"INSERT INTO cfd.CadenaOriginalComprobante(CadenaOriginal, IDComprobante) 
        //                VALUES(@cad, @idcomprobante) 
        //                SELECT SCOPE_IDENTITY()";
        //                }
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@cad", SqlDbType.NVarChar).Value = CadOriginal;
        //                comando.Parameters.Add("@idcomprobante", SqlDbType.Int).Value = IDComprobante;
        //                conexion.Open();
        //                int id = Convert.ToInt32(comando.ExecuteScalar());
        //                conexion.Close();
        //                return id;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                return -1;
        //            }
        //        }


        //        public string getRutaDirectorio(int ID_Contribuyente)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            string laRutaDirectorio = "";
        //            try
        //            {
        //                string sql = @"SELECT [RutaDirectorio] FROM [Empresa].[Contribuyente] WHERE ID_Contribuyente = @idcontribuyente";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@idcontribuyente", SqlDbType.Int).Value = ID_Contribuyente;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    laRutaDirectorio = reader["RutaDirectorio"].ToString();
        //                }
        //                reader.Close();
        //                conexion.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "Contribuyente", 0, string.Format("Error al consultar Contribuyente. Excepción: {0}", ex.Message), true);
        //            }
        //            return laRutaDirectorio;
        //        }

        //        public string AumentarFolio(int ID_SerieFolio)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            string Serie = "";
        //            try
        //            {
        //                var ultimoFolio = getFolioActual(ID_SerieFolio);
        //                var folio = ultimoFolio++;
        //                string sql = @"UPDATE Empresa.Serie_Folio SET FolioActual = @FolioActual where ID_Serie_Folio = @IdSerieFolio";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@FolioActual", SqlDbType.Int).Value = ultimoFolio;
        //                comando.Parameters.Add("@IdSerieFolio", SqlDbType.Int).Value = ID_SerieFolio;
        //                conexion.Open();
        //                comando.ExecuteScalar();
        //                conexion.Close();
        //                return Serie = string.Empty;

        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Actualizar Folio", "[Empresa].[Serie_Folio]", 0, string.Format("Error al Actualizar Folio Actual de la tabla [Empresa].[Serie_Folio]. Excepción: {0}", ex.Message), true);
        //                return string.Format("Error al Actualizar Folio Actual de la tabla [Empresa].[Serie_Folio]. Excepción: {0}", ex.Message);
        //            }
        //        }
        //        public string DeletePagoRollBack(int idPago)
        //        {
        //            string sql = @"DELETE FROM Cobranza.Pago Where IDPago = @IDPago";
        //            try
        //            {
        //                SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.AddWithValue("@IDPago", idPago);
        //                conexion.Open();
        //                int result = (int)comando.ExecuteNonQuery();
        //                conexion.Close();
        //                return "Se realizo RollBack del Pago de manera Exitosa.";
        //            }
        //            catch (Exception ex)
        //            {
        //                insertLog("DeletePagoRollBack", "[Cobranza].[Pago]", 0, string.Format("Error realizar el rollBack del pago {0},  Excepción: {1}", idPago, ex.Message), true);
        //                return mensajeError = "Ocurrio un problema al intentar actualizar el ID de timbre en el Comprobante. Error: " + Environment.NewLine + ex.Message;
        //            }
        //        }

        //        public bool DeleteCadenaOriginal(int idComprobante)
        //        {
        //            bool EliminarCadena = false;
        //            try
        //            {
        //                string sql = @"DELETE cfd.CadenaOriginalComprobante WHERE IDComprobante = @idComprobante ";
        //                SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.AddWithValue("@idComprobante", idComprobante);
        //                conexion.Open();
        //                int result = (int)comando.ExecuteNonQuery();
        //                conexion.Close();
        //                EliminarCadena = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                EliminarCadena = false;
        //            }
        //            return EliminarCadena;
        //        }
        //        public string DeleteComprobanteRollBack(int idComprobante)
        //        {
        //            bool seElimino = DeleteCadenaOriginal(idComprobante);
        //            if (seElimino)
        //            {
        //                try
        //                {
        //                    string sql = @"DELETE FROM cfd.ComprobantePago WHERE ID_ComprobantePago =  @IDComplemento";
        //                    SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //                    SqlCommand comando = new SqlCommand(sql, conexion);
        //                    comando.Parameters.AddWithValue("@IDComplemento", idComprobante);
        //                    conexion.Open();
        //                    int result = (int)comando.ExecuteNonQuery();
        //                    conexion.Close();
        //                    return "Se realizo RollBack del Comprobante de manera Exitosa.";
        //                }
        //                catch (Exception ex)
        //                {
        //                    insertLog("DeleteComprobanteRollBack", "[Cobranza].[Pago]", 0, string.Format("Error realizar el rollBack del Comprobante de Pago {0},  Excepción: {1}", idComprobante, ex.Message), true);
        //                    return mensajeError = "Ocurrio un problema al intentar eliminar el Comprobante. Error: " + Environment.NewLine + ex.Message;
        //                }
        //            }
        //            else
        //                return "no se pudo eliminar el comprobante porque no se elimino la cadena";
        //        }
        //        public string deleteDocRelacionadosRollBack(int idDocRelacionado)
        //        {
        //            string sql = @"	DELETE FROM cfd.DocumentosRelacionados where ID_Consecutivo  = @IDDocRelacionado";
        //            try
        //            {
        //                SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.AddWithValue("@IDDocRelacionado", idDocRelacionado);
        //                conexion.Open();
        //                int result = (int)comando.ExecuteNonQuery();
        //                conexion.Close();
        //                return "Se realizo RollBack del Doc. Relacionado de manera Exitosa.";
        //            }
        //            catch (Exception ex)
        //            {
        //                insertLog("deleteDocRelacionadosRollBack", "[Cobranza].[Pago]", 0, string.Format("Error realizar el rollBack del Doc. Relacionado de complemento {0},  Excepción: {1}", idDocRelacionado, ex.Message), true);
        //                return mensajeError = "Ocurrio un problema al intentar eliminar el documento relacionado . Error: " + Environment.NewLine + ex.Message;
        //            }
        //        }

        //        public string DeleteComplementoRollBack(int idComplemento)
        //        {
        //            string sql = @"DELETE FROM cfd.ComplementoPago WHERE ID_Complemento = @IDComplemento";
        //            try
        //            {
        //                SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.AddWithValue("@IDComplemento", idComplemento);
        //                conexion.Open();
        //                int result = (int)comando.ExecuteNonQuery();
        //                conexion.Close();
        //                return "Se realizo RollBack del Complemento de manera Exitosa.";
        //            }
        //            catch (Exception ex)
        //            {
        //                insertLog("DeleteComplementoRollBack", "[Cobranza].[Pago]", 0, string.Format("Error realizar el rollBack del Complemento de Pago {0},  Excepción: {1}", idComplemento, ex.Message), true);
        //                return mensajeError = "Ocurrio un problema al intentar eliminar el complemento. Error: " + Environment.NewLine + ex.Message;
        //            }
        //        }

        //        public int getTimbresDisponibles(int idSerieFolio)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            EmpesaSerieFolio EmpresaFolio = new EmpesaSerieFolio();
        //            try
        //            {
        //                string sql = @"SELECT FolioFinal, FolioActual FROM Empresa.Serie_Folio WHERE ID_Serie_Folio = @IdSerieFolio";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@IdSerieFolio", SqlDbType.Int).Value = idSerieFolio;
        //                conexion.Open();
        //                SqlDataReader reader = comando.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    EmpresaFolio.FolioFinal = (int)reader["FolioFinal"];
        //                    EmpresaFolio.FolioActual = (int)reader["FolioActual"];
        //                }
        //                reader.Close();
        //                conexion.Close();
        //                return EmpresaFolio.FoliosDisponibles = EmpresaFolio.FolioFinal - EmpresaFolio.FolioActual;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Consultar", "[Empresa].[Serie_Folio]", 0, string.Format("Error al consultar [Empresa].[Serie_Folio]. Excepción: {0}", ex.Message), true);
        //            }
        //            return 0;
        //        }

        private DataTable SqlQuery(string sql)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaDeConexion))
            {
                SqlCommand comando = new SqlCommand(sql, conexion);
                DataTable dtResult = new DataTable();
                try
                {
                    conexion.Open();
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        dtResult.Load(reader);
                        reader.Close();
                    }

                }
                catch
                {
                    dtResult = new DataTable();
                }
                conexion.Close();
                return dtResult;
            }
        }

        public List<DatosGridEntity> GetDatosGrid(FiltrosEntity elFiltro)
        {
            List<int> listarecibos = inmoDAL.getRecibos(elFiltro.Inmobiliaria);

            List<DatosGridEntity> listaDatosGrid = new List<DatosGridEntity>();

            string sql = @"SELECT        
	cfd.ComprobantePago.ID_ComprobantePago As ID,
	cfd.ComprobantePago.Serie, 
	cfd.ComprobantePago.Folio, 
	cfd.ComprobantePago.Fecha AS FechaEmision, 
	cfd.ComprobantePago.ID_Cliente, 
	cfd.ComprobantePago.Moneda,
	cfd.ComprobantePago.Total,
    cfd.ComprobantePago.IDPago
FROM 
	cfd.ComprobantePago 
	INNER JOIN cfd.TimbreDigital ON cfd.ComprobantePago.IDTimbreDigital = cfd.TimbreDigital.ID_Timbre 
	INNER JOIN Cobranza.Pago ON cfd.ComprobantePago.IDPago = Cobranza.Pago.IDPago
    INNER JOIN cfd.ComplementoPago ON cfd.ComplementoPago.ID_ComprobantePago = cfd.ComprobantePago.ID_ComprobantePago";

            sql += " WHERE cfd.ComprobantePago.[Fecha] >= '" + elFiltro.FechaDel.ToShortDateString() + "' AND cfd.ComprobantePago.[Fecha] <= '" + elFiltro.FechaAl.ToShortDateString() + "'";
            if(elFiltro.Serie.Length > 0)
            {
                sql += " AND cfd.ComprobantePago.Serie = '" + elFiltro.Serie + "'";
            }
            if(elFiltro.SerieDel > 0 && elFiltro.SerieAl > 0 && elFiltro.SerieDel <= elFiltro.SerieAl)
            {
                sql += " AND cfd.ComprobantePago.Folio >= " + elFiltro.SerieDel.ToString() + " AND cfd.ComprobantePago.Folio <= " + elFiltro.SerieAl.ToString();
            }

            DataTable dt = SqlQuery(sql);
            if (dt != null)
            {
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            var elRecibo = Convert.ToInt32(row["IDPago"]);
                            if (listarecibos.Contains(elRecibo))
                            {                                
                                DatosGridEntity elDato = new DatosGridEntity
                                {
                                    ID = Convert.ToInt32(row["ID"]),
                                    Serie = row["Serie"].ToString(),
                                    Folio = Convert.ToInt32(row["Folio"]),
                                    FechaEmision = Convert.ToDateTime(row["FechaEmision"]),
                                    Cliente = inmoDAL.getDatosCliente(row["ID_Cliente"].ToString().Trim()),
                                    Moneda = row["Moneda"].ToString(),
                                    Total = Convert.ToDecimal(row["Total"]),
                                    IDPago = Convert.ToInt32(row["IDPago"])
                                };

                                listaDatosGrid.Add(elDato);
                            }

                        }
                    }
                }
                catch
                {
                    listaDatosGrid = new List<DatosGridEntity>();
                }
            }

            return listaDatosGrid;
        }

        public int GetIdXmlComprobante(int ID_ComprobantePago)
        {
            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
            string sql = @"SELECT [ID_XML]
  FROM [BD_SaariCFD].[cfd].[ComprobantePago]
  WHERE [ID_ComprobantePago] = @idcomprobante";
            try
            {
                int elID = -1;
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@idcomprobante", SqlDbType.Int).Value = ID_ComprobantePago;
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    elID = (int)reader["ID_XML"];
                }
                reader.Close();
                conexion.Close();
                return elID;
            }
            catch
            {
                conexion.Close();
                return -1;
            }
        }

        public RutaContenidoXmlEntity GetrutaContenidoXml(int ID_XML)
        {
            RutaContenidoXmlEntity rutaContenidoXml = new RutaContenidoXmlEntity();
            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
            string sql = @"SELECT [rutaXml],[XmlContenido]   
  FROM [BD_SaariCFD].[cfd].[XMLComprobantePago]
  WHERE [IDXMLComprobantePago] =  @idxml";
            try
            {
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@idxml", SqlDbType.Int).Value = ID_XML;
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {                    
                    rutaContenidoXml.Ruta = reader["rutaXml"].ToString();
                    rutaContenidoXml.XmlDocumento = reader["XmlContenido"].ToString();
                }
                reader.Close();
                conexion.Close();
                
                return rutaContenidoXml;
            }
            catch
            {
                return null;
            }
        }

        public string GetIDContribuyente(string idArrendadora)
        {
            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
            string idContribuyente = "0";
            try
            {
                string sql = @"SELECT ID_Contribuyente
  FROM Empresa.Contribuyente
  WHERE ID_Arrendadora = @idArrendadora";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@idArrendadora", SqlDbType.NVarChar).Value = idArrendadora;
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    idContribuyente = reader["ID_Contribuyente"].ToString();
                }
                reader.Close();
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();                
            }
            return idContribuyente;
        }

        public string GetRutaDirectorio(int ID_Contribuyente)
        {
            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
            string laRutaDirectorio = "";
            try
            {
                string sql = @"SELECT [RutaDirectorio] FROM [Empresa].[Contribuyente] WHERE ID_Contribuyente = @idcontribuyente";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@idcontribuyente", SqlDbType.Int).Value = ID_Contribuyente;
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    laRutaDirectorio = reader["RutaDirectorio"].ToString();
                }
                reader.Close();
                conexion.Close();
            }
            catch 
            {
                conexion.Close();                
            }
            return laRutaDirectorio;
        }

        public string getCadenaOriginal(string UUID)
        {            
            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
            string sql = @"
        SELECT  CadenaOriginal FROM cfd.CadenaOriginalComprobante
                    JOIN cfd.ComprobantePago ON  ID_ComprobantePago = cfd.CadenaOriginalComprobante.IDComprobante
        			JOIN cfd.TimbreDigital on IDTimbreDigital = cfd.ComprobantePago.IDTimbreDigital
        			WHERE cfd.TimbreDigital.UUID = @UUID";
            try
            {
                string cadOriginal = string.Empty;
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@uuid", SqlDbType.NVarChar).Value = UUID;
                conexion.Open();
                cadOriginal = comando.ExecuteScalar().ToString();
                conexion.Close();
                return cadOriginal;
            }
            catch
            {
                conexion.Close();
                return string.Empty;
            }
        }

        public static string getRutaLogoContribuyente(int IDContribuyente)
        {
            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
            string sql = @"SELECT Logo FROM Empresa.Contribuyente WHERE ID_Contribuyente = @IDContribuyente";
            try
            {
                string logo = string.Empty;
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@IDContribuyente", SqlDbType.Int).Value = IDContribuyente;
                conexion.Open();
                logo = comando.ExecuteScalar().ToString();
                conexion.Close();
                return logo;
            }
            catch
            {
                conexion.Close();
                return string.Empty;
            }
        }

        public static string getRutaCedulaContribuyente(int IDContribuyente)
        {
            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
            string sql = @"SELECT CedulaFiscal FROM Empresa.Contribuyente WHERE ID_Contribuyente = @IDContribuyente";
            try
            {
                string cedula = string.Empty;
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@IDContribuyente", SqlDbType.Int).Value = IDContribuyente;
                conexion.Open();
                cedula = comando.ExecuteScalar().ToString();
                conexion.Close();
                return cedula;
            }
            catch
            {
                conexion.Close();
                return string.Empty;
            }
        }

        public static string getRegimenFiscal(string claveRegimen)
        {
            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
            string sql = @"SELECT Descripcion FROM SAT.RegimenFiscal Where ClaveRegimenFiscal = @claveRegimen";
            try
            {
                string cedula = string.Empty;
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@claveRegimen", SqlDbType.NVarChar).Value = claveRegimen;
                conexion.Open();
                cedula = comando.ExecuteScalar().ToString();
                conexion.Close();
                return cedula;
            }
            catch
            {
                conexion.Close();
                return string.Empty;
            }
        }

        public static string getFormaPago(string formaPago)
        {
            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
            string sql = @" SELECT Descripcion FROM SAT.FormaDePago Where ClaveFormaDePago = @FormaPago";
            try
            {
                string cedula = string.Empty;
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@FormaPago", SqlDbType.NVarChar).Value = formaPago;
                conexion.Open();
                cedula = comando.ExecuteScalar().ToString();
                conexion.Close();
                return cedula;
            }
            catch
            {
                conexion.Close();
                return string.Empty;
            }
        }

        public static string getFormaPago(int idComprobante)
        {
            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
            string sql = @"SELECT FormaPago,SAT.FormaDePago.Descripcion AS descripcion FROM cfd.ComprobantePago  
        JOIN SAT.FormaDePago ON  cfd.ComprobantePago.FormaPago =SAT.FormaDePago.ClaveFormaDePago
        WHERE ID_ComprobantePago =@IdComprobante";
            try
            {
                string FormaPago = string.Empty;
                string forma = string.Empty;
                string descripcion = string.Empty;
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@IdComprobante", SqlDbType.Int).Value = idComprobante;
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    forma = (string)reader["FormaPago"];
                    descripcion = (string)reader["descripcion"];
                }
                conexion.Close();
                FormaPago = forma + " - " + descripcion;
                return FormaPago;
            }
            catch
            {
                conexion.Close();
                return string.Empty;
            }
        }

        //        public static string verificarCfdiCancelado(int idCFD)
        //        {
        //            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
        //            string sql = @"SELECT * FROM cfd.CancelacionProcesada Where ID_CFD =  @ID_CFD";
        //            try
        //            {
        //                string existe = string.Empty;
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@ID_CFD", System.Data.SqlDbType.Int).Value = idCFD;
        //                conexion.Open();
        //                existe = comando.ExecuteScalar().ToString();
        //                conexion.Close();
        //                return existe;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                return string.Empty;
        //            }

        //            //return result;
        //        }

        //        public static string getFormaPagoYMetodo(string formaPago)
        //        {
        //            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
        //            string sql = @" SELECT Descripcion FROM SAT.FormaDePago Where ClaveFormaDePago = @FormaPago";
        //            try
        //            {
        //                string cedula = string.Empty;
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@FormaPago", System.Data.SqlDbType.NVarChar).Value = formaPago;
        //                conexion.Open();
        //                cedula = comando.ExecuteScalar().ToString();
        //                conexion.Close();
        //                return cedula;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                return string.Empty;
        //            }
        //        }
        //        public static string getMetodoPago(string metodoPago)
        //        {
        //            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
        //            string sql = @" SELECT Descripcion FROM SAT.MetodoDePago WHERE ClaveMetodoDePago= @MetodoPago";
        //            try
        //            {
        //                string cedula = string.Empty;
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@MetodoPago", System.Data.SqlDbType.NVarChar).Value = metodoPago;
        //                conexion.Open();
        //                cedula = comando.ExecuteScalar().ToString();
        //                conexion.Close();
        //                return cedula;
        //            }
        //            catch
        //            {
        //                conexion.Close();
        //                return string.Empty;
        //            }
        //        }

        //        public static string getTipoComprobante(string TipoComprobante)
        //        {
        //            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
        //            string sql = @"SELECT Descripcion FROM SAT.TipoComprobante WHERE ClaveTipoComprobante = @TipoComprobante";
        //            try
        //            {
        //                string cedula = string.Empty;
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@TipoComprobante", System.Data.SqlDbType.NVarChar).Value = TipoComprobante;
        //                conexion.Open();
        //                cedula = comando.ExecuteScalar().ToString();
        //                conexion.Close();
        //                return cedula;
        //            }
        //            catch
        //            {
        //                conexion.Close();
        //                return string.Empty;
        //            }
        //        }


        //        public static XmlDocument getXMLTimbradoComprobantePago(int idXML)
        //        {
        //            SqlConnection conexion = new SqlConnection(Configuraciones.CadenaConexionSQLServer);
        //            string sql = @" SELECT XmlContenido FROM cfd.XMLComprobantePago Where cfd.XMLComprobantePago.IDXMLComprobantePago = @idXML";
        //            try
        //            {
        //                string XmlContenido = string.Empty;
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@idXML", System.Data.SqlDbType.NVarChar).Value = idXML;
        //                conexion.Open();
        //                XmlContenido = comando.ExecuteScalar().ToString();
        //                conexion.Close();
        //                XmlDocument xdoc = new XmlDocument();
        //                xdoc.Load(XmlContenido);
        //                return xdoc;
        //            }
        //            catch
        //            {
        //                conexion.Close();
        //                return null;
        //            }
        //        }

        //        public bool updateIDXMLEnComprobante(int ID_ComprobantePago, int idXML)
        //        {
        //            string sql = @"UPDATE [cfd].[ComprobantePago]
        //   SET EstaTimbrado = @EstaTimbrado,
        //ID_XML= @IdXML
        // WHERE [ID_ComprobantePago] = @idcomprobante";
        //            try
        //            {
        //                SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.AddWithValue("@EstaTimbrado", false);
        //                comando.Parameters.AddWithValue("@IdXML", idXML);
        //                comando.Parameters.AddWithValue("@idcomprobante", ID_ComprobantePago);
        //                conexion.Open();
        //                int result = (int)comando.ExecuteNonQuery();
        //                conexion.Close();
        //                return result > 0 ? true : false;
        //            }
        //            catch (Exception ex)
        //            {
        //                error = true;
        //                mensajeError = "Ocurrio un problema al intentar actualizar el ID de XML en el Comprobante. Error: " + Environment.NewLine + ex.Message;
        //                return false; ;
        //            }
        //        }

        //        public string GuardarPendienteDeTimbrar(PagoEntity pagoPendiente)
        //        {
        //            SqlConnection conexion = new SqlConnection(cadenaDeConexion);
        //            try
        //            {

        //                string sql = @"INSERT INTO cfd.TimbrePendiente VALUES (@ID_Comprobante,@ID_Pago,@FechaPendiente)";
        //                SqlCommand comando = new SqlCommand(sql, conexion);
        //                comando.Parameters.Add("@ID_Comprobante", SqlDbType.Int).Value = pagoPendiente.ID_comrpobante;
        //                comando.Parameters.Add("@ID_Pago", SqlDbType.Int).Value = pagoPendiente.PagoID;
        //                comando.Parameters.Add("@FechaPendiente", SqlDbType.Date).Value = DateTime.Now.Date;
        //                conexion.Open();
        //                comando.ExecuteScalar();
        //                conexion.Close();
        //                return string.Empty;
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                insertLog("Guardar Pendiente de Timbrar", "cfd.TimbrePendiente", 0, string.Format("Error al intentar el comprobante pendiente de timbrar, pago : {0} ,ID Comprobante {1}. Excepción: {3}", pagoPendiente.PagoID, pagoPendiente.ID_comrpobante, ex.Message), true);
        //                return string.Format("Error al intentar el comprobante pendiente de timbrar, pago : {0} ,ID Comprobante {1}. Excepción: {3}", pagoPendiente.PagoID, pagoPendiente.ID_comrpobante, ex.Message);
        //            }
        //        }





    }
}
