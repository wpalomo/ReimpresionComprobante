using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReimpresionComprobante
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new FormReporte());
        //}



        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                string[] argumentos = Environment.GetCommandLineArgs();
                if (argumentos.Length > 1)
                {
                    string usuario = argumentos[1];                    
                    Application.Run(new FormReporte(usuario));
                }
                else
                {
                    MessageBox.Show("No se puede inicializar el módulo de esta forma", "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar: " + Environment.NewLine + ex.Message, "Saari", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
