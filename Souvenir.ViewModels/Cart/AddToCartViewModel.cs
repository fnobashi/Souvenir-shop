using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.ViewModels
{
    public class AddToCartViewModel
    {
        public long OrderId { get; set; }
        public int SouvenirId { get; set; }
        public long Price { get; set; }
    }
}
