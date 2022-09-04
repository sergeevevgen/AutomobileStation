using STOContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BusinessLogicsContracts
{
    public interface ITimeOfWorkLogic
    {
        List<TimeOfWorkViewModel> Read(TimeOfWorkBindingModel model);
        void CreateOrUpdate(TimeOfWorkBindingModel model);
        void Delete(TimeOfWorkBindingModel model);

    }
}
