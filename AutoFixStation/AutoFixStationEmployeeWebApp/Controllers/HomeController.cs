using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using AutoFixStationWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoFixStationEmployeeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Главная страница веб-сайта
        public IActionResult Index()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        //Тех. осмотры
        public IActionResult TOs()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIEmployee.GetRequest<List<TOViewModel>>($"api/to/gettolist?employeeId={Program.Employee.Id}"));
        }

        //Проверка на авторизацию
        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Employee);
        }

        //Проверка на авторизацию
        [HttpPost]
        public void Privacy(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password)
            && !string.IsNullOrEmpty(fio))
            {
                APIEmployee.PostRequest("api/employee/updatedata",
                new EmployeeBindingModel
                {
                    Id = Program.Employee.Id,
                    FIO = fio,
                    Login = login,
                    Password = password
                });
                Program.Employee.FIO = fio;
                Program.Employee.Login = login;
                Program.Employee.Password = password;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        //Ошибка
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [HttpGet]
        public IActionResult Exit()
        {
            Program.Employee = null;
            return Redirect("~/Home/Enter");
        }

        //Вход в веб-сайт
        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        //Вход в веб-сайт
        [HttpPost]
        public void Enter(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Program.Employee =
                APIEmployee.GetRequest<EmployeeViewModel>($"api/employee/login?login={login}&password={password}");

                if (Program.Employee == null)
                {
                    throw new Exception("Неверный логин/пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        //Регистрация
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Регистрация
        [HttpPost]
        public void Register(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password)
            && !string.IsNullOrEmpty(fio))
            {
                APIEmployee.PostRequest("api/employee/register",
                new EmployeeBindingModel
                {
                    FIO = fio,
                    Login = login,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }


        //Просмотр инфы о ТО (его статуса)
        [HttpGet]
        public IActionResult TOInfo(int toId)
        {
            TOViewModel tO = APIEmployee.GetRequest<TOViewModel>($"api/to/getto?toId={toId}");
            return View(tO);
        }

        //Создание ТО
        [HttpGet]
        public IActionResult CreateTO()
        {
            ViewBag.Cars = APIEmployee.GetRequest<List<CarViewModel>>("api/car/carlist");
            return View();
        }

        //Создание ТО
        [HttpPost]
        public void CreateTO(int carId)
        {
            APIEmployee.PostRequest("api/to/createto", new CreateTOBindingModel
            {
                CarId = carId,
                EmployeeId = Program.Employee.Id,
                Sum = null,
                Works = null
            });
            Response.Redirect("Index");
        }

        //Просмотр инфо об услуге
        [HttpGet]
        public IActionResult WorkInfo(int workId)
        {
            WorkViewModel work = APIEmployee.GetRequest<WorkViewModel>($"api/work/getwork?workId={workId}");
            WorkTypeViewModel workType = APIEmployee.GetRequest<WorkTypeViewModel>($"api/work/getworktype?worktypeId={work.WorkTypeId}");
            ViewBag.Status = work.WorkStatus;
            ViewBag.WorkBegin = work.WorkBegin;
            ViewBag.Count = work.Count;
            return View(workType);
        }

        //Просмотр автомобилей
        [HttpGet]
        public IActionResult Cars()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIEmployee.GetRequest<List<CarViewModel>>("api/car/carlist"));
        }

        //Добавление клиента/автомобиля
        [HttpGet]
        public IActionResult AddClient()
        {
            return View();
        }

        //Добавление клиента/автомобиля
        [HttpPost]
        public void AddClient(string brand, string model, string VIN, string number)
        {
            if (!string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(VIN) 
                && !string.IsNullOrEmpty(number))
            {
                APIEmployee.PostRequest("api/car/createorupdatecar",
                    new CarBindingModel
                    {
                        Brand = brand,
                        Model = model,
                        VIN = VIN,
                        OwnerPhoneNumber = number
                    });
                Response.Redirect("Cars");
            }
            else
                throw new Exception("Введите данные");
        }

        //Изменение клиента/автомобиля
        [HttpGet]
        public IActionResult EditCar(int carId)
        {
            return View(APIEmployee.GetRequest<CarViewModel>($"api/car/getcar?carId={carId}"));
        }

        //Добавление клиента/автомобиля
        [HttpPost]
        public void EditCar(int carId, string brand, string model, string VIN, string number)
        {
            if (!string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(VIN)
                && !string.IsNullOrEmpty(number))
            {
                CarViewModel carViewModel = APIEmployee.GetRequest<CarViewModel>($"api/car/getcar?carId={carId}");
                APIEmployee.PostRequest("api/car/createorupdatecar",
                    new CarBindingModel
                    {
                        Id = carId,
                        Brand = brand,
                        Model = model,
                        VIN = VIN,
                        OwnerPhoneNumber = number,
                        Records = carViewModel.Records
                    });
                Response.Redirect("Cars");
            }
            else
                throw new Exception("Введите данные");
        }

        //Подсчёт стоимости
        [HttpPost]
        public decimal[] Calc(decimal count, int type)
        {
            WorkTypeViewModel workType =
            APIEmployee.GetRequest<WorkTypeViewModel>($"api/work/getworktype?worktypeId={type}");
            decimal[] t = new decimal[2];
            t[0] = workType.Price * count;
            t[1] = workType.NetPrice * count;
            return t;
        }

        //Создание услуги
        [HttpGet]
        public IActionResult CreateWork()
        {
            List<WorkTypeViewModel> listtypes = APIEmployee
                .GetRequest<List<WorkTypeViewModel>>("api/work/getworktypelist");
            List<TOViewModel> listto = APIEmployee
                .GetRequest<List<TOViewModel>>($"api/to/gettolistinstart?employeeId={Program.Employee.Id}");
            List<StoreKeeperViewModel> listkeep = APIEmployee
                .GetRequest<List<StoreKeeperViewModel>>($"api/storekeeper/getstorekeeperlist");
            ViewBag.TOs = listto;
            ViewBag.WorkTypes = listtypes;
            ViewBag.StoreKeepers = listkeep;
            return View();
        }

        //Создание услуги
        [HttpPost]
        public void CreateWork(int toId, int workTypeId, int storeKeeperId, int count, decimal price, decimal netprice)
        {
            if (count == 0 || price == 0 || netprice == 0)
            {
                return;
            }
            APIEmployee.PostRequest("api/work/creatework", 
                new CreateWorkBindingModel
                {
                    TOId = toId,
                    WorkTypeId = workTypeId,
                    StoreKeeperId = storeKeeperId,
                    Count = count,
                    Price = price,
                    NetPrice = netprice
                });
            Response.Redirect("Index");
        }

        //Просмотр услуг
        [HttpGet]
        public IActionResult Works()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIEmployee.GetRequest<List<WorkViewModel>>($"api/work/getworklistbyemployee?employeeId={Program.Employee.Id}"));
        }

        //Изменение статуса ТО
        [HttpGet]
        public IActionResult ActionWithTO(int toId)
        {
            var tO = APIEmployee.GetRequest<TOViewModel>($"api/to/getto?toId={toId}");
            if (tO.Status.Equals("Принят"))
            {
                ViewBag.Action = "Выполняется";
            }
            else if (tO.Status.Equals("Выполняется"))
            {
                ViewBag.Action = "Готов";
            }
            else if (tO.Status.Equals("Готов"))
            {
                ViewBag.Action = "Выдан";
            }

            return View(tO);
        }

        //Действие с услугой
        [HttpPost]
        public void ActionWithTO(int toId, string action)
        {
            if (action.Equals("Выполняется"))
            {
                APIEmployee.PostRequest($"api/to/taketoinwork", new ChangeTOStatusBindingModel
                {
                    TOId = toId
                });
            }
            else if (action.Equals("Готов"))
            {
                APIEmployee.PostRequest($"api/to/finishto", new ChangeTOStatusBindingModel
                {
                    TOId = toId
                });
            }
            else if (action.Equals("Выдан"))
            {
                APIEmployee.PostRequest($"api/to/issueto", new ChangeTOStatusBindingModel
                {
                    TOId = toId
                });
            }
            Response.Redirect("Index");
        }

        //Изменение записи
        [HttpGet]
        public IActionResult EditRecord(int recordId)
        {
            return View(APIEmployee.GetRequest<ServiceRecordViewModel>($"api/to/getservicerecord?servicerecordId={recordId}"));
        }

        //Изменение записи
        [HttpPost]
        public void EditRecord(int recordId, string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                var record = APIEmployee.GetRequest<ServiceRecordViewModel>($"api/to/getservicerecord?servicerecordId={recordId}");
                APIEmployee.PostRequest("api/to/updateservicerecord", new ServiceRecordBindingModel
                {
                    Id = recordId,
                    Description = description,
                    CarId = record.CarId,
                    DateBegin = record.DateBegin,
                    DateEnd = record.DateEnd
                });
                Response.Redirect("Cars");
            }
        }
    }
}