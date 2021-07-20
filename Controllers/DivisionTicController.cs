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
    public class DivisionTicController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public DivisionTicController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var careers = _dbContext.Careers
                .Select(career => new CareerDTO
                {
                    Id = career.Id,
                    Name = career.Name,
                    Description = career.Description,
                    Division = career.Division,
                    PdfName = career.PdfName
                }).Where(careers => careers.Division == "Tecnologías de la Información y Comunicación").ToList();

            return View(careers);
        }
    }
}
