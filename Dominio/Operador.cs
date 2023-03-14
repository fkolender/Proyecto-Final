using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Operador : Usuario, IValidable
    {
        public DateTime FechaComienzo { get; set; }

        public Operador()
        {
            Rol = "OPE";
        }

        public Operador(string nombre, string apellido, string mail, string password, DateTime fechaComienzo)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Password = password;
            FechaComienzo = fechaComienzo;
            Rol = "OPE";
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
            if (Mail.Substring(0, 1) == "@" || Mail[Mail.Length - 1] == '@')
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