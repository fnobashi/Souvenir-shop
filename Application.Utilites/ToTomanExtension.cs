using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilites
{
    public static class ToTomanExtension
    {
        public static string ToToman(this long amount)
        {
            return amount.ToString("#,# تومان");
        }

        public static string ToToman(this int amount)
        {
            return amount.ToString("#,# تومان");
        }

    }
}
