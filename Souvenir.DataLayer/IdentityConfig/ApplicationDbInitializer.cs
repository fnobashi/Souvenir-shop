using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Souvenir.DataLayer
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes

    public partial class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public void InitializeIdentityForEF(ApplicationDbContext db)
        {

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(db));


            const string name = "admin@example.com";
            const string password = "Admin@123456";
            string[] Roles =  { "Admin", "NormalUser" };

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, EmailConfirmed = true  , Address = "null" , Phone="09123456789" , PhoneNumber ="09123456789" , PhoneNumberConfirmed = true , Family = "Admin" , Name = "Admin"  };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added

            foreach (var roleName in Roles)
            {
                var role = roleManager.FindByName(roleName);

                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    var roleresult = roleManager.Create(role);
                }
            }

            
            var adminRole = roleManager.FindByName("Admin");
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(adminRole.Name))
            {
                var result = userManager.AddToRole(user.Id, adminRole.Name);
            }
        }
    }

}