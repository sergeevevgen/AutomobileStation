using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.Enums
{
    public enum UnitMeasurement
    {
        [Display(Name = "ШТ")]
        шт = 0,
        [Display(Name = "КГ")]
        кг = 1,
        [Display(Name = "Л")]
        л = 2
    }
}
