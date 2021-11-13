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
                var result = LoginAsAdmin(dto);

                if(result == false)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", "Gestiones", new { Id = 1 });
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        public bool LoginAsAdmin(AdminDTO dto) 
        {
            bool userLogged;

            //Si el objeto viene vacio, el usuario no inicia sesión
            if (dto.Email == null && dto.Password == null)
                return userLogged = false;

            //Si existen los datos, los valida
            var admin = _dbContext.Admins.
                   Select(admin => new AdminDTO
                   {
                       Id = admin.Id,
                       Email = admin.Email,
                       Password = admin.Password
                   }).Where(admins => admins.Email == dto.Email && admins.Password == dto.Password).FirstOrDefault();

            //Si los datos no coinciden con un usuario administrador, el usuario no inicia sesión
            //De modo contrario, el usuario si inicia sesión
            if (admin == null)
                return userLogged = false;
            else
                return userLogged = true;

            return userLogged;
        }
    }
}
