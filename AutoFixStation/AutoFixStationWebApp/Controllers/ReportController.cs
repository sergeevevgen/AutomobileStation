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
            return View(APIEmployee.GetRequest<List<TOViewModel>>($"api/to/gettolist?employeeId={Program.Employee.Id}"));
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

                model.FileName = @"..\AutoFixStationEmployeeWebApp\wwwroot\reports\ReportTOSparePart.docx";
                APIEmployee.PostRequest("api/report/CreateReportTOSparePartsToWordFile", model);
                var fileName = "ReportTOSparePart.docx";
                var filePath = _environment.WebRootPath + @"\reports\" + fileName;
                return PhysicalFile(filePath, "application/docx", fileName);
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

                model.FileName = @"..\AutoFixStationEmployeeWebApp\wwwroot\reports\ReportTOSparePart.xlsx";
                APIEmployee.PostRequest("api/report/CreateReportTOSparePartsToExcelFile", model);
                var fileName = "ReportTOSparePart.xlsx";
                var filePath = _environment.WebRootPath + @"\reports\" + fileName;
                return PhysicalFile(filePath, "application/xlsx", fileName);
            }
            else
                throw new Exception("Выберите хотя бы одно ТО");
        }

        public IActionResult ReportPDF()
        {
            if (Program.Clerk == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ReportGetClientsPDF(DateTime dateFrom, DateTime dateTo)
        {
            ViewBag.Period = "C " + dateFrom.ToLongDateString() + " по " + dateTo.ToLongDateString();
            ViewBag.Report = APIClerk.GetRequest<List<ReportClientsViewModel>>($"api/report/GetClientsReport?dateFrom={dateFrom.ToLongDateString()}&dateTo={dateTo.ToLongDateString()}");
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
            model.FileName = @"..\BankClerkApp\wwwroot\ReportClientCurrency\ReportClientsPdf.pdf";
            APIClerk.PostRequest("api/report/CreateReportClientsToPdfFile", model);
            _mailKitWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = Program.Clerk.Email,
                Subject = "Отчет по клиентам. Банк \"Вы банкрот\"",
                Text = "Отчет по клиентам с " + dateFrom.ToShortDateString() + " по " + dateTo.ToShortDateString() +
                "\nРуководитель - " + Program.Clerk.ClerkFIO,
                FileName = model.FileName
            });
            return View();
        }
    }
}
