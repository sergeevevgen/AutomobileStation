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
        private readonly IEmployeeStorage _employeeStorage;
        private readonly IWorkTypeStorage _workTypeStorage;

        public ServiceRecordLogic(IServiceRecordStorage storage,
            IEmployeeStorage employeeStorage,
            IWorkTypeStorage workTypeStorage)
        {
            _storage = storage;
            _employeeStorage = employeeStorage;
            _workTypeStorage = workTypeStorage;
        }

        public void CreateOrUpdate(ServiceRecordBindingModel record,
            TOBindingModel tO)
        {
            var element = _storage.GetElement(new ServiceRecordBindingModel
            {
                CarId = record.CarId,
                DateBegin = record.DateBegin
            });

            if (element != null && element.Id != record.Id)
            {
                throw new Exception("Уже есть такая запись");
            }
            record.DateBegin = tO.DateCreate;
            record.DateEnd = tO.DateOver.Value;

            string empName = _employeeStorage
                .GetElement(new EmployeeBindingModel { Id = tO.EmployeeId }).FIO;
            string worksStr = "Мастер, проделавший работы: \n" 
                + empName + "\nПроведенные работы:\n";

            int i = 1;
            foreach(var work in tO.Works)
            {
                worksStr += i + ") " + work.Value.Item1 + ". В количестве " + work.Value.Item1 + "\n";
                i++;
            }

            var neededParts = new Dictionary<string, decimal>();
            foreach(var workType in _workTypeStorage.GetFullList())
            {
                foreach(var work in tO.Works)
                {
                    if (work.Value.Item1.Equals(workType.WorkName))
                    {
                        foreach (var parts in workType.WorkSpareParts.Values)
                        {
                            neededParts[parts.Item1] += parts.Item2;
                        }
                    }
                }
            }

            string sparePartsStr = "Использованные детали и компоненты: \n";
            i = 1;
            foreach(var part in neededParts)
            {
                sparePartsStr += i + ") " + part.Key + " В количестве " + part.Value;
                i++;
            }

            record.Description = worksStr + sparePartsStr;

            if (record.Id.HasValue)
            {
                _storage.Update(record);
            }
            else
            {
                _storage.Insert(record);
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