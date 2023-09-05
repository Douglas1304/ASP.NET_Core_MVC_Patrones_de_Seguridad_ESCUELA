using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatronesDeSeguridad.Data;
using PatronesDeSeguridad.Models;

namespace PatronesDeSeguridad.Controllers
{
    public class tblEstudiantesController : Controller
    {
        private readonly DbContext1 _context;

        public tblEstudiantesController(DbContext1 context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dbContext1 = _context.tblEstudiantes.Include(t => t.Gender).Where(e => e.EstadoEstudiante == true);
            return View(await dbContext1.ToListAsync());
        }

        // GET: tblEstudiantes/Inactivos
        public async Task<IActionResult> Inactivos()
        {
            var inactivos = await _context.tblEstudiantes.Include(t => t.Gender).Where(e => e.EstadoEstudiante == false).ToListAsync();
            return View(inactivos);
        }
        // GET: tblEstudiantes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.tblEstudiantes == null)
            {
                return NotFound();
            }

            var tblEstudiante = await _context.tblEstudiantes
                .Include(t => t.Gender)
                .FirstOrDefaultAsync(m => m.IdentificationNumber == id);
            if (tblEstudiante == null)
            {
                return NotFound();
            }

            return View(tblEstudiante);
        }

        // GET: tblEstudiantes/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.tblGeneros, "idGenero", "nombreGenero");
            return View();
        }

        // POST: tblEstudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdentificationNumber,FirstName,LastName1,LastName2,GenderId,FechaNacimiento,Phone,direccion,EstadoEstudiante")] tblEstudiante tblEstudiante)
        {
                // Check if IdentificationNumber already exists
                if (_context.tblEstudiantes.Any(e => e.IdentificationNumber == tblEstudiante.IdentificationNumber))
                {
                    ModelState.AddModelError("IdentificationNumber", "El número de identificación ya está en uso.");
                }
                else
                {
                    _context.Add(tblEstudiante);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            

            ViewData["GenderId"] = new SelectList(_context.tblGeneros, "idGenero", "nombreGenero", tblEstudiante.GenderId);
            return View(tblEstudiante);
        }

        // GET: tblEstudiantes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.tblEstudiantes == null)
            {
                return NotFound();
            }

            var tblEstudiante = await _context.tblEstudiantes.FindAsync(id);
            if (tblEstudiante == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.tblGeneros, "idGenero", "nombreGenero", tblEstudiante.GenderId);
            return View(tblEstudiante);
        }

        // POST: tblEstudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdentificationNumber,FirstName,LastName1,LastName2,GenderId,FechaNacimiento,Phone,direccion,EstadoEstudiante")] tblEstudiante tblEstudiante)
        {
            if (id != tblEstudiante.IdentificationNumber)
            {
                return NotFound();
            }


            try
            {
                _context.Update(tblEstudiante);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblEstudianteExists(tblEstudiante.IdentificationNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["GenderId"] = new SelectList(_context.tblGeneros, "idGenero", "nombreGenero", tblEstudiante.GenderId);
            return View(tblEstudiante);
        }

        // GET: tblEstudiantes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.tblEstudiantes == null)
            {
                return NotFound();
            }

            var tblEstudiante = await _context.tblEstudiantes
                .Include(t => t.Gender)
                .FirstOrDefaultAsync(m => m.IdentificationNumber == id);
            if (tblEstudiante == null)
            {
                return NotFound();
            }

            return View(tblEstudiante);
        }

        // POST: tblEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var estudiante = _context.tblEstudiantes.FirstOrDefault(c => c.IdentificationNumber == id);

            if (estudiante == null)
            {
                return NotFound();
            }
            estudiante.EstadoEstudiante = true;
            _context.tblEstudiantes.Update(estudiante);
            _context.SaveChanges();
            return RedirectToAction(nameof(Inactivos));
        }

        private bool tblEstudianteExists(string id)
        {
            return (_context.tblEstudiantes?.Any(e => e.IdentificationNumber == id)).GetValueOrDefault();
        }
    }
}
