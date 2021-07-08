using kisko.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kisko.Models;
using kisko.Entities;

namespace kisko.Controllers
{
    public class GestionProyectosController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public GestionProyectosController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var projects = _dbContext.Projects
                .Select(project => new ProjectDTO
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    GradeGroup = project.GradeGroup,
                    Img = project.Img
                }).ToList();
            return View(projects);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProjectDTO dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                GradeGroup = dto.GradeGroup,
                Img = dto.Img
            };

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
