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
    public class StoreKeeperLogic : IStoreKeeperLogic
    {
        private readonly IStoreKeeperStorage _storeKeeperStorage;

        public StoreKeeperLogic(IStoreKeeperStorage storeKeeperStorage)
        {
            _storeKeeperStorage = storeKeeperStorage;
        }

        public void CreateOrUpdate(StoreKeeperBindingModel model)
        {
            var element = _storeKeeperStorage.GetElement(new StoreKeeperBindingModel
            {
                Login = model.Login,
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть кладовщик с таким логином");
            }

            if (model.Id.HasValue)
            {
                _storeKeeperStorage.Update(model);
            }
            else
            {
                _storeKeeperStorage.Insert(model);
            }
        }

        public void Delete(StoreKeeperBindingModel model)
        {
            var element = _storeKeeperStorage.GetElement(new StoreKeeperBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _storeKeeperStorage.Delete(model);
        }

        public List<StoreKeeperViewModel> Read(StoreKeeperBindingModel model)
        {
            if (model == null)
            {
                return _storeKeeperStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<StoreKeeperViewModel>()
                {
                    _storeKeeperStorage.GetElement(model) 
                };
            }

            return _storeKeeperStorage.GetFilteredList(model);
        }
    }
}
