using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AdomdServer = Microsoft.AnalysisServices.AdomdServer;

namespace RegularExpressions
{

}

namespace Cube_Utility
{
    public class FraudDetection
    {

        public static string PersianNToWord(double n)
        {
            return NumberToWord.HumanReadableInteger.NumberToText(n, NumberToWord.Language.Persian);
        }
        public static string EnglishNToWord(double n)
        {
            return NumberToWord.HumanReadableInteger.NumberToText(n, NumberToWord.Language.English);
        }
        public static int IranSSNCheck(string ssn)
        {
            //sanitized SSN
            ssn = Regex.Replace(ssn, "[^0-9]+", "", RegexOptions.Compiled);

            if (ssn.Count() <= 10)
                ssn = ssn.PadLeft(10, '0');
            else
                return 0;
            if (ssn == "0000000000" ||
                ssn == "1111111111" ||
                ssn == "2222222222" ||
                ssn == "3333333333" ||
                ssn == "4444444444" ||
                ssn == "5555555555" ||
                ssn == "6666666666" ||
                ssn == "7777777777" ||
                ssn == "8888888888" ||
                ssn == "9999999999")
                return 0;

            int n=0;
            for (int i = 0; i < 9; i++)
            {
                n += int.Parse(ssn[i].ToString()) * (10-i);
            }
            if((n % 11)>1)
                n=11 - (n % 11);
            else
                n=(n % 11);

            if (n == int.Parse(ssn[9].ToString()))
                return 1;
            return 0;
        }
    }
    class ValFreq
    {
        
        public double Val;
        public int Freq;
        
        public ValFreq(double val, int freq)
        {
            Val = val;
            Freq = freq;
        }
    }
    public class Statistics
    {
        private static ValFreq Mode(double[] InArr)
        {
            if (InArr.Count() == 0)
                return new ValFreq(0,0);
            Array.Sort(InArr);
            int i = InArr.Length;
            double SoFarMode = 0;
            int SoFarModeCnt = 0;
            double Cur = InArr[0];
            int CurCnt = 1;


            for (int j = 1; j < i; j++)
            {
                if (InArr[j] == Cur)
                {
                    CurCnt++;
                }
                else
                {
                    if (CurCnt > SoFarModeCnt)
                    {
                        SoFarModeCnt = CurCnt;
                        SoFarMode = Cur;
                    }
                    CurCnt = 1;
                    Cur = InArr[j];

                }

            }
            if (CurCnt > SoFarModeCnt)
            {
                SoFarModeCnt = CurCnt;
                SoFarMode = Cur;
            }
            return new ValFreq(SoFarMode, SoFarModeCnt);
        }
        public static Double ModeRatio(AdomdServer.Set Inset, AdomdServer.Expression inExp)
        {
            if (Inset.Tuples.Count == 0)
                return 0;
            double[] q = new double[Inset.Tuples.Count];
            
            int i = 0;
            foreach (AdomdServer.Tuple tu in Inset)
            {
                q[i] = inExp.Calculate(tu).ToDouble();
                i++;
            }
            return (((double)Mode(q).Freq)/((double)q.Length));
            
            
        }
        public static Double ModeCount(AdomdServer.Set Inset, AdomdServer.Expression inExp)
        {
            double[] q = new double[Inset.Tuples.Count];
            int i = 0;
            foreach (AdomdServer.Tuple tu in Inset)
            {
                q[i] = inExp.Calculate(tu).ToDouble();
                i++;
            }
            return Mode(q).Freq;
        }
        public static Double Mode (AdomdServer.Set Inset,AdomdServer.Expression inExp)
        {

            //AdomdServer.Tuple tu= Inset.GetEnumerator().Current;
            //AdomdServer.Expression ex=new AdomdServer.Expression(InMeasure.na);
            //AdomdServer.MDXValue.FromTuple(tu);

            //ex.Calculate(tu);
            double[] q = new double[Inset.Tuples.Count];
            int i = 0;            
            
            foreach (AdomdServer.Tuple tu in Inset)
            {
                q[i] = inExp.Calculate(tu).ToDouble();
                i++;
            }
            return Mode(q).Val;
            //Array.Sort(q);
            //double SoFarMode=0;
            //int SoFarModeCnt=0;
            //double Cur=q[0];
            //int CurCnt=1;
            

            //for (int j = 1; j < i; j++)
            //{
            //    if (q[j] == Cur)
            //    {
            //        CurCnt++;
            //    }
            //    else
            //    {
            //        if (CurCnt > SoFarModeCnt)
            //        {
            //            SoFarModeCnt = CurCnt;
            //            SoFarMode = Cur;
            //        }
            //    }
                
            //}
            //if (CurCnt > SoFarModeCnt)
            //{
            //    SoFarModeCnt = CurCnt;
            //    SoFarMode = Cur;
            //}
            //return SoFarMode;
        }
    }
    public class RegularEx
    {
        public static string Replace(string str, string regx, string repwith)
        {
            //"[^0-9]+" to replace anything but numbers (Except [] and - i'm not sure.)
            return Regex.Replace(str, regx, repwith, RegexOptions.Compiled);
        }
    }

    public class Utilities
    {
        public static void test()
        {
        }
    }
    public class DateUtilities
    {
        public static string ShDateToGDate(string shdate,string format="")
        {
            System.Globalization.PersianCalendar prc = new System.Globalization.PersianCalendar();
            DateTime date = new DateTime(Int32.Parse(shdate.Substring(0, 4)), Int32.Parse(shdate.Substring(5, 2)), Int32.Parse(shdate.Substring(8, 2)), prc);
            if (format == "")
                format = "yyyy/MM/dd";
            return date.ToString(format);
            
        }
        public static string GDateToShDate(DateTime date)
        {
            System.Globalization.PersianCalendar prc = new System.Globalization.PersianCalendar();
            string year = prc.GetYear(date).ToString();

            string month = (prc.GetMonth(date)).ToString();
            string day = (prc.GetDayOfMonth(date)).ToString();

            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            string shdate = year + "/" + month + "/" + day;
            return shdate;
        }

        public static string NDGDateToShDate(DateTime date, int n)
        {
            DateTime d = date.AddDays(n);
            return GDateToShDate(d);
        }
        public static string NDSysDateToShDate(int n)
        {
            return NDGDateToShDate(DateTime.Now, n);
        }

        public static string NowToShDateKey()
        {

            DateTime ndate = DateTime.Now;
            System.Globalization.PersianCalendar prc = new System.Globalization.PersianCalendar();
            string year = prc.GetYear(ndate).ToString();

            string month = (prc.GetMonth(ndate)).ToString();
            string day = (prc.GetDayOfMonth(ndate) - 1).ToString();

            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            string shdate = year + month + day;
            return shdate;
        }

        public static string NNowToShDateKey(int n)
        {

            DateTime ndate = DateTime.Now.AddDays(n);
            System.Globalization.PersianCalendar prc = new System.Globalization.PersianCalendar();
            string year = prc.GetYear(ndate).ToString();

            string month = (prc.GetMonth(ndate)).ToString();
            string day = (prc.GetDayOfMonth(ndate)).ToString();

            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            string shdate = year + month + day;
            return shdate;
        }
        public static int DateDiff(string FirstDate, string SecondDate)
        {
            return ShDateDiff(FirstDate, SecondDate);
/*
            if (FirstDate == null || FirstDate == "")
                FirstDate = @"1300/01/01";

            if (SecondDate == null || SecondDate == "")
                SecondDate = @"1300/01/01";

            int Year1 = Int32.Parse(FirstDate.Substring(0, 4));
            int Month1 = Int32.Parse(FirstDate.Substring(5, 2));
            int Day1 = Int32.Parse(FirstDate.Substring(8, 2));

            int Year2 = Int32.Parse(SecondDate.Substring(0, 4));
            int Month2 = Int32.Parse(SecondDate.Substring(5, 2));
            int Day2 = Int32.Parse(SecondDate.Substring(8, 2));

            return (Year2 - Year1) * 365 + (Month2 - Month1) * 30 + (Day2 - Day1);
*/
        }
        public static int ShDateDiff(string FirstDate, string SecondDate)
        {
            if (FirstDate == null || FirstDate == "" || SecondDate == null || SecondDate == "")
                return 0;
            System.Globalization.PersianCalendar prc = new System.Globalization.PersianCalendar();
            DateTime fdate = new DateTime(Int32.Parse(FirstDate.Substring(0, 4)), Int32.Parse(FirstDate.Substring(5, 2)), Int32.Parse(FirstDate.Substring(8, 2)), prc);
            DateTime sdate = new DateTime(Int32.Parse(SecondDate.Substring(0, 4)), Int32.Parse(SecondDate.Substring(5, 2)), Int32.Parse(SecondDate.Substring(8, 2)), prc);
            
            int diff = DateTime.Compare(fdate, sdate);
            TimeSpan ts = fdate.Subtract(sdate);
            diff = ts.Days;

            return diff;
        }
    }

}
