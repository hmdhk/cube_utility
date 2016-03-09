using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;
namespace Cube_Utility
{
    public class StringUtilities
    {
        public static string to_char(DateTime Date,string Format,string Calendar)
        {
            System.Globalization.PersianCalendar prc = new System.Globalization.PersianCalendar();
            CultureInfo fair = new CultureInfo("fa-ir");
            fair.DateTimeFormat.Calendar = new PersianCalendar();
            
            DateTime dt = new DateTime(prc.GetYear(Date),prc.GetMonth(Date),prc.GetDayOfMonth(Date),prc);
            string d = Date.ToString("yyyy/MM/dd",fair);
            //
            
            return "";
        }

    }
}
