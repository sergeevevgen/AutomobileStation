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
    public class WorkTypeLogic : IWorkTypeLogic
    {
        private readonly IWorkTypeStorage _typeStorage;

        public WorkTypeLogic(IWorkTypeStorage typeStorage)
        {
            _typeStorage = typeStorage;
        }

        public void CreateOrUpdate(WorkTypeBindingModel model)
        {
            var element = _typeStorage.GetElement(new WorkTypeBindingModel
            {
                WorkName = model.WorkName
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть тип работ с таким названием");
            }

            if (model.Id.HasValue)
            {
                _typeStorage.Update(model);
            }
            else
            {
                _typeStorage.Insert(model);
            }
        }

        public void Delete(WorkTypeBindingModel model)
        {
            var element = _typeStorage.GetElement(new WorkTypeBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _typeStorage.Delete(model);
        }

        public List<WorkTypeViewModel> Read(WorkTypeBindingModel model)
        {
            if (model == null)
            {
                return _typeStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<WorkTypeViewModel> 
                {
                    _typeStorage.GetElement(model)
                };
            }

            return _typeStorage.GetFilteredList(model);
        }
    }
}