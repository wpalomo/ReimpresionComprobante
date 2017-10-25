using ReimpresionComprobante.Entities;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.nombreUsuario = getNombreUsuario();
        }

        private string getNombreUsuario()
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
            catch (Exception ex)
            {
                conexion.Close();                
                return string.Empty;
            }
        }

        public List<InmobiliariaEntity> getInmobiliarias()
        {
            OdbcConnection conexion = new OdbcConnection(cadenaDeConexion);
            try
            {
                string sql = "SELECT P0101_ID_ARR, P0102_N_COMERCIAL, P0103_RAZON_SOCIAL, P0106_RFC FROM T01_ARRENDADORA ORDER BY P0103_RAZON_SOCIAL";
                OdbcCommand comando = new OdbcCommand(sql, conexion);
                List<InmobiliariaEntity> listaInmobiliarias = new List<InmobiliariaEntity>();
                bool estaLimitado = this.estaLimitado();
                conexion.Open();
                OdbcDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    InmobiliariaEntity inmo = new InmobiliariaEntity();
                    inmo.ID = reader["P0101_ID_ARR"].ToString();
                    inmo.NombreComercial = reader["P0102_N_COMERCIAL"].ToString();
                    inmo.RazonSocial = reader["P0103_RAZON_SOCIAL"].ToString();
                    inmo.RFC = reader["P0106_RFC"].ToString();
                    if (!estaLimitado || tienePermisoInmobiliaria(inmo.ID))
                        listaInmobiliarias.Add(inmo);
                }
                reader.Close();
                conexion.Close();
                return listaInmobiliarias;
            }
            catch (Exception ex)
            {
                conexion.Close();               
                return null;
            }
        }

        public bool estaLimitado()
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
            catch (Exception ex)
            {
                conexion.Close();                
                return false;
            }
        }

        public bool tienePermisoInmobiliaria(string idInmobiliaria)
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
            catch (Exception ex)
            {
                conexion.Close();        
                return false;
            }
        }

        public CorreoEntity getConfiguracion()
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
            catch (Exception ex)
            {
                conexion.Close();                
                return null;
            }
        }

        public string getCorreoCliente(string idCliente)
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
            catch (Exception ex)
            {
                conexion.Close();
                return string.Empty;
            }
        }

        public string getXMLFilename(int idHistRec)
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
            catch (Exception ex)
            {
                conexion.Close();               
                return string.Empty;
            }
        }




    }
}
