using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Souvenir.ViewModels.Admin.Orders
{
    public class DetailsViewModel
    {
        public long OrderId { get; set; }
        public long ItemId { get; set; }
        [Display(Name = "نام محصول")]
        public string ProductName { get; set; }
        [Display(Name = "قیمت")]
        public long ProductPrice { get; set; }
        [Display(Name = "تعداد سفارش")]
        public int ProductAmount { get; set; }

    }
}
