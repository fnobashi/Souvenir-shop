using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.DataLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;
        public UserRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await Task.Run(() => {
                return db.Users;

            });
        }


        public string GetUserRoleName(string roleId)
        {
            return db.Roles.Find(roleId).Name;
        }
        public IdentityRole GetUserRoleById(string roleId)
        {
            return db.Roles.Find(roleId);
        }

        public string GetRoleIdByName(string name)
        {
            return db.Roles.First(role => role.Name == name).Id;
        }

        public ApplicationUser GetUserById(string userId)
        {
            return db.Users.Find(userId);
        }

        public void DeleteUser(ApplicationUser user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
        }

        public void UpdateUser(ApplicationUser user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
        }

        public long UsersCount()
        {
            return db.Users.Count();
        }


    }
}
