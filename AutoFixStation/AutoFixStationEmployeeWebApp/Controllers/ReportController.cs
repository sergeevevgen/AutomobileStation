using AutoFixStationBusinessLogic.Mail;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoFixStationEmployeeWebApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly MailKitWorker _mailKitWorker;

        public ReportController(ILogger<ReportController> logger, IWebHostEnvironment environment, MailKitWorker mailKitWorker)
        {
            _logger = logger;
            _environment = environment;
            _mailKitWorker = mailKitWorker;
        }

        public IActionResult ReportWordExcel()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIEmployee.GetRequest<List<TOViewModel>>($"api/to/GetTOListAllDone?employeeId={Program.Employee.Id}"));
        }

        [HttpPost]
        public IActionResult CreateReportTOSparePartsToWordFile(List<int> tosId)
        {
            if (tosId.Count != 0)
            {
                var model = new ReportBindingModel
                {
                    TOs = APIEmployee.GetRequest<List<TOViewModel>>($"api/to/gettos?tosId={tosId}")
                };

                model.FileName = @"..\AutoFixStationEmployeeWebApp\wwwroot\reports\ReportTOSparePart.doc";
                APIEmployee.PostRequest("api/report/CreateReportTOSparePartsToWordFile", model);
                var fileName = "ReportTOSparePart.doc";
                var filePath = _environment.WebRootPath + @"\reports\" + fileName;
                return PhysicalFile(filePath, "application/doc", fileName);
            }
            else
                throw new Exception("Выберите хотя бы одно ТО");
        }

        [HttpPost]
        public IActionResult CreateReportTOSparePartsToExcelFile(List<int> tosId)
        {
            if (tosId.Count != 0)
            {
                var model = new ReportBindingModel
                {
                    TOs = APIEmployee.GetRequest<List<TOViewModel>>($"api/to/gettos?tosId={tosId}")
                };

                model.FileName = @"..\AutoFixStationEmployeeWebApp\wwwroot\reports\ReportTOSparePart.xls";
                APIEmployee.PostRequest("api/report/CreateReportTOSparePartsToExcelFile", model);
                var fileName = "ReportTOSparePart.xls";
                var filePath = _environment.WebRootPath + @"\reports\" + fileName;
                return PhysicalFile(filePath, "application/xls", fileName);
            }
            else
                throw new Exception("Выберите хотя бы одно ТО");
        }

        public IActionResult ReportPDF()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ReportGetTOsPDF(DateTime dateFrom, DateTime dateTo)
        {
            ViewBag.Period = "C " + dateFrom.ToLongDateString() + " по " + dateTo.ToLongDateString();
            ViewBag.Report = APIEmployee.GetRequest<List<ReportTOsViewModel>>($"api/report/GetTOsReport?dateFrom={dateFrom.ToLongDateString()}&dateTo={dateTo.ToLongDateString()}");
            return View("ReportPdf");
        }

        [HttpPost]
        public IActionResult SendReportOnMail(DateTime dateFrom, DateTime dateTo)
        {
            var model = new ReportBindingModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            };
            model.FileName = @"..\AutoFixStationEmployeeWebApp\wwwroot\reports\ReportTOsPdf.pdf";
            APIEmployee.PostRequest("api/report/CreateReportTOsToPdfFile", model);
            _mailKitWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = /*Program.Employee.Login*/"",
                Subject = "Отчет по тех. осмотрам. СТО \"Руки-Крюки\"",
                Text = "Отчет по тех. осмотрам с " + dateFrom.ToShortDateString() + " по " + dateTo.ToShortDateString() +
                "\nРаботник - " + Program.Employee.FIO,
                FileName = model.FileName
            });
            ViewBag.Mail = Program.Employee.Login;
            return View();
        }
    }
}
