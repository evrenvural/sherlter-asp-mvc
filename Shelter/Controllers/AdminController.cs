using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shelter.Data;
using Shelter.Models.Base;
using Shelter.Models.Entities;
using Shelter.Models.Enums;
using Shelter.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        public AdminController(ILogger<BaseController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserTable()
        {
            using var context = new ApplicationDbContext();
            return PartialView("~/Views/Admin/_UserTable.cshtml", context.MyUsers.ToList());
        }

        #region Dog Table
            public ActionResult GetDogTable()
            {
                using (var context = new ApplicationDbContext())
                    return PartialView("~/Views/Admin/_DogTable.cshtml", context.Dogs.ToList());
            }

            [HttpPost]
            public async Task<ActionResult> AddDog(Dog dog)
            {
                dog.IsOwned = false;

                using var context = new ApplicationDbContext();
                context.Dogs.Add(dog);
                await context.SaveChangesAsync();

                return Json(true);
            }

            [HttpPut]
            public async Task<ActionResult> UpdateDog(Dog dog)
            {
                using var context = new ApplicationDbContext();
                context.Dogs.Update(dog);
                await context.SaveChangesAsync();

                return Json(true);
            }

            [HttpDelete]
            public async Task<ActionResult> DeleteDog(string dogId)
            {
                using var context = new ApplicationDbContext();
                context.Dogs.Remove(new Dog { Id = dogId });
                await context.SaveChangesAsync();

                return Json(true);
            }
        #endregion
    }
}
