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
    [Authorize]

    public class CandidatosController : Controller
    {

        private readonly EleccionesContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IMapper _mapper;

        public CandidatosController(EleccionesContext context, IHostingEnvironment hostingEnvironment, IMapper mapper)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this._mapper = mapper;
            
        }

        // GET: Candidatos
        public async Task<IActionResult> Index()
        {
            var eleccionesContext = _context.Candidatos.Include(c => c.PartidoPerteneceNavigation).Include(c => c.PuestoAspiraNavigation);
            return View(await eleccionesContext.ToListAsync());
        }

        // GET: Candidatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatos = await _context.Candidatos
                .Include(c => c.PartidoPerteneceNavigation)
                .Include(c => c.PuestoAspiraNavigation)
                .FirstOrDefaultAsync(m => m.IdCandidatos == id);
            if (candidatos == null)
            {
                return NotFound();
            }

            return View(candidatos);
        }

        // GET: Candidatos/Create
        public IActionResult Create()
        {
            ViewData["PartidoPertenece"] = new SelectList(_context.Partidos, "IdPartidos", "Descripcion");
            ViewData["PuestoAspira"] = new SelectList(_context.PuestoElecto, "IdPuestoE", "Descripcion");
            return View();
        }

        // POST: Candidatos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("IdCandidatos,Nombre,Apellido,PartidoPertenece,PuestoAspira,FotoPerfil,Estado")] Candidatos candidatos,*/CandidatosLDTO model)
        {
            var candidato = new Candidatos();
            if (ModelState.IsValid)
            {

                string uniqueName = null;
                if (model.Foto != null)
                {
                    var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueName = Guid.NewGuid().ToString() + "_" + model.Foto.FileName;
                    var filePath = Path.Combine(folderPath, uniqueName);


                    if (filePath != null) model.Foto.CopyTo(new FileStream(filePath, mode: FileMode.Create));
                }

                candidato = _mapper.Map<Candidatos>(model);

                candidato.FotoPerfil = uniqueName;

                _context.Add(candidato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            /* ViewData["PartidoPertenece"] = new SelectList(_context.Partidos, "IdPartidos", "Descripcion", candidatos.PartidoPertenece);
             ViewData["PuestoAspira"] = new SelectList(_context.PuestoElecto, "IdPuestoE", "Descripcion", candidatos.PuestoAspira);*/
            return View(model);
        }

        // GET: Candidatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatos = await _context.Candidatos.FindAsync(id);
            if (candidatos == null)
            {
                return NotFound();
            }
            var candidatoLDTO = _mapper.Map<CandidatosLDTO>(candidatos);

            ViewData["PartidoPertenece"] = new SelectList(_context.Partidos, "IdPartidos", "Descripcion", candidatos.PartidoPertenece);
            ViewData["PuestoAspira"] = new SelectList(_context.PuestoElecto, "IdPuestoE", "Descripcion", candidatos.PuestoAspira);
            return View(candidatoLDTO);
        }

        // POST: Candidatos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CandidatosLDTO lDTO)
        {
            if (id != lDTO.IdCandidatos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var candidato = await _context.Candidatos.FirstOrDefaultAsync(d => d.IdCandidatos == lDTO.IdCandidatos);


                    string uniqueName = null;
                    if (lDTO.Foto != null)
                    {
                        var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueName = Guid.NewGuid().ToString() + "_" + lDTO.Foto.FileName;
                        var filePath = Path.Combine(folderPath, uniqueName);




                        if (!string.IsNullOrEmpty(candidato.FotoPerfil))
                        {
                            var filePathEliminar = Path.Combine(folderPath, candidato.FotoPerfil);
                            if (System.IO.File.Exists(filePathEliminar))
                            {
                                //var fileInfo = new System.IO.FileInfo(filePathEliminar);
                                //fileInfo.Delete();
                                System.IO.File.Delete(filePathEliminar);
                            }
                        }

                        //el problema qcre que esta en esta linea, la voy a cambiar ok? dale 

                        if(filePath!=null)
                        {
                            using (var fs = new FileStream(filePath, FileMode.Create))
                            {
                                lDTO.Foto.CopyTo(fs);
                            }
                        }

                        //if (filePath != null) lDTO.Foto.CopyTo(new FileStream(filePath, mode: FileMode.Create));
                    }

                    candidato.Nombre = lDTO.Nombre;
                    candidato.Apellido = lDTO.Apellido;
                    candidato.PartidoPertenece = lDTO.PartidoPertenece;
                    candidato.PuestoAspira = lDTO.PuestoAspira;
                    candidato.FotoPerfil = uniqueName;
                    candidato.Estado = lDTO.Estado;




                    _context.Update(candidato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatosExists(lDTO.IdCandidatos))
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
            /*ViewData["PartidoPertenece"] = new SelectList(_context.Partidos, "IdPartidos", "Descripcion", candidatos.PartidoPertenece);
            ViewData["PuestoAspira"] = new SelectList(_context.PuestoElecto, "IdPuestoE", "Descripcion", candidatos.PuestoAspira);*/
            return View(lDTO);
        }

        // GET: Candidatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatos = await _context.Candidatos

                 .Include(c => c.PartidoPerteneceNavigation)
                 .Include(c => c.PuestoAspiraNavigation)
                .FirstOrDefaultAsync(m => m.IdCandidatos == id);
            if (candidatos == null)
            {
                return NotFound();
            }

            return View(candidatos);
        }

        // POST: Candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidatos = await _context.Candidatos.FindAsync(id);
            _context.Candidatos.Remove(candidatos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatosExists(int id)
        {
            return _context.Candidatos.Any(e => e.IdCandidatos == id);
        }
    }
}
