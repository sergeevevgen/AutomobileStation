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
    public class ServiceRecordLogic : IServiceRecordLogic
    {
        private readonly IServiceRecordStorage _storage;

        public ServiceRecordLogic(IServiceRecordStorage storage)
        {
            _storage = storage;
        }

        public void CreateOrUpdate(ServiceRecordBindingModel model)
        {
            var element = _storage.GetElement(new ServiceRecordBindingModel
            {
                CarId = model.CarId,
                DateBegin = model.DateBegin
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такая запись");
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

        public void Delete(ServiceRecordBindingModel model)
        {
            var element = _storage.GetElement(new ServiceRecordBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _storage.Delete(model);
        }

        public List<ServiceRecordViewModel> Read(ServiceRecordBindingModel model)
        {
            if (model == null)
            {
                return _storage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<ServiceRecordViewModel>() 
                {
                    _storage.GetElement(model)
                };
            }

            return _storage.GetFilteredList(model);
        }
    }
}
