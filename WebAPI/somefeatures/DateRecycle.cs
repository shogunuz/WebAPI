using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.somefeatures
{
    public class DateRecycle
    {

        public int qa { get; private set; }
        public int dev { get; private set; }
        public int tm { get; private set; }
        public int numberOfWorkers { get; private set; } // 365
        private string workers { get; set; }

        private Dictionary<int, Dictionary<string, string>> dictionary = new Dictionary<int, Dictionary<string, string>>();
        public string WorkerHolidaysGetRequest()
        {
            WebRequest request = WebRequest.Create("https://localhost:44342/api/WorkerHolidays");
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                workers = responseFromServer;
            }
            response.Close();

            return workers;
        }
        private void getDictOfH(string jsonInput)
        {
            Console.WriteLine(jsonInput + "\n\n");
            var objects = JsonConvert.DeserializeObject<List<object>>(jsonInput);
            var result = objects.Select(obj => JsonConvert.SerializeObject(obj)).ToArray();
            numberOfWorkers = result.Length;
            JObject jsonObj;
            for (int i = 0; i < numberOfWorkers; i++)
            {
                jsonObj = JObject.Parse(result[i]);
                Dictionary<string, string> dictObj = jsonObj.ToObject<Dictionary<string, string>>();
                dictionary.Add(i, new Dictionary<string, string>(dictObj));
            }

        }
        private void schetchik(string position)
        {
            switch (position)
            {
                case "QA":
                    qa++;
                    break;
                case "Developer":
                    dev++;
                    break;
                case "TeamLead":
                    tm++;
                    break;
                default: break;
            }
        }
        private void countingWorkers(WorkerHoliday workerHoliday)
        {
            for (int i = 0; i < numberOfWorkers; i++)
            {
                DateTime parsedDateStart = DateTime.ParseExact((dictionary[i]["DateStart"]).ToString(), "MM/dd/yyyy HH:mm:ss", null);
                DateTime parsedDateEnd = DateTime.ParseExact((dictionary[i]["DateEnd"]).ToString(), "MM/dd/yyyy HH:mm:ss", null);

                DateTime parsedDateStartWorker = DateTime.ParseExact((workerHoliday.DateStart).ToString(), "MM/dd/yyyy HH:mm:ss", null);
                DateTime parsedDateEndWorker = DateTime.ParseExact((workerHoliday.DateEnd).ToString(), "MM/dd/yyyy HH:mm:ss", null);

                if ((parsedDateStart <= parsedDateStartWorker && parsedDateStartWorker <= parsedDateEnd)
                   || (parsedDateStart <= parsedDateEndWorker && parsedDateEndWorker <= parsedDateEnd))
                {
                    schetchik(dictionary[i]["Position"]);
                }
            }
        }
        private bool psevd(WorkerHoliday woker)
        {
            bool res = false;
            switch (woker.Position)
            {
                case "QA":
                    countingWorkers(woker);
                    if (dev < 1)
                    {
                        if (qa < 4)
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
                        if (qa < 2)
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
                    countingWorkers(woker);
                    if (tm < 1)
                    {
                        if (qa < 2)
                        {
                            if (dev < 3)
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
                            if (dev < 1)
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
                    countingWorkers(woker);
                    if (dev < 1)
                    {
                        if (tm < 2)
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
            getDictOfH(WorkerHolidaysGetRequest());

            if (psevd(woker))
            { res = true; }

            return res;
        }

    }
}
