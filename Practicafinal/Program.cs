using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalPractica
{
    internal class Program
    {
        static void MostrarMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("  SISTEMA DE RESERVAS - CAFETERÍA UNI  ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar Reserva");
            Console.WriteLine("2. Cancelar Reserva");
            Console.WriteLine("3. Listar Reservas");
            Console.WriteLine("4. Ver Reportes");
            Console.WriteLine("5. Salir");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");
        }

        static void Main(string[] args)
        {
            MostrarMenu();
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Opción: Registrar Reserva");
                    break;
                case 2:
                    Console.WriteLine("Opción: Cancelar Reserva");
                    break;
                case 3:
                    Console.WriteLine("Opción: Listar Reservas");
                    break;
                case 4:
                    Console.WriteLine("Opción: Ver Reportes");
                    break;
                case 5:
                    Console.WriteLine("¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
        }
    }
}