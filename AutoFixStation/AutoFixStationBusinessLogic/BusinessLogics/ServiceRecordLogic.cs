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

        public void Create(ServiceRecordBindingModel record,
            TOBindingModel tO)
        {
            var element = _storage.GetElement(new ServiceRecordBindingModel
            {
                CarId = record.CarId
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
                worksStr += i + ") " + work.Value.Item1 + ". В количестве " + work.Value.Item2.Item1 + "\n";
                i++;
            }

            var neededParts = new Dictionary<string, (decimal, decimal)>();
            foreach (var work in tO.Works)
            {
                var worktype = _workTypeStorage.GetElement(new WorkTypeBindingModel { WorkName = work.Value.Item1 });
                foreach (var parts in worktype.WorkSpareParts)
                {
                    if (!neededParts.ContainsKey(parts.Value.Item1))
                        neededParts.Add(parts.Value.Item1, (parts.Value.Item2, parts.Value.Item3));
                    else
                    {
                        neededParts[parts.Value.Item1] = (neededParts[parts.Value.Item1].Item1 + parts.Value.Item2, parts.Value.Item3);
                    }
                }
            }

            string sparePartsStr = "Использованные детали и компоненты: \n";
            i = 1;
            foreach(var part in neededParts)
            {
                sparePartsStr += i + ") " + part.Key + ". В количестве " + part.Value.Item1 + ". Стоимость за ед. " + (int)part.Value.Item2 + "\n";
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

        public void Update(ServiceRecordBindingModel model)
        {
            var element = _storage.GetElement(new ServiceRecordBindingModel
            {
                CarId = model.CarId
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такая запись");
            }

            _storage.Update(model);
        }
    }
}