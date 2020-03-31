using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELECCIONES.Models;
using Microsoft.EntityFrameworkCore;


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


        public IActionResult Votacion()
        {
            

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

            EleccionesContext _context = new EleccionesContext();

            var test1 =  _context.Ciudadanos.Where(ced => ced.Cedula == _ciudadanos.Cedula).
                FirstOrDefault();
            

            if(test1 == null)
            {
                ModelState.AddModelError("","Cidadano no existe");
                return View();
            }

            if (test1.Estado == false)
            {

                ModelState.AddModelError("","Ciudadano inactivo");
                return View(_ciudadanos);
            }

          
           return RedirectToAction("votacion");

        }
        //public PuestoElecto(EleccionesContext context)
        //{
        //    _context2 = context;
        //}

        //// GET: PuestoElectoes
        //public async Task<IActionResult> Index2()
        //{
        //    return View(await _context2.PuestoElecto.ToListAsync());
        //}


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
