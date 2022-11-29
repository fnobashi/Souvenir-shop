using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilites
{
    public static class ToRialExtension
    {
        public static string toRial(this long currency)
        {
            return currency.ToString("#,# ریال");
        }
        public static string toRial(this int currency)
        {
            return currency.ToString("#,# ریال");
        }


    }
}
