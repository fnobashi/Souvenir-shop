using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Souvenir.ViewModels.Admin.Souvenirs
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        
        [Display(Name = "عکس")]
        public string Image{ get; set; }

        [Display(Name ="دسته بندی")]
        public string  Category { get; set; }

        [Display(Name = "استان")]
        public string Province{ get; set; }

        [Display(Name ="قیمت")]
        public string Price { get; set; }

        [Display(Name ="موجودی")]
        public int Inventory { get; set; }
        

    }
}
