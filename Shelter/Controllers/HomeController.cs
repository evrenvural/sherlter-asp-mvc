using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shelter.Data;
using Shelter.Models;
using Shelter.Models.Base;
using Shelter.Models.Entities;
using Shelter.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.Controllers
{
    public class HomeController : BaseController
    {
        readonly UserManager<User> _userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager) : base(logger)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            using var context = new ApplicationDbContext();
            var dogList = context.Dogs.ToList();
            return View(dogList);
        }

        public IActionResult DogDetail(string dogId)
        {
            using var context = new ApplicationDbContext();

            var dog = context.Dogs.Where(x => x.Id == dogId).FirstOrDefault();
            var userId = _userManager.GetUserId(User);
            var wasRequested = context.Requesteds.Where(x => x.User.Id == userId && x.Dog.Id == dog.Id).FirstOrDefault() != null;

            return View(new DogDetailViewModel { Dog = dog, WasRequested = wasRequested });
        }

        [HttpPost]
        public async Task<ActionResult> RequestOwn(string dogId)
        {
            using var context = new ApplicationDbContext();
            var dog = context.Dogs.Where(x => x.Id == dogId).FirstOrDefault();
            var user = await _userManager.GetUserAsync(User);

            context.Requesteds.Add(new Requested { Dog = dog, User = user });

            await context.SaveChangesAsync();

            return Json(true);
        }
    }
}
