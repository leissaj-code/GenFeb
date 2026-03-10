using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MiPrimeraEncarta
{
    // Este formulario es la pantalla de inicio de la aplicación
    // Muestra una animación mientras carga
    public partial class FormSplash : Form
    {
        // Colores principales que se van a usar en el diseño
        static readonly Color Naranja1 = Color.FromArgb(255, 162, 0);
        static readonly Color Naranja2 = Color.FromArgb(215, 82, 0);
        static readonly Color Verde1 = Color.FromArgb(92, 204, 38);
        static readonly Color Verde2 = Color.FromArgb(32, 128, 10);
        static readonly Color LogoDark = Color.FromArgb(22, 64, 8);

        // Timer para controlar la animación
        Timer _tmr;

        // Variables para el estado de la animación
                     
        float _progress = 0f;// _progress controla la barra de carga
        float _alpha = 0f; // _alpha controla la aparición del logo
        float _wave = 0f;// _wave controla la onda superior

        // Constructor del formulario
        public FormSplash()
        {
            // Quita bordes de la ventana
            FormBorderStyle = FormBorderStyle.None;

            // La ventana aparece en el centro
            StartPosition = FormStartPosition.CenterScreen;

            // Tamaño de la ventana
            Size = new Size(660, 430);

            // Activa doble buffer para que no parpadee
            DoubleBuffered = true;

            // No se muestra en la barra de tareas
            ShowInTaskbar = false;

            // Se muestra encima de otras ventanas
            TopMost = true;

            // Cuando carga, redondea las esquinas
            Load += (s, e) =>
            {
                var gp = new GraphicsPath();
                AddRoundRect(gp, new Rectangle(0, 0, Width - 1, Height - 1), 22);
                Region = new Region(gp);
            };

            // Si el usuario da clic, se salta la animación
            Click += (s, e) => ForceClose();

            // Se crea el timer de animación
            _tmr = new Timer { Interval = 22 };

            // Cada tick del timer actualiza la animación
            _tmr.Tick += (s, e) =>
            {
                // Aumenta el progreso poco a poco hasta 100
                _progress = Math.Min(100f, _progress + 1.6f);

                // Aumenta la transparencia del logo
                _alpha = Math.Min(1f, _alpha + 0.055f);

                // Aumenta la animación de la onda
                _wave = Math.Min(1f, _wave + 0.045f);

                // Redibuja el formulario
                Invalidate();

                // Cuando llegue al 100, se cierra
                if (_progress >= 100f)
                {
                    _tmr.Stop();
                    System.Threading.Thread.Sleep(220);
                    ForceClose();
                }
            };

            // Inicia el timer
            _tmr.Start();
        }

        // Este método fuerza el cierre del splash
        void ForceClose()
        {
            _tmr.Stop();
            DialogResult = DialogResult.OK;
            Close();
        }

        // Este método dibuja todo el formulario
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            var rc = ClientRectangle;

            // Fondo principal con degradado naranja
            using (var br = new LinearGradientBrush(rc, Naranja1, Naranja2, 128f))
                g.FillRectangle(br, rc);

            // Onda verde animada en la parte superior
            float waveH = 80f * _wave;
            using (var path = new GraphicsPath())
            {
                path.AddBezier(
                    0f, 0f,
                    rc.Width * 0.30f, waveH * 0.9f,
                    rc.Width * 0.68f, waveH * 0.3f,
                    rc.Width, waveH * 0.7f);
                path.AddLine(rc.Width, 0f, 0f, 0f);
                path.CloseFigure();

                using (var br2 = new LinearGradientBrush(
                    new RectangleF(0, 0, rc.Width, waveH + 1),
                    Verde1, Verde2, 90f))
                    g.FillPath(br2, path);
            }

            // Esferas decorativas del fondo
            PintarEsfera(g, rc.Width - 155, rc.Height - 115, 280, 220, Color.FromArgb(48, 255, 215, 0));
            PintarEsfera(g, 70, rc.Height - 65, 180, 148, Color.FromArgb(36, 255, 255, 80));
            PintarEsfera(g, rc.Width / 2, 55, 210, 160, Color.FromArgb(28, 255, 200, 0));

            // Hojas decorativas
            DrawLeaf(g, rc.Width - 138, 22, 1.05f);
            DrawLeaf(g, rc.Width - 88, 44, 0.68f);

            // Controla transparencia del logo
            int a = (int)(_alpha * 255f);

            // Posición del logo
            int lx = rc.Width / 2 - 182;
            int ly = rc.Height / 2 - 85;

            // Texto "Mi primera"
            using (var f = new Font("Arial", 21f, FontStyle.Bold))
            using (var b = new SolidBrush(Color.FromArgb(Math.Min(255, a), LogoDark)))
                g.DrawString("Mi primera", f, b, lx + 8, ly);

            // Sombra del texto Encarta
            using (var f = new Font("Arial Black", 60f, FontStyle.Bold))
            using (var b = new SolidBrush(Color.FromArgb(Math.Min(55, a / 5), Color.Black)))
                g.DrawString("Encarta", f, b, lx - 4, ly + 35);

            // Texto principal Encarta
            using (var f = new Font("Arial Black", 60f, FontStyle.Bold))
            using (var b = new SolidBrush(Color.FromArgb(Math.Min(255, a), LogoDark)))
                g.DrawString("Encarta", f, b, lx - 6, ly + 32);

            // Símbolo de marca registrada
            using (var f = new Font("Arial", 15f))
            using (var b = new SolidBrush(Color.FromArgb(Math.Min(255, a), Color.FromArgb(32, 80, 14))))
                g.DrawString("®", f, b, lx + 312, ly + 40);

            // Texto descriptivo debajo del logo
            using (var f = new Font("Segoe UI", 11.5f, FontStyle.Italic))
            using (var b = new SolidBrush(Color.FromArgb(Math.Min(215, a), Color.White)))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("Tu enciclopedia digital interactiva",
                    f, b, new RectangleF(0, ly + 108, rc.Width, 30), sf);

            // Texto que indica que se puede dar clic para saltar
            using (var f = new Font("Segoe UI", 8.5f))
            using (var b = new SolidBrush(Color.FromArgb(Math.Min(130, a), Color.White)))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("Clic para saltar", f, b,
                    new RectangleF(0, ly + 135, rc.Width, 20), sf);

            // Posición y tamaño de la barra de progreso
            int barX = 110;
            int barY = rc.Height - 74;
            int barW = rc.Width - 220;
            int barH = 14;

            // Fondo de la barra
            using (var gp = new GraphicsPath())
            {
                AddRoundRect(gp, new Rectangle(barX, barY, barW, barH), 7);
                using (var brBG = new SolidBrush(Color.FromArgb(58, 0, 0, 0)))
                    g.FillPath(brBG, gp);
            }

            // Parte rellena de la barra según el progreso
            int fillW = Math.Max(14, (int)(barW * _progress / 100f));
            using (var gp = new GraphicsPath())
            {
                AddRoundRect(gp, new Rectangle(barX, barY, fillW, barH), 7);

                using (var brF = new LinearGradientBrush(
                    new Rectangle(barX, barY, Math.Max(1, fillW), barH),
                    Color.FromArgb(172, 248, 62), Color.FromArgb(52, 172, 12), 0f))
                    g.FillPath(brF, gp);

                // Brillo en la parte superior de la barra
                using (var brS = new SolidBrush(Color.FromArgb(68, 255, 255, 255)))
                    g.FillRectangle(brS, barX + 1, barY + 1, fillW - 2, barH / 2 - 1);
            }

            // Texto del porcentaje de carga
            using (var f = new Font("Segoe UI", 9f))
            using (var b = new SolidBrush(Color.FromArgb(225, Color.White)))
                g.DrawString($"Cargando… {(int)_progress} %", f, b, barX, barY + barH + 5);

            // Texto de versión al final
            using (var f = new Font("Segoe UI", 8f))
            using (var b = new SolidBrush(Color.FromArgb(155, Color.White)))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("v1.0  –  © 2024  Mi Primera Encarta", f, b,
                    new RectangleF(0, rc.Height - 18, rc.Width, 16), sf);
        }

        // Este método pinta una esfera decorativa
        static void PintarEsfera(Graphics g, int cx, int cy, int rw, int rh, Color c)
        {
            var rect = new Rectangle(cx - rw / 2, cy - rh / 2, rw, rh);

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(rect);

                using (var br = new PathGradientBrush(path))
                {
                    br.CenterColor = c;
                    br.SurroundColors = new[] { Color.Transparent };
                    g.FillPath(br, path);
                }
            }
        }

        // Este método dibuja una hoja decorativa
        static void DrawLeaf(Graphics g, int x, int y, float scale)
        {
            var state = g.Save();

            // Mueve la posición de dibujo
            g.TranslateTransform(x, y);

            // Cambia el tamaño según la escala
            g.ScaleTransform(scale, scale);

            // Hoja grande
            using (var path = new GraphicsPath())
            {
                path.AddBezier(0, 50, 5, 0, 58, 10, 0, 100);
                path.AddBezier(0, 100, 10, 70, 20, 55, 0, 50);

                using (var br = new LinearGradientBrush(
                    new Rectangle(-2, -2, 62, 105),
                    Color.FromArgb(96, 218, 52), Color.FromArgb(44, 150, 18), 40f))
                    g.FillPath(br, path);

                using (var pen = new Pen(Color.FromArgb(30, 120, 10), 1f))
                    g.DrawPath(pen, path);
            }

            // Hoja pequeña del lado derecho
            using (var path = new GraphicsPath())
            {
                path.AddBezier(26, 26, 64, -6, 80, 18, 26, 68);
                path.AddBezier(26, 68, 46, 50, 40, 36, 26, 26);

                using (var br = new LinearGradientBrush(
                    new Rectangle(24, -8, 58, 78),
                    Color.FromArgb(118, 236, 68), Color.FromArgb(55, 170, 24), 40f))
                    g.FillPath(br, path);

                using (var pen = new Pen(Color.FromArgb(30, 120, 10), 1f))
                    g.DrawPath(pen, path);
            }

            // Restaura el estado original del dibujo
            g.Restore(state);
        }

        // Este método crea un rectángulo con bordes redondeados
        static void AddRoundRect(GraphicsPath p, Rectangle rc, int r)
        {
            p.AddArc(rc.X, rc.Y, r * 2, r * 2, 180, 90);
            p.AddArc(rc.Right - r * 2, rc.Y, r * 2, r * 2, 270, 90);
            p.AddArc(rc.Right - r * 2, rc.Bottom - r * 2, r * 2, r * 2, 0, 90);
            p.AddArc(rc.X, rc.Bottom - r * 2, r * 2, r * 2, 90, 90);
            p.CloseFigure();
        }
    }
}