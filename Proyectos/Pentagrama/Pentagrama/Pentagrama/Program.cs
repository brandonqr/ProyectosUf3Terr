using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pentagrama
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Modificar.
            var dllDirectory = @"C:\sdl\lib";
            Environment.SetEnvironmentVariable("PATH",
                Environment.GetEnvironmentVariable("PATH") + ";" + dllDirectory);
            Joc juego = new Joc();
            juego.Run();
            //juego.run();
        }
    }
}
