using System;
using System.Collections.Generic;

namespace MiPrimeraEncarta
{
    // Esta clase guarda todos los conceptos de la encarta
    public static partial class ConceptosData
    {
        // Arreglo bidimensional donde se guardan los datos
        // Cada fila es un concepto
        // Columnas:
        // [0] = categoría
        // [1] = emoji
        // [2] = título
        // [3] = contenido
        static readonly string[,] _datos =
        {
            // Categoría: Paisajes y regiones
            { "Paisajes y regiones", "🌍", "¿Qué es un paisaje?",    "Un paisaje es todo lo que podemos ver en un lugar: montañas, ríos, bosques y ciudades. Puede ser natural (sin intervención humana) o cultural (transformado por personas)." },
            { "Paisajes y regiones", "🌍", "Los polos",              "Las regiones polares están cubiertas de hielo todo el año. El Ártico es un océano helado; la Antártida es un continente. Las temperaturas pueden bajar a –80 °C." },
            { "Paisajes y regiones", "🌍", "Los desiertos",          "Son regiones muy secas con menos de 250 mm de lluvia al año. El Sahara es el desierto cálido más grande. El de la Patagonia es un ejemplo de desierto frío." },
            { "Paisajes y regiones", "🌍", "Las selvas tropicales",  "Tienen el mayor número de especies del planeta. El Amazonas produce el 20 % del oxígeno terrestre y alberga millones de especies animales y vegetales." },
            { "Paisajes y regiones", "🌍", "Los océanos",            "Cubren el 71 % de la Tierra. El Pacífico es el más grande. Regulan el clima global y son hábitat de cientos de miles de especies marinas." },
            { "Paisajes y regiones", "🌍", "Las montañas",           "El Everest mide 8 848 m. Las cadenas como los Andes, los Himalayas y los Alpes se formaron por el choque de placas tectónicas a lo largo de millones de años." },

            // Categoría: Los seres vivos
            { "Los seres vivos", "🐯", "¿Qué es un ser vivo?",   "Los seres vivos nacen, crecen, se reproducen y mueren. Están formados por células y necesitan energía para vivir. Se clasifican en cinco reinos principales." },
            { "Los seres vivos", "🐯", "Los mamíferos",          "Vertebrados de sangre caliente que alimentan a sus crías con leche materna. Van desde el diminuto ratón etrusco hasta la ballena azul, el animal más grande del planeta." },
            { "Los seres vivos", "🐯", "Las aves",               "Vertebrados con plumas, pico y alas. Aunque no todas vuelan (pingüinos, avestruces), todas tienen huesos ligeros. Existen unas 10 000 especies conocidas." },
            { "Los seres vivos", "🐯", "Los insectos",           "Son los animales más numerosos: más de un millón de especies descritas. Tienen 6 patas y cuerpo dividido en cabeza, tórax y abdomen. Las abejas polinizan el 70 % de los cultivos." },
            { "Los seres vivos", "🐯", "La fotosíntesis",        "Las plantas usan luz solar, CO₂ y agua para fabricar glucosa y liberar oxígeno. Es el proceso que sostiene casi toda la vida en la Tierra al producir la base de la cadena alimentaria." },
            { "Los seres vivos", "🐯", "La célula",              "Es la unidad básica de la vida. Las procariotas (bacterias) no tienen núcleo. Las eucariotas (animales, plantas, hongos) sí tienen núcleo con el ADN. Un humano adulto tiene ~37 billones de células." },

            // Categoría: Ciencia y técnica
            { "Ciencia y técnica", "🔬", "El método científico",     "Observación → pregunta → hipótesis → experimento → análisis → conclusión. Es la base del conocimiento científico moderno y garantiza resultados verificables." },
            { "Ciencia y técnica", "🔬", "La electricidad",          "Es el flujo de electrones a través de un conductor. Alessandro Volta inventó la pila en 1800. La corriente alterna, impulsada por Nikola Tesla, alimenta nuestros hogares." },
            { "Ciencia y técnica", "🔬", "La exploración espacial",  "El Sputnik (1957) fue el primer satélite artificial. Neil Armstrong pisó la Luna en 1969. La ISS orbita la Tierra desde el año 2000 con tripulación permanente." },
            { "Ciencia y técnica", "🔬", "La genética",              "El ADN guarda la información hereditaria. Watson y Crick describieron la doble hélice en 1953. El Proyecto Genoma Humano fue completado en 2003 con más de 3 000 millones de pares de bases." },
            { "Ciencia y técnica", "🔬", "La física cuántica",       "Estudia el comportamiento de partículas subatómicas. Sus principios son la base de láseres, transistores y computadoras cuánticas. El electrón puede comportarse como onda y como partícula." },
            { "Ciencia y técnica", "🔬", "La inteligencia artificial","Rama de la informática que permite a las máquinas aprender de datos. Usa redes neuronales para reconocer imágenes, traducir idiomas y tomar decisiones complejas." },

            // Categoría: Matemáticas
            { "Matemáticas", "🧮", "Los números",              "Naturales (1,2,3…), enteros, racionales e irracionales. Pi (π ≈ 3.14159) es irracional: sus decimales son infinitos y no se repiten. Todo número real ocupa un lugar en la recta numérica." },
            { "Matemáticas", "🧮", "La geometría",             "Estudia formas y espacio. Pitágoras demostró: a²+b²=c² en triángulos rectángulos. Los polígonos regulares tienen todos sus lados y ángulos iguales. El círculo tiene infinitos ejes de simetría." },
            { "Matemáticas", "🧮", "La estadística",           "Analiza datos para tomar decisiones. La media es el promedio; la mediana el valor central; la moda el más frecuente. Las gráficas de barras y circulares representan datos visualmente." },
            { "Matemáticas", "🧮", "Sucesión de Fibonacci",   "1, 1, 2, 3, 5, 8, 13, 21… Cada número es la suma de los dos anteriores. Aparece en flores, conchas marinas y proporciones de seres vivos. Está relacionada con el número áureo φ ≈ 1.618." },
            { "Matemáticas", "🧮", "Las fracciones",           "Representan partes de un entero. ½ es la mitad, ¾ son tres cuartas partes. Para sumarlas hay que igualar los denominadores. Son la base de los números decimales y porcentajes." },
            { "Matemáticas", "🧮", "Las probabilidades",       "Miden la posibilidad de que ocurra un evento. Un dado justo tiene probabilidad 1/6 para cada cara. La probabilidad va de 0 (imposible) a 1 (seguro). Son esenciales en ciencia y economía." },

            // Categoría: Deportes
            { "Deportes", "⚽", "El fútbol",              "Es el deporte más popular del mundo con más de 4 000 millones de seguidores. Se juega con 11 jugadores por equipo. El Mundial FIFA se celebra cada 4 años. Brasil ha ganado 5 veces el campeonato." },
            { "Deportes", "⚽", "Los Juegos Olímpicos",   "Nacieron en Grecia en el 776 a.C. y fueron renovados por Pierre de Coubertin en 1896. Se celebran cada 4 años. El lema olímpico es 'Citius, Altius, Fortius' (Más rápido, más alto, más fuerte)." },
            { "Deportes", "⚽", "El ajedrez",             "Nació en India hace más de 1 500 años. El tablero tiene 64 casillas y 32 piezas. Es reconocido como deporte por el COI. Jugar ajedrez mejora la concentración, la estrategia y las matemáticas." },
            { "Deportes", "⚽", "El atletismo",           "Incluye carreras, saltos y lanzamientos. Usain Bolt corrió 100 m en 9.58 s en 2009, récord mundial vigente. Es uno de los deportes más antiguos de los Juegos Olímpicos modernos." },
            { "Deportes", "⚽", "La natación",            "Trabaja todos los grupos musculares a la vez. Los estilos principales son crol, espalda, pecho y mariposa. Michael Phelps ganó 23 medallas de oro olímpicas a lo largo de su carrera." },
            { "Deportes", "⚽", "Beneficios del ejercicio","Fortalece el corazón, músculos y huesos. Libera endorfinas que mejoran el ánimo y reducen el estrés. La OMS recomienda al menos 60 minutos de actividad física diaria para niños y adolescentes." },

            // Categoría: Historia
            { "Historia", "🏺", "La prehistoria",          "Comienza con los primeros humanos (~3 millones de años) y termina con la escritura (~3 500 a.C.). El Homo sapiens surgió en África hace 300 000 años. El fuego y las herramientas de piedra cambiaron todo." },
            { "Historia", "🏺", "Primeras civilizaciones", "Mesopotamia (entre el Tigris y el Éufrates) y Egipto (junto al Nilo) inventaron la escritura cuneiforme y los jeroglíficos, las leyes y la astronomía. Las pirámides de Guiza tienen 4 500 años." },
            { "Historia", "🏺", "Grecia y Roma",           "Grecia aportó la democracia, la filosofía (Sócrates, Platón, Aristóteles) y los Juegos Olímpicos. Roma extendió el derecho romano y el latín, origen del español, francés e italiano." },
            { "Historia", "🏺", "La Edad Media",           "Del siglo V al XV. El feudalismo organizó la sociedad en señores y siervos. La Magna Carta (1215) limitó el poder del rey. La imprenta de Gutenberg (1450) revolucionó la cultura." },
            { "Historia", "🏺", "La Era Moderna",          "Colón llegó a América en 1492. La Revolución Francesa (1789) proclamó libertad, igualdad y fraternidad. La Revolución Industrial transformó la producción y la vida cotidiana del siglo XIX." },
            { "Historia", "🏺", "El siglo XX",             "Dos guerras mundiales, la bomba atómica (1945), la llegada a la Luna (1969), la caída del Muro de Berlín (1989) y el nacimiento de Internet marcaron el siglo más transformador de la historia." },

            // Categoría: Nuestra sociedad
            { "Nuestra sociedad", "👥", "La familia",          "Es la unidad básica de la sociedad. Puede ser nuclear (padres e hijos), extensa (abuelos, tíos) o monoparental. Transmite valores, cultura e identidad a las nuevas generaciones." },
            { "Nuestra sociedad", "👥", "Los derechos humanos","La ONU los proclamó en 1948: derecho a la vida, a la educación, a la salud y a la libertad. Son universales e inalienables. Todos los seres humanos nacen libres e iguales en dignidad." },
            { "Nuestra sociedad", "👥", "La globalización",    "Conecta economías, culturas y personas de todo el mundo. Internet y el comercio internacional acercan países. Genera retos: desigualdad, migraciones forzadas y cambio climático." },
            { "Nuestra sociedad", "👥", "La democracia",       "Es el gobierno del pueblo. Los ciudadanos eligen a sus representantes mediante el voto libre y secreto. Nació en Atenas hace 2 500 años y hoy es el sistema de gobierno más extendido." },

            // Categoría: Lengua y literatura
            { "Lengua y literatura", "✒️", "El lenguaje",     "Existen unas 7 000 lenguas vivas. El español lo hablan más de 500 millones como lengua nativa. El lenguaje permite comunicar ideas, emociones y conocimiento entre personas y generaciones." },
            { "Lengua y literatura", "✒️", "La gramática",    "Estudia las reglas del idioma. Sustantivos, verbos, adjetivos y adverbios son las categorías principales. El sujeto y el predicado forman la estructura básica de toda oración." },
            { "Lengua y literatura", "✒️", "La poesía",       "Es la expresión más musical del lenguaje. Usa versos, rima y ritmo para transmitir emociones. Garcilaso de la Vega y Pablo Neruda son dos de los grandes poetas del español." },
            { "Lengua y literatura", "✒️", "La narrativa",    "Incluye el cuento, la novela y la fábula. Cervantes escribió El Quijote (1605), la primera novela moderna. Gabriel García Márquez creó el realismo mágico con Cien años de soledad." },

            // Categoría: Las artes
            { "Las artes", "🎨", "Las artes visuales","Pintura, escultura, arquitectura y fotografía. Leonardo da Vinci pintó la Mona Lisa. Pablo Picasso creó el cubismo. Frida Kahlo es símbolo del arte mexicano del siglo XX." },
            { "Las artes", "🎨", "La música",         "Arte de combinar sonidos y silencios. Beethoven compuso su 9ª Sinfonía siendo sordo. Mozart escribió más de 600 obras. El jazz y el rock nacieron en el siglo XX y transformaron la cultura global." },
            { "Las artes", "🎨", "La danza",          "Expresión artística a través del movimiento corporal. Incluye ballet clásico, danzas folclóricas (jarabe tapatío, flamenco) y danza contemporánea. Desarrolla coordinación, disciplina y expresividad." },
            { "Las artes", "🎨", "El cine",           "Arte del siglo XX. Los hermanos Lumière hicieron la primera proyección pública en 1895. Hollywood y Bollywood son los mayores centros de producción cinematográfica del mundo." },

            // Categoría: Juega y aprende
            { "Juega y aprende", "🔤", "Los acertijos",     "¿Tengo ciudades sin casas, agua sin peces, montañas sin árboles? ¡Un mapa! Los acertijos desarrollan el pensamiento lógico, la creatividad y la resolución de problemas." },
            { "Juega y aprende", "🔤", "Los trabalenguas",  "'Tres tristes tigres…' Los trabalenguas mejoran la dicción, la memoria y la pronunciación. Son también una herramienta útil para aprender a hablar idiomas extranjeros con fluidez." },

            // Categoría: Programación
            { "Programación", "💻", "¿Qué es programar?",        "Programar es dar instrucciones precisas a una computadora usando un lenguaje que ella entiende. Los programas son secuencias de instrucciones que resuelven problemas o realizan tareas." },
            { "Programación", "💻", "Los algoritmos",             "Un algoritmo es una secuencia finita y ordenada de pasos para resolver un problema. Como una receta de cocina: tiene ingredientes (datos), pasos (instrucciones) y un resultado final." },
            { "Programación", "💻", "Las variables",              "Una variable es un espacio en memoria donde guardamos datos con un nombre. En C#: int edad = 10; guarda el número 10 con el nombre 'edad'. El valor puede cambiar durante la ejecución." },
            { "Programación", "💻", "Los condicionales",          "Permiten tomar decisiones: si (if) algo es verdad, ejecuta A; si no (else), ejecuta B. Ejemplo: if (llueve) abrirParaguas(); else salirACaminar(); Son la base de toda lógica de programas." },
            { "Programación", "💻", "Los bucles",                 "Los bucles repiten instrucciones sin copiar el código. El bucle for repite un número fijo de veces. El while repite mientras una condición sea verdadera. Evitan duplicación de código." },
            { "Programación", "💻", "Las funciones",              "Una función es un bloque de código reutilizable con un nombre. int Suma(int a, int b){ return a+b; } La podemos llamar muchas veces desde distintos lugares del programa." },
            { "Programación", "💻", "Lenguajes de programación",  "Existen más de 700 lenguajes. C# y Java son ideales para aplicaciones de escritorio; Python para ciencia de datos; JavaScript para la web; Scratch para aprender programación visualmente." },
            { "Programación", "💻", "Internet y la web",          "Internet es la red global de computadoras conectadas. La web usa HTML (estructura), CSS (estilo) y JavaScript (comportamiento). Tim Berners-Lee inventó la World Wide Web en 1989." },
            { "Programación", "💻", "Inteligencia Artificial",    "La IA permite a las máquinas aprender de datos sin ser programadas explícitamente. Las redes neuronales imitan el cerebro. ChatGPT, Alexa y los autos autónomos son ejemplos de IA en uso." },
            { "Programación", "💻", "Seguridad informática",      "Protege sistemas y datos de accesos no autorizados. Usa contraseñas seguras, cifrado y antivirus. Un hacker ético busca vulnerabilidades para corregirlas antes que los ciberdelincuentes." },
        };

        // Esta clase representa un concepto ya organizado
        // Sirve para trabajar más fácil con los datos
        public class Concepto
        {
            // Guarda la categoría del concepto
            public string Categoria { get; set; }

            // Guarda el emoji del concepto
            public string Emoji { get; set; }

            // Guarda el título del concepto
            public string Titulo { get; set; }

            // Guarda la explicación o información del concepto
            public string Contenido { get; set; }
        }

        // Aquí se guardarán todos los conceptos ya convertidos en objetos
        public static readonly Concepto[] Todos;

        // Constructor estático
        // Se ejecuta una sola vez al iniciar la clase
        static ConceptosData()
        {
            // Obtiene cuántas filas tiene el arreglo bidimensional
            int n = _datos.GetLength(0);

            // Se crea el arreglo plano con ese tamaño
            Todos = new Concepto[n];

            // Recorre todas las filas del arreglo original
            for (int i = 0; i < n; i++)
                Todos[i] = new Concepto
                {
                    // Se pasa cada columna al objeto Concepto
                    Categoria = _datos[i, 0],
                    Emoji = _datos[i, 1],
                    Titulo = _datos[i, 2],
                    Contenido = _datos[i, 3],
                };
        }

        // Método para buscar conceptos
        // Busca en categoría, título y contenido
        public static List<Concepto> Buscar(string query)
        {
            // Convierte el texto a minúsculas y quita espacios al inicio y final
            string q = query.ToLower().Trim();

            // Lista donde se guardarán los resultados encontrados
            var result = new List<Concepto>();

            // Recorre todos los conceptos
            foreach (var c in Todos)
            {
                // Si encuentra coincidencia en categoría, título o contenido
                if (c.Categoria.ToLower().Contains(q) ||
                    c.Titulo.ToLower().Contains(q) ||
                    c.Contenido.ToLower().Contains(q))

                    // Agrega el concepto a la lista de resultados
                    result.Add(c);
            }

            // Regresa la lista con los resultados
            return result;
        }

        // Método para obtener conceptos de una sola categoría
        public static List<Concepto> PorCategoria(string categoria)
        {
            // Lista donde se guardan los conceptos encontrados
            var result = new List<Concepto>();

            // Recorre todos los conceptos
            foreach (var c in Todos)

                // Compara la categoría ignorando mayúsculas y minúsculas
                if (string.Equals(c.Categoria, categoria, StringComparison.OrdinalIgnoreCase))

                    // Si coincide, lo agrega a la lista
                    result.Add(c);

            // Regresa la lista final
            return result;
        }
    }
}