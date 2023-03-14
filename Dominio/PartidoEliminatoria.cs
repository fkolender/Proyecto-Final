using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class PartidoEliminatoria : Partido
    {
        public bool Alargue { get; set; }
        public bool Penales { get; set; }
        public Etapa Etapa { get; set; }

        public PartidoEliminatoria()
        {

        }

        public override string GetTipo()
        {
            return "E";
        }

        public override void AltaIncidencia(Incidencia i)
        {
            try
            {
                i.Validar();
                if (!Seleccion1.GetJugadores().Contains(i.Jugador) && !Seleccion2.GetJugadores().Contains(i.Jugador))
                {
                    throw new Exception("No se ha encontrado al jugador en ninguna de las 2 selecciones.");
                }
                if (i.Minuto == -1)
                {
                    if (Penales)
                    {
                        if (!Finalizado)
                        {
                            GetIncidencias().Add(i);
                        }

                    }
                    else
                    {
                        throw new Exception("No se puede dar de alta una incidencia en -1 si no hubo penales en el partido.");
                    }
                }
                else if (i.Minuto > 90)
                {
                    if (Alargue)
                    {
                        if (!Finalizado)
                        {
                            GetIncidencias().Add(i);
                        }
                    }
                    else
                    {
                        throw new Exception("No se puede dar de alta una incidencia en minutos mayores a 90 si no hubo alargue.");
                    }
                }
                else
                {
                    if (!Finalizado)
                    {
                        GetIncidencias().Add(i);
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PartidoEliminatoria(bool alargue, bool penales, Etapa etapa, Seleccion seleccion1, Seleccion seleccion2, DateTime fecha, bool finalizado, string resultado, List<Incidencia> incidencias) : base(seleccion1, seleccion2, fecha, finalizado, resultado, incidencias)
        {
            Alargue = alargue;
            Penales = penales;
            Etapa = etapa;
        }

        public override string Resultado()
        {
            int contadorGolesSeleccion1 = 0;
            int contadorGolesSeleccion2 = 0;
            foreach (Incidencia i in GetIncidencias())
            {
                if (i.Jugador.Pais.Nombre == Seleccion1.Pais.Nombre && (int)i.Tipo_Incidencia == 2)
                {
                    contadorGolesSeleccion1++;
                }
                else if (i.Jugador.Pais.Nombre == Seleccion2.Pais.Nombre && (int)i.Tipo_Incidencia == 2)
                {
                    contadorGolesSeleccion2++;
                }
            }

            if (contadorGolesSeleccion1 > contadorGolesSeleccion2)
            {
                if (Penales)
                {
                    ResultadoPartido = "Empate en tiempo de juego. Ganador: [" + Seleccion1.Pais.Nombre + "] en tanda de penales";

                }
                else
                {

                    ResultadoPartido = "Ganador: [" + Seleccion1.Pais.Nombre + "]";
                }

            }
            else
            {
                if (Penales)
                {
                    ResultadoPartido = "Empate en tiempo de juego. Ganador: [" + Seleccion2.Pais.Nombre + "] en tanda de penales";
                }
                else
                {
                    ResultadoPartido = "Ganador: [" + Seleccion2.Pais.Nombre + "]";
                }
            }

            Finalizado = true;
            return ResultadoPartido;
        }
    }
}
