using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Souvenir.ViewModels.Admin.User
{
    public class UsersListViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "نام کاربری")]       
        public string UserName { get; set; }
        [Display(Name ="نقش ها")]
        public List<UserRoles> Roles { get; set; }

    }

    public class UserRoles
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
    }

}
