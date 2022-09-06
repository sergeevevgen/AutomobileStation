using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;

namespace AutoFixStationBusinessLogic.BusinessLogics
{
    public class TimeOfWorkLogic : ITimeOfWorkLogic
    {
        private readonly ITimeOfWorkStorage _storage;

        public TimeOfWorkLogic(ITimeOfWorkStorage storage)
        {
            _storage = storage;
        }

        public void CreateOrUpdate(TimeOfWorkBindingModel model)
        {
            var element = _storage.GetElement(new TimeOfWorkBindingModel
            {
                Hours = model.Hours,
                Mins = model.Mins
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть время выполнения с такими параметрами");
            }

            if (model.Id.HasValue)
            {
                _storage.Update(model);
            }
            else
            {
                _storage.Insert(model);
            }
        }

        public void Delete(TimeOfWorkBindingModel model)
        {
            var element = _storage.GetElement(new TimeOfWorkBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _storage.Delete(model);
        }

        public List<TimeOfWorkViewModel> Read(TimeOfWorkBindingModel model)
        {
            if (model == null)
            {
                return _storage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<TimeOfWorkViewModel>()
                {
                    _storage.GetElement(model)
                };
            }

            return _storage.GetFilteredList(model);
        }
    }
}