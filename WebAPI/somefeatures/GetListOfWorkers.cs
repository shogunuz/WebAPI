using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

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
        public Dictionary<int, Dictionary<string, string>> GetListOfHolidaysPublic()
        {
            return GetListOfHolidays();
        }
        }
}
