using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportTOSparePartViewModel> TOSpareParts { get; set; }
        public List<ReportWorkTypeSparePartViewModel> WorkTypeSpareParts { get; set; }
    }
}