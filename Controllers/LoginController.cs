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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace kisko.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AdminDTO dto)
        {
            try
            {
                /*
                 using (_dbContext)
                {
                    var admin = (from d in _dbContext.Admins
                                 where d.Email == dto.Email && d.Password == dto.Password
                                 select d).FirstOrDefault();
                    if (admin == null)
                    {
                        ViewBag.Error = "Correo o contraseña invalidas";
                        return RedirectToAction("Index");
                    }
                }
                 */

                var admin = _dbContext.Admins.
                    Select(admin => new AdminDTO
                {
                    Id = admin.Id,
                    Email = admin.Email,
                    Password = admin.Password
                }).Where(admins => admins.Email == dto.Email && admins.Password == dto.Password).FirstOrDefault();

                if(admin == null)
                {
                    ViewBag.Error = "Correo o contraseña invalidas";
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Gestiones", new { Id = admin.Id});
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
    }
}
