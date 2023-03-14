using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public abstract class Usuario
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        public Usuario()
        {
            Id = UltimoId;
            UltimoId++;
        }

        public Usuario(string rol, string nombre, string apellido, string mail, string password)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Password = password;
            Rol = rol;
        }
    }
}
