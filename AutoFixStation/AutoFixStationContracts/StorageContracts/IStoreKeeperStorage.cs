using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.StorageContracts
{
    public interface IStoreKeeperStorage
    {
        List<StoreKeeperViewModel> GetFullList();
        List<StoreKeeperViewModel> GetFilteredList(StoreKeeperBindingModel model);

        StoreKeeperViewModel GetElement(StoreKeeperBindingModel model);

        void Insert(StoreKeeperBindingModel model);

        void Update(StoreKeeperBindingModel model);

        void Delete(StoreKeeperBindingModel model);
    }
}
