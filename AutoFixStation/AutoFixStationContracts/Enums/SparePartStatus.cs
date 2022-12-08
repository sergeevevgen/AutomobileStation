using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.Enums
{
    public enum SparePartStatus
    {
        [Display(Name = "Б/У")]
        БУ = 0,
        [Display(Name = "Новая")]
        Новая = 1
    }
}
