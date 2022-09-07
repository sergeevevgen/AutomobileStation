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
    public class SparePartLogic : ISparePartLogic
    {
        private readonly ISparePartStorage _storage;

        public SparePartLogic(ISparePartStorage storage)
        {
            _storage = storage;
        }

        public void CreateOrUpdate(SparePartBindingModel model)
        {
            var element = _storage.GetElement(new SparePartBindingModel
            {
                Name = model.Name
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть деталь с таким названием");
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

        public void Delete(SparePartBindingModel model)
        {
            var element = _storage.GetElement(new SparePartBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _storage.Delete(model);
        }

        public List<SparePartViewModel> Read(SparePartBindingModel model)
        {
            if (model == null)
            {
                return _storage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<SparePartViewModel>
                { 
                    _storage.GetElement(model) 
                };
            }

            return _storage.GetFilteredList(model);
        }
    }
}