using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELECCIONES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using ELECCIONES.LDTO;
using AutoMapper;

namespace ELECCIONES.Controllers
{
    public class PartidosController : Controller
    {
        private readonly EleccionesContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IMapper _mapper;

        public PartidosController(EleccionesContext context, IHostingEnvironment hostingEnvironment, IMapper mapper)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this._mapper = mapper;
        }

        // GET: Partidos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Partidos.ToListAsync());
        }

        // GET: Partidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partidos = await _context.Partidos
                .FirstOrDefaultAsync(m => m.IdPartidos == id);
            if (partidos == null)
            {
                return NotFound();
            }

            return View(partidos);
        }

        // GET: Partidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("IdPartidos,Nombre,Descripcion,LogoPartido,Estado")] Partidos partidos,*/PartidosLDTO model)
        {
            var partido = new Partidos();
            if (ModelState.IsValid)
            {
                string uniqueName = null;
                if(model.Logo != null)
                {
                    var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueName = Guid.NewGuid().ToString() + "_" + model.Logo.FileName;
                    var filePath = Path.Combine(folderPath, uniqueName);

                    if (filePath != null) model.Logo.CopyTo(new FileStream(filePath, mode: FileMode.Create));
                }

                partido = _mapper.Map<Partidos>(model);

                partido.LogoPartido = uniqueName;

                _context.Add(partido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        // GET: Partidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partidos = await _context.Partidos.FindAsync(id);
            if (partidos == null)
            {
                return NotFound();
            }
            var PartidosLDTO = _mapper.Map<PartidosLDTO>(partidos);
            return View(PartidosLDTO);
        }

        // POST: Partidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,/* [Bind("IdPartidos,Nombre,Descripcion,LogoPartido,Estado")] Partidos partidos*/PartidosLDTO lDTO)
        {
            if (id != lDTO.IdPartidos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var partido = await _context.Partidos.FirstOrDefaultAsync(d => d.IdPartidos == lDTO.IdPartidos);


                    string uniqueName = null;
                    if (lDTO.Logo != null)
                    {
                        var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueName = Guid.NewGuid().ToString() + "_" + lDTO.Logo.FileName;
                        var filePath = Path.Combine(folderPath, uniqueName);




                        if (!string.IsNullOrEmpty(partido.LogoPartido))
                        {
                            var filePathEliminar = Path.Combine(folderPath, partido.LogoPartido);
                            if (System.IO.File.Exists(filePathEliminar))
                            {
                                var fileInfo = new System.IO.FileInfo(filePathEliminar);
                                fileInfo.Delete();
                            }
                        }


                        if (filePath != null) lDTO.Logo.CopyTo(new FileStream(filePath, mode: FileMode.Create));
                    }

                    partido.Nombre = lDTO.Nombre;
                    partido.Descripcion = lDTO.Descripcion;
                    partido.LogoPartido = uniqueName;
                    partido.Estado = lDTO.Estado;



                    _context.Update(partido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartidosExists(lDTO.IdPartidos))
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
            return View(lDTO);
        }

        // GET: Partidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partidos = await _context.Partidos
                .FirstOrDefaultAsync(m => m.IdPartidos == id);
            if (partidos == null)
            {
                return NotFound();
            }

            return View(partidos);
        }

        // POST: Partidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partidos = await _context.Partidos.FindAsync(id);
            _context.Partidos.Remove(partidos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartidosExists(int id)
        {
            return _context.Partidos.Any(e => e.IdPartidos == id);
        }
    }
}
