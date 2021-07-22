using kisko.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kisko.Models;
using kisko.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace kisko.Controllers
{
    public class ProyectosController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ProyectosController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index() { 
        
            var projects = _dbContext.Projects
                .Select(project => new ProjectDTO
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    GradeGroup = project.GradeGroup,
                    Img = project.Img,
                }).ToList();
            return View(projects);
        }

        public IActionResult Info(int Id)
        {
            var projects = _dbContext.Projects
                .Select(project => new ProjectDTO
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    GradeGroup = project.GradeGroup,
                    Img = project.Img,
                    Img2 = project.Img2,
                    Img3 = project.Img3,
                    Video = project.Video,
                    Student = project.Student,
                    SecondStudent = project.SecondStudent
                }).ToList();
            var proj = projects.Where(s => s.Id == Id).FirstOrDefault();
            return View(proj);
        }

    }
}
