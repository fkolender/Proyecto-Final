using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebAppMVC.Controllers
{
    public class PeriodistaController : Controller
    {
        Sistema s = Sistema.Instancia;
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("LogueadoRol") != null)
            {
                List<Partido> partidos = s.GetPartidos();
                return View(partidos);
            }
            return RedirectToAction("Error", "Home");
            }

        public IActionResult Reseñas()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "PER")
            {
                Periodista periodista = s.GetPeriodista(HttpContext.Session.GetInt32("LogueadoId"));
                List<Reseña> reseñas = periodista.GetReseñas();
                return View(reseñas);
            }
            return RedirectToAction("Error", "Home");
            }

        public IActionResult CreateReseña()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "PER")
            {
                return View();
            }
            return RedirectToAction("Error", "Home");
            }

        [HttpPost]
        public IActionResult CreateReseña(Reseña r, int pID) 
        {
            try
            {
                    s.LlenarDatosReseña(r, pID, HttpContext.Session.GetString("LogueadoMail"));
                    s.AltaReseña(r);
                    ViewBag.msg = "Reseña dada de alta con éxito.";
                }
                catch (Exception e)
                {
                    ViewBag.msg = e.Message;
                }
                return View();
            }
        }
    }

