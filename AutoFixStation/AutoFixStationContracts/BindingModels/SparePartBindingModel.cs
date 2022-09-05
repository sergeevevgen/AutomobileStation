﻿using AutoFixStationContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BindingModels
{
    public class SparePartBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string FactoryNumber { get; set; }

        public decimal Price { get; set; }

        public SparePartStatus Type { get; set; }

        public UnitMeasurement UMeasurement { get; set; }
    }
}
