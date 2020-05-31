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
        public string RecyclingDate(string str)
        {
            str = str.Remove(0, 4).Remove(12, 54).Remove(24);
            //string[] time = str.Split(' ').ToArray();
          /* 
            Sometmp = months(time[0].ToString()); // (String.Join(time[0]))
            time[0] = time[1];
            time[1] = Sometmp;
            time[2] = time[2].ToString() + " /";

            Sometmp = months(time[4].ToString()); // (String.Join(time[0]))
            time[4] = time[5];
            time[5] = Sometmp;
            */
           // str = string.Join(" ", time);
            return str.Trim();
        }
        
    }
}
