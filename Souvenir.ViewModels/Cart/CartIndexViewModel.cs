using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Souvenir.ViewModels.Cart
{

    public class CartIndexViewModel
    {
        public List<CartItemsViewModel> Items { get; set; }
        public int TotalCount { get; set; }
        public long TotalPrice { get; set; }
        public long OrderId { get; set; }
    }

}
