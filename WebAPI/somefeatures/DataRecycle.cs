using System;
using WebAPI.Models;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace WebAPI.somefeatures
{
    public class DataRecycle 
    {
        private QuantityOfEachPosition Positions;
        private const string QA = "QA";
        private const string Developer = "Developer";
        private const string TeamLead = "TeamLead";
        private const string UrlLink = "https://localhost:44342/api/WorkerHolidays";
        public DataRecycle()
        {
            Positions = new QuantityOfEachPosition();
        }
         
       
        private void Counter(string position)
        {
            switch (position)
            {
                case QA:
                    Positions.QA++;
                    break;
                case Developer:
                    Positions.Dev++;
                    break;
                case TeamLead:
                    Positions.TL++;
                    break;
                default: break;
            }
        }
        private void GetListOfHolidays(WorkerHoliday workerholiday)
        {
            WebRequest request = WebRequest.Create(UrlLink);
            using (WebResponse response = request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                reader.SupportMultipleContent = true;
                var serializer = new JsonSerializer();
                /* Хочу читать по кусочкам, а не целую порцию,
                 * ибо не факт, что я всегда буду получать маленькую 
                 * порцию данных.
                 */
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
                            Counter(tmpWorker.Position);

                            //Проверяем сотрудника, на предмет уже имеющегося отпуска в этом периоде
                            if (tmpWorker.PMId == workerholiday.PMId)
                                Positions.Selfself++;
                        }
                        else if ((workerholiday.DateStart <= parsedDateStart && parsedDateStart <= workerholiday.DateEnd)
                              || (workerholiday.DateStart <= parsedDateEnd && parsedDateEnd <= workerholiday.DateEnd))
                        {
                            Counter(tmpWorker.Position);
                            if (tmpWorker.PMId == workerholiday.PMId)
                                Positions.Selfself++;
                        }
                    }
                }
            }
        }

        //Проверяем по алгоритму, чтобы уточнить, не превысилось ли кол-во сотрудников
        //отправленных в отпуск, если метод даст true, значит лимит не превысился
        private bool IsPossibleToSendToHoliday(WorkerHoliday worker)
        {
            bool res = false;
            switch (worker.Position)
            {
                case QA:
                     if (Positions.Dev == 0&& Positions.Selfself== 0)
                        {
                            if (Positions.QA < 3)
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
                            if (Positions.QA < 1)
                            {
                                res = true;
                            }
                            else
                            {
                                res = false;
                            }
                        }
                    
                    break;
                case Developer:
                    if (Positions.TL == 0 && Positions.Selfself == 0)
                    {
                        if (Positions.QA < 2)
                        {
                            if (Positions.Dev < 2)
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
                            if (Positions.Dev == 0)
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
                case TeamLead:
                    if (Positions.Dev == 0 && Positions.Selfself == 0)
                    {
                        if (Positions.TL < 1)
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
                GetListOfHolidays(worker);
                if (IsPossibleToSendToHoliday(worker) == true)
                { res = true; }
            }

            return res;
        }
       
    }
}
