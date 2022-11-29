using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Souvenir.ViewModels.Admin.Orders
{
    public class IndexViewModel
    {
        public long Id { get; set; }
        [Display(Name = "نام مشتری")]
        public string UserFullName { get; set; }
        [Display(Name = "آدرس")]
        public string UserAddress { get; set; }
        [Display(Name = "تعداد سفارشات")]
        public int ItemsCount { get; set; }
        [Display(Name = "مبلغ کل")]
        public long TotalPayment { get; set; }
        [Display(Name = "وضعیت")]
        public Status Status { get; set; }

        [Display(Name ="تاریخ سفارش")]
        public DateTime OrderDate { get; set; }

    }
}



public enum Status {

    Registered =0 , 
    InProgress = 1 ,
    Sent = 2 , 
    Cancelled = 3
}




