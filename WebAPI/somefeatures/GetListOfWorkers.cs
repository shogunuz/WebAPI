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

        //Делаю запрос в БД чтобы получить json данные о сотрудниках, отправленных на отпуск
        private string GetStringOfH()
        {
            string workers;
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

        //Обрабатываю string, отделяю пользователей и добавляю в словарь словарей.
        private Dictionary<int, Dictionary<string, string>> CalcDictOfH(string jsonInput)
        {
            Dictionary<int, Dictionary<string, string>> dictionary = new Dictionary<int, Dictionary<string, string>>();
            Console.WriteLine(jsonInput + "\n\n");
            var objects = JsonConvert.DeserializeObject<List<object>>(jsonInput);
            var result = objects.Select(obj => JsonConvert.SerializeObject(obj)).ToArray();
            NumberOfWorkers = result.Length;
            JObject jsonObj;
            for (int i = 0; i < NumberOfWorkers; i++)
            {
                jsonObj = JObject.Parse(result[i]);
                Dictionary<string, string> dictObj = jsonObj.ToObject<Dictionary<string, string>>();
                dictionary.Add(i, new Dictionary<string, string>(dictObj));
            }
            return dictionary;
        }
        public Dictionary<int, Dictionary<string, string>> GetListOfHolidays()
        {
            return CalcDictOfH(GetStringOfH());
        }

    }
}
