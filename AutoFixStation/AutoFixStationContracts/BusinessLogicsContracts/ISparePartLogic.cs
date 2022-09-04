using STOContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BusinessLogicsContracts
{
    public interface ISparePartLogic
    {
        List<SparePartViewModel> Read(SparePartBindingModel model);
        void CreateOrUpdate(SparePartBindingModel model);
        void Delete(SparePartBindingModel model);

    }
}
