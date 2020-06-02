using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using WebAPI.Models;

namespace WebAPI.somefeatures
{
    public class GetListOfWorkers
    {
        public int NumberOfWorkers { get; private set; } // 365
        
        private Dictionary<int, Dictionary<string, string>> GetListOfHolidays()
        {
            Dictionary<int, Dictionary<string, string>> dictionary = new Dictionary<int, Dictionary<string, string>>();
            WebRequest request = WebRequest.Create("https://localhost:44342/api/WorkerHolidays");
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                var objects = JsonConvert.DeserializeObject<List<object>>(reader.ReadToEnd());
                var result = objects.Select(obj => JsonConvert.SerializeObject(obj)).ToArray();
                NumberOfWorkers = result.Length;
                JObject jsonObj;
                for (int i = 0; i < result.Length; i++)
                {
                    jsonObj = JObject.Parse(result[i]);
                    Dictionary<string, string> dictObj = jsonObj.ToObject<Dictionary<string, string>>();
                    dictionary.Add(i, new Dictionary<string, string>(dictObj));
                }
            }
            response.Close();
            return dictionary;
        }
        private Dictionary<int, Dictionary<string, string>> GetListOfHolidaysTew()
        {
            Dictionary<int, Dictionary<string, string>> dictionary = new Dictionary<int, Dictionary<string, string>>();
            WebRequest request = WebRequest.Create("https://localhost:44342/api/WorkerHolidays");
            WebResponse response = request.GetResponse();
            int i = 0;
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
                        dictionary.Add(i, new Dictionary<string, string>
                        {
                            ["PMId"] = (worker.PMId).ToString(),
                            ["IdForH"] = (worker.IdForH).ToString(),
                            ["FIO"] = (worker.FIO),
                            ["Position"] = (worker.Position),
                            ["DateStart"] = (worker.DateStart).ToString(),
                            ["DateEnd"] = (worker.DateEnd).ToString()
                        });
                        i++;
                        NumberOfWorkers++;
                    }
                }
            }
            response.Close();
            return dictionary;
        }

        public Dictionary<int, Dictionary<string, string>> GetListOfHolidaysPublic()
        {
            return GetListOfHolidaysTew();
        }
    }
}
