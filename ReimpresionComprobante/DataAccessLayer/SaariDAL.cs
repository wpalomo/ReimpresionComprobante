using ReimpresionComprobante.Entities;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;

namespace ReimpresionComprobante.DataAccessLayer
{
    public class SaariDAL
    {
        private string cadenaDeConexion = string.Empty;
        private string usuario = string.Empty;
        private string nombreUsuario = string.Empty;

        public SaariDAL(string cadenaDeConexion, string usuario, string cadenaDeConexionSQLServer)
        {
            this.cadenaDeConexion = cadenaDeConexion;
            this.usuario = usuario;
            this.nombreUsuario = usuario;
        }

        private string GetNombreUsuario()
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            try
            {
                string sql = "SELECT CAMPO1 FROM GRUPOS WHERE USUARIO = ?";
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                comando.Parameters.Add("@User", OdbcType.VarChar).Value = usuario;
                conexion.Open();
                string result = comando.ExecuteScalar().ToString();
                conexion.Close();
                return result;
            }
            catch 
            {
                conexion.Close();                
                return string.Empty;
            }
        }

        public List<InmobiliariaEntity> GetInmobiliarias()
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            try
            {
                string sql = "SELECT P0101_ID_ARR, P0102_N_COMERCIAL, P0103_RAZON_SOCIAL, P0106_RFC FROM T01_ARRENDADORA ORDER BY P0103_RAZON_SOCIAL";
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                List<InmobiliariaEntity> listaInmobiliarias = new List<InmobiliariaEntity>();
                bool estaLimitado = this.EstaLimitado();
                conexion.Open();
                OdbcDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    InmobiliariaEntity inmo = new InmobiliariaEntity();
                    inmo.ID = reader["P0101_ID_ARR"].ToString();
                    inmo.NombreComercial = reader["P0102_N_COMERCIAL"].ToString();
                    inmo.RazonSocial = reader["P0103_RAZON_SOCIAL"].ToString();
                    inmo.RFC = reader["P0106_RFC"].ToString();
                    if (!estaLimitado || TienePermisoInmobiliaria(inmo.ID))
                        listaInmobiliarias.Add(inmo);
                }
                reader.Close();
                conexion.Close();
                return listaInmobiliarias;
            }
            catch 
            {
                conexion.Close();               
                return null;
            }
        }

        public InmobiliariaEntity GetInmobiliaria(string idArr)
        {
            var inmos = GetInmobiliarias();
            if (inmos != null)
                return inmos.SingleOrDefault(i => i.ID == idArr);
            else
                return null;
        }

        public string GetIDContribuyente(string idInmobiliaria)
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            string IDContribuyente = "";
            try
            {
                string sql = @"SELECT T01_ARRENDADORA.P0122_CAMPO15
                                FROM T01_ARRENDADORA
                                WHERE T01_ARRENDADORA.P0101_ID_ARR = '" + idInmobiliaria + "'";
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                conexion.Open();
                OdbcDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["P0122_CAMPO15"] != null)
                    {
                        IDContribuyente = reader["P0122_CAMPO15"].ToString();
                    }
                }
                reader.Close();
                conexion.Close();
            }
            catch
            {
                conexion.Close();         
            }
            return IDContribuyente;
        }

        public bool EstaLimitado()
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            try
            {
                string sql = "SELECT COUNT(*) FROM T47_EMPRESAS_US WHERE P4702_ID_USUARIO = ?";
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                comando.Parameters.Add("@User", OdbcType.NVarChar).Value = usuario;
                conexion.Open();
                int count = (int)comando.ExecuteScalar();
                conexion.Close();
                return count > 0;
            }
            catch 
            {
                conexion.Close();                
                return false;
            }
        }

        public bool TienePermisoInmobiliaria(string idInmobiliaria)
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            try
            {
                string sql = "SELECT COUNT(*) FROM T47_EMPRESAS_US WHERE P4702_ID_USUARIO = ? AND P4701_ID_EMPRESA = ?";
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                comando.Parameters.Add("@User", OdbcType.VarChar).Value = usuario;
                comando.Parameters.Add("@IDArr", OdbcType.VarChar).Value = idInmobiliaria;
                conexion.Open();
                int count = (int)comando.ExecuteScalar();
                conexion.Close();
                return count > 0;
            }
            catch 
            {
                conexion.Close();        
                return false;
            }
        }

        public CorreoEntity GetConfiguracion()
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            try
            {
                string sql = @"SELECT CAMPO1 AS UserName, CAMPO2 AS Mail, CAMPO3 AS Pass, CAMPO4 AS Server, CAMPO_NUM1 AS Port
FROM GRUPOS WHERE USUARIO = ?";
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                comando.Parameters.Add("@User", OdbcType.VarChar).Value = this.usuario;
                CorreoEntity correo = new CorreoEntity();
                conexion.Open();
                OdbcDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    correo.NombreUsuario = reader["UserName"].ToString();
                    correo.Correo = reader["Mail"].ToString();
                    correo.Contrasenia = reader["Pass"].ToString();
                    correo.Servidor = reader["Server"].ToString();
                    correo.Puerto = string.IsNullOrEmpty(reader["Port"].ToString()) ? 0 : Convert.ToInt32(reader["Port"]);
                    correo.Usuario = this.usuario;
                    break;
                }
                reader.Close();
                conexion.Close();
                return correo;
            }
            catch
            {
                conexion.Close();                
                return null;
            }
        }

        public string GetCorreoCliente(string idCliente)
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            try
            {
                string sql = "SELECT P0604_DIRECCION FROM T06_MAIL_WEB WHERE P0601_ID_ENTE = ? AND P0602_TIPO_ENTE = 2 AND P0603_TIPO_SERV = 'E' AND P0605_ORDEN = 1";
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                comando.Parameters.Add("@IDCte", OdbcType.VarChar).Value = idCliente;
                conexion.Open();
                string email = comando.ExecuteScalar().ToString();
                conexion.Close();
                return email;
            }
            catch (Exception e)
            {
                conexion.Close();
                return string.Empty;
            }
        }

        public string GetXMLFilename(int idHistRec)
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            try
            {
                string sql = "SELECT P4023_CAMPO1 FROM T40_CFD WHERE P4001_ID_HIST_REC = ?";
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                comando.Parameters.Add("@IdHist", OdbcType.Int).Value = idHistRec;
                conexion.Open();
                string fileName = comando.ExecuteScalar().ToString();
                conexion.Close();
                return fileName;
            }
            catch
            {
                conexion.Close();               
                return string.Empty;
            }
        }

        public List<int> GetRecibos(string arrendadora)
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            List<int> listareciboa = new List<int>();

            try
            {
                string sql = @"select CAMPO_NUM5 from T24_HISTORIA_RECIBOS 
Where P2406_STATUS = 2 And P2401_ID_ARRENDADORA = '" + arrendadora + "' And CAMPO_NUM5 is not null And CAMPO_NUM5 > 0 Order By CAMPO_NUM5"; ;
                                
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                conexion.Open();
                OdbcDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var elemento = reader["CAMPO_NUM5"];
                    if (elemento == DBNull.Value)
                    {
                        listareciboa.Add(-1);
                    }
                    else
                    {
                        if (int.TryParse(elemento.ToString(), out int value))
                        {
                            listareciboa.Add(Convert.ToInt32(value));
                        }
                        else
                        {
                            var elDecimal = Convert.ToDouble(elemento.ToString());
                            listareciboa.Add((int)elDecimal);
                        }
                    }

                }
                reader.Close();
                conexion.Close();
                return listareciboa;
            }
            catch 
            {
                conexion.Close();
                return null;
            }
        }

        public string GetDatosCliente(string ID_Cliente)
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            string result = "";
            try
            {
                string sql = @"SELECT T02_ARRENDATARIO.P0203_NOMBRE, T02_ARRENDATARIO.P0204_RFC
FROM T02_ARRENDATARIO
WHERE T02_ARRENDATARIO.P0201_ID =  '" + ID_Cliente + "'";

                OdbcCommand comando = new OdbcCommand(sql, conexion);
                conexion.Open();
                OdbcDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    result = reader["P0203_NOMBRE"].ToString();
                    
                }
                reader.Close();
                conexion.Close();
            }
            catch 
            {
                conexion.Close();
            }
            return result;
        }



    }
}
