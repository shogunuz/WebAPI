using WebAPI.Models;

namespace WebAPI.somefeatures
{
    public class IsPossibleAddNewHoliday
    {
        private GetListOfWorkersOnHoliday getList;

        public IsPossibleAddNewHoliday()
        {
            getList = new GetListOfWorkersOnHoliday();
        }
        
        //Проверяем по алгоритму, чтобы уточнить, не превысилось ли кол-во сотрудников
        //отправленных в отпуск, если метод даст true, значит лимит не превысился
        private bool IsPossibleToSendToHoliday(WorkerHoliday worker)
        {
            bool res = false;
            switch (worker.Position)
            {
                case PositionsStrings.QA:
                     if (getList.Positions.Dev == 0&& getList.Positions.Selfself== 0)
                        {
                            if (getList.Positions.QA < 3)
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
                            if (getList.Positions.QA < 1)
                            {
                                res = true;
                            }
                            else
                            {
                                res = false;
                            }
                        }
                    
                    break;
                case PositionsStrings.Dev:
                    if (getList.Positions.TL == 0 && getList.Positions.Selfself == 0)
                    {
                        if (getList.Positions.QA < 2)
                        {
                            if (getList.Positions.Dev < 2)
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
                            if (getList.Positions.Dev == 0)
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
                case PositionsStrings.TL:
                    if (getList.Positions.Dev == 0 && getList.Positions.Selfself == 0)
                    {
                        if (getList.Positions.TL < 1)
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
                getList.GetListOfWorkersPb(worker);
                if (IsPossibleToSendToHoliday(worker) == true)
                { res = true; }
            }

            return res;
        }
       
    }
}