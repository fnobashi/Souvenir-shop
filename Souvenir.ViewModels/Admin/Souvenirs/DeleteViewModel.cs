using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Souvenir.ViewModels.Admin.Souvenirs
{
    public class DeleteViewModel
    {
        public int Id{ get; set; }
        [Display(Name ="نام محصول : ")]
        public string Name { get; set; }
        [Display(Name = "دسته بندی : ")]
        public string Category { get; set; }
        [Display(Name ="استان : ")]
        public string Provice { get; set; }
        [Display(Name = "عکس : ")]
        public string ImageSrc  { get; set; }
    }
}
