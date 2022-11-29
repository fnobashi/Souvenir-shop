using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Souvenir.DataLayer;
using System.Threading.Tasks;
using Souvenir.ViewModels.Admin.User;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Souvenir.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        private readonly UnitOfWork db;
        public UserController()
        {
            db = new UnitOfWork();
        }


        public async Task<ActionResult> Index(bool IsAjax = false)
        {
            var users = await db.Users.GetAllUsersAsync();
            var model = new List<UsersListViewModel>();

            foreach (var item in users)
            {
                var modelItem = new UsersListViewModel
                {
                    Name = item.Name,
                    UserName = item.UserName,
                    UserId = item.Id,
                    Roles = new List<UserRoles>()
                };

                foreach (var roles in item.Roles)
                {
                    var role = new UserRoles
                    {
                        RoleID = roles.RoleId,
                        RoleName = db.Users.GetUserRoleName(roles.RoleId)
                    };

                    modelItem.Roles.Add(role);
                }

                model.Add(modelItem);
            }

            if (IsAjax)
            {
                return PartialView("AjaxIndex", model);
            }

            return View(model);
        }

        public ActionResult UserDetail(string userId)
        {

            var user = db.Users.GetUserById(userId);
            var model = new UserViewModel
            {
                Name = user.Name,
                UserName = user.UserName,
                UserId = user.Id,
                Roles = new List<UserRoles>()
            };

            foreach (var item in user.Roles)
            {
                var role = new UserRoles
                {
                    RoleID = item.RoleId,
                    RoleName = db.Users.GetUserRoleName(item.RoleId)
                };
                model.Roles.Add(role);
            }

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PromoteUserToAdmin(string UserId)
        {
            var user = db.Users.GetUserById(UserId);
            IdentityUserRole role = new IdentityUserRole
            {
                UserId = UserId,
                RoleId = db.Users.GetRoleIdByName("Admin")
            };
            user.Roles.Add(role);
            db.Save();

            return RedirectToAction("Index", new { IsAjax = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PromoteUserToNormalUser(string UserId)
        {
            var user = db.Users.GetUserById(UserId);
            var role = user.Roles.First(r => r.RoleId == db.Users.GetRoleIdByName("Admin"));
            user.Roles.Remove(role);
            db.Save();
            return RedirectToAction("Index", new { IsAjax = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(string UserId)
        {
            var user = db.Users.GetUserById(UserId);

            var model = new UpdateUserViewModel
            {
                Name = user.Name,
                Family = user.Family,
                Address = user.Address,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Phone = user.Phone,
                UserId = user.Id,
                UserName = user.UserName,

            };

          

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(UpdateUserViewModel model)
        {
            var user = db.Users.GetUserById(model.UserId);

            user.Address = model.Address;
            user.Name = model.Name;
            user.Family = model.Family;
            user.UserName = model.UserName;
            user.PhoneNumber = model.Phone;
            user.Phone = model.Phone;
            user.Email = model.Email;
            user.EmailConfirmed = model.EmailConfirmed;

            db.Users.UpdateUser(user);
            db.Save();

            return RedirectToAction("Index");
        }


        public ActionResult DeleteUser(string userId)
        {

            var user = db.Users.GetUserById(userId);
            var model = new UserViewModel
            {
                Name = user.Name,
                UserName = user.UserName,
                UserId = user.Id,
                Roles = new List<UserRoles>()
            };

            foreach (var item in user.Roles)
            {
                var role = new UserRoles
                {
                    RoleID = item.RoleId,
                    RoleName = db.Users.GetUserRoleName(item.RoleId)
                };
                model.Roles.Add(role);
            }


            return View(model);
        }
      


    }
}