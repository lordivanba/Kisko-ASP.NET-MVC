using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kisko.Models;

namespace kisko.Controllers
{
    public class GestionesController : Controller
    {
        public int Id = 0;
        public IActionResult Index(int Id)
        {

            if (Id == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.AdminId = Id;
            return View();
        }
    }
}
