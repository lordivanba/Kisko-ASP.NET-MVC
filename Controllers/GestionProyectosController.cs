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
using System.Text.RegularExpressions;

namespace kisko.Controllers
{
    public class GestionProyectosController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _hosting;
        public int Id = 0;
        public GestionProyectosController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnviroment)
        {
            _dbContext = dbContext;
            _hosting = hostEnviroment;
        }

        public IActionResult Index(int Id)
        {
            if(Id == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            var projects = _dbContext.Projects
                .Select(project => new ProjectDTO
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    GradeGroup = project.GradeGroup,
                    Student = project.Student == null ? "N/A" : project.Student,
                    SecondStudent = project.SecondStudent == null ? "N/A" : project.SecondStudent

                }).ToList();
            ViewBag.AdminId = Id;
            return View(projects);
        }

        public IActionResult Add()
        {

            var students = _dbContext.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Lastname = s.Lastname
            });

            ViewBag.Students = students;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProjectDTO dto)
        {
            var fileName = UploadPhotoProject(dto.Photo);
            var students = _dbContext.Students
            .Select(student => new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Lastname = student.Lastname
            }).ToList();
            var stud = students.Where(s => s.Id == dto.StudentId).FirstOrDefault();
            var stud2 = students.Where(s => s.Id == dto.SecondStudentId).FirstOrDefault();
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                GradeGroup = dto.GradeGroup,
                Img = dto.Img,
                Video = dto.Video,
                StudentId = dto.StudentId,
                Student = $"{stud.Name} {stud.Lastname}",
                SecondStudentId = dto.SecondStudentId,
                SecondStudent = $"{stud2.Name} {stud2.Lastname}",
            };

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new { Id = 1 });
        }

        public IActionResult Edit(int Id)
        {
            var students = _dbContext.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Lastname = s.Lastname
            });

            ViewBag.Students = students;

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
                    StudentId = project.StudentId,
                    Student = project.Student,
                    SecondStudentId = project.SecondStudentId,
                    SecondStudent = project.SecondStudent                    
                }).ToList();
            var proj = projects.Where(s => s.Id == Id).FirstOrDefault();
            return View(proj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectDTO dto)
        {
            var fileName = UploadPhotoProject(dto.Photo);
            var fileName2 = UploadPhotoProject(dto.Photo2);
            var fileName3 = UploadPhotoProject(dto.Photo3);
            var students = _dbContext.Students
            .Select(student => new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Lastname = student.Lastname
            }).ToList();
            var stud = students.Where(s => s.Id == dto.StudentId).FirstOrDefault();
            var stud2 = students.Where(s => s.Id == dto.SecondStudentId).FirstOrDefault();

            var project = await _dbContext.Projects.FirstOrDefaultAsync(project => project.Id == id);

            if (project == null)
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
            project.StudentId = dto.StudentId;
            project.Student = $"{stud.Name} {stud.Lastname}";
            project.SecondStudentId = dto.SecondStudentId;
            project.SecondStudent = $"{stud2.Name} {stud2.Lastname}";

            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { Id = 1 });
        }

        public IActionResult Delete(int Id)
        {
            var project = new Project()
            {
                Id = Id
            };

            _dbContext.Projects.Attach(project);
            _dbContext.Projects.Remove(project);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { Id = 1 });
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

        /*public bool RegistrarProyecto(ProjectDTO dto) 
        {
            bool error = false;
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                GradeGroup = dto.GradeGroup,
                Img = dto.Img,
                Video = dto.Video,
                StudentId = dto.StudentId,
                Student = dto.Student,
                SecondStudentId = dto.SecondStudentId,
                SecondStudent = dto.SecondStudent,
            };
            try
            {
                _dbContext.Projects.Add(project);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                error = true;
            }

            if (error == true)
                return false;
            else
                return true;
        }

        public bool ValidarCamposProyecto(ProjectDTO dto) 
        {
            bool Name = false;
            if (dto.Name != null && dto.Name.Contains("!@#$%^&*()_+{};:,.<>?")) 
            {
                Name = true;
            }

            if (Name == true)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }*/
    }
}
