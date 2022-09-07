using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BusinessLogicsContracts
{
    public interface IServiceRecordLogic
    {
        List<ServiceRecordViewModel> Read(ServiceRecordBindingModel model);
        void CreateOrUpdate(ServiceRecordBindingModel record, TOBindingModel tO);
        void Delete(ServiceRecordBindingModel model);
    }
}
