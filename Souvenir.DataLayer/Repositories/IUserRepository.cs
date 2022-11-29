using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.DataLayer
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        ApplicationUser GetUserById(string userId);
        string GetUserRoleName(string roleId);
        string GetRoleIdByName(string name);
        IdentityRole GetUserRoleById(string roleId);
        void DeleteUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        long UsersCount();

    }
}
