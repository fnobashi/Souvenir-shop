using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.DataLayer.Dto
{
    public class PaymentResultDto
    {
        public string UserId{ get; set; }
        public int SouvenirCount { get; set; }
        public long Price { get; set; }

    }


}
