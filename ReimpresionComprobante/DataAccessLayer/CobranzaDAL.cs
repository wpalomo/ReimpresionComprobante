using ReimpresionComprobante.BusinessLayer;
using ReimpresionComprobante.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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

        public ComprobantePagoEntity GetDatosComprobante(int ID_comrpobante)
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
            List<int> listarecibos = inmoDAL.GetRecibos(elFiltro.Inmobiliaria);

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
                                    ID_Cliente = row["ID_Cliente"].ToString().Trim(),
                                    Cliente = inmoDAL.GetDatosCliente(row["ID_Cliente"].ToString().Trim()),
                                    Moneda = row["Moneda"].ToString(),
                                    Total = Convert.ToDecimal(row["Total"]),
                                    IDPago = Convert.ToInt32(row["IDPago"])
                                };

                                listaDatosGrid.Add(elDato);
                            }

                        }
                        listaDatosGrid = listaDatosGrid.OrderBy(o => o.Cliente).ToList();
                    }
                }
                catch(Exception e)
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
  FROM [cfd].[ComprobantePago]
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
  FROM [cfd].[XMLComprobantePago]
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

        public string GetCadenaOriginal(string UUID)
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

        public static string GetRutaLogoContribuyente(int IDContribuyente)
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

        public static string GetRutaCedulaContribuyente(int IDContribuyente)
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

        public static string GetRegimenFiscal(string claveRegimen)
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

        public static string GetFormaPago(string formaPago)
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

        public static string GetFormaPago(int idComprobante)
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

        

    }
}
