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
    public class tblMateriasController : Controller
    {
        private readonly DbContext1 _context;

        public tblMateriasController(DbContext1 context)
        {
            _context = context;
        }

        // GET: tblMaterias
        public async Task<IActionResult> Index()
        {
            var materiasActivas = await _context.tblMaterias
                .Where(m => m.EstadoMateria == true)
                .ToListAsync();

            return View(materiasActivas);
        }

        public async Task<IActionResult> Inactivos()
        {
            var materiasInactivas = await _context.tblMaterias
                .Where(m => m.EstadoMateria == false)
                .ToListAsync();

            return View(materiasInactivas);
        }

        // GET: tblMaterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tblMaterias == null)
            {
                return NotFound();
            }

            var tblMateria = await _context.tblMaterias
                .FirstOrDefaultAsync(m => m.idMateria == id);
            if (tblMateria == null)
            {
                return NotFound();
            }

            return View(tblMateria);
        }

        // GET: tblMaterias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tblMaterias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idMateria,nombreMateria,descripcionMateria,EstadoMateria")] tblMateria tblMateria)
        {
         
                _context.Add(tblMateria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(tblMateria);
        }

        // GET: tblMaterias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tblMaterias == null)
            {
                return NotFound();
            }

            var tblMateria = await _context.tblMaterias.FindAsync(id);
            if (tblMateria == null)
            {
                return NotFound();
            }
            return View(tblMateria);
        }

        // POST: tblMaterias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idMateria,nombreMateria,descripcionMateria,EstadoMateria")] tblMateria tblMateria)
        {
            if (id != tblMateria.idMateria)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(tblMateria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblMateriaExists(tblMateria.idMateria))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(tblMateria);
        }

        // GET: tblMaterias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tblMaterias == null)
            {
                return NotFound();
            }

            var tblMateria = await _context.tblMaterias
                .FirstOrDefaultAsync(m => m.idMateria == id);
            if (tblMateria == null)
            {
                return NotFound();
            }

            return View(tblMateria);
        }

        // POST: tblMaterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tblMaterias == null)
            {
                return Problem("Entity set 'DbContext1.tblMaterias' is null.");
            }

            var tblMateria = await _context.tblMaterias.FindAsync(id);

            if (tblMateria != null)
            {
                tblMateria.EstadoMateria = true; // Cambiar el estado a true en lugar de eliminar
                _context.Entry(tblMateria).State = EntityState.Modified; // Marcar como modificado
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Inactivos));
        }

        private bool tblMateriaExists(int id)
        {
          return (_context.tblMaterias?.Any(e => e.idMateria == id)).GetValueOrDefault();
        }
    }
}
