using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BusinessLogicsContracts
{
    public interface ITOLogic
    {
        List<TOViewModel> Read(TOBindingModel model);
        void CreateTO(CreateTOBindingModel model);
        void TakeTOInWork(ChangeTOStatusBindingModel model);
        void FinishTO(ChangeTOStatusBindingModel model);
        void IssueTO(ChangeTOStatusBindingModel model);
    }
}