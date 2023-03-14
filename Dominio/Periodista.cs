using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Periodista : Usuario, IValidable, IComparable<Periodista>
    {
        private List<Reseña> Reseñas { get; set; }

        public Periodista()
        {
            Id = UltimoId;
            UltimoId++;
            Rol = "PER";
            Reseñas = new List<Reseña>();
        }
        public Periodista(string nombre, string apellido, string mail, string password)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Password = password;
            Rol = "PER";
            Reseñas = new List<Reseña>();
        }

        public override string ToString()
        {
            return "Id: " + Id + " Nombre: " + Nombre + " Apellido: " + Apellido + " Mail: " + Mail + " Password: " + Password;
        }

        public void AgregarReseña(DateTime fecha, Partido partido, string titulo, string contenido)
        {
            Reseña r = new Reseña(this, fecha, partido, titulo, contenido);
            try
            {
                r.Validar();
                Reseñas.Add(r);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public List<Reseña> GetReseñas()
        {
            return Reseñas;
        }

        public int CompareTo(Periodista other) //REVISAR!
        {
            if(Apellido.CompareTo(other.Apellido) == 1) { return 1; }
            else if (Apellido.CompareTo(other.Apellido) == -1) { return -1; }
            else
            {
                if (Nombre.CompareTo(other.Nombre) == 1) { return 1; }
                else if (Nombre.CompareTo(other.Nombre) == -1) { return -1; }
                else
                {
                    return 0;
                }
            }
        }

        public void Validar()
        {
            if (Nombre == "" || Apellido == "" || Mail == "" || Password == "")
            {
                throw new Exception("No se pueden introducir campos vacíos.");
            }
            if (!Mail.Contains('@'))
            {
                throw new Exception("El mail debe de contener un @.");
            }
            if (Mail.Substring(0, 1) == "@" || Mail[Mail.Length-1] == '@')
            {
                throw new Exception("El mail no puede comenzar o terminar con @.");
            }
            if (Password.Length < 8)
            {
                throw new Exception("La password debe de tener al menos 8 caracteres.");
            }
        }

        
    }
}
