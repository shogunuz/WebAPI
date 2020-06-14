using System;
using WebAPI.Models;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace WebAPI.somefeatures
{
    public class DateRecycle 
    {
        private QuantityOfEachPosition quantityOfEachPosition;
    
        public DateRecycle()
        {
            quantityOfEachPosition = new QuantityOfEachPosition();
        }
        ~DateRecycle()
        {
        }
         
       
        private void Schetchik(string position)
        {
            switch (position)
            {
                case "QA":
                    quantityOfEachPosition.QA++;
                    break;
                case "Developer":
                    quantityOfEachPosition.Dev++;
                    break;
                case "TeamLead":
                    quantityOfEachPosition.TL++;
                    break;
                default: break;
            }
        }
        private void GetListOfHolidaysTew(WorkerHoliday workerholiday)
        {
            WebRequest request = WebRequest.Create("https://localhost:44342/api/WorkerHolidays");
            using (WebResponse response = request.GetResponse())
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
                        WorkerHoliday tmpWorker = serializer.Deserialize<WorkerHoliday>(reader);

                        DateTime parsedDateStart = DateTime.Parse((tmpWorker.DateStart).ToString());
                        DateTime parsedDateEnd = DateTime.Parse((tmpWorker.DateEnd).ToString());

                       if ((parsedDateStart <= workerholiday.DateStart && workerholiday.DateStart <= parsedDateEnd)
                        || (parsedDateStart <= workerholiday.DateEnd && workerholiday.DateEnd <= parsedDateEnd))
                        {
                            /*
                            * Каждый раз включаю счётчик чтобы в итоге знать сколько всего сотрудников
                            * отправлено на отпуск в том периоде, в который собираемся добавить текущего(нового)
                            * сотрудника.
                            */
                            Schetchik(tmpWorker.Position);

                            //Проверяем сотрудника, на предмет уже имеющегося отпуска в этом периоде
                            if (tmpWorker.PMId == workerholiday.PMId)
                                quantityOfEachPosition.Selfself++;
                        }
                        else if ((workerholiday.DateStart <= parsedDateStart && parsedDateStart <= workerholiday.DateEnd)
                              || (workerholiday.DateStart <= parsedDateEnd && parsedDateEnd <= workerholiday.DateEnd))
                        {
                            Schetchik(tmpWorker.Position);
                            if (tmpWorker.PMId == workerholiday.PMId)
                                quantityOfEachPosition.Selfself++;
                        }
                    }
                }
            }
        }

        //Проверяем по алгоритму, чтобы уточнить, не превысилось ли кол-во сотрудников
        //отправленных в отпуск, если метод даст true, значит лимит не превысился
        private bool Proverka(WorkerHoliday worker)
        {
            bool res = false;
            switch (worker.Position)
            {
                case "QA":
                     if (quantityOfEachPosition.Dev == 0&& quantityOfEachPosition.Selfself== 0)
                        {
                            if (quantityOfEachPosition.QA < 3)
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
                            if (quantityOfEachPosition.QA < 1)
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
                    if (quantityOfEachPosition.TL == 0 && quantityOfEachPosition.Selfself == 0)
                    {
                        if (quantityOfEachPosition.QA < 2)
                        {
                            if (quantityOfEachPosition.Dev < 2)
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
                            if (quantityOfEachPosition.Dev == 0)
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
                    if (quantityOfEachPosition.Dev == 0 && quantityOfEachPosition.Selfself == 0)
                    {
                        if (quantityOfEachPosition.TL < 1)
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

        public bool HolidayCalc(WorkerHoliday worker)
        {
            bool res = false;

            if(worker is WorkerHoliday)
            {
                GetListOfHolidaysTew(worker);
                if (Proverka(worker) == true)
                { res = true; }
            }

            return res;
        }
       
    }
}
