using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cube_Utility;


namespace DebugerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string str = "";
            str = Cube_Utility.DateUtilities.NDSysDateToShDate(0);

            int n=0;
            //str = Cube_Utility.Utilities.NDSysDateToShDate(-2);
            //str=Cube_Utility.RegularEx.Replace(str,"/","");
            string t = Cube_Utility.StringUtilities.to_char(DateTime.Now, "yyyy/MM/dd", "fa-ir");
            string d = Cube_Utility.DateUtilities.ShDateToGDate("1391/09/25");
            string con = "Provider=OraOLEDB.Oracle.1;Password=jtest;Persist Security Info=True;User ID=jtest;Data Source=192.168.101.217/dwsa;";
            string sql = "select * from dimdate";
            string res = Cube_Utility.SQLQuery.ExecuteSQL_SingleString(con, sql);

            n = Cube_Utility.FraudDetection.IranSSNCheck("0532266439");
            string s="";
            s=NumberToWord.HumanReadableInteger.NumberToText(1987867765654543, NumberToWord.Language.Persian);
            Console.WriteLine(s);
            string fdate = "1391/01/10";
            string sdate = "1391/01/01";
            Console.WriteLine(n);
            n = Cube_Utility.DateUtilities.ShDateDiff(fdate,sdate );
            n = Cube_Utility.DateUtilities.DateDiff(fdate, sdate);
           // Console.WriteLine(str);



        }
    }
}
