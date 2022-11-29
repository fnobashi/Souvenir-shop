using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Souvenir.ViewModels.Admin.Categories
{
    public class UpdateViewModel
    {

        public int Id { get; set; }
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "لطفا نام دسته بندی را وارد نمایید")]
        public string CategoryName { get; set; }
        public string ImageSrc { get; set; }

    }
}
