using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Souvenir.DataLayer
{
    public class Souvenirs
    {
        [Key]
        public int SouvenirId { get; set; }
        [Required]
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
        [Display(Name="قیمت محصول")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public long Price { get; set; }
        [Display(Name= "بازدید")]
        public long Visit { get; set; }
        public int Inventory { get; set; }
        public string MainImage { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        //relations
        public virtual SouvenirsCategory Category { get; set; }
        public virtual List<Comments> Comments { get; set; }
        public virtual Province Province { get; set; }
        public virtual List<CartItem> UsersCart { get; set; }
        public virtual List<SouvenirImages> OtherImages { get; set; }

        public Souvenirs()
        {

        }

    }
}
