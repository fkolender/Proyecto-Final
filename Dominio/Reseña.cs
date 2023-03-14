using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Reseña: IValidable, IComparable<Reseña>
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; }
        public Periodista Periodista { get; set; }
        public DateTime Fecha { get; set; }
        public Partido Partido { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }

        public Reseña()
        {

        }
        public Reseña(Periodista periodista, DateTime fecha, Partido partido, string titulo, string contenido)
        {
            Id = UltimoId;
            UltimoId++;
            Periodista = periodista;
            Fecha = fecha;
            Partido = partido;
            Titulo = titulo;
            Contenido = contenido;
        }

        public void Validar()
        {
            if (Periodista == null || Fecha == null || Partido == null || Titulo == "" || Contenido == "")
            {
                throw new Exception("Ningún atributo puede estar vacío.");
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public int CompareTo(Reseña other)
        {
            if(Fecha.CompareTo(other.Fecha) == 1) { return -1; }
            else if(Fecha.CompareTo(other.Fecha) == -1) { return 1; }
            else { return 0; }
        }
    }
}
