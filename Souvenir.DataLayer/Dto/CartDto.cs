using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.DataLayer.Dto
{
    public class CartDto
    {
        public string Id { get; set; }
        public string UserFullName { get; set; }
        public string UserAddress { get; set; }
        public int ItemsCount { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {

        Registered = 0,
        InProgress = 1,
        Sent = 2,
        Cancelled = 3
    }

}
