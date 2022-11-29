using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace Application.Utilites
{
    public static class ShamsiCalendar
    {

        public static string ToShamsi(this DateTime date)
        {
            var pc = new PersianCalendar();
            var shamsiDate = pc.GetYear(date).ToString("0000") + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");
            return shamsiDate;
        }

    }
}
