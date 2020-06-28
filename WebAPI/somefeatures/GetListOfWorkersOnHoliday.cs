using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WebAPI.somefeatures
{
    public class GetListOfWorkersOnHoliday
    {
        
        public QuantityOfEachPosition Positions { get; private set; }

        public GetListOfWorkersOnHoliday()
        {
            Positions = new QuantityOfEachPosition();
        }
        private void GetListOfWorkersPr(WorkerHoliday workerholiday)
        {
            WebRequest request = WebRequest.Create(ConstStrings.UrlLink);
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

       public void GetListOfWorkersPb(WorkerHoliday workerholiday)
        {
            GetListOfWorkersPr(workerholiday);
        }
        private void Counter(string position)
        {
            switch (position)
            {
                case ConstStrings.QA:
                    Positions.QA++;
                    break;
                case ConstStrings.Dev:
                    Positions.Dev++;
                    break;
                case ConstStrings.TL:
                    Positions.TL++;
                    break;
                default: break;
            }
        }
    }
}
