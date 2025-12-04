using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalPractica
{
    internal class Program
    {
        // Definición de la clase Combo
        class Combo
        {
            public int id;
            public string nombre;
            public double precio;
        }

        // Definición de la clase Reserva
        class Reserva
        {
            public string nombreEstudiante;
            public int idCombo;
            public string nombreCombo;
            public double precioCombo;
        }

        // Arrays y variables globales
        static Combo[] combos = new Combo[3];
        static Reserva[,] reservas = new Reserva[2, 20]; // 2 turnos, 20 reservas máximo
        static int[] contadorReservas = new int[2]; // Contador de reservas por turno
        static string[] turnos = { "Mañana", "Tarde" };

        static void DefinirCombos()
        {
            combos[0] = new Combo { id = 1, nombre = "Café + Pan", precio = 5.50 };
            combos[1] = new Combo { id = 2, nombre = "Jugo + Sándwich", precio = 7.00 };
            combos[2] = new Combo { id = 3, nombre = "Té + Pasteles", precio = 6.50 };
        }

        static void MostrarCombos()
        {
            Console.WriteLine("\n========== MENÚ DE COMBOS ==========");
            for (int i = 0; i < combos.Length; i++)
            {
                Console.WriteLine($"{combos[i].id}. {combos[i].nombre} - S/. {combos[i].precio}");
            }
            Console.WriteLine("===================================\n");
        }

        static void MostrarTurnos()
        {
            Console.WriteLine("\n========== SELECCIONAR TURNO ==========");
            for (int i = 0; i < turnos.Length; i++)
            {
                Console.WriteLine($"{i}. {turnos[i]} ({contadorReservas[i]}/20 reservas)");
            }
            Console.WriteLine("======================================\n");
        }

        static void RegistrarReserva()
        {
            Console.Clear();
            Console.WriteLine("========== REGISTRAR RESERVA ==========");

            // Mostrar combos
            MostrarCombos();

            // Seleccionar combo
            Console.Write("Seleccione el ID del combo: ");
            int idCombo = int.Parse(Console.ReadLine());

            if (idCombo < 1 || idCombo > combos.Length)
            {
                Console.WriteLine("Combo inválido");
                return;
            }

            // Mostrar turnos
            MostrarTurnos();

            // Seleccionar turno
            Console.Write("Seleccione el turno (0=Mañana, 1=Tarde): ");
            int turno = int.Parse(Console.ReadLine());

            if (turno < 0 || turno > 1)
            {
                Console.WriteLine("Turno inválido");
                return;
            }

            // Validar cupo
            if (contadorReservas[turno] >= 20)
            {
                Console.WriteLine("No hay cupo disponible en este turno");
                return;
            }

            // Datos del estudiante
            Console.Write("Nombre del estudiante: ");
            string nombreEstudiante = Console.ReadLine();

            // Crear reserva
            Combo comboSeleccionado = combos[idCombo - 1];
            Reserva nuevaReserva = new Reserva
            {
                nombreEstudiante = nombreEstudiante,
                idCombo = comboSeleccionado.id,
                nombreCombo = comboSeleccionado.nombre,
                precioCombo = comboSeleccionado.precio
            };

            // Agregar a la matriz
            reservas[turno, contadorReservas[turno]] = nuevaReserva;
            contadorReservas[turno]++;

            Console.WriteLine($"\n✓ Reserva registrada exitosamente para {turnos[turno]}");
        }

        static void CancelarReserva()
        {
            Console.Clear();
            Console.WriteLine("========== CANCELAR RESERVA ==========");

            MostrarTurnos();

            Console.Write("Seleccione el turno (0=Mañana, 1=Tarde): ");
            int turno = int.Parse(Console.ReadLine());

            if (turno < 0 || turno > 1)
            {
                Console.WriteLine("Turno inválido");
                return;
            }

            if (contadorReservas[turno] == 0)
            {
                Console.WriteLine("No hay reservas en este turno");
                return;
            }

            Console.Write("Nombre del estudiante a cancelar: ");
            string nombreBuscar = Console.ReadLine();

            bool encontrado = false;
            for (int i = 0; i < contadorReservas[turno]; i++)
            {
                if (reservas[turno, i].nombreEstudiante.ToLower() == nombreBuscar.ToLower())
                {
                    // Desplazar reservas
                    for (int j = i; j < contadorReservas[turno] - 1; j++)
                    {
                        reservas[turno, j] = reservas[turno, j + 1];
                    }
                    contadorReservas[turno]--;
                    encontrado = true;
                    Console.WriteLine($"\n✓ Reserva de {nombreBuscar} cancelada exitosamente");
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine($"No se encontró reserva para {nombreBuscar}");
            }
        }

        static void ListarReservas()
        {
            Console.Clear();
            Console.WriteLine("========== LISTAR RESERVAS ==========\n");

            bool hayReservas = false;

            for (int t = 0; t < turnos.Length; t++)
            {
                Console.WriteLine($"\n--- TURNO {turnos[t].ToUpper()} ({contadorReservas[t]}/20) ---");

                if (contadorReservas[t] == 0)
                {
                    Console.WriteLine("Sin reservas");
                    continue;
                }

                hayReservas = true;
                for (int i = 0; i < contadorReservas[t]; i++)
                {
                    Console.WriteLine($"{i + 1}. {reservas[t, i].nombreEstudiante} - {reservas[t, i].nombreCombo} (S/. {reservas[t, i].precioCombo})");
                }
            }

            if (!hayReservas)
            {
                Console.WriteLine("\nNo hay reservas registradas");
            }
        }

        static double CalcularIngresosTurno(int turno)
        {
            double total = 0;
            for (int i = 0; i < contadorReservas[turno]; i++)
            {
                total += reservas[turno, i].precioCombo;
            }
            return total;
        }

        static double CalcularIngresosTotal()
        {
            double total = 0;
            for (int t = 0; t < turnos.Length; t++)
            {
                total += CalcularIngresosTurno(t);
            }
            return total;
        }

        static void VerReportes()
        {
            Console.Clear();
            Console.WriteLine("========== REPORTES ==========\n");

            Console.WriteLine("INGRESOS POR TURNO:");
            for (int t = 0; t < turnos.Length; t++)
            {
                double ingresos = CalcularIngresosTurno(t);
                Console.WriteLine($"{turnos[t]}: S/. {ingresos:F2} ({contadorReservas[t]} reservas)");
            }

            double ingresosTotal = CalcularIngresosTotal();
            Console.WriteLine($"\nINGRESO TOTAL: S/. {ingresosTotal:F2}");

            // Combo más vendido
            int[] ventasCombo = new int[3];
            for (int t = 0; t < turnos.Length; t++)
            {
                for (int i = 0; i < contadorReservas[t]; i++)
                {
                    ventasCombo[reservas[t, i].idCombo - 1]++;
                }
            }

            Console.WriteLine("\nCOMBOS MÁS VENDIDOS:");
            for (int i = 0; i < combos.Length; i++)
            {
                Console.WriteLine($"{combos[i].nombre}: {ventasCombo[i]} ventas");
            }
        }

        static void BuscarReserva()
        {
            Console.Clear();
            Console.WriteLine("========== BUSCAR RESERVA ==========");

            Console.Write("Nombre del estudiante: ");
            string nombreBuscar = Console.ReadLine();

            bool encontrado = false;

            for (int t = 0; t < turnos.Length; t++)
            {
                for (int i = 0; i < contadorReservas[t]; i++)
                {
                    if (reservas[t, i].nombreEstudiante.ToLower() == nombreBuscar.ToLower())
                    {
                        Console.WriteLine($"\n✓ Reserva encontrada:");
                        Console.WriteLine($"Estudiante: {reservas[t, i].nombreEstudiante}");
                        Console.WriteLine($"Turno: {turnos[t]}");
                        Console.WriteLine($"Combo: {reservas[t, i].nombreCombo}");
                        Console.WriteLine($"Precio: S/. {reservas[t, i].precioCombo}");
                        encontrado = true;
                        break;
                    }
                }
                if (encontrado) break;
            }

            if (!encontrado)
            {
                Console.WriteLine($"No se encontró reserva para {nombreBuscar}");
            }
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("  SISTEMA DE RESERVAS - CAFETERÍA UNI  ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar Reserva");
            Console.WriteLine("2. Cancelar Reserva");
            Console.WriteLine("3. Listar Reservas");
            Console.WriteLine("4. Ver Reportes");
            Console.WriteLine("5. Buscar Reserva");
            Console.WriteLine("6. Salir");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");
        }

        static void Main(string[] args)
        {
            DefinirCombos();
            int opcion = 0;
            bool continuar = true;

            while (continuar)
            {
                MostrarMenu();
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarReserva();
                        break;
                    case 2:
                        CancelarReserva();
                        break;
                    case 3:
                        ListarReservas();
                        break;
                    case 4:
                        VerReportes();
                        break;
                    case 5:
                        BuscarReserva();
                        break;
                    case 6:
                        continuar = false;
                        Console.WriteLine("\n¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }

                if (continuar && opcion != 6)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}