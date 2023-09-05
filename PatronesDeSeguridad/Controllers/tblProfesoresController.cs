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
    public class tblProfesoresController : Controller
    {
        private readonly DbContext1 _context;

        public tblProfesoresController(DbContext1 context)
        {
            _context = context;
        }

        // GET: tblProfesores

        // Mostrar profesores activos
        public async Task<IActionResult> Index()
        {
            var profesoresActivos = await _context.tblProfesores
                .Where(p => p.EstadoProfesor == true)
                .ToListAsync();

            return View(profesoresActivos);
        }

        // Mostrar profesores inactivos
        public async Task<IActionResult> Inactivos()
        {
            var profesoresInactivos = await _context.tblProfesores
                .Where(p => p.EstadoProfesor == false)
                .ToListAsync();

            return View(profesoresInactivos);
        }

        // GET: tblProfesores/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.tblProfesores == null)
            {
                return NotFound();
            }

            var tblProfesor = await _context.tblProfesores
                .FirstOrDefaultAsync(m => m.IdentificationNumber == id);
            if (tblProfesor == null)
            {
                return NotFound();
            }

            return View(tblProfesor);
        }

        // GET: tblProfesores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tblProfesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdentificationNumber,FirstName,LastName1,LastName2,Phone,Token,EstadoProfesor")] tblProfesor tblProfesor)
        {
         
                // Check if IdentificationNumber already exists
                if (_context.tblProfesores.Any(p => p.IdentificationNumber == tblProfesor.IdentificationNumber))
                {
                    ModelState.AddModelError("IdentificationNumber", "El número de identificación ya está en uso.");
                }
                else
                {
                    _context.Add(tblProfesor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            

            return View(tblProfesor);
        }


        // GET: tblProfesores/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.tblProfesores == null)
            {
                return NotFound();
            }

            var tblProfesor = await _context.tblProfesores.FindAsync(id);
            if (tblProfesor == null)
            {
                return NotFound();
            }
            return View(tblProfesor);
        }

        // POST: tblProfesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdentificationNumber,FirstName,LastName1,LastName2,Phone,Token,EstadoProfesor")] tblProfesor tblProfesor)
        {
            if (id != tblProfesor.IdentificationNumber)
            {
                return NotFound();
            }

         
                try
                {
                    _context.Update(tblProfesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblProfesorExists(tblProfesor.IdentificationNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(tblProfesor);
        }

        // GET: tblProfesores/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.tblProfesores == null)
            {
                return NotFound();
            }

            var tblProfesor = await _context.tblProfesores
                .FirstOrDefaultAsync(m => m.IdentificationNumber == id);
            if (tblProfesor == null)
            {
                return NotFound();
            }

            return View(tblProfesor);
        }

        // POST: tblProfesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.tblProfesores == null)
            {
                return Problem("Entity set 'DbContext1.tblProfesores' is null.");
            }

            var tblProfesor = await _context.tblProfesores.FindAsync(id);

            if (tblProfesor != null)
            {
                // Cambiar el estado del profesor a activo (true)
                tblProfesor.EstadoProfesor = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Inactivos));
        }

        private bool tblProfesorExists(string id)
        {
          return (_context.tblProfesores?.Any(e => e.IdentificationNumber == id)).GetValueOrDefault();
        }
    }
}
