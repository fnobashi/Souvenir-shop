using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Souvenir.DataLayer
{
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }
        [Display(Name = "نام استان")]
        [MaxLength(100, ErrorMessage = "نام استان نمیتواند بیشتر از 100 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProvinceName { get; set; }
        public int ProvinceNo { get; set; }
        //relations 
        public virtual List<Souvenirs> Souvenir { get; set; }

        public Province()
        {

        }

    }
}
