using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.StorageContracts
{
    public interface ISparePartStorage
    {
        List<SparePartViewModel> GetFullList();
        List<SparePartViewModel> GetFilteredList(SparePartBindingModel model);

        SparePartViewModel GetElement(SparePartBindingModel model);

        void Insert(SparePartBindingModel model);

        void Update(SparePartBindingModel model);

        void Delete(SparePartBindingModel model);
    }
}
