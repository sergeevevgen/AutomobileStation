using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoFixStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;
        public ReportController(IReportLogic reportLogic)
        {
            _reportLogic = reportLogic;
        }

        [HttpPost]
        public void CreateReportTOSparePartsToWordFile(ReportBindingModel model) => _reportLogic.SaveTOSparePartToWordFile(model);

        [HttpPost]
        public void CreateReportTOSparePartsToExcelFile(ReportBindingModel model) => _reportLogic.SaveTOSparePartToExcelFile(model);

        [HttpPost]
        public void CreateReportTOsToPdfFile(ReportBindingModel model) => _reportLogic.SaveTOsByDateToPdfFile(model);

        [HttpGet]
        public List<ReportTOsViewModel> GetTOsReport(string dateFrom, string dateTo) => _reportLogic.GetTOs(new ReportBindingModel { DateFrom = Convert.ToDateTime(dateFrom), DateTo = Convert.ToDateTime(dateTo) });
    }
}
