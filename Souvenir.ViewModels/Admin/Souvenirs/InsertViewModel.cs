using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Souvenir.ViewModels.Admin.Souvenirs
{
    public class InsertViewModel
    {
    
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از 250 کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name ="دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int ProvinceId { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortDescription { get; set; }

        [Display(Name = "شرح محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [AllowHtml]
        public string Description { get; set; }
        
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long Price { get; set; }

        [Display(Name = "موجودی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Inventory { get; set; }

    }
}
