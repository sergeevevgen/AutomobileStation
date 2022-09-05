using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BusinessLogicsContracts
{
    public interface IWorkLogic
    {
        List<WorkViewModel> Read(WorkBindingModel model);
        void CreateWork(CreateWorkBindingModel model);
        void TakeWorkInWork(ChangeWorkStatusBindingModel model);
        void FinishWork(ChangeWorkStatusBindingModel model);
    }
}