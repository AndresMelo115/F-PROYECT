using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELECCIONES.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ELECCIONES.Helper;

namespace ELECCIONES.Controllers
{
    public class HomeController : Controller
    {
        private readonly Ciudadanos _ciudadanos;
        private readonly EleccionesContext _context;
        //private readonly PuestoElecto context;
        
        public HomeController(EleccionesContext context)
        {
            this._context = context;
        }  

        public IActionResult Resultado()
        {
            return View("ProcesoVotacion","Resultadoes");
        }


        public IActionResult Votacion()
        {
            //Ciudadanos ciudadanos = new Ciudadanos();

            ViewBag.NombreSession = HttpContext.Session.GetString(Configuracion.KeyNombre);
            ViewBag.ApellidoSession = HttpContext.Session.GetString(Configuracion.KeyApellido);

            return View(_context.PuestoElecto.ToList());
        }       


        [HttpGet]
        public IActionResult Index()
        {
           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(Ciudadanos _ciudadanos)
      {
            //Ciudadanos ciudadanos = new Ciudadanos();
            


            EleccionesContext _context = new EleccionesContext();

            var test1 =  _context.Ciudadanos.Where(ced => ced.Cedula == _ciudadanos.Cedula).
                FirstOrDefault();            

            if(test1 == null)
            {
                ModelState.AddModelError("","Ciudadano no existe en el padron");
                return View();
            }

            if (test1.Estado == false)
            {

                ModelState.AddModelError("","Ciudadano inactivo");
                return View(_ciudadanos);
            }
            HttpContext.Session.SetInt32(Configuracion.KeyCedudala, test1.IdCiudadanos);
            HttpContext.Session.SetString(Configuracion.KeyNombre,test1.Nombre);
            HttpContext.Session.SetString(Configuracion.KeyApellido,test1.Apellido);
            return RedirectToAction("votacion");

        }
        public async Task <IActionResult> ProcesoVotacion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test2 = _context.PuestoElecto.Where(Pues => Pues.IdPuestoE == id).
               FirstOrDefault();

            var Candidatos = _context.Candidatos
                .Where(cand => cand.PuestoAspira == id)
                .Include(c => c.PartidoPerteneceNavigation)
                .Include(c => c.PuestoAspiraNavigation);

            ViewBag.Puestoelegido = test2.Nombre;



            if (Candidatos == null)
            {
                return NotFound();
            }
            return View(await Candidatos.ToListAsync());

        }

        //[HttpGet]
        //public IActionResult Votar(int? id)
        //{
        //    return View();
        //}

        
        public async Task<IActionResult> Votar( int? id)
        {
            Resultado _resultado = new Resultado();
            var Test3 = _context.Elecciones.Where(ele => ele.Estado == true).
               FirstOrDefault(); 
            

            if (ModelState.IsValid)
            {
                _resultado.IdCandidatos = id;
                _resultado.IdCiudadanos = HttpContext.Session.GetInt32(Configuracion.KeyCedudala);
                _resultado.IdElecciones =Test3.IdElecciones;

                _context.Add(_resultado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Votacion));
            }
        
            return View("Votacion");
        }



        public IActionResult admin()
        {
            //Ciudadanos cedula = new Ciudadanos();
            return View("Adminpage");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
