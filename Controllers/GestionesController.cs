using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kisko.Controllers
{
    public class GestionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
