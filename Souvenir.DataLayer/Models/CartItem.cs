using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Souvenir.DataLayer
{
    public class CartItem
    {
        [Key]
        public long ItemID { get; set; }
        [ForeignKey("Cart") , Required]
        public long OrderID { get; set; }
        [ForeignKey("Souvenir")]
        public int SouvenirId { get; set; }
        public int Count { get; set; }
        public long Price { get; set; }

        // relations 
        public Souvenirs Souvenir { get; set; }
        public Cart Cart { get; set; }

        public CartItem()
        {

        }

    }
}
