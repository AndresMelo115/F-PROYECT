using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELECCIONES.Models;
using Microsoft.AspNetCore.Authorization;

namespace ELECCIONES.Controllers
{   
    
    public class EleccionesController : Controller
    {
        private readonly EleccionesContext _context;

        public EleccionesController(EleccionesContext context)
        {
            _context = context;
        }

        // GET: Elecciones
        public async Task<IActionResult> Index()
        {
            var eleccionesContext = _context.Elecciones.Include(e => e.IdCandidatosNavigation).Include(e => e.IdCiudadanosNavigation);
            return View(await eleccionesContext.ToListAsync());
        }

        // GET: Elecciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elecciones = await _context.Elecciones
                .Include(e => e.IdCandidatosNavigation)
                .Include(e => e.IdCiudadanosNavigation)
                .FirstOrDefaultAsync(m => m.IdElecciones == id);
            if (elecciones == null)
            {
                return NotFound();
            }

            return View(elecciones);
        }

        // GET: Elecciones/Create
        public IActionResult Create()
        {
            ViewData["IdCandidatos"] = new SelectList(_context.Candidatos, "IdCandidatos", "Apellido");
            ViewData["IdCiudadanos"] = new SelectList(_context.Ciudadanos, "IdCiudadanos", "Apellido");
            return View();
        }

        // POST: Elecciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdElecciones,Nombre,FechaRealizacion,Estado,IdCandidatos,IdCiudadanos")] Elecciones elecciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(elecciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidatos"] = new SelectList(_context.Candidatos, "IdCandidatos", "Apellido", elecciones.IdCandidatos);
            ViewData["IdCiudadanos"] = new SelectList(_context.Ciudadanos, "IdCiudadanos", "Apellido", elecciones.IdCiudadanos);
            return View(elecciones);
        }

        // GET: Elecciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elecciones = await _context.Elecciones.FindAsync(id);
            if (elecciones == null)
            {
                return NotFound();
            }
            ViewData["IdCandidatos"] = new SelectList(_context.Candidatos, "IdCandidatos", "Apellido", elecciones.IdCandidatos);
            ViewData["IdCiudadanos"] = new SelectList(_context.Ciudadanos, "IdCiudadanos", "Apellido", elecciones.IdCiudadanos);
            return View(elecciones);
        }

        // POST: Elecciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdElecciones,Nombre,FechaRealizacion,Estado,IdCandidatos,IdCiudadanos")] Elecciones elecciones)
        {
            if (id != elecciones.IdElecciones)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elecciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EleccionesExists(elecciones.IdElecciones))
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
            ViewData["IdCandidatos"] = new SelectList(_context.Candidatos, "IdCandidatos", "Apellido", elecciones.IdCandidatos);
            ViewData["IdCiudadanos"] = new SelectList(_context.Ciudadanos, "IdCiudadanos", "Apellido", elecciones.IdCiudadanos);
            return View(elecciones);
        }

        // GET: Elecciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elecciones = await _context.Elecciones
                .Include(e => e.IdCandidatosNavigation)
                .Include(e => e.IdCiudadanosNavigation)
                .FirstOrDefaultAsync(m => m.IdElecciones == id);
            if (elecciones == null)
            {
                return NotFound();
            }

            return View(elecciones);
        }

        // POST: Elecciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var elecciones = await _context.Elecciones.FindAsync(id);
            _context.Elecciones.Remove(elecciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EleccionesExists(int id)
        {
            return _context.Elecciones.Any(e => e.IdElecciones == id);
        }
    }
}
