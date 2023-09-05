using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PatronesDeSeguridad.Data;
using PatronesDeSeguridad.Models;

namespace PatronesDeSeguridad.Controllers
{
    public class EstadisticasController : Controller
    {
        private readonly DbContext1 _context;

        public EstadisticasController(DbContext1 context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var topEstudiantes = _context.tblMatriculaEstudiantes
                .Where(m => m.Calificacion > 0)
                .GroupBy(m => m.Estudiante)
                .Select(g => new EstudianteViewModel
                {
                    NombreEstudiante = $"{g.Key.FirstName} {g.Key.LastName1} {g.Key.LastName2}",
                    CalificacionPromedio = g.Average(m => m.Calificacion),
                    MatriculasConCalificacionMayorACero = g.Count()
                })
                .OrderByDescending(e => e.CalificacionPromedio)
                .Take(3)
                .ToList();

            var viewModel = new EstadisticasViewModel
            {
                TopEstudiantes = topEstudiantes
            };

            return View(viewModel);
        }

    }
}
