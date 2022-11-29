using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;


namespace Souvenir.ViewModels
{
    public class SouvenirViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        [Display(Name = "نام سوغاتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از 250 کاراکتر باشد")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از 250 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "توضیح مختصر")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Display(Name = "شرح محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Required(ErrorMessage ="لطفا عکس محصول را انتحاب کنید")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase[] ImageFiles { get; set; }


    }

}
