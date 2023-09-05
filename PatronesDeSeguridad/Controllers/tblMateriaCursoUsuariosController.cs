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
    public class tblMateriaCursoUsuariosController : Controller
    {
        private readonly DbContext1 _context;

        public tblMateriaCursoUsuariosController(DbContext1 context)
        {
            _context = context;
        }

        // GET: tblMateriaCursoUsuarios
        public IActionResult Index()
        {
            var asignacionesActivas = _context.tblMateriasCursosUsuarios
                .Include(t => t.Curso).ThenInclude(c => c.Level)
                .Include(t => t.Materia)
                .Include(t => t.Profesor)
                .Where(t => t.EstadoAsignacion)  // Filtrar por EstadoAsignacion igual a true
                .ToList();

            foreach (var asignacion in asignacionesActivas)
            {
                asignacion.Profesor.IdentificationNumber = $"{asignacion.Profesor.FirstName} {asignacion.Profesor.LastName1} {asignacion.Profesor.LastName2}";
            }

            return View(asignacionesActivas);
        }

        public IActionResult Inactivos()
        {
            var asignacionesInactivas = _context.tblMateriasCursosUsuarios
                .Include(t => t.Curso).ThenInclude(c => c.Level)
                .Include(t => t.Materia)
                .Include(t => t.Profesor)
                .Where(t => !t.EstadoAsignacion)  // Filtrar por EstadoAsignacion igual a false (inactivos)
                .ToList();

            foreach (var asignacion in asignacionesInactivas)
            {
                asignacion.Profesor.IdentificationNumber = $"{asignacion.Profesor.FirstName} {asignacion.Profesor.LastName1} {asignacion.Profesor.LastName2}";
            }

            return View(asignacionesInactivas);
        }

        // GET: tblMateriaCursoUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tblMateriasCursosUsuarios == null)
            {
                return NotFound();
            }

            var tblMateriaCursoUsuario = await _context.tblMateriasCursosUsuarios
                .Include(t => t.Curso)
                    .ThenInclude(c => c.Level) // Include the Level of the Curso
                .Include(t => t.Materia)
                .Include(t => t.Profesor)
                .FirstOrDefaultAsync(m => m.idMateriaCursoUsuario == id);

            if (tblMateriaCursoUsuario == null)
            {
                return NotFound();
            }

            // Set the display name for Profesor
            tblMateriaCursoUsuario.Profesor.IdentificationNumber = $"{tblMateriaCursoUsuario.Profesor.FirstName} {tblMateriaCursoUsuario.Profesor.LastName1} {tblMateriaCursoUsuario.Profesor.LastName2}";

            return View(tblMateriaCursoUsuario);
        }

        // GET: tblMateriaCursoUsuarios/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.tblCursos, "idCurso", "nombreCurso");
            ViewData["MateriaId"] = new SelectList(_context.tblMaterias, "idMateria", "nombreMateria");

            // Modificar la obtención de profesores para incluir su nombre completo
            var profesoresSelectList = _context.tblProfesores
                .Where(p => p.EstadoProfesor)
                .Select(p => new SelectListItem
                {
                    Value = p.IdentificationNumber,
                    Text = $"{p.FirstName} {p.LastName1} {p.LastName2}"
                })
                .ToList();

            ViewData["ProfesorId"] = new SelectList(profesoresSelectList, "Value", "Text");

            return View();
        }

        // POST: tblMateriaCursoUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idMateriaCursoUsuario,identificadorAsignacion,CursoId,MateriaId,ProfesorId,EstadoAsignacion")] tblMateriaCursoUsuario tblMateriaCursoUsuario)
        {
            
                // Check if identificadorAsignacion already exists
                if (_context.tblMateriasCursosUsuarios.Any(m => m.identificadorAsignacion == tblMateriaCursoUsuario.identificadorAsignacion))
                {
                    ModelState.AddModelError("identificadorAsignacion", "El identificador de asignación ya está en uso.");
                }
                else
                {
                    _context.Add(tblMateriaCursoUsuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            

            ViewData["CursoId"] = new SelectList(_context.tblCursos, "idCurso", "nombreCurso", tblMateriaCursoUsuario.CursoId);
            ViewData["MateriaId"] = new SelectList(_context.tblMaterias, "idMateria", "nombreMateria", tblMateriaCursoUsuario.MateriaId);
            ViewData["ProfesorId"] = new SelectList(_context.tblProfesores, "IdentificationNumber", "IdentificationNumber", tblMateriaCursoUsuario.ProfesorId);
            return View(tblMateriaCursoUsuario);
        }


        // GET: tblMateriaCursoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tblMateriasCursosUsuarios == null)
            {
                return NotFound();
            }

            var tblMateriaCursoUsuario = await _context.tblMateriasCursosUsuarios.FindAsync(id);
            if (tblMateriaCursoUsuario == null)
            {
                return NotFound();
            }
            // Modificar la obtención de profesores para incluir su nombre completo
            var profesoresSelectList = _context.tblProfesores
                .Where(p => p.EstadoProfesor)
                .Select(p => new SelectListItem
                {
                    Value = p.IdentificationNumber,
                    Text = $"{p.FirstName} {p.LastName1} {p.LastName2}"
                })
                .ToList();

            ViewData["ProfesorId"] = new SelectList(profesoresSelectList, "Value", "Text",tblMateriaCursoUsuario.ProfesorId);
            ViewData["CursoId"] = new SelectList(_context.tblCursos, "idCurso", "nombreCurso", tblMateriaCursoUsuario.CursoId);
            ViewData["MateriaId"] = new SelectList(_context.tblMaterias, "idMateria", "nombreMateria", tblMateriaCursoUsuario.MateriaId);
            return View(tblMateriaCursoUsuario);
        }

        // POST: tblMateriaCursoUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idMateriaCursoUsuario,identificadorAsignacion,CursoId,MateriaId,ProfesorId,EstadoAsignacion")] tblMateriaCursoUsuario tblMateriaCursoUsuario)
        {
            if (id != tblMateriaCursoUsuario.idMateriaCursoUsuario)
            {
                return NotFound();
            }


            try
            {
                _context.Update(tblMateriaCursoUsuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblMateriaCursoUsuarioExists(tblMateriaCursoUsuario.idMateriaCursoUsuario))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["CursoId"] = new SelectList(_context.tblCursos, "idCurso", "nombreCurso", tblMateriaCursoUsuario.CursoId);
            ViewData["MateriaId"] = new SelectList(_context.tblMaterias, "idMateria", "nombreMateria", tblMateriaCursoUsuario.MateriaId);
            ViewData["ProfesorId"] = new SelectList(_context.tblProfesores, "IdentificationNumber", "IdentificationNumber", tblMateriaCursoUsuario.ProfesorId);
            return View(tblMateriaCursoUsuario);
        }

        // GET: tblMateriaCursoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tblMateriasCursosUsuarios == null)
            {
                return NotFound();
            }

            var tblMateriaCursoUsuario = await _context.tblMateriasCursosUsuarios
                .Include(t => t.Curso)
                    .ThenInclude(c => c.Level) // Include the Level of the Curso
                .Include(t => t.Materia)
                .Include(t => t.Profesor)
                .FirstOrDefaultAsync(m => m.idMateriaCursoUsuario == id);

            if (tblMateriaCursoUsuario == null)
            {
                return NotFound();
            }

            // Set the display name for Profesor
            tblMateriaCursoUsuario.Profesor.IdentificationNumber = $"{tblMateriaCursoUsuario.Profesor.FirstName} {tblMateriaCursoUsuario.Profesor.LastName1} {tblMateriaCursoUsuario.Profesor.LastName2}";

            return View(tblMateriaCursoUsuario);
        }


        // POST: tblMateriaCursoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // Acción para confirmar el borrado
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMateriaCursoUsuario = await _context.tblMateriasCursosUsuarios.FindAsync(id);

            if (tblMateriaCursoUsuario == null)
            {
                return NotFound();
            }

            tblMateriaCursoUsuario.EstadoAsignacion = true; // Cambia el estado a true
            _context.Update(tblMateriaCursoUsuario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Acción para activar
        //public async Task<IActionResult> Activar(int id)
        //{
        //    var tblMateriaCursoUsuario = await _context.tblMateriasCursosUsuarios.FindAsync(id);

        //    if (tblMateriaCursoUsuario == null)
        //    {
        //        return NotFound();
        //    }

        //    tblMateriaCursoUsuario.EstadoAsignacion = true; // Cambia el estado a true
        //    _context.Update(tblMateriaCursoUsuario);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}
        private bool tblMateriaCursoUsuarioExists(int id)
        {
            return (_context.tblMateriasCursosUsuarios?.Any(e => e.idMateriaCursoUsuario == id)).GetValueOrDefault();
        }
        [HttpGet]
        public IActionResult ObtenerDescripcion(int MateriaId)
        {
            // Obtener el PrecioValue desde la base de datos basado en el servicioId
            var descriptionMateria = _context.tblMaterias
                .Where(s => s.idMateria == MateriaId)
                .Select(s => s.descripcionMateria)
                .FirstOrDefault();

            return Json(new { descriptionMateria });
        }
        public IActionResult ObtenerNivelCurso(int CursoId)
        {
            // Obtener el nombre del nivel del curso basado en el CursoId
            var nivelCurso = _context.tblCursos
                .Where(c => c.idCurso == CursoId)
                .Select(c => c.Level.nombreNivel)
                .FirstOrDefault();

            return Json(new { nivelCurso });
        }
    }
}
