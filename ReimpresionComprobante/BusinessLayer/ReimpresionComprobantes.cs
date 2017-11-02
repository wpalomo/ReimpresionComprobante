using ReimpresionComprobante.BusinessLayer.Reports;
using ReimpresionComprobante.DataAccessLayer;
using ReimpresionComprobante.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ReimpresionComprobante.BusinessLayer
{
    public class ReimpresionComprobantes
    {
        private string cadenaDeConexion = string.Empty;
        private string usuario = string.Empty;
        private string nombreUsuario = string.Empty;
        private SaariDAL inmoDAL = null;
        private CobranzaDAL cobranzaDAL = null;
        private CorreoElectronico enviar = null;

        public ReimpresionComprobantes(string cadenaDeConexion, string usuario, string cadenaDeConexionSQLServer)
        {
            this.cadenaDeConexion = cadenaDeConexion;
            this.usuario = usuario;
            this.nombreUsuario = usuario;
            inmoDAL = new SaariDAL(Configuraciones.CadenaConexionODBC, Acceso.Usuario, Configuraciones.CadenaConexionSQLServer);
            cobranzaDAL = new CobranzaDAL(Configuraciones.CadenaConexionSQLServer, Acceso.Usuario);
            enviar = new CorreoElectronico();
        }

        public List<InmobiliariaEntity> GetInmobiliarias()
        {
            try
            {
                List<InmobiliariaEntity> listaInmobiliarias = inmoDAL.GetInmobiliarias();
                return listaInmobiliarias;
            }
            catch
            {
                return null;
            }            
        }

        public List<DatosGridEntity> GetDatosGrid(FiltrosEntity elFiltro)
        {
            try
            {
                List<DatosGridEntity> listaDatosGrid = cobranzaDAL.GetDatosGrid(elFiltro);
                return listaDatosGrid;
            }
            catch
            {
                return null;
            }
        }


        public string GetXmlComprobante (int ID_ComprobantePago, string inmobiliaria)
        {
            try
            {
                int elIdXml = cobranzaDAL.GetIdXmlComprobante(ID_ComprobantePago);
                RutaContenidoXmlEntity rutaContenidoXml = cobranzaDAL.GetrutaContenidoXml(elIdXml);
                var laRuta = rutaContenidoXml.Ruta.Replace(@".xml",@".pdf");
                
                if (!File.Exists(rutaContenidoXml.Ruta))//No existe el Xml en el disco
                {
                    var serializer = new XmlSerializer(typeof(Comprobante));
                    Comprobante cfdiv33;

                    using (TextReader reader = new StringReader(rutaContenidoXml.XmlDocumento.ToString()))
                    {
                        cfdiv33 = (Comprobante)serializer.Deserialize(reader);
                    }

                    XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
                    xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
                    xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    xmlNameSpace.Add("pago10", "http://www.sat.gob.mx/Pagos");
                    xmlNameSpace.Add("schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd");

                    XmlSerializer xmlSerialize = new XmlSerializer(typeof(Comprobante));
                    string pathXml = Path.GetDirectoryName(rutaContenidoXml.Ruta);
                    
                    Directory.CreateDirectory(pathXml);
                    string xmlName = rutaContenidoXml.Ruta;
                    
                    Encoding utf8WithoutBom = new UTF8Encoding(false);
                    
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(xmlName, Encoding.UTF8);
                    xmlTextWriter.Formatting = Formatting.Indented;
                    
                    xmlSerialize.Serialize(xmlTextWriter, cfdiv33, xmlNameSpace);
                    xmlTextWriter.Close();
                }

                if (File.Exists(laRuta))
                {
                    
                    if(Configuraciones.EnviarImprimir == false)
                    {
                        System.Diagnostics.Process.Start(laRuta);
                    }
                    else
                    {
                        var idcontribuyente = inmoDAL.GetIDContribuyente(inmobiliaria);
                        var idcontribuyentesql = cobranzaDAL.GetIDContribuyente(idcontribuyente);
                        int ContribuyenteID = Convert.ToInt32(idcontribuyentesql);

                        var serializer = new XmlSerializer(typeof(Comprobante));
                        Comprobante cfdiv33;

                        using (TextReader reader = new StringReader(rutaContenidoXml.XmlDocumento.ToString()))
                        {
                            cfdiv33 = (Comprobante)serializer.Deserialize(reader);
                        }

                        ComprobantePDF comporbantePdf = new ComprobantePDF();
                        bool imprimir = true;
                        bool abrir = true;
                        string result = comporbantePdf.GenerarComprobante(cfdiv33, ContribuyenteID, rutaContenidoXml.Ruta, false, ID_ComprobantePago, imprimir, abrir);
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                    }
                }
                else
                {
                    var idcontribuyente = inmoDAL.GetIDContribuyente(inmobiliaria);
                    var idcontribuyentesql = cobranzaDAL.GetIDContribuyente(idcontribuyente);
                    int ContribuyenteID = Convert.ToInt32(idcontribuyentesql);

                    var serializer = new XmlSerializer(typeof(Comprobante));
                    Comprobante cfdiv33;

                    using (TextReader reader = new StringReader(rutaContenidoXml.XmlDocumento.ToString()))
                    {
                        cfdiv33 = (Comprobante)serializer.Deserialize(reader);
                    }

                    ComprobantePDF comporbantePdf = new ComprobantePDF();
                    bool imprimir = false;
                    bool abrir = true;
                    string result = comporbantePdf.GenerarComprobante(cfdiv33, ContribuyenteID, rutaContenidoXml.Ruta, false, ID_ComprobantePago, imprimir, abrir);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                }                
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        public string EnviarCorreo(int ID_ComprobantePago, string inmobiliaria, string IDCliente)
        {
            try
            {
                int elIdXml = cobranzaDAL.GetIdXmlComprobante(ID_ComprobantePago);
                RutaContenidoXmlEntity rutaContenidoXml = cobranzaDAL.GetrutaContenidoXml(elIdXml);
                var laRuta = rutaContenidoXml.Ruta.Replace(@".xml", @".pdf");

                if (!File.Exists(rutaContenidoXml.Ruta))//No existe el Xml en el disco
                {
                    var serializer = new XmlSerializer(typeof(Comprobante));
                    Comprobante cfdiv33;

                    using (TextReader reader = new StringReader(rutaContenidoXml.XmlDocumento.ToString()))
                    {
                        cfdiv33 = (Comprobante)serializer.Deserialize(reader);
                    }

                    XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
                    xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
                    xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    xmlNameSpace.Add("pago10", "http://www.sat.gob.mx/Pagos");
                    xmlNameSpace.Add("schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd");

                    XmlSerializer xmlSerialize = new XmlSerializer(typeof(Comprobante));
                    string pathXml = Path.GetDirectoryName(rutaContenidoXml.Ruta);

                    Directory.CreateDirectory(pathXml);
                    string xmlName = rutaContenidoXml.Ruta;

                    Encoding utf8WithoutBom = new UTF8Encoding(false);

                    XmlTextWriter xmlTextWriter = new XmlTextWriter(xmlName, Encoding.UTF8);
                    xmlTextWriter.Formatting = Formatting.Indented;

                    xmlSerialize.Serialize(xmlTextWriter, cfdiv33, xmlNameSpace);
                    xmlTextWriter.Close();
                }

                if (!File.Exists(laRuta))//No existe el Pdf en el disco
                {
                    var idcontribuyente = inmoDAL.GetIDContribuyente(inmobiliaria);
                    var idcontribuyentesql = cobranzaDAL.GetIDContribuyente(idcontribuyente);
                    int ContribuyenteID = Convert.ToInt32(idcontribuyentesql);

                    var serializer = new XmlSerializer(typeof(Comprobante));
                    Comprobante cfdiv33;

                    using (TextReader reader = new StringReader(rutaContenidoXml.XmlDocumento.ToString()))
                    {
                        cfdiv33 = (Comprobante)serializer.Deserialize(reader);
                    }

                    ComprobantePDF comporbantePdf = new ComprobantePDF();
                    string result = comporbantePdf.GenerarComprobante(cfdiv33, ContribuyenteID, rutaContenidoXml.Ruta, false, ID_ComprobantePago, Configuraciones.EnviarImprimir, false);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                }

                //correo
                string resultEnviar = enviar.enviarFactura(rutaContenidoXml.Ruta, IDCliente);
                if (!string.IsNullOrEmpty(resultEnviar))
                {
                    return "Error al enviar CFDi por correo:" + Environment.NewLine + resultEnviar;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        public string GetRutaLog(int ID_ComprobantePago)
        {
            try
            {
                int elIdXml = cobranzaDAL.GetIdXmlComprobante(ID_ComprobantePago);
                RutaContenidoXmlEntity rutaContenidoXml = cobranzaDAL.GetrutaContenidoXml(elIdXml);
                return Path.GetDirectoryName(rutaContenidoXml.Ruta);
            }
            catch
            {
                return string.Empty;
            }
        }


    }
}
