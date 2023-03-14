using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebAppMVC.Controllers
{
    public class OperadorController : Controller
    {
        Sistema s = Sistema.Instancia;

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "OPE")
            {
                List<Seleccion> selecciones = s.GetSelecciones();
                return View(selecciones);
            }
            return RedirectToAction("Error");
            }

        public IActionResult Partidos() 
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "OPE")
            {
                List<Partido> partidos = s.GetPartidos();
                return View(partidos);
            }
            return RedirectToAction("Error");
            }

        public IActionResult Reseñas(int id)
        {
            List<Reseña> reseñas = s.GetPeriodistaPorId(id).GetReseñas();
            return View(reseñas);
        }

        public IActionResult Periodistas()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "OPE")
            {
                List<Periodista> listaPeriodistas = s.GetPeriodistas();
                return View(listaPeriodistas);
            }
            return RedirectToAction("Error");
        }

        public IActionResult EntreFechas()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "OPE")
            {
                return View();
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public IActionResult EntreFechas(DateTime fecha1, DateTime fecha2)
        {
            if(fecha1 > fecha2) //las da vuelta si el usuario las ingresa al revés
            {
                DateTime auxFecha = fecha1;
                fecha1 = fecha2;
                fecha2 = auxFecha;
            }

            List <Partido> aux = s.PartidosEntreFecha(fecha1, fecha2);

            return View(aux);
        }

        public IActionResult PartidosRoja()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "OPE")
            {
                return View();
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public IActionResult PartidosRoja(string mail)
        {
            List<Partido> lista = s.PartidosRojaSegunPeriodista(mail);
            return View(lista); 
        }

        public IActionResult MasGoles()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "OPE")
            {
                List<Seleccion> selecciones = s.SeleccionConMasGoles();
                return View(selecciones);
            }
            return RedirectToAction("Error");
        }

        public IActionResult DetallesPartido(int id)
        {
            s.GetPartidoPorID(id).Resultado();
            return View(s.GetPartidoPorIDConCasteo(id));
        }
    }
}
