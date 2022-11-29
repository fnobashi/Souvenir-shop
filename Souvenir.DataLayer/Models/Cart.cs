using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Souvenir.DataLayer
{
    public class Cart
    {
        [Key]
        public long OrderID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public bool IsFinally { get; set; }
        public DateTime DateTime { get; set; }
        public long TotalPrice { get; set; }
        public int Status { get; set; } = 0;
        //relations

        public virtual List<CartItem> Items { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }    
        

        public Cart()
        {

        }

    }
}
