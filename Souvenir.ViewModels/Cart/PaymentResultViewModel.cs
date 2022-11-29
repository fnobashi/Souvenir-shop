using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.ViewModels.Cart
{
    public class PaymentResultViewModel
    {
        public long UserId { get; set; }
        public int SouvenirsCount { get; set; }
        public string Price { get; set; }
        
    }
}
