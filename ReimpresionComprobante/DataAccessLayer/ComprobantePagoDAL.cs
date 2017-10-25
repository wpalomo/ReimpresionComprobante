using FastReport;
using ReimpresionComprobante.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimpresionComprobante.DataAccessLayer
{
    public class ComprobantePagoDAL
    {
        public string MsgError = string.Empty;
        public string CadenaDeConexion { get { return Properties.Settings.Default.ConnectionStringCFD; } }        

        private DataTable SqlQuery(string sql)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaDeConexion))
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
                catch (Exception ex)
                {
                    MsgError = ex.Message;
                    dtResult = new DataTable();
                }
                conexion.Close();
                return dtResult;
            }
        }

        public List<ComprobantePagoEntity> getComprobantesDePago(BackgroundWorker worker)
        {
            
            List<ComprobantePagoEntity> listaComprobantes = new List<ComprobantePagoEntity>();

            string sqlCmd = @"SELECT [ID_ComprobantePago], [Serie], [Folio], [Fecha], [FormaPago], [Subtotal], [Descuento], [Moneda], [TipoCambio], [Total], 
                              [MetodoPago], [LugarExpedicion] ,[ID_SerieFolio], [ID_Contribuyente],[ID_Cliente],[UsoCFDI],[IDPago]                            
                              FROM [cfd].[ComprobantePago]";

            DataTable dt = SqlQuery(sqlCmd);

            if(dt!=null && string.IsNullOrEmpty(MsgError))
            {
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        int inc = 80 / dt.Rows.Count;
                        int pct = inc;
                        foreach (DataRow row in dt.Rows)
                        {
                            ComprobantePagoEntity pago = new ComprobantePagoEntity();
                            //pago.IDComprobante = Convert.ToInt32(row["ID_ComprobantePago"]);
                            pago.Serie = row["Serie"].ToString();
                            pago.Folio = Convert.ToInt32(row["Folio"]);
                            //pago.Fecha = Convert.ToDateTime(row["Fecha"]);
                            //pago.FormaDePago = row["FormaPago"].ToString();
                            pago.Moneda = row["Moneda"].ToString();
                            //pago.TipoDeCambio = Convert.ToDecimal(row["TipoCambio"]);
                            pago.Total = Convert.ToDecimal(row["Total"]);
                            listaComprobantes.Add(pago);
                            if (pct <= 80)
                            {
                                worker.ReportProgress(pct);
                                pct += inc;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MsgError = ex.Message;
                    listaComprobantes = new List<ComprobantePagoEntity>();
                }
            }                     

            return listaComprobantes;
        }

    }
}
