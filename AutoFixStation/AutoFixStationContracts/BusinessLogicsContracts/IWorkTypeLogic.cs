﻿using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BusinessLogicsContracts
{
    public interface IWorkTypeLogic
    {
        List<WorkTypeViewModel> Read(WorkTypeBindingModel model);
        void CreateOrUpdate(WorkTypeBindingModel model);
        void Delete(WorkTypeBindingModel model);
    }
}