using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Souvenir.ViewModels.Admin.Categories
{
    public  class IndexViewModel
    {
        public int Id { get; set; }
        [Display(Name ="نام دسته بندی")]
        public string  Name { get; set; }
        [Display(Name = "عکس")]
        public string ImageSrc { get; set; }

    }
}
