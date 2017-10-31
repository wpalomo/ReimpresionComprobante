using ReimpresionComprobante.DataAccessLayer;
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
    public class XMLManager
    {
        private Comprobante CFDIv33;
        private string xmlFileName = string.Empty;
        public string msgError { get; set; }
        public bool existeError { get; set; }
        public XMLManager()
        {
            CFDIv33 = new Comprobante();
            msgError = string.Empty;
            existeError = false;
        }

        public Comprobante DeserializeFile(string xmlPath)
        {
            try
            {
                // Se abre y se lee el archivo xml
                FileStream fs = new FileStream(xmlPath, FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                // Se crea una instancia del serializador especificando el tipo de objeto
                XmlSerializer serializer = new XmlSerializer(typeof(Comprobante));
                // Se deserializa, asignando al objeto
                CFDIv33 = (Comprobante)serializer.Deserialize(reader);
                fs.Close();
                existeError = false;
                msgError = string.Empty;
                // Se retorna el objeto resultante
                return CFDIv33;
            }
            catch (Exception ex)
            {
                msgError = "Ocurrió un problema al deserializar el archivo xml." + Environment.NewLine + ex.Message;
                existeError = true;
                return null;
            }
        }
    }
}
