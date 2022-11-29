using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.Entity;


namespace Souvenir.DataLayer
{
    public static class UserManagerExtentions
    {
        public static async Task<string> GetNameAsync(this UserManager<ApplicationUser> userManager, string userid)
        {
            var result = await userManager?.Users?.FirstOrDefaultAsync(um => um.Id == userid);
            return result.Name.ToString();
        }

        public static async Task<string> GetFamilyAsync(this UserManager<ApplicationUser> userManager, string userid)
        {
            var result = await userManager?.Users?.FirstOrDefaultAsync(um => um.Id == userid);
            return result.Family.ToString();
        }

        public static async Task<string> GetUserAddressAsync(this UserManager<ApplicationUser> userManager, string userid)
        {
            var result = await userManager?.Users?.FirstOrDefaultAsync(um => um.Id == userid);
            return result.Address.ToString();

        }

        public static async Task<string> GetUserNameAsync(this UserManager<ApplicationUser> userManager, string userId)
        {
            var result = await userManager?.Users?.FirstOrDefaultAsync(um => um.Id == userId);
            return result.UserName.ToString();
        }



    }
}