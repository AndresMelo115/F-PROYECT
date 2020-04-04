using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELECCIONES.Models;
using ELECCIONES.Helper;
using Microsoft.AspNetCore.Http;

namespace ELECCIONES.Controllers
{
    public class ResultadoesController : Controller
    {
        private readonly EleccionesContext _context;

        public ResultadoesController(EleccionesContext context)
        {
            _context = context;
        }
       
        // GET: Resultadoes
        public async Task<IActionResult> Index(int id)
        {
            var eleccionesContext = _context.Resultado.Include(r => r.IdCandidatosNavigation).Include(r => r.IdCiudadanosNavigation).Include(r => r.IdEleccionesNavigation);          
            HttpContext.Session.SetInt32(Configuracion.Keyelec, id);
           
            var query = (from re in _context.Resultado
                         join c in _context.Candidatos
                         on re.IdCandidatos equals c.IdCandidatos
                         join pl in _context.PuestoElecto
                         on c.PuestoAspira equals pl.IdPuestoE
                         where re.IdElecciones == id
                         select pl.IdPuestoE
                                                ).Distinct().ToList();
            //ViewBag.F = query;

            List<PuestoElecto> puestoElectos = new List<PuestoElecto>();

            foreach (var item in query)
            {
                puestoElectos.Add(_context.PuestoElecto.Find(item));
            }

            return View(puestoElectos);

        }

        public IActionResult PostResult(int? id)        {              
    
                var idEleccion = HttpContext.Session.GetInt32(Configuracion.Keyelec);

                ViewBag.idEleccion = idEleccion;

                var context = (from re in _context.Resultado
                               join c in _context.Candidatos
                               on re.IdCandidatos equals c.IdCandidatos
                               join pl in _context.PuestoElecto
                               on c.PuestoAspira equals pl.IdPuestoE
                               where re.IdElecciones == idEleccion &&
                               pl.IdPuestoE == id
                               select new Candidatos
                               {
                                   IdCandidatos = c.IdCandidatos,
                                   Nombre = c.Nombre,
                                   Apellido = c.Apellido,
                                   PartidoPerteneceNavigation = c.PartidoPerteneceNavigation,
                                   FotoPerfil = c.FotoPerfil,

                               }
                               ).Distinct();

                var listCandidatosFull = (from re in _context.Resultado
                                          join c in _context.Candidatos
                                          on re.IdCandidatos equals c.IdCandidatos
                                          join pl in _context.PuestoElecto
                                          on c.PuestoAspira equals pl.IdPuestoE
                                          where re.IdElecciones == idEleccion &&
                                          pl.IdPuestoE == id
                                          select new Candidatos
                                          {
                                              IdCandidatos = c.IdCandidatos,
                                              Nombre = c.Nombre,
                                              Apellido = c.Apellido,
                                              PartidoPerteneceNavigation = c.PartidoPerteneceNavigation,
                                              FotoPerfil = c.FotoPerfil,

                                          }
                                         ).ToList();

                ViewBag.List = listCandidatosFull;


                return View(context);              


        }

        //public async Task<IActionResult> unload(int? id)                          XXXXX EN CONSTRUCCION XXXX
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var test2 = _context.PuestoElecto.Where(Pues => Pues.IdPuestoE == id).
        //       FirstOrDefault();

        //    var Result = _context.Resultado
        //        .Where(res => res.IdCandidatosNavigation.PuestoAspira == id)
        //      .Include(r => r.IdCandidatosNavigation)
        //      .Include(r => r.IdCiudadanosNavigation)
        //      .Include(r => r.IdEleccionesNavigation);

        //    ViewBag.Puestoelegido = test2.Nombre;

           

        //    if (Result == null)
        //    {
        //        return NotFound();
        //    }




        //    return View(await Result.ToListAsync());

        //}
        // GET: Resultadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultado
                .Include(r => r.IdCandidatosNavigation)
                .Include(r => r.IdCiudadanosNavigation)
                .Include(r => r.IdEleccionesNavigation)
                .FirstOrDefaultAsync(m => m.IdResultado == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // GET: Resultadoes/Create
        public IActionResult Create()
        {
            ViewData["IdCandidatos"] = new SelectList(_context.Candidatos, "IdCandidatos", "Apellido");
            ViewData["IdCiudadanos"] = new SelectList(_context.Ciudadanos, "IdCiudadanos", "Apellido");
            ViewData["IdElecciones"] = new SelectList(_context.Elecciones, "IdElecciones", "Nombre");
            return View();
        }

        // POST: Resultadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdResultado,IdElecciones,IdCandidatos,IdCiudadanos")] Resultado resultado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidatos"] = new SelectList(_context.Candidatos, "IdCandidatos", "Apellido", resultado.IdCandidatos);
            ViewData["IdCiudadanos"] = new SelectList(_context.Ciudadanos, "IdCiudadanos", "Apellido", resultado.IdCiudadanos);
            ViewData["IdElecciones"] = new SelectList(_context.Elecciones, "IdElecciones", "Nombre", resultado.IdElecciones);
            return View(resultado);
        }

        // GET: Resultadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultado.FindAsync(id);
            if (resultado == null)
            {
                return NotFound();
            }
            ViewData["IdCandidatos"] = new SelectList(_context.Candidatos, "IdCandidatos", "Apellido", resultado.IdCandidatos);
            ViewData["IdCiudadanos"] = new SelectList(_context.Ciudadanos, "IdCiudadanos", "Apellido", resultado.IdCiudadanos);
            ViewData["IdElecciones"] = new SelectList(_context.Elecciones, "IdElecciones", "Nombre", resultado.IdElecciones);
            return View(resultado);
        }

        // POST: Resultadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdResultado,IdElecciones,IdCandidatos,IdCiudadanos")] Resultado resultado)
        {
            if (id != resultado.IdResultado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultadoExists(resultado.IdResultado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidatos"] = new SelectList(_context.Candidatos, "IdCandidatos", "Apellido", resultado.IdCandidatos);
            ViewData["IdCiudadanos"] = new SelectList(_context.Ciudadanos, "IdCiudadanos", "Apellido", resultado.IdCiudadanos);
            ViewData["IdElecciones"] = new SelectList(_context.Elecciones, "IdElecciones", "Nombre", resultado.IdElecciones);
            return View(resultado);
        }

        // GET: Resultadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultado
                .Include(r => r.IdCandidatosNavigation)
                .Include(r => r.IdCiudadanosNavigation)
                .Include(r => r.IdEleccionesNavigation)
                .FirstOrDefaultAsync(m => m.IdResultado == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // POST: Resultadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _context.Resultado.FindAsync(id);
            _context.Resultado.Remove(resultado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultadoExists(int id)
        {
            return _context.Resultado.Any(e => e.IdResultado == id);
        }
    }
}
