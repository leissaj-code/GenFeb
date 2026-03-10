using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MiPrimeraEncarta
{
    // Este formulario sirve para hacer búsquedas dentro de la encarta
    public partial class FormBusqueda : Form
    {
        // Colores verdes que voy a usar en la parte superior
        static readonly Color VerdeH = Color.FromArgb(45, 158, 16);
        static readonly Color VerdeH2 = Color.FromArgb(28, 110, 8);

        // Aquí declaro los controles que voy a usar en el formulario
        TextBox txtQ;
        FlowLayoutPanel flowRes;
        Label lblStatus;
        Panel scrollPanel;

        // Constructor del formulario
        // Recibe un texto opcional para buscar desde el inicio
        public FormBusqueda(string queryInicial = "")
        {
            // Título de la ventana
            Text = "Búsqueda — Mi Primera Encarta";

            // Tamaño inicial
            ClientSize = new Size(720, 580);

            // Tamaño mínimo
            MinimumSize = new Size(540, 420);

            // Se abre en el centro del formulario padre
            StartPosition = FormStartPosition.CenterParent;

            // Activo doble buffer para evitar parpadeos
            DoubleBuffered = true;

            // Color de fondo general
            BackColor = Color.FromArgb(247, 249, 246);

            // Construyo toda la interfaz
            BuildUI();

            // Si sí viene una búsqueda inicial, la coloca y busca
            if (!string.IsNullOrEmpty(queryInicial))
            {
                txtQ.Text = queryInicial;
                DoSearch();
            }
        }

        // Este método crea toda la interfaz del formulario
        void BuildUI()
        {
            // Panel superior que funciona como encabezado
            var header = new Panel { Dock = DockStyle.Top, Height = 78 };
            DB(header);

            // Evento para pintar el encabezado manualmente
            header.Paint += (s, e) =>
            {
                var g = e.Graphics;
                var rc = header.ClientRectangle;

                // Fondo verde con degradado
                using (var br = new LinearGradientBrush(rc, VerdeH, VerdeH2, 90f))
                    g.FillRectangle(br, rc);

                // Curva decorativa en la parte inferior
                using (var path = new GraphicsPath())
                {
                    path.AddBezier(0, rc.Height,
                        rc.Width / 3, rc.Height - 22,
                        rc.Width * 2 / 3, rc.Height - 8,
                        rc.Width, rc.Height - 16);
                    path.AddLine(rc.Width, rc.Height, 0, rc.Height);
                    path.CloseFigure();

                    using (var brS = new SolidBrush(Color.FromArgb(36, 0, 0, 0)))
                        g.FillPath(brS, path);
                }
            };

            // Ícono de búsqueda
            var lIco = new Label
            {
                Text = "🔍",
                Font = new Font("Segoe UI Emoji", 26f),
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location = new Point(14, 16)
            };

            // Título principal
            var lTit = new Label
            {
                Text = "Búsqueda en Mi Primera Encarta",
                Font = new Font("Arial", 15f, FontStyle.Bold),
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location = new Point(62, 24)
            };

            // Agrego el ícono y el título al encabezado
            header.Controls.AddRange(new Control[] { lIco, lTit });

            // Panel que contiene la caja de búsqueda y el botón
            var barSrch = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Color.White,
                Padding = new Padding(12, 11, 12, 9)
            };

            // Caja de texto donde se escribe lo que se quiere buscar
            txtQ = new TextBox
            {
                Font = new Font("Segoe UI", 11f),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(12, 13),
                Height = 30,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
            };

            // Si el usuario presiona Enter, hace la búsqueda
            txtQ.KeyPress += (s, e) => { if (e.KeyChar == '\r') DoSearch(); };

            // Botón para buscar
            var btnGo = new Button
            {
                Text = "Buscar",
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                BackColor = VerdeH,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Height = 30,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
            };

            // Quito el borde del botón
            btnGo.FlatAppearance.BorderSize = 0;

            // Efecto cuando pasa el mouse
            btnGo.MouseEnter += (s, e) => btnGo.BackColor = VerdeH2;
            btnGo.MouseLeave += (s, e) => btnGo.BackColor = VerdeH;

            // Cuando se da clic, busca
            btnGo.Click += (s, e) => DoSearch();

            // Agrego caja y botón al panel de búsqueda
            barSrch.Controls.AddRange(new Control[] { txtQ, btnGo });

            // Ajusta posiciones y tamaños cuando cambie el tamaño del panel
            barSrch.Resize += (s, e) =>
            {
                btnGo.Width = 90;
                btnGo.Location = new Point(barSrch.Width - 104, 13);
                txtQ.Width = barSrch.Width - 118;
            };

            // Etiqueta que muestra mensajes como cantidad de resultados
            lblStatus = new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Text = "Escribe una palabra y pulsa Buscar.",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Italic),
                ForeColor = Color.Gray,
                BackColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 0, 0),
            };

            // Panel principal con scroll para mostrar resultados
            scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(244, 247, 243),
            };
            DB(scrollPanel);

            // FlowLayoutPanel donde se colocan las tarjetas de resultados
            flowRes = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                Padding = new Padding(12, 12, 12, 18),
            };

            // Agrego el contenedor de resultados al panel con scroll
            scrollPanel.Controls.Add(flowRes);

            // Cuando cambia el tamaño, ajusta el ancho de las tarjetas
            scrollPanel.Resize += (s, e) =>
            {
                flowRes.Width = scrollPanel.Width - 24;
                RefreshCards();
            };

            // Agrego todos los controles al formulario
            Controls.Add(scrollPanel);
            Controls.Add(lblStatus);
            Controls.Add(barSrch);
            Controls.Add(header);

            // Cuando el formulario se muestre, enfoca la caja de texto
            Shown += (s, e) =>
            {
                txtQ.Focus();
                flowRes.Width = scrollPanel.Width - 24;
            };
        }

        // Este método hace la búsqueda
        void DoSearch()
        {
            // Obtiene el texto escrito sin espacios al inicio y final
            string q = txtQ.Text.Trim();

            // Limpia resultados anteriores
            flowRes.Controls.Clear();

            // Si no escribió nada, muestra mensaje y sale
            if (string.IsNullOrEmpty(q))
            {
                lblStatus.Text = "Escribe una palabra y pulsa Buscar.";
                return;
            }

            // Busca en los datos usando la clase ConceptosData
            var res = ConceptosData.Buscar(q);

            // Si no encontró nada
            if (res.Count == 0)
            {
                lblStatus.Text = $"   Sin resultados para \"{q}\".";

                // Mensaje visual de que no hubo resultados
                var lEmpty = new Label
                {
                    Text = "😕  No se encontraron resultados.\nIntenta con otra palabra.",
                    Font = new Font("Segoe UI", 11f),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Padding = new Padding(16, 20, 0, 0),
                    BackColor = Color.Transparent,
                };

                flowRes.Controls.Add(lEmpty);
            }
            else
            {
                // Si sí encontró resultados, muestra cuántos hay
                lblStatus.Text = $"   {res.Count} resultado(s) para \"{q}\":";

                // Recorre todos los resultados y crea una tarjeta por cada uno
                foreach (var c in res)
                    flowRes.Controls.Add(CardResult(c));
            }

            // Ajusta el tamaño de las tarjetas
            RefreshCards();
        }

        // Este método construye una tarjeta de resultado
        Panel CardResult(ConceptosData.Concepto c)
        {
            // Obtiene un color dependiendo de la categoría
            Color acento = CatColor(c.Categoria);

            // Panel principal de la tarjeta
            var card = new Panel
            {
                Height = 86,
                Margin = new Padding(0, 0, 0, 9),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
            };
            DB(card);

            // Se dibuja la tarjeta manualmente
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Dibuja fondo y borde redondeado
                using (var gp = RR(new Rectangle(0, 0, card.Width - 1, card.Height - 1), 10))
                {
                    g.FillPath(Brushes.White, gp);
                    using (var pen = new Pen(Color.FromArgb(205, 220, 205), 1.2f))
                        g.DrawPath(pen, gp);
                }
            };

            // Barra lateral de color
            var bar = new Panel { Width = 5, Dock = DockStyle.Left, BackColor = acento };
            card.Controls.Add(bar);

            // Panel interno donde va el texto
            var inner = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(11, 6, 8, 5),
            };

            // Etiqueta de categoría
            var lCat = new Label
            {
                Text = c.Emoji + "  " + c.Categoria,
                Dock = DockStyle.Top,
                Height = 20,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = acento,
                BackColor = Color.Transparent,
                UseMnemonic = false,
            };

            // Etiqueta del título
            var lTit = new Label
            {
                Text = c.Titulo,
                Dock = DockStyle.Top,
                Height = 23,
                Font = new Font("Arial", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(26, 26, 26),
                BackColor = Color.Transparent,
                UseMnemonic = false,
            };

            // Si el contenido es muy largo, lo recorta para mostrar una vista previa
            string prev = c.Contenido.Length > 102
                ? c.Contenido.Substring(0, 99) + "…"
                : c.Contenido;

            // Etiqueta de vista previa del contenido
            var lPrev = new Label
            {
                Text = prev,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(105, 105, 105),
                BackColor = Color.Transparent,
                UseMnemonic = false,
            };

            // Agrego etiquetas al panel interno
            inner.Controls.Add(lPrev);
            inner.Controls.Add(lTit);
            inner.Controls.Add(lCat);

            // Agrego el panel interno a la tarjeta
            card.Controls.Add(inner);

            // Acción para abrir la categoría al hacer clic
            Action abrir = () => new FormCategoria(c.Categoria, c.Emoji).ShowDialog(this);

            // Todos estos controles reaccionan al clic
            foreach (Control ctrl in new Control[] { card, inner, lCat, lTit, lPrev })
                ctrl.Click += (s, e) => abrir();

            // Efecto visual al pasar el mouse
            card.MouseEnter += (s, e) => { card.BackColor = Color.FromArgb(242, 252, 242); card.Invalidate(); };
            card.MouseLeave += (s, e) => { card.BackColor = Color.White; card.Invalidate(); };

            return card;
        }

        // Este método ajusta el ancho de las tarjetas
        void RefreshCards()
        {
            int w = Math.Max(80, flowRes.Width - 26);

            foreach (Control c in flowRes.Controls)
                if (c is Panel p) p.Width = w;
        }

        // Este método regresa un color según la categoría
        static Color CatColor(string cat)
        {
            switch (cat.ToLower().Trim())
            {
                case "paisajes y regiones": return Color.FromArgb(46, 139, 87);
                case "los seres vivos": return Color.FromArgb(34, 139, 34);
                case "ciencia y técnica": return Color.FromArgb(21, 101, 192);
                case "matemáticas": return Color.FromArgb(106, 27, 154);
                case "deportes": return Color.FromArgb(183, 28, 28);
                case "historia": return Color.FromArgb(78, 52, 46);
                case "nuestra sociedad": return Color.FromArgb(230, 81, 0);
                case "lengua y literatura": return Color.FromArgb(26, 35, 126);
                case "las artes": return Color.FromArgb(136, 14, 79);
                case "juega y aprende": return Color.FromArgb(245, 127, 23);
                case "programación": return Color.FromArgb(0, 105, 92);

                // Color por defecto si no coincide ninguna categoría
                default: return Color.FromArgb(50, 155, 15);
            }
        }

        // Crea una figura con bordes redondeados
        static GraphicsPath RR(Rectangle rc, int r)
        {
            var p = new GraphicsPath();
            p.AddArc(rc.X, rc.Y, r * 2, r * 2, 180, 90);
            p.AddArc(rc.Right - r * 2, rc.Y, r * 2, r * 2, 270, 90);
            p.AddArc(rc.Right - r * 2, rc.Bottom - r * 2, r * 2, r * 2, 0, 90);
            p.AddArc(rc.X, rc.Bottom - r * 2, r * 2, r * 2, 90, 90);
            p.CloseFigure();
            return p;
        }

        // Activa doble buffer en un control
        // Esto ayuda a que no parpadee al dibujarse
        static void DB(Control c)
        {
            var pi = typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            pi?.SetValue(c, true, null);
        }
    }
}