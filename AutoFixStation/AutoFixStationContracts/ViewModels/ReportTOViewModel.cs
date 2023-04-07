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

        public string WorkName { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public decimal Sum { get; set; }

        public Dictionary<int, (string, decimal, decimal)> SpareParts { get; set; }
    }
}
