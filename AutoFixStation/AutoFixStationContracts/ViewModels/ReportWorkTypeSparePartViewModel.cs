using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.ViewModels
{
    public class ReportWorkTypeSparePartViewModel
    {
        public string WorkTypeName { get; set; }

        public int TotalCount { get; set; }

        public List<Tuple<string, decimal>> SpareParts { get; set; }
    }
}
