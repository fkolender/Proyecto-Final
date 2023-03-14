using Dominio;
using System;
using System.Collections.Generic;

namespace Interfaz
{
    class Program
    {
        static void Main(string[] args)
        {
            int loop = 1;
            Sistema s = Sistema.Instancia; // Instancia de Sistema.cs controlada por Singleton.
            

            while (loop == 1)
            {
                Console.Clear();
                Console.WriteLine("Bienvenido");
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Dar de alta un periodista.");
                Console.WriteLine("2. Asignar valor de referencia de jugadores.");
                Console.WriteLine("3. Listar partidos de un jugador según su ID.");
                Console.WriteLine("4. Listar jugadores que han sido expulsados al menos una vez, ordenados por valor de mercado y nombre.");
                Console.WriteLine("5. Mostrar el partido con más goles según una selección.");
                Console.WriteLine("6. Listar jugadores que hayan convertido al menos 1 gol en un partido.");
                Console.WriteLine("0. Salir.");

                string input = "";

                while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6" && input != "0")
                {
                    input = Console.ReadLine(); // Mientras lo ingresado no sea una opción válida del menú, se solicitará su ingreso una vez más.
                }

                switch (input)
                {
                    case "1":
                        try
                        {
                            Console.WriteLine("Nombre del periodista: ");
                            string nombre = Console.ReadLine();

                            Console.WriteLine("Apellido del periodista: ");
                            string apellido = Console.ReadLine();

                            Console.WriteLine("Mail del periodista: ");
                            string mail = Console.ReadLine();

                            Console.WriteLine("Contraseña del periodista: ");
                            string password = Console.ReadLine();

                            Dominio.Periodista periodista = new Periodista(nombre, apellido, mail, password);
                            s.AltaPeriodista(periodista);
                            Console.WriteLine("Alta exitosa.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;

                    case "2":
                        try
                        {
                            Console.WriteLine("Ingresar valor de referencia: ");
                            string valorReferencia = Console.ReadLine();
                            double valor = Int32.Parse(valorReferencia);
                            s.ValidarValorReferencia(valor);
                            Console.WriteLine("El valor de referencia ha sido modificado con éxito.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;

                    case "3":
                        Console.WriteLine("Ingresar ID del jugador: ");
                        try
                        {
                            int idJugador = Int32.Parse(Console.ReadLine());
                            s.ValidarIdNegativo(idJugador);
                            List<Partido> listaPartidos = s.ListarPartidosDeJugador(idJugador);

                            foreach (Partido partido in listaPartidos)
                            {
                                Console.WriteLine("Disputado el día: " + partido.Fecha);
                                Console.WriteLine("Disputado entre: " + partido.Seleccion1.Pais.Nombre + " y " + partido.Seleccion2.Pais.Nombre);
                                Console.WriteLine("Cantidad de incidencias: " + partido.GetIncidencias().Count);
                                Console.WriteLine("---------------------------");
                            }
                            if (listaPartidos.Count == 0)
                            {
                                Console.WriteLine("No hay registros de jugadores con ese ID.");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case "4":
                        System.Collections.Generic.List<Jugador> listaExpulsados = s.Expulsados();
                        if (listaExpulsados.Count == 0)
                        {
                            Console.WriteLine("No hay registro de jugadores expulsados.");
                        }
                        else
                        {
                            foreach (Jugador j in listaExpulsados)
                            {
                                Console.WriteLine(j);
                                Console.WriteLine("----");
                            }
                        }
                        break;

                    case "5":
                        Console.WriteLine("Ingresar el país de la selección");
                        string nombreSeleccion = Console.ReadLine();
                        Seleccion seleccionDeseada = null;
                        foreach (Seleccion seleccion in s.GetSelecciones())
                        {
                            if (nombreSeleccion.ToLower() == seleccion.Pais.Nombre.ToLower())
                            {
                                seleccionDeseada = seleccion;
                            }
                        }
                        if (seleccionDeseada == null)
                        {
                            Console.WriteLine("No se encontró la selección ingresada.");
                        }
                        else
                        {
                            List<Partido> p = s.PartidoMasGolesSegunSeleccion(seleccionDeseada);
                            if (p.Count == 0)
                            {
                                Console.WriteLine("No hay registros de partidos para esa selección.");
                            }
                            else
                            {
                                foreach (Partido partido in p)
                                {
                                    Console.WriteLine(partido);
                                    Console.WriteLine("---------------------");
                                }
                            }

                        }
                        break;

                    case "6":
                        System.Collections.Generic.List<Jugador> listaJugadores = s.ListarJugadoresAlMenosUnGol();
                        if (listaJugadores.Count == 0)
                        {
                            Console.WriteLine("No hay registros de jugadores.");
                        }
                        foreach (Jugador j in listaJugadores)
                        {
                            Console.WriteLine(j.NombreCompleto + " - " + j.ValorMercado + " - " + j.CategoriaFinanciera());
                        }
                        break;

                    case "0":
                        Console.WriteLine("Ha salido del programa.");
                        loop = 0;
                        break;

                }

                Console.ReadKey();
            }

        }
    }
}
