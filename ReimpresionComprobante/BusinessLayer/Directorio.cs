using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimpresionComprobante.BusinessLayer
{
    class Directorio
    {
        //Config saariDataConfigs = new Config();
        //ReadWriteConfigFile csConfigFile = new ReadWriteConfigFile();

        private static string RutaArchivo = string.Empty;

        private string rutaAplicacion;

        public static string Ruta
        {
            get { return RutaArchivo; }
            set { RutaArchivo = value; }
        }

        /// <summary>
        /// Crear Directorios de acuerdo al RFC del Contribuyente
        /// </summary>
        /// <param name="RFC">RFC del Contribuyente</param>
        /// <returns>Ruta completa del Directorio Creado</returns>
        public string createDirectory(string RFC)
        {
            string rutaDefault = string.Empty;
            //Obtener DirectorioDefault de la Tabla Config de la Base de Datos
            ////DataRow drDirectorioDefault = saariDataConfigs.getDataConfigBySetting("DirectorioCFD");
            //if (drDirectorioDefault != null)
            //{
            //    rutaDefault = drDirectorioDefault["Valor"].ToString();
            //}
            //else //Obtener de xmlConfig
            //{
            //    //rutaDefault = csConfigFile.getCurrentAppConfigValue(ReadWriteConfigFile.configFile, "DirectorioCFD");
            //    /*7.2.0.10*/
            //    rutaDefault = Properties.Settings.Default.DirectorioCFD;
            //}

            DirectoryInfo Directorio = new DirectoryInfo(rutaDefault + "/" + RFC);
            if (Directorio.Exists == false)
            {
                Directorio.Create();
                Directorio.CreateSubdirectory("Certificados");
                Directorio.CreateSubdirectory("Firmas");
                Directorio.CreateSubdirectory("CFD");
                Directorio.CreateSubdirectory("Informes");
                Directorio.CreateSubdirectory("Otros");
            }
            else
            {
                DirectoryInfo subDirCertificados = new DirectoryInfo(Directorio.FullName + "//Certificados");
                if (subDirCertificados.Exists == false)
                    subDirCertificados.Create();
                DirectoryInfo subDirFirmas = new DirectoryInfo(Directorio.FullName + "//Firmas");
                if (subDirFirmas.Exists == false)
                    subDirFirmas.Create();
                DirectoryInfo subDirCFD = new DirectoryInfo(Directorio.FullName + "//CFD");
                if (subDirCFD.Exists == false)
                    subDirCFD.Create();
                DirectoryInfo subDirInformes = new DirectoryInfo(Directorio.FullName + "//Informes");
                if (subDirInformes.Exists == false)
                    subDirInformes.Create();
                DirectoryInfo subDirOtros = new DirectoryInfo(Directorio.FullName + "//Otros");
                if (subDirOtros.Exists == false)
                    subDirOtros.Create();
            }

            rutaAplicacion = rutaDefault;
            return Directorio.FullName;
        }

        /// <summary>
        /// Vertificacion de existencia de Directorio
        /// </summary>
        /// <param name="path">Ruta del Directorio a verificar</param>
        /// <returns>true si ruta existe; false si ruta no existe</returns>
        public bool existDirectory(string path)
        {
            DirectoryInfo Directorio = new DirectoryInfo(path);
            if (Directorio.Exists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Vertificacion de existencia de Archivo
        /// </summary>
        /// <param name="path">Ruta del Archivo a verificar</param>
        /// <returns>true si ruta existe; false si ruta no existe</returns>
        public bool existFile(string path)
        {
            FileInfo Archivo = new FileInfo(path);
            if (Archivo.Exists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
