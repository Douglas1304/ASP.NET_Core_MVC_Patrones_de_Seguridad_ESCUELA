using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatronesDeSeguridad.Data;
using PatronesDeSeguridad.Models;
using Rotativa.AspNetCore;

namespace PatronesDeSeguridad.Controllers
{
    public class tblMatriculaEstudiantesController : Controller
    {
        private readonly DbContext1 _context;

        public tblMatriculaEstudiantesController(DbContext1 context)
        {
            _context = context;
        }

        public async Task<IActionResult> ImprimirTodaslasMatriculas()
        {
            var dbContext1 = _context.tblMatriculaEstudiantes
              .Include(t => t.Asignacion)
                  .ThenInclude(a => a.Curso)
                      .ThenInclude(c => c.Level)
              .Include(t => t.Asignacion)
                  .ThenInclude(a => a.Materia)
              .Include(t => t.Asignacion)
                  .ThenInclude(a => a.Profesor)
              .Include(t => t.Estudiante);

            return new ViewAsPdf("ImprimirTodaslasMatriculas", dbContext1)
            {
                //FileName = $"Cita_{idCita}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        // GET: tblMatriculaEstudiantes
        public async Task<IActionResult> Index()
        {
            var dbContext1 = _context.tblMatriculaEstudiantes
                .Include(t => t.Asignacion)
                    .ThenInclude(a => a.Curso)
                        .ThenInclude(c => c.Level)
                .Include(t => t.Asignacion)
                    .ThenInclude(a => a.Materia)
                .Include(t => t.Asignacion)
                    .ThenInclude(a => a.Profesor)
                .Include(t => t.Estudiante);

            return View(await dbContext1.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> _agregarCalificacion(int id)
        {
            if (id == null || _context.tblMatriculaEstudiantes == null)
            {
                return NotFound();
            }

            var tblMatriculaEstudiante = await _context.tblMatriculaEstudiantes.FindAsync(id);
            if (tblMatriculaEstudiante == null)
            {
                return NotFound();
            }
            ViewData["AsignacionId"] = new SelectList(_context.tblMateriasCursosUsuarios, "idMateriaCursoUsuario", "idMateriaCursoUsuario", tblMatriculaEstudiante.AsignacionId);
            ViewData["EstudianteId"] = new SelectList(_context.tblEstudiantes, "IdentificationNumber", "IdentificationNumber", tblMatriculaEstudiante.EstudianteId);
            return PartialView("_agregarCalificacion", tblMatriculaEstudiante);
        }


        // GET: tblMatriculaEstudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tblMatriculaEstudiantes == null)
            {
                return NotFound();
            }

            var tblMatriculaEstudiante = await _context.tblMatriculaEstudiantes
                .Include(t => t.Asignacion)
                .Include(t => t.Estudiante)
                .FirstOrDefaultAsync(m => m.idMatriculaEstudiante == id);
            if (tblMatriculaEstudiante == null)
            {
                return NotFound();
            }

            return View(tblMatriculaEstudiante);
        }

        //public IActionResult Create()
        //{
        //    var asignaciones = _context.tblMateriasCursosUsuarios.Where(a => a.EstadoAsignacion).ToList();
        //    ViewData["AsignacionId"] = new SelectList(asignaciones, "idMateriaCursoUsuario", "identificadorAsignacion");
        //    ViewData["EstudianteId"] = new SelectList(_context.tblEstudiantes, "IdentificationNumber", "IdentificationNumber");
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult GetAsignacionDetails(int id)
        //{
        //    var asignacion = _context.tblMateriasCursosUsuarios
        //        .Include(a => a.Curso)
        //        .ThenInclude(c => c.Level)
        //        .Include(a => a.Materia)
        //        .Include(a => a.Profesor)
        //        .FirstOrDefault(a => a.idMateriaCursoUsuario == id);

        //    if (asignacion != null)
        //    {
        //        var asignacionDetails = new
        //        {
        //            curso = asignacion.Curso,
        //            nivel = asignacion.Curso.Level,
        //            materia = asignacion.Materia,
        //            profesor = asignacion.Profesor
        //        };

        //        return Json(asignacionDetails);
        //    }

        //    return Json(null);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("idMatriculaEstudiante,EstudianteId,AsignacionId,FechaMatricula,EstadoMatricula")] tblMatriculaEstudiante tblMatriculaEstudiante)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(tblMatriculaEstudiante);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var asignaciones = _context.tblMateriasCursosUsuarios.Where(a => a.EstadoAsignacion).ToList();
        //    ViewData["AsignacionId"] = new SelectList(asignaciones, "idMateriaCursoUsuario", "identificadorAsignacion");
        //    ViewData["EstudianteId"] = new SelectList(_context.tblEstudiantes, "IdentificationNumber", "IdentificationNumber");

        //    return View(tblMatriculaEstudiante);
        //}
        public IActionResult Create()
        {
            var estudiantes = _context.tblEstudiantes.ToList();
            ViewBag.EstudianteId = estudiantes
                .Select(e => new SelectListItem
                {
                    Value = e.IdentificationNumber,
                    Text = $"{e.FirstName} {e.LastName1} {e.LastName2} ({e.IdentificationNumber})"
                })
                .ToList();

            var asignaciones = _context.tblMateriasCursosUsuarios
                .Where(a => a.EstadoAsignacion) // Filtrar solo donde EstadoAsignacion sea true
                .ToList();

            ViewData["AsignacionId"] = new SelectList(asignaciones, "idMateriaCursoUsuario", "identificadorAsignacion");
            return View();
        }


        public IActionResult LoadRelatedData(int id)
        {
            var asignacion = _context.tblMateriasCursosUsuarios
                                   .Include(a => a.Curso)
                                   .Include(a => a.Curso.Level)
                                   .Include(a => a.Materia)
                                   .Include(a => a.Profesor)
                                   .Where(a => a.idMateriaCursoUsuario == id && a.EstadoAsignacion)
                                   .FirstOrDefault();

            if (asignacion == null)
            {
                return NotFound();
            }

            var result = new
            {
                nombreCurso = asignacion.Curso.nombreCurso,
                nombreNivel = asignacion.Curso.Level.nombreNivel,
                nombreMateria = asignacion.Materia.nombreMateria,
                descripcionMateria = asignacion.Materia.descripcionMateria,
                nombreProfesor = $"{asignacion.Profesor.FirstName} {asignacion.Profesor.LastName1} {asignacion.Profesor.LastName2}"
            };

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idMatriculaEstudiante,EstudianteId,AsignacionId,FechaMatricula,EstadoMatricula")] tblMatriculaEstudiante tblMatriculaEstudiante)
        {

            _context.Add(tblMatriculaEstudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            ViewData["AsignacionId"] = new SelectList(_context.tblMateriasCursosUsuarios, "idMateriaCursoUsuario", "idMateriaCursoUsuario", tblMatriculaEstudiante.AsignacionId);
            ViewData["EstudianteId"] = new SelectList(_context.tblEstudiantes, "IdentificationNumber", "IdentificationNumber", tblMatriculaEstudiante.EstudianteId);

            return View(tblMatriculaEstudiante);
        }

        public IActionResult validarEstudiante(int? id)
        {
            if (id == null || _context.tblMatriculaEstudiantes == null)
            {
                return NotFound();
            }

            var tblMatriculaEstudiante = _context.tblMatriculaEstudiantes.Find(id);
            if (tblMatriculaEstudiante == null)
            {
                return NotFound();
            }

            return View(tblMatriculaEstudiante);
        }

        [HttpPost]
        public async Task<IActionResult> ValidarEstudiante(string Token, int idMatriculaEstudiante)
        {
            var matricula = await _context.tblMatriculaEstudiantes
                .Include(m => m.Estudiante)
                .FirstOrDefaultAsync(m => m.idMatriculaEstudiante == idMatriculaEstudiante);

            if (matricula == null || matricula.Estudiante.IdentificationNumber != Token)
            {
                ViewData["TokenErrorMessage"] = "El token ingresado no coincide con el estudiante.";
                return View(nameof(validarEstudiante)); // Reemplaza "TuVista" con el nombre de tu vista
            }

            return RedirectToAction("ImprimirMatricula", new { id = idMatriculaEstudiante });
        }

        public async Task<IActionResult> ImprimirMatricula(int id)
        {
            var tblMatriculaEstudiante = await _context.tblMatriculaEstudiantes
               .Include(t => t.Asignacion)
                   .ThenInclude(a => a.Curso)
                    .ThenInclude(c => c.Level)
               .Include(t => t.Asignacion)
                   .ThenInclude(a => a.Materia)
               .Include(t => t.Asignacion)
                   .ThenInclude(a => a.Profesor)
               .Include(t => t.Estudiante)
               .FirstOrDefaultAsync(m => m.idMatriculaEstudiante == id);

            if (tblMatriculaEstudiante == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("ImprimirCalificion", tblMatriculaEstudiante)
            {
                //FileName = $"Cita_{idCita}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
        // GET: tblMatriculaEstudiantes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.tblMatriculaEstudiantes == null)
            {
                return NotFound();
            }

            var tblMatriculaEstudiante = _context.tblMatriculaEstudiantes.Find(id);
            if (tblMatriculaEstudiante == null)
            {
                return NotFound();
            }

            return View(tblMatriculaEstudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string Token, bool EstadoMatricula, tblMatriculaEstudiante matriculaEstudiante)
        {
            if (id != matriculaEstudiante.idMatriculaEstudiante)
            {
                return NotFound();
            }

            try
            {
                var existingMatriculaEstudiante = _context.tblMatriculaEstudiantes
                    .Include(m => m.Asignacion.Profesor) // Incluimos el profesor de la asignación
                    .FirstOrDefault(m => m.idMatriculaEstudiante == id);

                if (existingMatriculaEstudiante == null)
                {
                    return NotFound();
                }

                // Validamos el token del profesor relacionado con la asignación
                if (existingMatriculaEstudiante.Asignacion.Profesor.Token != Token)
                {
                    ViewData["TokenErrorMessage"] = "Token incorrecto";
                    return View(existingMatriculaEstudiante); // La vista actual, que es "Edit"
                }

                // Actualizamos el estado de la matrícula
                existingMatriculaEstudiante.EstadoMatricula = EstadoMatricula;

                // Solo actualizamos las propiedades especificadas
                existingMatriculaEstudiante.FechaCalificacion = matriculaEstudiante.FechaCalificacion;
                existingMatriculaEstudiante.Calificacion = matriculaEstudiante.Calificacion;

                _context.Update(existingMatriculaEstudiante);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblMatriculaEstudianteExists(matriculaEstudiante.idMatriculaEstudiante))
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


        // GET: tblMatriculaEstudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tblMatriculaEstudiantes == null)
            {
                return NotFound();
            }

            var tblMatriculaEstudiante = await _context.tblMatriculaEstudiantes
                .Include(t => t.Asignacion)
                .Include(t => t.Estudiante)
                .FirstOrDefaultAsync(m => m.idMatriculaEstudiante == id);
            if (tblMatriculaEstudiante == null)
            {
                return NotFound();
            }

            return View(tblMatriculaEstudiante);
        }

        // POST: tblMatriculaEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tblMatriculaEstudiantes == null)
            {
                return Problem("Entity set 'DbContext1.tblMatriculaEstudiantes'  is null.");
            }
            var tblMatriculaEstudiante = await _context.tblMatriculaEstudiantes.FindAsync(id);
            if (tblMatriculaEstudiante != null)
            {
                _context.tblMatriculaEstudiantes.Remove(tblMatriculaEstudiante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblMatriculaEstudianteExists(int id)
        {
            return (_context.tblMatriculaEstudiantes?.Any(e => e.idMatriculaEstudiante == id)).GetValueOrDefault();
        }
    }
}
