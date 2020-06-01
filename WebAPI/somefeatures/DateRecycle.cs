using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.somefeatures
{
    public class DateRecycle
    {

        public int Qa { get; private set; }
        public int Dev { get; private set; }
        public int TL { get; private set; }
        public int Selfself { get; private set; } = 0;
        public int IdNumber { get; private set; }
        public int NumberOfWorkers { get; private set; } // 365

        private readonly GetListOfWorkers getListOfWorkers = new GetListOfWorkers();

        private Dictionary<int, Dictionary<string, string>> dictionary = new Dictionary<int, Dictionary<string, string>>();
      
       
        private void Schetchik(string position)
        {
            switch (position)
            {
                case "QA":
                    Qa++;
                    break;
                case "Developer":
                    Dev++;
                    break;
                case "TeamLead":
                    TL++;
                    break;
                default: break;
            }
        }
        private void CountingWorkers(WorkerHoliday workerHoliday)
        {
            int cnt = 0;
            for (int i = 0; i < NumberOfWorkers; i++)
            {
                cnt = Int32.Parse(dictionary[i]["PMId"]);
                DateTime parsedDateStart = DateTime.ParseExact((dictionary[i]["DateStart"]).ToString(), "MM/dd/yyyy HH:mm:ss", null);
                DateTime parsedDateEnd = DateTime.ParseExact((dictionary[i]["DateEnd"]).ToString(), "MM/dd/yyyy HH:mm:ss", null);
                
                if ((parsedDateStart <= workerHoliday.DateStart && workerHoliday.DateStart <= parsedDateEnd)
                   || (parsedDateStart <= workerHoliday.DateEnd && workerHoliday.DateEnd <= parsedDateEnd))
                {
                    Schetchik(dictionary[i]["Position"]);
                    if (cnt == workerHoliday.PMId)
                        Selfself++;
                }
                else if ((workerHoliday.DateStart <= parsedDateStart && parsedDateStart <= workerHoliday.DateEnd)
                 || (workerHoliday.DateStart <= parsedDateEnd && parsedDateEnd <= workerHoliday.DateEnd))
                {
                    Schetchik(dictionary[i]["Position"]);
                    if (cnt == workerHoliday.PMId)
                        Selfself++;
                }
            }
        }
        private bool Proverka(WorkerHoliday woker)
        {
            bool res = false;
            switch (woker.Position)
            {
                case "QA":
                    CountingWorkers(woker);
                     if (Dev == 0&&Selfself==0)
                        {
                            if (Qa < 3)
                            {
                                res = true;
                            }
                            else
                            {
                                res = false;
                            }
                        }
                        else
                        {
                            if (Qa < 1)
                            {
                                res = true;
                            }
                            else
                            {
                                res = false;
                            }
                        }
                    
                    break;
                case "Developer":
                    CountingWorkers(woker);
                    if (TL == 0 && Selfself == 0)
                    {
                        if (Qa < 2)
                        {
                            if (Dev < 2)
                            {
                                res = true;
                            }
                            else
                            {
                                res = false;
                            }
                        }
                        else
                        {
                            if (Dev == 0)
                            {
                                res = true;
                            }
                            else
                            {
                                res = false;
                            }
                        }
                    }
                    else
                    {
                        res = false;
                    }
                    break;
                case "TeamLead":
                    CountingWorkers(woker);
                    if (Dev == 0 && Selfself == 0)
                    {
                        if (TL < 1)
                        {
                            res = true;
                        }
                        else
                        {
                            res = false;
                        }
                    }
                    else
                    {
                        res = false;
                    }
                    break;
                default: break;
            }

            return res;
        }

        public bool HolidayCalc(WorkerHoliday woker)
        {
            bool res = false;
            try
            {
                dictionary = getListOfWorkers.GetListOfHolidays();
                NumberOfWorkers = getListOfWorkers.NumberOfWorkers;
            }
            catch (Exception) { }

            if (Proverka(woker)==true)
            { res = true; }

            return res;
        }

    }
}
