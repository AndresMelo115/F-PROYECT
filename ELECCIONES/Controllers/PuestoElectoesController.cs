using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELECCIONES.Models;

namespace ELECCIONES.Controllers
{
    public class PuestoElectoesController : Controller
    {
        private readonly EleccionesContext _context;

        public PuestoElectoesController(EleccionesContext context)
        {
            _context = context;
        }

        // GET: PuestoElectoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PuestoElecto.ToListAsync());
        }

        // GET: PuestoElectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestoElecto = await _context.PuestoElecto
                .FirstOrDefaultAsync(m => m.IdPuestoE == id);
            if (puestoElecto == null)
            {
                return NotFound();
            }

            return View(puestoElecto);
        }

        // GET: PuestoElectoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PuestoElectoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPuestoE,Nombre,Descripcion,Estado")] PuestoElecto puestoElecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puestoElecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(puestoElecto);
        }

        // GET: PuestoElectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestoElecto = await _context.PuestoElecto.FindAsync(id);
            if (puestoElecto == null)
            {
                return NotFound();
            }
            return View(puestoElecto);
        }

        // POST: PuestoElectoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPuestoE,Nombre,Descripcion,Estado")] PuestoElecto puestoElecto)
        {
            if (id != puestoElecto.IdPuestoE)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puestoElecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuestoElectoExists(puestoElecto.IdPuestoE))
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
            return View(puestoElecto);
        }

        // GET: PuestoElectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestoElecto = await _context.PuestoElecto
                .FirstOrDefaultAsync(m => m.IdPuestoE == id);
            if (puestoElecto == null)
            {
                return NotFound();
            }

            return View(puestoElecto);
        }

        // POST: PuestoElectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var puestoElecto = await _context.PuestoElecto.FindAsync(id);
            _context.PuestoElecto.Remove(puestoElecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuestoElectoExists(int id)
        {
            return _context.PuestoElecto.Any(e => e.IdPuestoE == id);
        }
    }
}
