﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationBusinessLogic.OfficePackage.HelperEnums;

namespace AutoFixStationBusinessLogic.OfficePackage.HelperModels
{
    public class WordTextProperties
    {
        public string Size { get; set; }

        public bool Bold { get; set; }

        public WordJustificationType JustificationType { get; set; }
    }
}
