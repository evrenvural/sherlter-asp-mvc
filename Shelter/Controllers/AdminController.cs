﻿using Microsoft.AspNetCore.Authorization;
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
        readonly UserManager<User> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ILogger<BaseController> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : base(logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Dog Table
        public ActionResult GetDogTable()
        {
            using var context = new ApplicationDbContext();
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

        #region User Table
        public ActionResult GetUserTable()
        {
            return PartialView("~/Views/Admin/_UserTable.cshtml", _userManager.Users.ToList());
        }

        [HttpPost]
        public async Task<ActionResult> SetUserRole(string userId)
        {
            using var context = new ApplicationDbContext();
            var user = context.MyUsers.Where(x => x.Id == userId).First();

            if (user.MyUserType == UserTypeEnum.ADMIN)
            {
                await _userManager.AddToRoleAsync(user, "USER");
                user.MyUserType = UserTypeEnum.USER;
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "ADMIN");
                user.MyUserType = UserTypeEnum.ADMIN;
            }

            await context.SaveChangesAsync();

            return Json(true);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            using var context = new ApplicationDbContext();
            context.MyUsers.Remove(new User { Id = userId });
            await context.SaveChangesAsync();

            return Json(true);
        }
        #endregion
    }
}
