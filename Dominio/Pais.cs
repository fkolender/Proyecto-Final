using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Pais: IValidable
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; }
        public string Nombre { get; set; }
        public string Alpha3 { get; set; }

        public Pais()
        {

        }

        public Pais(string nombre, string alpha3)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Alpha3 = alpha3;
        }

        public void Validar()
        {   
            if (Nombre == "")
            {
                throw new Exception("Nombre no puede estar vacío");
            }
            if (Alpha3.Length < 3 || Alpha3.Length > 3)
            {
                throw new Exception("Código Alpha3 debe ser de 3 caracteres.");
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
