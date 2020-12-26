using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shelter.Data;
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
            using var context = new ApplicationDbContext();
            var dogList = context.Dogs.ToList();
            return View(dogList);
        }

        public IActionResult DogDetail(string dogId)
        {
            return View(model: dogId);
        }
    }
}
