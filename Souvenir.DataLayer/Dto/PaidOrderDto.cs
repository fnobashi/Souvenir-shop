using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.DataLayer.Dto
{
    public class PaidOrderDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string CustomerAddress { get; set; }
        public int ItemsCount { get; set; }
        public long TotalPayment { get; set; }
        public DateTime OrderDate { get; set; }
        public Status Status { get; set; }
    }
}

