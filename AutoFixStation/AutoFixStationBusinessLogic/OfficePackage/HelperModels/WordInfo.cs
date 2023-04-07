using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationContracts.ViewModels;
using System.Collections.Generic;

namespace AutoFixStationBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportWorkTypeSPViewModel> WorkTypeSP { get; set; }
    }
}
