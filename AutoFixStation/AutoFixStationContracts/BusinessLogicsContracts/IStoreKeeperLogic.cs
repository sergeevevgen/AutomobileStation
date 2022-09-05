using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BusinessLogicsContracts
{
    public interface IStoreKeeperLogic
    {
        List<StoreKeeperViewModel> Read(StoreKeeperBindingModel model);
        void CreateOrUpdate(StoreKeeperBindingModel model);
        void Delete(StoreKeeperBindingModel model);
    }
}