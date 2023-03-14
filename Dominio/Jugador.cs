using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Jugador: IValidable, IComparable<Jugador>
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Camiseta { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public double Altura { get; set; }
        public string Pie { get; set; }
        public double ValorMercado { get; set; }
        public string Moneda { get; set; }
        public Pais Pais { get; set; }
        public string Puesto { get; set; }
        public static double valorReferenciaJugadores { get; set; } = 25000000;

        public Jugador()
        {

        }
        public int CompareTo(Jugador other) //Creo que el sort está bien así. Con la precarga nos fijamos y cualquier cosa es cambiar un número nomás.
        {
            if (ValorMercado.CompareTo(other.ValorMercado) > 0)
            {
                return -1;
            }
            else if (ValorMercado.CompareTo(other.ValorMercado) < 0)
            {
                return 1;
            }
            else // En caso que el ValorMercado sea igual en ambos Jugadores, se entra en el else ya que no se contempla (ValorMercado.CompareTo(other.ValorMercado) == 0).
            {
                if (NombreCompleto.CompareTo(other.NombreCompleto) > 0)
                {
                    return 1;
                }
                else if (NombreCompleto.CompareTo(other.NombreCompleto) < 0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

        }

        public override string ToString()
        {
            return "Id: " + Id + " Nombre completo: " + NombreCompleto + " Camiseta: " + Camiseta + " Fecha de nacimiento: " + Fecha_Nacimiento + " Altura: " + Altura + " Pie hábil: " + Pie + " Valor de mercado: " + ValorMercado + " Pais: " + Pais.Nombre + " Puesto: " + Puesto;
        }

        public Jugador(int id, string camiseta, string nombreCompleto, DateTime fecha_Nacimiento, double altura, string pie, double valorMercado, string moneda, Pais pais, string puesto)
        {
            Id = id;
            Camiseta = camiseta;
            NombreCompleto = nombreCompleto;
            Fecha_Nacimiento = fecha_Nacimiento;
            Altura = altura;
            Pie = pie;
            ValorMercado = valorMercado;
            Moneda = moneda;
            Pais = pais;
            Puesto = puesto;
        }

        public void Validar()
        {
            if (String.IsNullOrEmpty(NombreCompleto) || String.IsNullOrEmpty(Camiseta) || Fecha_Nacimiento == null || Altura < 0 || String.IsNullOrEmpty(Pie) || Pais == null || String.IsNullOrEmpty(Puesto) || String.IsNullOrEmpty(Moneda))
            {
                throw new Exception("No pueden haber datos vacíos.");
            }
            if (ValorMercado < 0)
            {
                throw new Exception("El valor de mercado debe ser un número mayor a cero.");
            }
        }

        public string CategoriaFinanciera()
        {
            string categoria;
            if (ValorMercado >= Jugador.valorReferenciaJugadores)
            {
                categoria = "VIP";
            } else { 
                categoria = "Estandar";
            }
            return categoria;
        }

        public override bool Equals(object obj)
        {
            return obj is Jugador jugador &&
                   Id == jugador.Id &&
                   NombreCompleto == jugador.NombreCompleto &&
                   Camiseta == jugador.Camiseta &&
                   Fecha_Nacimiento == jugador.Fecha_Nacimiento &&
                   Altura == jugador.Altura &&
                   Pie == jugador.Pie &&
                   ValorMercado == jugador.ValorMercado &&
                   Moneda == jugador.Moneda &&
                   EqualityComparer<Pais>.Default.Equals(Pais, jugador.Pais) &&
                   Puesto == jugador.Puesto;
        }
    }
}
