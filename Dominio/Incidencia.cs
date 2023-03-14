using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Incidencia : IValidable
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; }
        public int Minuto { get; set; }
        public Tipo_Incidencia Tipo_Incidencia { get; set; }
        public Jugador Jugador { get; set; }

        public void Validar()
        {
            if (Jugador == null)
            {
                throw new Exception("Jugador no puede ser null.");
            }
            if (Minuto < -1 || Minuto >= 120) 
            {
                // Se toma en cuenta el -1 para tiempo de penales.
                throw new Exception("Minuto debe de estar comprendido entre -1 y 120");
            }
        }

        public Incidencia()
        {

        }

        public Incidencia(int minuto, Tipo_Incidencia tipo_Incidencia, Jugador jugador)
        {
            Id = UltimoId;
            UltimoId++;
            Minuto = minuto;
            Tipo_Incidencia = tipo_Incidencia;
            Jugador = jugador;
        }
    }
}
