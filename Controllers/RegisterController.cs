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
using Microsoft.AspNetCore.Identity;

namespace kisko.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public RegisterController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AdminDTO dto)
        {
            var admin = new Admin
            {
                Fullname = dto.FullName,
                Email = dto.Email,
                Password = dto.Password
            };

            _dbContext.Admins.Add(admin);
            _dbContext.SaveChanges();
            return RedirectToAction("Index","Login");
        }
    }
}
