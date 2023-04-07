using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationContracts.ViewModels;

namespace AutoFixStationContracts.BindingModels
{
    public class ReportSparePartBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<WorkTypeViewModel> WorkTypes { get; set; }
    }
}
