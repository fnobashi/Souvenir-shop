using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Souvenir.ViewModels.Admin.Categories
{
    public class DeleteViewModel
    {
        public int ID{ get; set; }
        [Display(Name ="نام دسته بندی")]
        public string CategoryName { get; set; }
        public string ImageSrc { get; set; }

    }
}
