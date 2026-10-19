using System;
using System.IO; // Sirve para leer o escribir archivos

namespace Practica_5
{
    class Program
    {
        // Arreglos para terminos y significados
        static string[] terminos = new string[20];
        static string[] significados = new string[20];
        static int contador = 10; // Terminos cargados. 
        static string archivo = "diccionario.txt"; // Nombre del archivo

        static void Main(string[] args)
        {
            Cargararchivo(); //Cuando inicia carga los archivos

            //Terminos 
            terminos[0] = "dato";
            significados[0] = "Informacion cargada para usar.";

            terminos[1] = "variable";
            significados[1] = "Guarda un valor.";

            terminos[2] = "texto";
            significados[2] = "Cadena de caractees.";

            terminos[3] = "numero";
            significados[3] = "Valor que puede sumarse, restarse, multiplicarse, dividirse, etc.";

            terminos[4] = "suma";
            significados[4] = "Operacion que junta numeros para un resultado.";

            terminos[5] = "resta";
            significados[5] = "Operacion que saca una cantidad.";

            terminos[6] = "bucle";
            significados[6] = "Repite una accion varias veces.";

            terminos[7] = "condicion";
            significados[7] = "Decide si algo se cumple o no.";

            terminos[8] = "funcion";
            significados[8] = "Bloque de codigo que realiza una tarea.";

            terminos[9] = "programa";
            significados[9] = "Conjunto de instrucciones que hace algo.";

            int opcion;
            do
            {
                //menu
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("===========================================");
                Console.WriteLine("     DICCIONARIO DE PROGRAMACIÓN");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Agregar termino");
                Console.WriteLine("2. Mostrar terminos");
                Console.WriteLine("3. Buscar termino");
                Console.WriteLine("4. Actualizar termino");
                Console.WriteLine("5. Eliminar termino");
                Console.WriteLine("6. Salir");
                Console.WriteLine("===========================================");
                Console.Write("Selecciona una opcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opcion invalida. Intenta nuevamente.\n");
                    continue;
                }

                switch (opcion)
                {
                    case 1: Agregartermino(); break;
                    case 2: Mostrarterminos(); break;
                    case 3: Buscartermino(); break;
                    case 4: Actualizartermino(); break;
                    case 5: Eliminartermino(); break;
                    case 6: Console.WriteLine("Saliendo."); break;
                    default: Console.WriteLine("Opcion no valida\n"); break;
                }

            } while (opcion != 6);
        }

        //Guarda lo que agregamos en un archivo
        static void Guardararchivo()
        {
            using (StreamWriter sw = new StreamWriter(archivo))
            {
                for (int i = 0; i < contador; i++)
                {
                    sw.WriteLine($"{terminos[i]}|{significados[i]}");
                }
            }
        }

        //Carga los datos
        static void Cargararchivo()
        {
            if (File.Exists(archivo))
            {
                string[] lineas = File.ReadAllLines(archivo);
                contador = 0;
                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split('|');
                    if (partes.Length == 2)
                    {
                        terminos[contador] = partes[0];
                        significados[contador] = partes[1];
                        contador++;
                    }
                }
            }
        }

        // Crear
        static void Agregartermino()
        {
            if (contador >= terminos.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El diccionario está lleno.\n");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Ingrese el termino: ");
            string termino = Console.ReadLine();

            Console.Write("Ingrese el significado: ");
            string significado = Console.ReadLine();

            terminos[contador] = termino;
            significados[contador] = significado;
            contador++;

            Guardararchivo(); //Es un auto guardado
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Termino agregado correctamente.\n");
        }

        // Leer
        static void Mostrarterminos()
        {
            if (contador == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay terminos registrados.\n");
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n--- TERMINOS REGISTRADOS ---");
            for (int i = 0; i < contador; i++)
            {
                Console.WriteLine($"{i + 1}. {terminos[i]}: {significados[i]}");
            }
            Console.WriteLine();
        }

        // Buscar
        static void Buscartermino()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Ingrese el termino a buscar: ");
            string buscar = Console.ReadLine();

            for (int i = 0; i < contador; i++)
            {
                if (terminos[i].Equals(buscar, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Significado: {significados[i]}\n");
                    return;
                }
            }
            Console.WriteLine("Termino no encontrado.\n");
        }

        // Actualizar
        static void Actualizartermino()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Ingrese el termino que desea actualizar: ");
            string buscar = Console.ReadLine();

            for (int i = 0; i < contador; i++)
            {
                if (terminos[i].Equals(buscar, StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Nuevo termino: ");
                    terminos[i] = Console.ReadLine();

                    Console.Write("Nuevo significado: ");
                    significados[i] = Console.ReadLine();

                    Guardararchivo(); //Auto guardado
                    Console.WriteLine("Termino actualizado correctamente.\n");
                    return;
                }
            }
            Console.WriteLine("Termino no encontrado.\n");
        }

        // Eliminar
        static void Eliminartermino()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Ingrese el termino que desea eliminar: ");
            string buscar = Console.ReadLine();

            for (int i = 0; i < contador; i++)
            {
                if (terminos[i].Equals(buscar, StringComparison.OrdinalIgnoreCase))
                {
                    for (int j = i; j < contador - 1; j++)
                    {
                        terminos[j] = terminos[j + 1];
                        significados[j] = significados[j + 1];
                    }
                    contador--;

                    Guardararchivo(); //Otro auto guardado 
                    Console.WriteLine("Termino eliminado correctamente.\n");
                    return;
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Termino no encontrado.\n");
        }
    }
}
