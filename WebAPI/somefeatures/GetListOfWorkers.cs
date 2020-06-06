using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WebAPI.Models;

namespace WebAPI.somefeatures
{
    public class GetListOfWorkers
    {
        public GetListOfWorkers()
        {
            NumberOfWorkersOnHoliday = 0;
        }
        private int numberOfWorkers;
        public int NumberOfWorkersOnHoliday
        {
            get { return numberOfWorkers; }
            set
            {
                numberOfWorkers = value;
            }
        }
        private Dictionary<int, Dictionary<string, string>> GetListOfHolidaysTew()
        {
            Dictionary<int, Dictionary<string, string>> dictionary = new Dictionary<int, Dictionary<string, string>>();
            WebRequest request = WebRequest.Create("https://localhost:44342/api/WorkerHolidays");
            using(WebResponse response = request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                reader.SupportMultipleContent = true;
                var serializer = new JsonSerializer();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        WorkerHoliday worker = serializer.Deserialize<WorkerHoliday>(reader);
                        dictionary.Add(NumberOfWorkersOnHoliday, new Dictionary<string, string>
                        {
                            ["PMId"] = (worker.PMId).ToString(),
                            ["IdForH"] = (worker.IdForH).ToString(),
                            ["FIO"] = (worker.FIO),
                            ["Position"] = (worker.Position),
                            ["DateStart"] = (worker.DateStart).ToString(),
                            ["DateEnd"] = (worker.DateEnd).ToString()
                        });
                        NumberOfWorkersOnHoliday++;
                    }
                }
            }
            return dictionary;
        }

        public Dictionary<int, Dictionary<string, string>> GetListOfHolidaysPublic()
        {
            return GetListOfHolidaysTew();
        }
    }
}
