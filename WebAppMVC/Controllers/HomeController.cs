using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAppMVC.Models;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        Sistema s = Sistema.Instancia;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("LogueadoId") == null)
            {
                List<Seleccion> selecciones = s.GetSelecciones();
                return View(selecciones);
            }
            return RedirectToAction("Error");
        }

        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") == null)
            {
                return View();
            }
            return RedirectToAction("Error");
            }

        public IActionResult Registro()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") == null)
            {
                return View();
            }
            return RedirectToAction("Error");
            }

        [HttpPost]
        public IActionResult Registro(Periodista p)
        {
            try
            {
                s.AltaPeriodista(p);
                ViewBag.msg = "Se ha registrado correctamente.";
            }
            catch (Exception e)
            {
                ViewBag.msg = "Error: " + e.Message;
            }

            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") == null)
            {
                return View();
            }
            return RedirectToAction("Error");
            }

        [HttpPost]
        public IActionResult Login(string Mail, string Password)
        {
            Usuario usuarioLogueado = s.BuscarUsuario(Mail, Password);

            if (usuarioLogueado != null)//Si trae un usuario, guarda en sessions los datos de este.
            {
                    HttpContext.Session.SetString("LogueadoNombre", usuarioLogueado.Nombre);
                    HttpContext.Session.SetInt32("LogueadoId", usuarioLogueado.Id);
                    HttpContext.Session.SetString("LogueadoRol", usuarioLogueado.Rol);
                    HttpContext.Session.SetString("LogueadoMail", usuarioLogueado.Mail);
                    HttpContext.Session.SetString("LogueadoPassword", usuarioLogueado.Password);

                if(HttpContext.Session.GetString("LogueadoRol") == "OPE")//Dependiendo del tipo de usuario lo redirecciona a las páginas de cada uno, en vez de un redirect genérico que podría llevar a la página de error dependiendo del caso.
                {
                    return RedirectToAction("Index", "Operador");
                }
                else //Es periodista
                {
                    return RedirectToAction("Index", "Periodista");
                }
            }
            else
            {
                ViewBag.msg = "Datos incorrectos";
                return View();
            }
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Error");
            }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
