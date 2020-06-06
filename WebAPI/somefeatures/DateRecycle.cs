﻿using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.somefeatures
{
    public class DateRecycle
    {
        private QuantityOfEachPosition quantityOfEachPosition;

        private Dictionary<int, Dictionary<string, string>> dictionary;
        public Dictionary<int, Dictionary<string, string>> Dict
        {
            get => dictionary;
            private set => dictionary = value;
        }
        public DateRecycle()
        {
            quantityOfEachPosition = new QuantityOfEachPosition();
            dictionary = new Dictionary<int, Dictionary<string, string>>();
            Dict = new Dictionary<int, Dictionary<string, string>>();
        }

        private int _numberOfWorkersOnHoliday;
        private int NumberOfWorkersOnHoliday
        {
            get => _numberOfWorkersOnHoliday;
            set => _numberOfWorkersOnHoliday = value;
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
        private void CountingWorkers(WorkerHoliday workerHoliday)
        {
            for (int i = 0; i < NumberOfWorkersOnHoliday; i++)
            {
                Int32.TryParse((Dict[i]["PMId"]), out int cnt);
                DateTime parsedDateStart = DateTime.Parse(Dict[i]["DateStart"]);
                DateTime parsedDateEnd = DateTime.Parse(Dict[i]["DateEnd"]);

                if ((parsedDateStart <= workerHoliday.DateStart && workerHoliday.DateStart <= parsedDateEnd)
                   || (parsedDateStart <= workerHoliday.DateEnd && workerHoliday.DateEnd <= parsedDateEnd))
                {
                    /*
                     * Каждый раз включаю счётчик чтобы в итоге знать сколько всего сотрудников
                     * отправлено на отпуск в том периоде, в который собираемся добавить текущего(нового)
                     * сотрудника.
                     */
                    Schetchik(Dict[i]["Position"]);
                    
                    //Проверяем добавили ли сотрудника, которого уже отправили в отпуск в этом периоде
                    if (cnt == workerHoliday.PMId)
                        quantityOfEachPosition.Selfself++;
                }
                else if ((workerHoliday.DateStart <= parsedDateStart && parsedDateStart <= workerHoliday.DateEnd)
                 || (workerHoliday.DateStart <= parsedDateEnd && parsedDateEnd <= workerHoliday.DateEnd))
                {
                    Schetchik(Dict[i]["Position"]);
                    if (cnt == workerHoliday.PMId)
                        quantityOfEachPosition.Selfself++;
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
                    CountingWorkers(worker);
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
                    CountingWorkers(worker);
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
                    CountingWorkers(worker);
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
            GetListOfWorkers getListOfWorkers = new GetListOfWorkers();

            try
            {
                Dict = getListOfWorkers.GetListOfHolidaysPublic();
                NumberOfWorkersOnHoliday = getListOfWorkers.NumberOfWorkersOnHoliday;
            }
            catch (Exception) { }

            if (Proverka(worker) ==true)
            { res = true; }

            getListOfWorkers = null;//обрываем ссылку на объект getListOfWorkers

            return res;
        }

    }
}
