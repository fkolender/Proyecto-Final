using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Dominio
{
    public abstract class Partido : IValidable
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; }
        public Seleccion Seleccion1 { get; set; }
        public Seleccion Seleccion2 { get; set; }
        public DateTime Fecha { get; set; }
        public bool Finalizado { get; set; }
        public string ResultadoPartido { get; set; }
        private List<Incidencia> Incidencias { get; set; }

        public Partido()
        {

        }

        public override string ToString()
        {
            return "Id: " + Id + " Partido: " + Seleccion1.Pais.Nombre + " contra " + Seleccion2.Pais.Nombre + " Fecha: " + Fecha + " Finalizado: " + Finalizado + " Resultado: " + ResultadoPartido;
        }

        public Partido(Seleccion seleccion1, Seleccion seleccion2, DateTime fecha, bool finalizado, string resultado, List<Incidencia> incidencias)
        {
            Id = UltimoId;
            UltimoId++;
            Seleccion1 = seleccion1;
            Seleccion2 = seleccion2;
            Fecha = fecha;
            Finalizado = finalizado;
            ResultadoPartido = resultado;
            Incidencias = incidencias;
        }


        public void Validar()
        {
            if (Seleccion1 == null || Seleccion2 == null)
            {
                throw new Exception("Seleccion1 y/o Seleccion2 no pueden ser null.");
            }

            if (Fecha == null || Fecha < new DateTime(2022, 11, 20) || Fecha > new DateTime(2022, 12, 18))
            {
                throw new Exception("La fecha no puede ser null, ni menor a 2022/11/20 o mayor a 2022/12/18");
            }
            
        }

        public abstract string Resultado(); // Cada subclase de Partido (grupos o eliminatoria) calcula el resultado a su manera.

        public virtual void AltaIncidencia(Incidencia i)
        {
            try
            {
                i.Validar();
                if (!Seleccion1.GetJugadores().Contains(i.Jugador) && !Seleccion2.GetJugadores().Contains(i.Jugador))
                {
                    throw new Exception("No se ha encontrado al jugador en ninguna de las 2 selecciones.");
                }
                if (!Finalizado)
                {
                    Incidencias.Add(i);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public abstract string GetTipo(); //Metodo que devuelve string con el rol (subclase), G para grupo y E para eliminatoria. Se hace abstracto acá porque no hay partidos que no sean de Grupo ni de Eliminatoria.

        public List<Incidencia> GetIncidencias()
        {
            return Incidencias;
        }

        public List<Incidencia> GetIncidenciasSeleccion(string seleccion, int tipo) //Devuelve todas las incidencias de un tipo que realizó una selección en el partido.
        {
            List<Incidencia> aux = new List<Incidencia>();
            if (seleccion == Seleccion1.Pais.Nombre || seleccion == Seleccion2.Pais.Nombre)
            {
                foreach(Incidencia i in Incidencias)
                {
                    if ((int)i.Tipo_Incidencia == tipo && seleccion == i.Jugador.Pais.Nombre)
                    {
                        aux.Add(i);
                    }
                }
            }
            return aux;
        }
    }
}
