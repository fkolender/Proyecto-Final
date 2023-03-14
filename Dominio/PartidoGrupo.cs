using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class PartidoGrupo : Partido
    {
        public char Grupo { get; set; }

        public PartidoGrupo()
        {

        }

        public PartidoGrupo(char grupo, Seleccion seleccion1, Seleccion seleccion2, DateTime fecha, bool finalizado, string resultado, List<Incidencia> incidencias) : base(seleccion1, seleccion2, fecha, finalizado, resultado, incidencias)
        {
            Grupo = grupo;
        }

        public override string GetTipo()
        {
            return "G";
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
                ResultadoPartido = "Ganador: [" + Seleccion1.Pais.Nombre + "]";
            }
            else if (contadorGolesSeleccion1 < contadorGolesSeleccion2)
            {
                ResultadoPartido = "Ganador: [" + Seleccion2.Pais.Nombre + "]";
            }
            else
            {
                ResultadoPartido = "Empate";
            }
            Finalizado = true;
            return ResultadoPartido;
        }
    }
}
