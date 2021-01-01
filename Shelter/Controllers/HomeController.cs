using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shelter.Data;
using Shelter.Models;
using Shelter.Models.Base;
using Shelter.Models.Entities;
using Shelter.Models.Enums;
using Shelter.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace Shelter.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, IStringLocalizer<HomeController> localizer) : base(logger, localizer)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            FillViewDatas();
            using var context = new ApplicationDbContext();
            var dogList = context.Dogs.Where(x => x.IsActive).ToList();
            return View(dogList);
        }

        public async Task<IActionResult> DogDetail(string dogId)
        {
            using var context = new ApplicationDbContext();

            var dog = context.Dogs.Include(x => x.User).Where(x => x.Id == dogId && x.IsActive).FirstOrDefault();
            var identityUser = await _userManager.GetUserAsync(User);
            var request = context.Requesteds.Where(x => x.User.Id == identityUser.Id && x.Dog.Id == dog.Id).FirstOrDefault();
            var wasRequested = request != null && request.Status == RequestStatusEnum.IS_WAITING;

            return View(new DogDetailViewModel { Dog = dog, WasRequested = wasRequested });
        }

        [HttpPost]
        public async Task<ActionResult> RequestOwn(string dogId)
        {
            using var context = new ApplicationDbContext();
            var dog = context.Dogs.Where(x => x.Id == dogId && x.IsActive).FirstOrDefault();
            var identityUser = await _userManager.GetUserAsync(User);

            context.Requesteds.Add(new Requested { Dog = dog, UserId = identityUser.Id });

            await context.SaveChangesAsync();

            return Json(true);
        }

    }
}
