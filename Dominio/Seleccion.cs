using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Seleccion: IValidable, IComparable<Seleccion>
    {
        private List<Jugador> Jugadores { get; set; }
        public Pais Pais { get; set; }

        public Seleccion()
        {

        }
        public Seleccion(Pais pais)
        {
            Jugadores = new List<Jugador>();
            Pais = pais;
        }

        public void Validar()
        {
            if (Pais.Nombre == "" || Pais.Alpha3 == "")
            {
                throw new Exception("El pais no puede tener atributos vacíos.");
            }
            if(Jugadores.Count < 11)
            {
                throw new Exception("La selección debe tener un mínimo de 11 jugadores.");
            }
        }

        public void AgregarJugador(Jugador j)
        {
            try
            {
                j.Validar();
                if (!Jugadores.Contains(j))
                {
                    Jugadores.Add(j);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public List<Jugador> GetJugadores()
        {
            return Jugadores;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public int CompareTo(Seleccion other)
        {
            return Pais.Nombre.CompareTo(other.Pais.Nombre);
        }

        public override bool Equals(object obj)
        {
            return obj is Seleccion seleccion &&
                   EqualityComparer<List<Jugador>>.Default.Equals(Jugadores, seleccion.Jugadores) &&
                   EqualityComparer<Pais>.Default.Equals(Pais, seleccion.Pais);
        }
    }
}
