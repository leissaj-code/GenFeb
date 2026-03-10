using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MiPrimeraEncarta
{
    public partial class Form1 : Form
    {
        // Colores principales que voy a usar en la interfaz
        static readonly Color VerdeClaro = Color.FromArgb(100, 200, 40);
        static readonly Color VerdeOscuro = Color.FromArgb(30, 120, 10);
        static readonly Color Amarillo = Color.FromArgb(255, 215, 0);
        static readonly Color Naranja = Color.FromArgb(255, 135, 0);

        // Arreglo de categorías del lado izquierdo
        // Cada categoría tiene nombre, emoji y color
        static readonly (string Nombre, string Emoji, string Color)[] CategIzq =
        {
            ("Paisajes y\nregiones",  "🌍", "#FFD700"),
            ("Los seres\nvivos",      "🐯", "#FFD700"),
            ("Ciencia y\ntécnica",    "🔬", "#FFD700"),
            ("Matemáticas",           "🧮", "#FFD700"),
            ("Deportes",              "⚽", "#FFD700"),
        };

        // Arreglo de categorías del lado derecho
        static readonly (string Nombre, string Emoji, string Color)[] CategDer =
        {
            ("Historia",              "🏺", "#FFD700"),
            ("Nuestra\nsociedad",     "👥", "#FFD700"),
            ("Lengua y\nliteratura",  "✒️", "#FFD700"),
            ("Las artes",             "🎨", "#FFD700"),
            ("Juega y\naprende",      "🔤", "#FFD700"),
            ("Programación",          "💻", "#FFD700"),
        };

        // Declaración de paneles, caja de texto y arreglos de botones
        Panel pnlBar, pnlMain;
        TextBox txtQ;
        Button[] bL = new Button[5];
        Button[] bR = new Button[6];

        public Form1()
        {
            // Aquí normalmente se inicializan los componentes del diseñador
            //InitializeComponent();

            // Yo construyo toda la interfaz desde código
            Build();
        }

        void Build()
        {
            // Título del formulario
            Text = "Mi Primera Encarta";

            // Tamaño inicial de la ventana
            ClientSize = new Size(1140, 720);

            // Tamaño mínimo para que no se deforme mucho
            MinimumSize = new Size(960, 600);

            // La ventana inicia en el centro
            StartPosition = FormStartPosition.CenterScreen;

            // Color de fondo general
            BackColor = Naranja;

            // Activa doble buffer para evitar parpadeos
            DoubleBuffered = true;

            // Panel de arriba que funciona como barra superior
            pnlBar = new Panel { Dock = DockStyle.Top, Height = 58, BackColor = Color.Transparent };

            // También le activo doble buffer
            DB(pnlBar);

            // Evento para dibujar la barra manualmente
            pnlBar.Paint += BarPaint;

            // Botones de navegación de la barra superior
            string[] icons = { "🏠", "◀", "▶", "📄", "👤" };
            int nx = 8;

            // Se van agregando uno por uno
            foreach (var ic in icons)
            {
                pnlBar.Controls.Add(NavBtn(ic, nx));
                nx += 52;
            }

            // Etiqueta de buscar
            var lbl = new Label
            {
                Text = "Buscar",
                ForeColor = Color.FromArgb(0, 60, 0),
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                AutoSize = true,
                BackColor = Color.Transparent,
                Location = new Point(nx + 10, 18)
            };
            pnlBar.Controls.Add(lbl);

            // Caja de texto para escribir la búsqueda
            txtQ = new TextBox
            {
                Location = new Point(nx + 68, 16),
                Width = 260,
                Font = new Font("Segoe UI", 10f),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Si el usuario presiona enter, ejecuta la búsqueda
            txtQ.KeyPress += (s, e) => { if (e.KeyChar == '\r') Buscar(); };
            pnlBar.Controls.Add(txtQ);

            // Botón para buscar
            var btnGo = NavBtn("➡", nx + 336);
            btnGo.Click += (s, e) => Buscar();
            pnlBar.Controls.Add(btnGo);

            // Etiqueta tipo logo en la esquina superior derecha
            var logoLbl = new Label
            {
                Text = "Mi Primera\nEncarta",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Arial", 9f, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 55, 0),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopRight
            };
            pnlBar.Controls.Add(logoLbl);

            // Ajusta la posición del logo cuando cambia el tamaño
            pnlBar.Layout += (s, e) => logoLbl.Location = new Point(pnlBar.Width - logoLbl.Width - 10, 6);

            // Panel principal donde van las categorías y el fondo dibujado
            pnlMain = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            DB(pnlMain);
            pnlMain.Paint += MainPaint;

            // Cuando cambie el tamaño, se redibuja y acomoda los botones
            pnlMain.Resize += (s, e) => { pnlMain.Invalidate(); Layout2(); };

            // Crear botones de categorías de izquierda y derecha
            for (int i = 0; i < 5; i++)
            {
                var ci = CategIzq[i];
                var cd = CategDer[i];

                bL[i] = CatBtn(ci.Nombre, ci.Emoji, true);
                bR[i] = CatBtn(cd.Nombre, cd.Emoji, false);

                pnlMain.Controls.Add(bL[i]);
                pnlMain.Controls.Add(bR[i]);
            }

            // Agrega el último botón que falta del lado derecho
            bR[5] = CatBtn(CategDer[5].Nombre, CategDer[5].Emoji, false);
            pnlMain.Controls.Add(bR[5]);

            // Agrega los paneles al formulario
            Controls.Add(pnlMain);
            Controls.Add(pnlBar);

            // Al cargar la ventana acomoda todo
            Load += (s, e) => Layout2();

            // Si cambia el tamaño del form, vuelve a acomodar
            Resize += (s, e) => Layout2();
        }

        // Este método acomoda los botones dependiendo del tamaño del formulario
        void Layout2()
        {
            int w = pnlMain.Width;
            int h = pnlMain.Height;

            // Ancho de botones
            int bw = Math.Min(260, w / 4 + 20);

            // Espacio entre botones
            int gap = 8;

            // Alto de botones
            int bh = Math.Max(60, (h - gap * 5 - 20) / 6);

            // Posición inicial vertical derecha
            int gyR = Math.Max(8, (h - bh * 6 - gap * 5) / 2);

            // Posición inicial vertical izquierda
            int gyL = Math.Max(8, (h - bh * 5 - gap * 4) / 2);

            // Acomoda botones izquierdos
            for (int i = 0; i < 5; i++)
            {
                int yL = gyL + i * (bh + gap);
                bL[i].Bounds = new Rectangle(10, yL, bw, bh);
                bL[i].Region = RgRound(bw, bh, 16);
            }

            // Acomoda botones derechos
            for (int i = 0; i < 6; i++)
            {
                int yR = gyR + i * (bh + gap);
                bR[i].Bounds = new Rectangle(w - bw - 10, yR, bw, bh);
                bR[i].Region = RgRound(bw, bh, 16);
            }
        }

        // Método que dibuja la barra superior
        void BarPaint(object s, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rc = pnlBar.ClientRectangle;

            // Fondo con degradado verde
            using (var br = new LinearGradientBrush(rc,
                Color.FromArgb(110, 210, 50), Color.FromArgb(50, 155, 18), 90f))
                g.FillRectangle(br, rc);

            // Línea oscura abajo
            using (var p = new Pen(VerdeOscuro, 4f))
                g.DrawLine(p, 0, rc.Height - 2, rc.Width, rc.Height - 2);

            // Línea clara arriba para dar brillo
            using (var p2 = new Pen(Color.FromArgb(160, 255, 255, 100), 1.5f))
                g.DrawLine(p2, 0, 1, rc.Width, 1);
        }

        // Método que dibuja el fondo principal y el logo central
        void MainPaint(object s, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rc = pnlMain.ClientRectangle;

            // Fondo naranja con degradado
            using (var br = new LinearGradientBrush(rc,
                Color.FromArgb(255, 160, 0), Color.FromArgb(255, 108, 0), 120f))
                g.FillRectangle(br, rc);

            // Formas decorativas tipo ondas
            DrawWave(g, rc.Width / 2 - 30, rc.Height / 2 + 20, 500, 420,
                Color.FromArgb(210, 255, 205, 0), Color.FromArgb(0, 255, 145, 0));

            DrawWave(g, rc.Width - 350, rc.Height - 180, 440, 340,
                Color.FromArgb(150, 255, 200, 0), Color.FromArgb(0, 255, 145, 0));

            DrawWave(g, 80, -40, 300, 260,
                Color.FromArgb(90, 255, 230, 60), Color.FromArgb(0, 255, 145, 0));

            // Centro de la ventana
            int cx = rc.Width / 2;
            int cy = rc.Height / 2;

            // Posición del texto del logo
            int lx = cx - 170;
            int ly = cy - 95;

            // Sombra del texto Encarta
            using (var f = new Font("Arial Black", 54f, FontStyle.Bold))
            using (var b = new SolidBrush(Color.FromArgb(35, 0, 0, 0)))
                g.DrawString("Encarta", f, b, lx - 8, ly + 38);

            // Texto Mi primera
            using (var f = new Font("Arial", 24f, FontStyle.Bold))
            using (var b = new SolidBrush(Color.FromArgb(28, 72, 12)))
                g.DrawString("Mi primera", f, b, lx + 6, ly + 2);

            // Texto Encarta
            using (var f = new Font("Arial Black", 54f, FontStyle.Bold))
            using (var b = new SolidBrush(Color.FromArgb(22, 65, 8)))
                g.DrawString("Encarta", f, b, lx - 8, ly + 35);

            // Símbolo de marca registrada
            using (var f = new Font("Arial", 14f))
            using (var b = new SolidBrush(Color.FromArgb(32, 80, 14)))
                g.DrawString("®", f, b, lx + 295, ly + 42);

            // Dibuja hojas decorativas
            DrawLeaves(g, lx + 230, ly - 8);
        }

        // Dibuja una figura ovalada con degradado para decorar
        static void DrawWave(Graphics g, int cx, int cy, int rw, int rh, Color c, Color e2)
        {
            var rect = new Rectangle(cx - rw / 2, cy - rh / 2, rw, rh);

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(rect);

                using (var br = new PathGradientBrush(path))
                {
                    br.CenterColor = c;
                    br.SurroundColors = new[] { e2 };
                    g.FillPath(br, path);
                }
            }
        }

        // Dibuja las hojas del logo
        static void DrawLeaves(Graphics g, int x, int y)
        {
            // Hoja grande
            using (var path = new GraphicsPath())
            {
                path.AddBezier(x, y + 50, x + 5, y, x + 58, y + 10, x, y + 100);
                path.AddBezier(x, y + 100, x + 10, y + 70, x + 20, y + 55, x, y + 50);

                using (var br = new LinearGradientBrush(new Rectangle(x - 2, y - 2, 62, 105),
                    Color.FromArgb(95, 215, 50), Color.FromArgb(45, 148, 18), 40f))
                    g.FillPath(br, path);

                using (var pen = new Pen(Color.FromArgb(30, 120, 10), 1f))
                    g.DrawPath(pen, path);
            }

            // Hoja pequeña
            using (var path = new GraphicsPath())
            {
                path.AddBezier(x + 26, y + 26, x + 64, y - 6, x + 80, y + 18, x + 26, y + 68);
                path.AddBezier(x + 26, y + 68, x + 46, y + 50, x + 40, y + 36, x + 26, y + 26);

                using (var br = new LinearGradientBrush(new Rectangle(x + 24, y - 8, 58, 78),
                    Color.FromArgb(118, 235, 68), Color.FromArgb(55, 168, 24), 40f))
                    g.FillPath(br, path);

                using (var pen = new Pen(Color.FromArgb(30, 120, 10), 1f))
                    g.DrawPath(pen, path);
            }
        }

        // Crea botones redondos de navegación
        Button NavBtn(string icon, int x)
        {
            var b = new Button
            {
                Text = icon,
                Font = new Font("Segoe UI Emoji", 13f),
                Size = new Size(46, 46),
                Location = new Point(x, 6),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(80, 170, 35),
                ForeColor = Color.White,
            };

            // Bordes del botón
            b.FlatAppearance.BorderSize = 2;
            b.FlatAppearance.BorderColor = VerdeOscuro;

            // Forma redonda
            b.Region = RgRound(46, 46, 23);

            // Cambia color al pasar el mouse
            b.MouseEnter += (s, e) => b.BackColor = Color.FromArgb(40, 130, 15);
            b.MouseLeave += (s, e) => b.BackColor = Color.FromArgb(80, 170, 35);

            return b;
        }

        // Crea botones de categorías
        Button CatBtn(string texto, string emoji, bool izq)
        {
            var btn = new Button
            {
                BackColor = Amarillo,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,

                // En Tag guardo el nombre sin saltos de línea
                Tag = texto.Replace("\n", " "),
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.BorderColor = Amarillo;

            // Evento para dibujar el botón a mano
            btn.Paint += (s, e) => PaintCat(e.Graphics, btn, texto, emoji, izq);

            // Efecto hover
            btn.MouseEnter += (s, e) => { btn.BackColor = Color.FromArgb(255, 238, 50); btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { btn.BackColor = Amarillo; btn.Invalidate(); };

            // Al hacer click abre la categoría
            btn.Click += (s, e) => AbrirCategoria(texto.Replace("\n", " "), emoji);

            return btn;
        }

        // Dibuja el contenido visual del botón de categoría
        static void PaintCat(Graphics g, Button btn, string texto, string emoji, bool izq)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            var rc = btn.ClientRectangle;

            // Fondo del botón con degradado
            using (var br = new LinearGradientBrush(rc,
                Color.FromArgb(255, 240, 60), btn.BackColor, 90f))
            {
                using (var path = PathRound(new Rectangle(0, 0, rc.Width, rc.Height), 14))
                    g.FillPath(br, path);
            }

            // Borde verde del botón
            using (var pen = new Pen(Color.FromArgb(45, 135, 12), 3.5f))
            using (var path = PathRound(new Rectangle(2, 2, rc.Width - 4, rc.Height - 4), 13))
                g.DrawPath(pen, path);

            // Área donde se dibuja el emoji
            int ez = Math.Min(rc.Height - 8, 58);
            int ex = izq ? 5 : rc.Width - ez - 5;
            var eRect = new Rectangle(ex, (rc.Height - ez) / 2, ez, ez);

            // Fondo suave del emoji
            using (var br2 = new SolidBrush(Color.FromArgb(30, 255, 180, 0)))
                g.FillEllipse(br2, eRect);

            // Dibuja el emoji
            using (var fe = new Font("Segoe UI Emoji", ez * 0.43f))
            {
                var sz = g.MeasureString(emoji, fe);
                g.DrawString(emoji, fe, Brushes.Black,
                    ex + (ez - sz.Width) / 2f, (rc.Height - sz.Height) / 2f);
            }

            // Posición del texto
            float txtX = izq ? ez + 12 : 8;
            float txtW = rc.Width - ez - 22;

            // Dibuja el texto del botón
            using (var ft = new Font("Arial", Math.Max(9f, rc.Height * 0.145f), FontStyle.Bold))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (var brt = new SolidBrush(Color.FromArgb(22, 65, 8)))
                g.DrawString(texto, ft, brt, new RectangleF(txtX, 0, txtW, rc.Height), sf);
        }

        // Abre el formulario de una categoría
        void AbrirCategoria(string nombre, string emoji)
        {
            var f = new FormCategoria(nombre, emoji);
            f.ShowDialog(this);
        }

        // Realiza la búsqueda con el texto escrito
        void Buscar()
        {
            string q = txtQ.Text.Trim();
            using (var fb = new FormBusqueda(q))
                fb.ShowDialog(this);
        }

        // Crea una figura con esquinas redondeadas
        static GraphicsPath PathRound(Rectangle rc, int r)
        {
            var p = new GraphicsPath();
            p.AddArc(rc.X, rc.Y, r, r, 180, 90);
            p.AddArc(rc.Right - r, rc.Y, r, r, 270, 90);
            p.AddArc(rc.Right - r, rc.Bottom - r, r, r, 0, 90);
            p.AddArc(rc.X, rc.Bottom - r, r, r, 90, 90);
            p.CloseFigure();
            return p;
        }

        // Convierte la figura redondeada en región
        static Region RgRound(int w, int h, int r)
            => new Region(PathRound(new Rectangle(0, 0, w, h), r));

        // Activa el doble buffer en un control usando reflexión
        // Esto ayuda a que no parpadee al dibujar
        static void DB(Control c)
        {
            var pi = typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            pi?.SetValue(c, true, null);
        }
    }
}