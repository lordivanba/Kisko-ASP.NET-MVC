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
    public class GestionProyectosController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _hosting;
        public GestionProyectosController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnviroment)
        {
            _dbContext = dbContext;
            _hosting = hostEnviroment;
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
                    Img = project.Img,
                    Video = project.Video
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
            var fileName = UploadPhotoProject(dto.Photo);
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                GradeGroup = dto.GradeGroup,
                Img = dto.Img,          
                Video = dto.Video
            };

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
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
                    Video = project.Video
                }).ToList();
            var proj = projects.Where(s => s.Id == Id).FirstOrDefault();
            return View(proj);
        }


        /*
         
            [HttpPost]
        public IActionResult Edit(Project proj)
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
            var project = projects.Where(s => s.Id == proj.Id).FirstOrDefault();
            projects.Remove(project);
            projects.Add(project);

            _dbContext.Projects.Update(proj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
         */
        public IActionResult Delete(int Id)
        {
            var project = new Project()
            {
                Id = Id
            };

            _dbContext.Projects.Attach(project);
            _dbContext.Projects.Remove(project);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        private async Task<Project> GetProjectById(int id)
        {
            return await _dbContext.Projects.AsNoTracking()
                .FirstOrDefaultAsync(project => project.Id == id);
        }

        private static ProjectDTO CreateDtoFromObject(Project obj)
        {
            var dto = new ProjectDTO
            {
                Name = obj.Name,
                Description = obj.Description,
                GradeGroup = obj.GradeGroup,
                Img = obj.Img
            };
            return dto;
        }


        private string UploadPhotoProject(IFormFile file)
        {
            if (file == null)
            {
                return "not-found.png";
            }

            var fileName = string.Empty;
            string uploadFolder = Path.Combine(_hosting.WebRootPath, "images");
            fileName = $"{Guid.NewGuid()}_{file.FileName.Trim()}";
            var filePath = Path.Combine(uploadFolder, fileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            return fileName;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectDTO dto)
        {
            var fileName = UploadPhotoProject(dto.Photo);
            var fileName2 = UploadPhotoProject(dto.Photo2);
            var fileName3 = UploadPhotoProject(dto.Photo3);

            var project = await _dbContext.Projects.FirstOrDefaultAsync(project => project.Id == id);

            if(project == null)
            {
                return NotFound();
            }

            project.Name = dto.Name;
            project.Description = dto.Description;
            project.GradeGroup = dto.GradeGroup;
            project.Video = dto.Video;
            project.Img = fileName;
            project.Img2 = fileName2;
            project.Img3 = fileName3;

            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
