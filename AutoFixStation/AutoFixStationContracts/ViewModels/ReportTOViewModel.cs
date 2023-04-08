using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.ViewModels
{
    public class ReportTOViewModel
    {
        public int TOId { get; set; }
        public string CarName { get; set; }
        public string CarId { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public Dictionary<int, (string, decimal, decimal)> SpareParts { get; set; }

        public string sParts;
        public List<string> ServiceRecords { get; set; }
    }
}
