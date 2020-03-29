using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELECCIONES.Models;
using Microsoft.AspNetCore.Authorization;
using ELECCIONES.LDTO;

namespace ELECCIONES.Controllers
{
    [Authorize]
    public class CiudadanosController : Controller
    {
        private readonly EleccionesContext _context;


     

        public CiudadanosController(EleccionesContext context)
        {
            _context = context;
        }

        // GET: Ciudadanos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ciudadanos.ToListAsync());
        }

        // GET: Ciudadanos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudadanos = await _context.Ciudadanos
                .FirstOrDefaultAsync(m => m.IdCiudadanos == id);
            if (ciudadanos == null)
            {
                return NotFound();
            }

            return View(ciudadanos);
        }

        // GET: Ciudadanos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ciudadanos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCiudadanos,Cedula,Nombre,Apellido,Email,Estado")] Ciudadanos ciudadanos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciudadanos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciudadanos);
        }

        // GET: Ciudadanos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudadanos = await _context.Ciudadanos.FindAsync(id);
            if (ciudadanos == null)
            {
                return NotFound();
            }
            return View(ciudadanos);
        }

        // POST: Ciudadanos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCiudadanos,Cedula,Nombre,Apellido,Email,Estado")] Ciudadanos ciudadanos)
        {
            if (id != ciudadanos.IdCiudadanos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciudadanos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadanosExists(ciudadanos.IdCiudadanos))
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
            return View(ciudadanos);
        }

        // GET: Ciudadanos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudadanos = await _context.Ciudadanos
                .FirstOrDefaultAsync(m => m.IdCiudadanos == id);
            if (ciudadanos == null)
            {
                return NotFound();
            }

            return View(ciudadanos);
        }

        // POST: Ciudadanos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciudadanos = await _context.Ciudadanos.FindAsync(id);
            _context.Ciudadanos.Remove(ciudadanos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiudadanosExists(int id)
        {
            return _context.Ciudadanos.Any(e => e.IdCiudadanos == id);
        }
    }
}
