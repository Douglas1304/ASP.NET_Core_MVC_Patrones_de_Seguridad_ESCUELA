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
    public class tblCursosController : Controller
    {
        private readonly DbContext1 _context;

        public tblCursosController(DbContext1 context)
        {
            _context = context;
        }

        // GET: tblCursos
        public async Task<IActionResult> Index1()
        {
            var dbContext1 = _context.tblCursos.Include(t => t.Level)
                .Where(t => t.EstadoCurso == true);
            return View(await dbContext1.ToListAsync());
        }
        public async Task<IActionResult> Inactivos()
        {
            var dbContext1 = _context.tblCursos.Include(t => t.Level)
                .Where(t => t.EstadoCurso == false);
            return View(await dbContext1.ToListAsync());
        }

        // GET: tblCursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tblCursos == null)
            {
                return NotFound();
            }

            var tblCurso = await _context.tblCursos
                .Include(t => t.Level)
                .FirstOrDefaultAsync(m => m.idCurso == id);
            if (tblCurso == null)
            {
                return NotFound();
            }

            return View(tblCurso);
        }

        // GET: tblCursos/Create
        public IActionResult Create()
        {
            ViewData["LevelId"] = new SelectList(_context.tblNiveles, "idNivel", "nombreNivel");
            return View();
        }

        // POST: tblCursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idCurso,nombreCurso,LevelId,EstadoCurso")] tblCurso tblCurso)
        {

            _context.Add(tblCurso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index1));

            ViewData["LevelId"] = new SelectList(_context.tblNiveles, "idNivel", "nombreNivel", tblCurso.LevelId);
            return View(tblCurso);
        }

        // GET: tblCursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tblCursos == null)
            {
                return NotFound();
            }

            var tblCurso = await _context.tblCursos.FindAsync(id);
            if (tblCurso == null)
            {
                return NotFound();
            }
            ViewData["LevelId"] = new SelectList(_context.tblNiveles, "idNivel", "nombreNivel", tblCurso.LevelId);
            return View(tblCurso);
        }

        // POST: tblCursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idCurso,nombreCurso,LevelId,EstadoCurso")] tblCurso tblCurso)
        {
            if (id != tblCurso.idCurso)
            {
                return NotFound();
            }


            try
            {
                _context.Update(tblCurso);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCursoExists(tblCurso.idCurso))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index1));

            ViewData["LevelId"] = new SelectList(_context.tblNiveles, "idNivel", "nombreNivel", tblCurso.LevelId);
            return View(tblCurso);
        }

        // GET: tblCursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tblCursos == null)
            {
                return NotFound();
            }

            var tblCurso = await _context.tblCursos
                .Include(t => t.Level)
                .FirstOrDefaultAsync(m => m.idCurso == id);
            if (tblCurso == null)
            {
                return NotFound();
            }

            return View(tblCurso);
        }

        // POST: tblCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = _context.tblCursos.FirstOrDefault(c => c.idCurso == id);
            if(curso == null)
            {
                return NotFound();
            }
            curso.EstadoCurso = true;
            _context.tblCursos.Update(curso);
            _context.SaveChanges();
            return RedirectToAction(nameof(Inactivos));
        }

        private bool tblCursoExists(int id)
        {
            return (_context.tblCursos?.Any(e => e.idCurso == id)).GetValueOrDefault();
        }
    }
}
