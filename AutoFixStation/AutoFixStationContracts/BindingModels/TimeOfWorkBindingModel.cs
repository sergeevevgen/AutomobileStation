﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BindingModels
{
    public class TimeOfWorkBindingModel
    {
        public int? Id { get; set; }
        public int Hours { get; set; }
        public int Mins { get; set; }
    }
}
