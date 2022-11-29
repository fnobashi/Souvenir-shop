using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Souvenir.ViewModels.Manage
{
    public class OrdersListViewModel
    {
        [Display(Name = "تاریخ")]
        public DateTime Date { get; set; }
        [Display(Name = "انواع محصولات")]
        public int Amount { get; set; }
        [Display(Name = "مبلغ کل")]
        public long TotalPrice { get; set; }
        [Display(Name = "وضعیت سفارش")]
        public Status Status { get; set; }

    }
}
