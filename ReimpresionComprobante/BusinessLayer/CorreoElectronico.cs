using ReimpresionComprobante.DataAccessLayer;
using ReimpresionComprobante.Entities;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Drawing.Printing;

namespace ReimpresionComprobante.BusinessLayer
{
    public class CorreoElectronico
    {
        private CorreoEntity correo = null;
        private SaariDAL saariDal = null;
        private PrintDocument printDocument1 = new PrintDocument();
        private string stringToPrint;

        /// <summary>
        /// Constructor de la clase que inicializa la configuración de correo del usuario y la capa de acceso a datos
        /// </summary>
        public CorreoElectronico()
        {
            saariDal = new SaariDAL(Properties.Settings.Default.ConnectionStringODBC, "Administrador", Properties.Settings.Default.ConnectionStringCFD);
            correo = saariDal.GetConfiguracion();
        }

        /// <summary>
        /// Envia los archivos de facturación (XML y PDF) al cliente por corre electrónico
        /// </summary>
        /// <param name="recibo">Entidad de recibo correspondiente a la factura que se enviará</param>
        /// <returns></returns>
        public string enviarFactura(ReciboEntity recibo)
        {
            try
            {
                if (correo != null)
                {
                    if (!string.IsNullOrEmpty(correo.Correo) && !string.IsNullOrEmpty(correo.Servidor) && !string.IsNullOrEmpty(correo.Contrasenia) && correo.Puerto > 0)
                    {
                        string emailCliente = saariDal.GetCorreoCliente(recibo.IDCliente);
                        if (!string.IsNullOrEmpty(emailCliente))
                        {
                            string fileXml = saariDal.GetXMLFilename(recibo.IDHistRec);
                            if (!string.IsNullOrEmpty(fileXml))
                            {
                                string filePdf = fileXml.Replace(".xml", ".pdf");
                                if (File.Exists(fileXml) && File.Exists(filePdf))
                                {
                                    SmtpClient clienteMail = new SmtpClient();

                                    clienteMail.Host = correo.Servidor;
                                    clienteMail.Port = correo.Puerto;
                                    clienteMail.Credentials = new NetworkCredential(correo.Correo, correo.Contrasenia);

                                    if (clienteMail.Host.ToLower().Contains("hotmail") || clienteMail.Host.ToLower().Contains("live") || clienteMail.Host.ToLower().Contains("gmail") || clienteMail.Host.Contains(Properties.Settings.Default.habilitarSMTPSSL))
                                        clienteMail.EnableSsl = true;
                                    clienteMail.DeliveryMethod = SmtpDeliveryMethod.Network;

                                    MailMessage mail = new MailMessage();
                                    mail.From = new MailAddress(correo.Correo);
                                    emailCliente = emailCliente.Replace(';', ',');
                                    mail.To.Add(emailCliente);
                                    mail.Subject = Configuraciones.Asunto;
                                    mail.Body = Configuraciones.Cuerpo;
                                    mail.Attachments.Add(new Attachment(fileXml));
                                    mail.Attachments.Add(new Attachment(filePdf));

                                    try
                                    {
                                        clienteMail.Send(mail);
                                        return string.Empty;
                                    }
                                    catch (Exception ex)
                                    {
                                        return "Error al enviar correo: " + Environment.NewLine + ex.Message;
                                    }
                                }
                                else
                                    return string.Format("Comprube que existe el archivo XML ({0}) y el archivo PDF ({1}) de la factura", fileXml, filePdf);
                            }
                            else
                                return "Error al obtener el nombre del archivo XML previamente generado";
                        }
                        else
                            return "No se encontró el correo electrónico del cliente";
                    }
                    else
                        return "El usuario no tiene configurado correctamente el correo electrónico";
                }
                else
                    return "Hubo un error al obtener la configuración de correo electrónico del usuario";
            }
            catch (Exception ex)
            {
                return string.Format("Hubo un error al enviar el CFDi {0} por correo: {1} {2}", recibo.SerieFolio, Environment.NewLine, ex.Message);
            }
        }

        public string enviarFactura(string nombreComprobante, string idCliente)
        {
            try
            {
                if (correo != null)
                {
                    if (!string.IsNullOrEmpty(correo.Correo) && !string.IsNullOrEmpty(correo.Servidor) && !string.IsNullOrEmpty(correo.Contrasenia) && correo.Puerto > 0)
                    {
                        string emailCliente = saariDal.GetCorreoCliente(idCliente);
                        if (!string.IsNullOrEmpty(emailCliente))
                        {
                            bool esPDF = nombreComprobante.Contains(".pdf");
                            string filePdf = string.Empty;
                            string fileXml = string.Empty;

                            if (esPDF)
                            {
                                filePdf = nombreComprobante.Replace(".pdf", ".xml");
                                fileXml = nombreComprobante;
                            }
                            else
                            {
                                filePdf = nombreComprobante.Replace(".xml", ".pdf");
                                fileXml = nombreComprobante;

                            }
                            if (File.Exists(fileXml) && File.Exists(filePdf))
                            {
                                SmtpClient clienteMail = new SmtpClient();

                                clienteMail.Host = correo.Servidor;
                                clienteMail.Port = correo.Puerto;
                                clienteMail.Credentials = new NetworkCredential(correo.Correo, correo.Contrasenia);

                                if (clienteMail.Host.ToLower().Contains("hotmail") || clienteMail.Host.ToLower().Contains("live") || clienteMail.Host.ToLower().Contains("gmail") || clienteMail.Host.Contains(Properties.Settings.Default.habilitarSMTPSSL))
                                    clienteMail.EnableSsl = true;
                                clienteMail.DeliveryMethod = SmtpDeliveryMethod.Network;

                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress(correo.Correo);
                                emailCliente = emailCliente.Replace(';', ',');
                                mail.To.Add(emailCliente);
                                mail.Subject = Configuraciones.Asunto;
                                mail.Body = Configuraciones.Cuerpo;
                                mail.Attachments.Add(new Attachment(fileXml));
                                mail.Attachments.Add(new Attachment(filePdf));
                                try
                                {
                                    clienteMail.Send(mail);
                                    return string.Empty;
                                }
                                catch (Exception ex)
                                {
                                    return "Error al enviar correo: " + Environment.NewLine + ex.Message;
                                }
                            }
                            else
                                return string.Format("Comprube que existe el archivo XML ({0}) y el archivo PDF ({1}) de la factura", fileXml, filePdf);
                            //}
                            //else
                            //    return "Error al obtener el nombre del archivo XML previamente generado";
                        }
                        else
                            return "No se encontró el correo electrónico del cliente";
                    }
                    else
                        return "El usuario no tiene configurado correctamente el correo electrónico";
                }
                else
                    return "Hubo un error al obtener la configuración de correo electrónico del usuario";
            }
            catch (Exception ex)
            {
                return string.Format("Hubo un error al enviar el CFDi {0} por correo: {1} {2}", 2, Environment.NewLine, ex.Message);
            }
        }

        public string enviarImprimir(string NombreDoc)
        {
            string errores = string.Empty;
            //LeerArchivo(NombreDoc);
            return errores;
        }
    }

}
