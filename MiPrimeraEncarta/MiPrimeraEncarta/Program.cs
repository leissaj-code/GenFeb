using System;
using System.Windows.Forms;

namespace MiPrimeraEncarta
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ── Mostrar Splash antes del formulario principal ───────────
            using (var splash = new FormSplash())
                splash.ShowDialog();   // bloquea hasta que la barra llega al 100 %

            // ── Lanzar aplicación principal ─────────────────────────────
            Application.Run(new Form1());
        }
    }
}