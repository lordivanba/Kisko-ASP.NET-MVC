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
    public class GestionCarrerasController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _hosting;
        public int Id = 0;
        public GestionCarrerasController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnviroment)
        {
            _dbContext = dbContext;
            _hosting = hostEnviroment;
        }
        public IActionResult Index(int Id)
        {
            if (Id == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            var careers = _dbContext.Careers
                .Select(career => new CareerDTO
                {
                    Id = career.Id,
                    Name = career.Name,
                    Description = career.Description,
                    Division = career.Division,
                    PdfName = career.PdfName
                }).ToList();
            ViewBag.AdminId = Id;
            return View(careers);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CareerDTO dto)
        {
            var career = new Career
            {
                Name = dto.Name,
                Description = dto.Description,
                Division = dto.Division
            };

            _dbContext.Careers.Add(career);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new { Id = 1 });
        }

        public IActionResult Delete(int Id)
        {
            var career = new Career()
            {
                Id = Id
            };

            _dbContext.Careers.Attach(career);
            _dbContext.Careers.Remove(career);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { Id = 1 });
        }

        public IActionResult Edit(int Id)
        {
            var careers = _dbContext.Careers
                .Select(career => new CareerDTO
                {
                    Id = career.Id,
                    Name = career.Name,
                    Description = career.Description,
                    Division = career.Division
                }).ToList();
            var career = careers.Where(s => s.Id == Id).FirstOrDefault();
            return View(career);
        }

        private string UploadPdf(IFormFile file)
        {
            if (file == null)
            {
                return "not-found.pdf";
            }

            var fileName = string.Empty;
            string uploadFolder = Path.Combine(_hosting.WebRootPath, "archives");
            fileName = $"{Guid.NewGuid()}_{file.FileName.Trim()}";
            var filePath = Path.Combine(uploadFolder, fileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            return fileName;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CareerDTO dto)
        {
            var fileName = UploadPdf(dto.Pdf);

            var career = await _dbContext.Careers.FirstOrDefaultAsync(career => career.Id == id);

            if (career == null)
            {
                return NotFound();
            }

            career.Name = dto.Name;
            career.Description = dto.Description;
            career.Division = dto.Division;
            career.PdfName = fileName;

            _dbContext.Careers.Update(career);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { Id = 1 });
        }


    }
}
