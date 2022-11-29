using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Souvenir.ViewModels.Admin.Categories
{
    public class InsertViewModel
    {
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "لطفا نام دسته بندی را وارد نمایید")]
        public string Name { get; set; }

    }
}
