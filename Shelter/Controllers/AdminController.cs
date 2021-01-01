using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shelter.Data;
using Shelter.Models.Base;
using Shelter.Models.Entities;
using Shelter.Models.Enums;
using Shelter.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Shelter.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        readonly UserManager<User> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ILogger<BaseController> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IStringLocalizer<HomeController> localizer) : base(logger, localizer)
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
            return PartialView("~/Views/Admin/_DogTable.cshtml", context.Dogs.Include(x => x.User).Where(x => x.IsActive).ToList());
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
            var dog = context.Dogs.Where(x => x.Id == dogId && x.IsActive).FirstOrDefault();
            dog.IsActive = false;
            context.Dogs.Update(dog);
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
            var identityUser = await _userManager.FindByIdAsync(userId);

            var roles = await _userManager.GetRolesAsync(identityUser);

            if (roles.Contains("Admin"))
            {
                await _userManager.RemoveFromRolesAsync(identityUser, roles.ToArray());
                await _userManager.AddToRoleAsync(identityUser, "USER");
                context.MyUsers.Where(x => x.Id == userId).First().MyUserType = UserTypeEnum.USER;
            }
            else
            {
                await _userManager.RemoveFromRolesAsync(identityUser, roles.ToArray());
                await _userManager.AddToRoleAsync(identityUser, "ADMIN");
                context.MyUsers.Where(x => x.Id == userId).First().MyUserType = UserTypeEnum.ADMIN;
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

        #region Request Table
        public ActionResult GetRequestTable()
        {
            using var context = new ApplicationDbContext();
            var requestList = context.Requesteds.Include(x => x.Dog).Include(x => x.User).ToList();
            return PartialView("~/Views/Admin/_RequestTable.cshtml", requestList);
        }

        [HttpPost]
        public async Task<ActionResult> AcceptRequest(string requestId, string dogId, string userId)
        {
            using var context = new ApplicationDbContext();

            var request = context.Requesteds.Include(x => x.Dog).Include(x => x.User).Where(x => x.Id == requestId).FirstOrDefault();
            request.Status = RequestStatusEnum.ACCEPTED;
            request.Dog.IsOwned = true;
            request.Dog.User = request.User;

            var anotherUserRequestList = context.Requesteds.Where(x => x.Id != requestId && x.UserId == userId);
            foreach (var anotherRequest in anotherUserRequestList)
            {
                anotherRequest.Status = RequestStatusEnum.REJECTED;
            }

            var anotherDogRequestList = context.Requesteds.Include(x => x.Dog).Where(x => x.Id != requestId && x.Dog.Id == dogId);
            foreach (var anotherRequest in anotherDogRequestList)
            {
                anotherRequest.Status = RequestStatusEnum.REJECTED;
            }

            await context.SaveChangesAsync();

            return Json(true);
        }

        [HttpPost]
        public async Task<ActionResult> RejectRequest(string requestId)
        {
            using var context = new ApplicationDbContext();

            var request = context.Requesteds.Where(x => x.Id == requestId).FirstOrDefault();
            request.Status = RequestStatusEnum.REJECTED;

            await context.SaveChangesAsync();

            return Json(true);
        }
        #endregion
    }
}
