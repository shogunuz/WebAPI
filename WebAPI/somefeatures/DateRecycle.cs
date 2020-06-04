using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.somefeatures
{
    public class DateRecycle
    {
        private int _idnumber;
        public int IdNumber { get => _idnumber; set => _idnumber = value; }
        private int NumberOfWorkers;
        private NumbersOfPositions numbersOfPositions = new NumbersOfPositions();

        private Dictionary<int, Dictionary<string, string>> dictionary = new Dictionary<int, Dictionary<string, string>>();
      
       
        private void Schetchik(string position)
        {
            switch (position)
            {
                case "QA":
                    numbersOfPositions.QA++;
                    break;
                case "Developer":
                    numbersOfPositions.Dev++;
                    break;
                case "TeamLead":
                    numbersOfPositions.TL++;
                    break;
                default: break;
            }
        }
        private void CountingWorkers(WorkerHoliday workerHoliday)
        {
            for (int i = 0; i < NumberOfWorkers; i++)
            {
                Int32.TryParse((dictionary[i]["PMId"]), out int cnt);
                DateTime parsedDateStart = DateTime.Parse(dictionary[i]["DateStart"]);
                DateTime parsedDateEnd = DateTime.Parse(dictionary[i]["DateEnd"]);

                if ((parsedDateStart <= workerHoliday.DateStart && workerHoliday.DateStart <= parsedDateEnd)
                   || (parsedDateStart <= workerHoliday.DateEnd && workerHoliday.DateEnd <= parsedDateEnd))
                {
                    /*
                     * Каждый раз включаю счётчик чтобы в итоге знать сколько всего сотрудников
                     * отправлено на отпуск в том периоде, в который собираемся добавить текущего(нового)
                     * сотрудника.
                     */
                    Schetchik(dictionary[i]["Position"]);
                    
                    //Проверяем добавили ли сотрудника, которого уже отправили в отпуск в этом периоде
                    if (cnt == workerHoliday.PMId)
                        numbersOfPositions.Selfself++;
                }
                else if ((workerHoliday.DateStart <= parsedDateStart && parsedDateStart <= workerHoliday.DateEnd)
                 || (workerHoliday.DateStart <= parsedDateEnd && parsedDateEnd <= workerHoliday.DateEnd))
                {
                    Schetchik(dictionary[i]["Position"]);
                    if (cnt == workerHoliday.PMId)
                        numbersOfPositions.Selfself++;
                }
            }
        }
        private bool Proverka(WorkerHoliday worker)
        {
            bool res = false;
            switch (worker.Position)
            {
                case "QA":
                    CountingWorkers(worker);
                     if (numbersOfPositions.Dev == 0&& numbersOfPositions.Selfself== 0)
                        {
                            if (numbersOfPositions.QA < 3)
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
                            if (numbersOfPositions.QA < 1)
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
                    CountingWorkers(worker);
                    if (numbersOfPositions.TL == 0 && numbersOfPositions.Selfself == 0)
                    {
                        if (numbersOfPositions.QA < 2)
                        {
                            if (numbersOfPositions.Dev < 2)
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
                            if (numbersOfPositions.Dev == 0)
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
                    CountingWorkers(worker);
                    if (numbersOfPositions.Dev == 0 && numbersOfPositions.Selfself == 0)
                    {
                        if (numbersOfPositions.TL < 1)
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
            GetListOfWorkers getListOfWorkers = new GetListOfWorkers();

            try
            {
                dictionary = getListOfWorkers.GetListOfHolidaysPublic();
                NumberOfWorkers = getListOfWorkers.NumberOfWorkers;
            }
            catch (Exception) { }

            if (Proverka(worker) ==true)
            { res = true; }

            getListOfWorkers = null;//обрываем ссылку на объект getListOfWorkers

            return res;
        }

    }
}
