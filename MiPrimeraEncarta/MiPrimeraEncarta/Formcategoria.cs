using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MiPrimeraEncarta
{
    public partial class FormCategoria : Form
    {
        // Aquí guardo la información de cada categoría
        // Cada categoría tiene:
        // Item1 = título
        // Item2 = color en hexadecimal
        // Item3 = arreglo de temas y contenido
        static readonly Dictionary<string, Tuple<string, string, string[]>> Info =
            new Dictionary<string, Tuple<string, string, string[]>>(StringComparer.OrdinalIgnoreCase)
            {
                ["Paisajes y regiones"] = Tuple.Create(
                "Paisajes y Regiones del Mundo", "#2E8B57",
                new[] {
                    "🌍  ¿Qué es un paisaje?",
                    "Un paisaje es todo lo que podemos ver en un lugar: montañas, ríos, bosques y ciudades. Los paisajes pueden ser naturales (sin intervención humana) o culturales (modificados por las personas).",
                    "🏔️  Regiones naturales",
                    "La Tierra tiene grandes regiones: los polos, con hielo eterno; los desiertos, secos y calurosos; las selvas tropicales, llenas de vida; y las praderas, con extensos pastos verdes.",
                    "🌊  Los océanos",
                    "Cubren el 71% de la superficie terrestre. Los cinco océanos son el Pacífico (el más grande), el Atlántico, el Índico, el Ártico y el Antártico. Regulan el clima del planeta.",
                    "⛰️  Las montañas",
                    "El Everest (8 848 m) es la montaña más alta del mundo. Las cadenas montañosas como los Andes, los Himalayas y los Alpes fueron formadas por el movimiento de las placas tectónicas.",
                    "🌿  Biomas terrestres",
                    "Un bioma es una región con clima, flora y fauna similares. Los principales son: la tundra, el bosque boreal, el bosque templado, la sabana, el desierto y la selva tropical.",
                }
            ),
                ["Los seres vivos"] = Tuple.Create(
                "Los Seres Vivos", "#228B22",
                new[] {
                    "🔬  ¿Qué es un ser vivo?",
                    "Los seres vivos nacen, crecen, se reproducen y mueren. Necesitan energía para vivir y están formados por células. Se dividen en cinco reinos: animales, plantas, hongos, protistas y bacterias.",
                    "🐯  Los animales",
                    "Existen más de 8 millones de especies animales. Se clasifican en vertebrados (con columna vertebral: mamíferos, aves, reptiles, anfibios y peces) e invertebrados (insectos, arácnidos, moluscos...).",
                    "🌱  Las plantas",
                    "Las plantas producen su propio alimento mediante la fotosíntesis: absorben luz solar, CO₂ y agua para fabricar glucosa y liberar oxígeno. Son la base de casi todas las cadenas alimentarias.",
                    "🍄  Los hongos",
                    "No son plantas ni animales. No hacen fotosíntesis; obtienen nutrientes descomponiendo materia orgánica. Son esenciales para reciclar nutrientes en los ecosistemas.",
                    "🦠  Las células",
                    "La célula es la unidad básica de la vida. Las células procariotas (bacterias) no tienen núcleo. Las eucariotas (animales, plantas, hongos) sí tienen núcleo con el ADN.",
                }
            ),
                ["Ciencia y técnica"] = Tuple.Create(
                "Ciencia y Técnica", "#1565C0",
                new[] {
                    "⚗️  ¿Qué es la ciencia?",
                    "La ciencia es el conjunto de conocimientos obtenidos mediante observación y razonamiento. El método científico sigue pasos: observación → hipótesis → experimento → conclusión.",
                    "⚡  La electricidad",
                    "Es el flujo de electrones a través de un conductor. La electricidad puede ser estática o dinámica (corriente). Inventos clave: la pila (Volta, 1800), el generador (Faraday, 1831).",
                    "🚀  La exploración espacial",
                    "En 1957 se lanzó el Sputnik, el primer satélite artificial. En 1969, Neil Armstrong pisó la Luna. Hoy la ISS orbita la Tierra y sondas como Voyager viajan más allá del Sistema Solar.",
                    "💻  La tecnología digital",
                    "Los ordenadores usan el sistema binario (0 y 1). El transistor, inventado en 1947, fue la pieza clave para los chips modernos. Internet nació en 1983 y revolucionó la comunicación global.",
                    "🧬  La genética",
                    "El ADN es la molécula que guarda la información hereditaria. James Watson y Francis Crick describieron su doble hélice en 1953. El Proyecto Genoma Humano se completó en 2003.",
                }
            ),
                ["Matemáticas"] = Tuple.Create(
                "Matemáticas", "#6A1B9A",
                new[] {
                    "🔢  Los números",
                    "Los números naturales (1,2,3…) sirven para contar. Los enteros incluyen negativos. Los racionales son fracciones. Los irracionales (como π o √2) tienen decimales infinitos sin repetición.",
                    "📐  La geometría",
                    "Estudia las formas y el espacio. Pitágoras demostró que en todo triángulo rectángulo: a²+b²=c². Los polígonos regulares tienen todos sus lados y ángulos iguales.",
                    "📊  La estadística",
                    "Permite analizar datos. La media es el promedio, la mediana el valor central y la moda el más frecuente. Las gráficas de barras, circulares y lineales representan datos visualmente.",
                    "➕  Las operaciones",
                    "La suma (adición), resta (sustracción), multiplicación y división son las cuatro operaciones básicas. La potenciación y la radicación son operaciones derivadas de ellas.",
                    "∞  Matemáticas en la naturaleza",
                    "La sucesión de Fibonacci (1,1,2,3,5,8…) aparece en las espirales de los girasoles y conchas. El número áureo φ ≈ 1.618 se relaciona con proporciones bellas en la naturaleza y el arte.",
                }
            ),
                ["Deportes"] = Tuple.Create(
                "Deportes", "#B71C1C",
                new[] {
                    "⚽  El fútbol",
                    "Es el deporte más popular del mundo. Se juega con 11 jugadores por equipo. La FIFA organiza el Mundial cada 4 años desde 1930. Brasil ha ganado 5 veces la Copa del Mundo.",
                    "🏊  Los Juegos Olímpicos",
                    "Nacieron en la Antigua Grecia (776 a.C.) y fueron recuperados por Pierre de Coubertin en 1896. Se celebran cada 4 años. El lema es \"Citius, Altius, Fortius\" (Más rápido, más alto, más fuerte).",
                    "🎾  Deportes de raqueta",
                    "El tenis, el bádminton y el squash se juegan con raquetas. El tenis tiene 4 torneos Grand Slam: Wimbledon, Roland Garros, US Open y el Abierto de Australia.",
                    "🏋️  Beneficios del deporte",
                    "El ejercicio regular fortalece el corazón, los músculos y los huesos. Libera endorfinas que mejoran el estado de ánimo. La OMS recomienda al menos 60 minutos de actividad física diaria para niños.",
                    "🧠  Deporte y mente",
                    "La actividad física mejora la memoria, la concentración y el aprendizaje. Los deportes de equipo desarrollan habilidades sociales: cooperación, respeto y liderazgo.",
                }
            ),
                ["Historia"] = Tuple.Create(
                "Historia Universal", "#4E342E",
                new[] {
                    "🏺  La prehistoria",
                    "Comienza con los primeros humanos (hace ~3 millones de años) y termina con la escritura (~3 500 a.C.). El Homo sapiens surgió en África hace 300 000 años. El fuego y las herramientas cambiaron todo.",
                    "🌾  Las primeras civilizaciones",
                    "Mesopotamia (entre el Tigris y el Éufrates) y Egipto (junto al Nilo) fueron las cunas de la civilización. Inventaron la escritura cuneiforme y los jeroglíficos, las leyes y la astronomía.",
                    "🏛️  Grecia y Roma",
                    "Grecia dio al mundo la democracia, la filosofía (Sócrates, Platón, Aristóteles) y los Juegos Olímpicos. Roma extendió su imperio por Europa, África y Asia, dejando el derecho romano y el latín.",
                    "⚔️  La Edad Media",
                    "Del siglo V al XV. El feudalismo organizaba la sociedad en señores y siervos. Las Cruzadas, la Magna Carta (1215) y la Peste Negra marcaron esta época. Terminó con el Renacimiento.",
                    "🌎  La Era Moderna",
                    "Colón llegó a América en 1492. La Revolución Francesa (1789) proclamó libertad, igualdad y fraternidad. La Revolución Industrial transformó la producción y la vida cotidiana.",
                }
            ),
                ["Nuestra sociedad"] = Tuple.Create(
                "Nuestra Sociedad", "#E65100",
                new[] {
                    "👨‍👩‍👧  La familia",
                    "Es la unidad básica de la sociedad. Puede ser nuclear (padres e hijos), extensa (abuelos, tíos, primos) o monoparental. La familia transmite valores, cultura e identidad.",
                    "🏫  La escuela",
                    "Es el espacio donde aprendemos conocimientos y habilidades sociales. La educación es un derecho fundamental. La UNESCO promueve la educación para todos en el mundo.",
                    "⚖️  Los derechos humanos",
                    "En 1948 la ONU proclamó la Declaración Universal de los Derechos Humanos: derecho a la vida, a la educación, a la salud y a la libertad. Todos los seres humanos nacen libres e iguales.",
                    "🌐  La globalización",
                    "Conecta economías, culturas y personas de todo el mundo. Internet y el comercio internacional acercan a países. También genera retos: desigualdad, migraciones y cambio climático.",
                    "♻️  Ciudadanía responsable",
                    "Un buen ciudadano respeta las leyes, cuida el medio ambiente, participa en su comunidad y respeta a los demás. Pequeñas acciones cotidianas construyen una sociedad mejor.",
                }
            ),
                ["Lengua y literatura"] = Tuple.Create(
                "Lengua y Literatura", "#1A237E",
                new[] {
                    "📖  El lenguaje",
                    "Es el sistema de comunicación más complejo del mundo. Existen unas 7 000 lenguas vivas. El español es hablado por más de 500 millones de personas y es el segundo idioma más hablado nativamente.",
                    "✍️  La gramática",
                    "Estudia las reglas del idioma. Las categorías gramaticales son: sustantivos, verbos, adjetivos, adverbios, pronombres, preposiciones y conjunciones. El sujeto y el predicado forman la oración.",
                    "📜  La literatura",
                    "Es el arte de expresar ideas y emociones con palabras. Los géneros literarios son la narrativa (cuento, novela), la lírica (poesía) y el drama (teatro). Homero escribió la Ilíada y la Odisea.",
                    "🦄  La mitología",
                    "Las civilizaciones antiguas usaban mitos para explicar el mundo. Zeus gobernaba el Olimpo griego; Ra era el dios sol egipcio. Los mitos prehispánicos como Quetzalcóatl son parte de nuestra herencia.",
                    "📰  La comunicación",
                    "Los medios de comunicación informan y entretienen: prensa, radio, televisión e internet. La alfabetización mediática nos ayuda a identificar noticias falsas y fuentes confiables.",
                }
            ),
                ["Las artes"] = Tuple.Create(
                "Las Artes", "#880E4F",
                new[] {
                    "🎨  Las artes visuales",
                    "Pintura, escultura, arquitectura, fotografía y cine son artes visuales. Leonardo da Vinci pintó la Mona Lisa y La Última Cena. Pablo Picasso creó el cubismo. Frida Kahlo es símbolo del arte mexicano.",
                    "🎵  La música",
                    "Es el arte de combinar sonidos y silencios. Las notas musicales son: Do, Re, Mi, Fa, Sol, La, Si. Beethoven compuso la 9ª Sinfonía sordo. Mozart escribió más de 600 obras antes de morir a los 35 años.",
                    "🩰  La danza",
                    "Es la expresión artística a través del movimiento. Existen danzas clásicas (ballet), folklóricas (jarabe tapatío, flamenco) y contemporáneas. La danza desarrolla coordinación y expresión.",
                    "🎭  El teatro",
                    "Nació en la Antigua Grecia. Shakespeare escribió Hamlet, Romeo y Julieta y Macbeth. El teatro incluye comedias, tragedias y dramas. Las máscaras, el escenario y el vestuario son elementos clave.",
                    "🏛️  La arquitectura",
                    "Es el arte de diseñar y construir espacios. Las pirámides de Egipto, el Coliseo Romano, la Sagrada Familia de Gaudí y el Empire State son obras maestras que definen épocas y civilizaciones.",
                }
            ),
                ["Juega y aprende"] = Tuple.Create(
                "Juega y Aprende", "#F57F17",
                new[] {
                    "🧩  Los juegos de mesa",
                    "El ajedrez nació en la India hace más de 1 500 años. El tablero tiene 64 casillas y 32 piezas. Jugar ajedrez mejora la concentración, la estrategia y las matemáticas.",
                    "🔤  Los trabalenguas",
                    "\"Tres tristes tigres tragaban trigo en un trigal\" – Los trabalenguas son frases difíciles de pronunciar. Ayudan a mejorar la dicción y la memoria. ¡Inténtalo 5 veces rápido!",
                    "🧠  Los acertijos",
                    "Tengo ciudades pero no casas, montañas pero no árboles, agua pero no peces... ¿Qué soy? ¡Un mapa! Los acertijos desarrollan el pensamiento lógico y la creatividad.",
                    "🎲  Los juegos y el aprendizaje",
                    "Jugar estimula el cerebro. Los videojuegos educativos mejoran la memoria, la resolución de problemas y la coordinación. El juego libre también desarrolla la imaginación y las habilidades sociales.",
                    "🌟  Curiosidades asombrosas",
                    "• Un caracol puede dormir 3 años seguidos.\n• El corazón humano late ~100 000 veces al día.\n• La miel nunca se echa a mal.\n• El pulpo tiene tres corazones.\n• Los delfines tienen nombres propios.",
                }
            ),

                ["Programación"] = Tuple.Create(
                "Programación y Computadoras", "#006064",
                new[]
                {
                    "💻  ¿Qué es programar?",
                    "Programar es dar instrucciones precisas a una computadora en un lenguaje que ella entiende. Los programas son secuencias de pasos que resuelven problemas o automatizan tareas cotidianas.",

                    "🔢  Los algoritmos",
                    "Un algoritmo es una secuencia finita y ordenada de pasos para resolver un problema. Como una receta de cocina: tiene ingredientes (datos de entrada), pasos (instrucciones) y un resultado final.",

                    "🔀  Variables y condicionales",
                    "Una variable guarda un dato con nombre: int edad = 10; Los condicionales toman decisiones: if (llueve) abrirParaguas(); else salirACaminar(); Son la base de cualquier lógica de programa.",

                    "🔁  Bucles y funciones",
                    "Los bucles repiten instrucciones (for, while) sin copiar el código. Las funciones agrupan pasos reutilizables con un nombre: int Suma(int a, int b){ return a+b; } Mejoran la organización.",

                    "🌐  Internet y la web",
                    "Internet es la red global de computadoras. La web usa HTML (estructura), CSS (diseño) y JavaScript (comportamiento). Tim Berners-Lee inventó la World Wide Web en 1989 para compartir información.",
                }
            ),

            };

        // Estas variables guardan el nombre y emoji de la categoría seleccionada
        string _nombre, _emoji;

        // Panel del encabezado
        Panel pnlHeader;

        // FlowLayoutPanel donde se agregan las tarjetas
        FlowLayoutPanel flow;

        public FormCategoria(string nombre, string emoji)
        {
            // Guardo los datos recibidos
            _nombre = nombre;
            _emoji = emoji;

            // Construyo el formulario
            BuildForm();
        }

        void BuildForm()
        {
            // Título de la ventana
            Text = _nombre + " — Mi Primera Encarta";

            // Tamaño inicial
            ClientSize = new Size(860, 640);

            // Posición al abrir
            StartPosition = FormStartPosition.CenterParent;

            // Activo doble buffer
            DoubleBuffered = true;

            // Fondo blanco
            BackColor = Color.White;

            // Tamaño mínimo
            MinimumSize = new Size(680, 500);

            // Variables para guardar los datos de la categoría
            string titulo;
            string hexStr;
            string[] temas;

            // Si la categoría existe en el diccionario, toma sus datos
            if (Info.ContainsKey(_nombre))
            {
                var t = Info[_nombre];
                titulo = t.Item1;
                hexStr = t.Item2;
                temas = t.Item3;
            }
            else
            {
                // Si no existe, pone valores por defecto
                titulo = "Categoría";
                hexStr = "#006341";
                temas = new[] { "Contenido próximamente…", "" };
            }

            // Convierte el color hexadecimal a Color
            Color acento = ParseHexColor(hexStr);

            // Panel superior del formulario
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.Transparent };
            EnableDoubleBuffer(pnlHeader);

            // Evento para dibujar el encabezado
            pnlHeader.Paint += (s, e) => DrawHeader(e.Graphics, pnlHeader.ClientRectangle, acento);

            // Etiqueta del emoji
            var lblEmoji = new Label
            {
                Text = _emoji,
                Font = new Font("Segoe UI Emoji", 34f),
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location = new Point(18, 18),
            };
            pnlHeader.Controls.Add(lblEmoji);

            // Etiqueta del título de la categoría
            var lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Arial", 18f, FontStyle.Bold),
                AutoSize = false,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Bounds = new Rectangle(90, 15, 700, 70),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            };
            pnlHeader.Controls.Add(lblTitulo);

            // Botón para cerrar la ventana
            var btnX = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Size = new Size(36, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(80, 0, 0, 0),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };

            // Quito borde del botón
            btnX.FlatAppearance.BorderSize = 0;

            // Al dar clic se cierra el formulario
            btnX.Click += (s, e) => Close();

            // Ajusta su posición cuando cambie el tamaño
            pnlHeader.Resize += (s, e) => btnX.Location = new Point(pnlHeader.Width - 44, 8);
            btnX.Location = new Point(ClientSize.Width - 44, 8);

            pnlHeader.Controls.Add(btnX);

            // Panel con scroll
            var scroll = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20),
            };

            // FlowLayoutPanel donde van las tarjetas
            flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 8, 0, 20),
            };

            scroll.Controls.Add(flow);

            // Cuando cambia el tamaño, actualiza las tarjetas
            scroll.Resize += (s, e) => { flow.Width = scroll.Width - 40; RefreshCards(); };

            // Recorre el arreglo de temas de dos en dos
            // Uno es subtítulo y el otro es cuerpo
            for (int i = 0; i < temas.Length - 1; i += 2)
                flow.Controls.Add(BuildCard(temas[i], temas[i + 1], acento));

            // Agrega controles al formulario
            Controls.Add(scroll);
            Controls.Add(pnlHeader);

            // Cuando se muestra la ventana, ajusta tarjetas
            Shown += (s, e) => { flow.Width = scroll.Width - 40; RefreshCards(); };
        }

        // Este método dibuja el encabezado
        static void DrawHeader(Graphics g, Rectangle rc, Color acento)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Fondo con degradado
            using (var br = new LinearGradientBrush(rc, acento, MakeDarker(acento, 0.62f), 135f))
                g.FillRectangle(br, rc);

            // Curva decorativa en la parte de abajo
            using (var path = new GraphicsPath())
            {
                path.AddBezier(0, rc.Height, rc.Width / 3, rc.Height - 26,
                    rc.Width * 2 / 3, rc.Height - 10, rc.Width, rc.Height - 18);
                path.AddLine(rc.Width, rc.Height, 0, rc.Height);
                path.CloseFigure();

                using (var brS = new SolidBrush(Color.FromArgb(38, 0, 0, 0)))
                    g.FillPath(brS, path);
            }
        }

        // Este método crea una tarjeta con subtítulo y contenido
        Panel BuildCard(string subtitulo, string cuerpo, Color acento)
        {
            var card = new Panel
            {
                Width = 800,
                BackColor = Color.White,
                Padding = new Padding(20, 14, 20, 14),
                Margin = new Padding(0, 0, 0, 12),
            };

            EnableDoubleBuffer(card);

            // Se dibuja la tarjeta manualmente
            card.Paint += (s, e) => DrawCard(e.Graphics, card.ClientRectangle);

            // Barra lateral de color
            var barra = new Panel { Width = 5, Dock = DockStyle.Left, BackColor = acento };
            card.Controls.Add(barra);

            // Panel interno
            var inner = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(12, 0, 0, 0),
            };

            // Etiqueta del subtítulo
            var lblT = new Label
            {
                Text = subtitulo,
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 28,
                Font = new Font("Arial", 11f, FontStyle.Bold),
                ForeColor = acento,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            // Etiqueta del contenido
            var lblB = new Label
            {
                Text = cuerpo,
                Dock = DockStyle.Top,
                AutoSize = false,
                Font = new Font("Segoe UI", 10f),
                ForeColor = Color.FromArgb(45, 45, 45),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopLeft,
                UseMnemonic = false,
                Padding = new Padding(0, 4, 0, 0),
            };

            // Agrega contenido al panel interno
            inner.Controls.Add(lblB);
            inner.Controls.Add(lblT);

            // Agrega panel interno a la tarjeta
            card.Controls.Add(inner);

            // Ajusta altura de la tarjeta según el texto
            card.Layout += (s, e) =>
            {
                lblB.Width = inner.Width - 4;

                using (var g = lblB.CreateGraphics())
                {
                    var sz = g.MeasureString(cuerpo, lblB.Font, new SizeF(lblB.Width - 4, 9999));
                    lblB.Height = (int)sz.Height + 10;
                }

                card.Height = lblT.Height + lblB.Height + 40;
            };

            return card;
        }

        // Este método dibuja el fondo y borde de la tarjeta
        static void DrawCard(Graphics g, Rectangle rc)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var path = MakeRoundedPath(new Rectangle(0, 0, rc.Width - 1, rc.Height - 1), 10))
            {
                g.FillPath(Brushes.White, path);

                using (var pen = new Pen(Color.FromArgb(215, 215, 215), 1f))
                    g.DrawPath(pen, path);
            }
        }

        // Este método refresca el tamaño de todas las tarjetas
        void RefreshCards()
        {
            foreach (Control c in flow.Controls)
                if (c is Panel p)
                {
                    p.Width = flow.Width - 4;
                    p.PerformLayout();
                }
        }

        // Convierte un color hexadecimal a Color
        static Color ParseHexColor(string hex)
        {
            try { return ColorTranslator.FromHtml(hex); }
            catch { return Color.FromArgb(0, 99, 65); }
        }

        // Hace un color más oscuro
        static Color MakeDarker(Color c, float factor)
            => Color.FromArgb(c.A, (int)(c.R * factor), (int)(c.G * factor), (int)(c.B * factor));

        // Crea una figura con bordes redondeados
        static GraphicsPath MakeRoundedPath(Rectangle rc, int r)
        {
            var p = new GraphicsPath();
            p.AddArc(rc.X, rc.Y, r, r, 180, 90);
            p.AddArc(rc.Right - r, rc.Y, r, r, 270, 90);
            p.AddArc(rc.Right - r, rc.Bottom - r, r, r, 0, 90);
            p.AddArc(rc.X, rc.Bottom - r, r, r, 90, 90);
            p.CloseFigure();
            return p;
        }

        // Activa doble buffer para evitar parpadeos
        static void EnableDoubleBuffer(Control c)
        {
            var pi = typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            pi?.SetValue(c, true, null);
        }
    }
}