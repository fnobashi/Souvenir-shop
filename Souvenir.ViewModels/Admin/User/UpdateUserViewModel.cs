using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
        

namespace Souvenir.ViewModels.Admin.User
{
    public class UpdateUserViewModel
    {
        public string UserId { get; set; }
        [Display(Name ="نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Family { get; set; }
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name ="شماره موبایل")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "شماره وارد شده صحیح نمی باشد")]
        public string Phone { get; set; }
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email{ get; set; }
        [Display(Name = "تایید ایمیل")]
        public bool EmailConfirmed { get; set; }
    }



}
