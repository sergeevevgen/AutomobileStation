using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.ViewModels
{
    public class ReportTOSparePartViewModel
    {
        public int TOId { get; set; }
        public string CarName { get; set; }
        public Dictionary<int, (string, decimal, decimal)> SpareParts { get; set; }
    }
}
