using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.somefeatures
{
    public class DateRecycle
    {

        private int numberOfMonth { get; set; } = 0;

        private List <int> currentWorker { get; set; }
        public static string RecyclingDate(string str)
        {
            string Sometmp;
            var charsToRemove = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu","Fri","Sat" };
            str = str.Remove(0, 4).Remove(12, 54).Remove(24);
            string[] time = str.Split(' ').ToArray();
          /* 
            Sometmp = months(time[0].ToString()); // (String.Join(time[0]))
            time[0] = time[1];
            time[1] = Sometmp;
            time[2] = time[2].ToString() + " /";

            Sometmp = months(time[4].ToString()); // (String.Join(time[0]))
            time[4] = time[5];
            time[5] = Sometmp;
            */
            str = string.Join(" ", time);
            return str.Trim();
        }
        private static string months(string month)
        {
            switch (month)
            {
                case "Jan":
                    month = "01";
                    break;
                case "Feb":
                    month = "02";
                    break;
                case "Mar":
                    month = "03";
                    break;
                case "Apr":
                    month = "04";
                    break;
                case "May":
                    month = "05";
                    break;
                case "Jun":
                    month = "06";
                    break;
                case "Jul":
                    month = "07";
                    break;
                case "Aug":
                    month = "08";
                    break;
                case "Sep":
                    month = "09";
                    break;
                case "Oct":
                    month = "10";
                    break;
                case "Nov":
                    month = "11";
                    break;
                case "Dec":
                    month = "12";
                    break;
                default:
                    break;
            }
            return month;
        }
    }
}
