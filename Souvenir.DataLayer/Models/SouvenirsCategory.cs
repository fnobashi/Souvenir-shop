using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Souvenir.DataLayer
{
    public class SouvenirsCategory
    {
        [Key]
        public int CategoryId { get; set; }
        [Display(Name ="دسته بندی")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]    
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از 250 کاراکتر باشد")]
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public bool IsDeleted { get; set; }

        //realtions
        public virtual List<Souvenirs> Souvenirs { get; set; }


        public SouvenirsCategory()
        {

        }

    }
}
