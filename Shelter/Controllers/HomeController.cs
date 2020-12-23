using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shelter.Models;
using Shelter.Models.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
