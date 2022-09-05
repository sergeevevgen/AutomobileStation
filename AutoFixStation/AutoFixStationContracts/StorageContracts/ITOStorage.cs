using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.StorageContracts
{
    public interface ITOStorage
    {
        List<TOViewModel> GetFullList();
        List<TOViewModel> GetFilteredList(TOBindingModel model);

        TOViewModel GetElement(TOBindingModel model);

        void Insert(TOBindingModel model);

        void Update(TOBindingModel model);

        void Delete(TOBindingModel model);
    }
}
