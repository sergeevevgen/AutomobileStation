using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.Enums;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;

namespace AutoFixStationBusinessLogic.BusinessLogics
{
    public class WorkLogic : IWorkLogic
    {
        private readonly IWorkStorage _workStorage;
        private readonly IWorkTypeStorage _workTypeStorage;
        private readonly ITimeOfWorkStorage _timeOfWorkStorage;
        private readonly ITOStorage _tOStorage;

        public WorkLogic(IWorkStorage workStorage,
            IWorkTypeStorage workTypeStorage,
            ITimeOfWorkStorage timeOfWorkStorage,
            ITOStorage tOStorage)
        {
            _workStorage = workStorage;
            _workTypeStorage = workTypeStorage;
            _timeOfWorkStorage = timeOfWorkStorage;
            _tOStorage = tOStorage;
        }

        public void CreateWork(CreateWorkBindingModel model)
        {
            _workStorage.Insert(new WorkBindingModel
            {
                StoreKeeperId = model.StoreKeeperId,
                WorkTypeId = model.WorkTypeId,
                TOId = model.TOId,
                WorkName = model.Name,
                Price = model.Price,
                NetPrice = model.NetPrice,
                WorkStatus = WorkStatus.Принят,
                Count = model.Count
            });
        }

        public void FinishWork(ChangeWorkStatusBindingModel model)
        {
            var work = _workStorage
                .GetElement(new WorkBindingModel { Id = model.WorkId });
            if (work == null)
            {
                throw new Exception("Услуга не найдена");
            }

            if (work.WorkStatus
                != Enum.GetName(typeof(WorkStatus), 1))
            {
                throw new Exception("Услуга не в статусе \"Выполняется\"");
            }

            var time = _timeOfWorkStorage.GetElement(new TimeOfWorkBindingModel
            {
                Id = _workTypeStorage
                    .GetElement(new WorkTypeBindingModel
                    { 
                        Id = work.WorkTypeId
                    }).TimeOfWorkId
            });

            DateTime dateTime = new DateTime(work.WorkBegin.Value.Year, work.WorkBegin.Value.Month, work.WorkBegin.Value.Day, time.Hours, time.Mins, 0);
            int time1 = (int) TimeSpan.FromTicks(dateTime.Ticks).TotalMinutes;
            int time2 = (int) TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMinutes;
            
            if (time2 < time1)
            {
                throw new Exception("Услуга ещё не выполнена");
            }

            _workStorage.Update(new WorkBindingModel
            {
                Id = work.Id,
                StoreKeeperId = work.StoreKeeperId,
                WorkTypeId = work.WorkTypeId,
                TOId = work.TOId,
                WorkName = work.WorkName,
                Price = work.Price,
                NetPrice = work.NetPrice,
                WorkStatus = WorkStatus.Готов,
                WorkBegin = work.WorkBegin,
                Count = work.Count
            });
        }

        public List<WorkViewModel> Read(WorkBindingModel model)
        {
            if (model == null)
            {
                return _workStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WorkViewModel> 
                {
                    _workStorage.GetElement(model)
                };
            }
            return _workStorage.GetFilteredList(model);
        }

        public void TakeWorkInWork(ChangeWorkStatusBindingModel model)
        {
            var work = _workStorage
                .GetElement(new WorkBindingModel { Id = model.WorkId });
            
            if (work == null)
            {
                throw new Exception("Услуга не найдена");
            }

            if (work.WorkStatus 
                != Enum.GetName(typeof(WorkStatus), 0))
            {
                throw new Exception("Услуга не в статусе \"Принят\"");
            }

            if (_tOStorage.GetElement(new TOBindingModel 
            {
                Id = work.TOId
            }).Status != Enum.GetName(typeof(TOStatus), 1))
            {
                throw new Exception("ТО #" + work.TOId + " не в статусе \"Выполняется\"");
            }

            _workStorage.Update(new WorkBindingModel
            {
                Id = work.Id,
                StoreKeeperId = work.StoreKeeperId,
                WorkTypeId = work.WorkTypeId,
                TOId = work.TOId,
                WorkName = work.WorkName,
                Price = work.Price,
                NetPrice= work.NetPrice,
                WorkStatus = WorkStatus.Выполняется,
                WorkBegin = DateTime.Now,
                Count = work.Count
            });
        }
    }
}
