using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Souvenir.ViewModels;
using Souvenir.DataLayer;
using Souvenir.ViewModels.Manage;

namespace Souvenir.Web.Controllers
{
    public class UsersManagerController : Controller
    {
        private readonly UnitOfWork db;
        public UsersManagerController()
        {
            db = new UnitOfWork();
        }

        public UsersManagerController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // Here Users Can Manage Their Accounts 

        [Route("User/Manage")]
        // Users/Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {

            // check message here later 
            ViewBag.StatusMessage =
              message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
              : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
              : message == ManageMessageId.SetTwoFactorSuccess ? "Your two factor provider has been set."
              : message == ManageMessageId.Error ? "An error has occurred."
              : message == ManageMessageId.AddPhoneSuccess ? "The phone number was added."
              : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
              : "";

            var userId = User.Identity.GetUserId();
           
            ApplicationUser user =  new ApplicationUser
            {
                UserName = await UserManager.GetUserNameAsync(userId) , 
                Email = await UserManager.GetEmailAsync(userId),
                Name = await UserManager.GetNameAsync(userId),
                Family = await UserManager.GetFamilyAsync(userId) , 
                Address = await UserManager.GetUserAddressAsync(userId) , 
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),   
            };

            var model = new IndexViewModel
            {
                UserId = userId,
                Name = user.Name , 
                Family = user.Family , 
                UserName = user.UserName , 
                Email = user.Email , 
                Address = user.Address , 
                PhoneNumber = user.PhoneNumber ,
                HasPassword = await HasPassword(),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Manage/Orders")]
        public async Task<ActionResult> OrdersList(string UserId)
        {

            var userCarts = await db.Cart.GetCartsByUserIdAsync(UserId);

            var model = new List<OrdersListViewModel>();

            foreach (var item in userCarts)
            {
                var modelItem = new OrdersListViewModel
                {
                    Amount = item.Items.Count , 
                    TotalPrice = item.TotalPrice , 
                    Date = item.DateTime, 
                    Status = (Status)item.Status
                };
                model.Add(modelItem);
            }


            model.OrderBy(m => m.Date);
              
            return View(model);
        }


        [HttpGet]
        [Route("Users/UpdateUser")]
        public async Task<ActionResult> UpdateUserInformation()
        {

            var userId = User.Identity.GetUserId();

            var model = new UpdateUserViewModel
            {
                UserId = userId,
                Name = await UserManager.GetNameAsync(userId),
                Family = await UserManager.GetFamilyAsync(userId) , 
                Address  = await UserManager.GetUserAddressAsync(userId) , 
                UserName = await UserManager.GetUserNameAsync(userId)
            };

            return View("UpdateUserInformation", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/UpdateUser")]
        public async Task<ActionResult> UpdateUserInformation(UpdateUserViewModel model)
        {

            // Update user here and log out then redirect to Login Page or dont log out and return to manage User page instead 

            // rethink on how this controller work later

            var user = await UserManager.FindByIdAsync(model.UserId);

            user.Name = model.Name;
            user.Family = model.Family;
            user.Address = model.Address;
            user.UserName = model.UserName;


          

            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded) {
                return RedirectToAction("Index");
            }
            else
            {
                AddErrors(result);
            }

            // if we got this far then somthing went wrong ...
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Manage/RemeberBrowser")]
        public ActionResult RememberBrowser()
        {
            var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(User.Identity.GetUserId());
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, rememberBrowserIdentity);
            return RedirectToAction("Index", "UsersManager");
        }

        //
        // POST: /Manage/ForgetBrowser
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Manage/ForgetBrowser")]
        public ActionResult ForgetBrowser()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            return RedirectToAction("Index", "UsersManager");
        }

        //
        // POST: /Manage/EnableTFA
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Manage/EnableTFA")]
        public async Task<ActionResult> EnableTFA()
        {
            var userId = User.Identity.GetUserId();
            await UserManager.SetTwoFactorEnabledAsync(userId, true);
            var user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "UsersManager");
        }

        //
        // POST: /Manage/DisableTFA
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Manage/DisableTFA")]
        
        public async Task<ActionResult> DisableTFA()
        {
            var userId = User.Identity.GetUserId();
            await UserManager.SetTwoFactorEnabledAsync(userId, false);
            var user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "UsersManager");
        }



        [HttpGet]
        [Route("User/ChangePassword")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            var userId = User.Identity.GetUserId();
            var result = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }





        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private async Task<bool> HasPassword()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }
        #endregion
    }
}
