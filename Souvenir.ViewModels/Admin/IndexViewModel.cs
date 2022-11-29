using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Souvenir.ViewModels.Admin
{
    public class IndexViewModel
    {
        [Display(Name = "فروش یک ماه اخیر")]
        public long ThisMonthSale { get; set; }
        [Display(Name ="تعداد کاربران سایت")]
        public long UsersCount { get; set; }
        [Display(Name ="تعداد محصولات سایت")]
        public long ProductsCount { get; set; }
        [Display(Name ="سفارشات جدید")]
        public int NewOrders { get; set; }
        [Display(Name = "سفارشات در حال انجام")]
        public int InProgressOrders { get; set; }

    }
}
