using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.StorageContracts
{
    public interface ITimeOfWorkStorage
    {
        List<TimeOfWorkViewModel> GetFullList();
        List<TimeOfWorkViewModel> GetFilteredList(TimeOfWorkBindingModel model);

        TimeOfWorkViewModel GetElement(TimeOfWorkBindingModel model);

        void Insert(TimeOfWorkBindingModel model);

        void Update(TimeOfWorkBindingModel model);

        void Delete(TimeOfWorkBindingModel model);
    }
}
