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
    public class DivisionIndustrialController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public DivisionIndustrialController(ApplicationDbContext dbContext)
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
                }).Where(careers => careers.Division == "Industrial").ToList();

            return View(careers);
        }
    }
}
