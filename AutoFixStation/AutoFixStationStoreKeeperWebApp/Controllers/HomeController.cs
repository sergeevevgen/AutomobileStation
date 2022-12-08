using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.Enums;
using AutoFixStationContracts.ViewModels;
using AutoFixStationStoreKeeperWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoFixStationStoreKeeperWebApp.Controllers
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
            if (Program.StoreKeeper == null)
            {
                return Redirect("~/Home/Enter");
            }

            return
            View(APIStoreKeeper.GetRequest<List<WorkViewModel>>($"api/work/getworklist?storekeeperId={Program.StoreKeeper.Id}"));
        }

        //Проверка на авторизацию
        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.StoreKeeper == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.StoreKeeper);
        }

        //Проверка на авторизацию
        [HttpPost]
        public void Privacy(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password)
            && !string.IsNullOrEmpty(fio))
            {
                APIStoreKeeper.PostRequest("api/storekeeper/updatedata",
                new StoreKeeperBindingModel
                {
                    Id = Program.StoreKeeper.Id,
                    FIO = fio,
                    Login = login,
                    Password = password
                });
                Program.StoreKeeper.FIO = fio;
                Program.StoreKeeper.Login = login;
                Program.StoreKeeper.Password = password;
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
                Program.StoreKeeper =
                APIStoreKeeper.GetRequest<StoreKeeperViewModel>($"api/storekeeper/login?login={login}&password={password}");

                if (Program.StoreKeeper == null)
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
                APIStoreKeeper.PostRequest("api/storekeeper/register",
                new StoreKeeperBindingModel
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

        //Время работы
        [HttpGet]
        public IActionResult TimeOfWorks()
        {   
            return View(APIStoreKeeper.GetRequest<List<TimeOfWorkViewModel>>("api/work/gettimeofworklist"));
        }

        //Время работы (создание)
        [HttpGet]
        public IActionResult CreateTimeOfWork()
        {
            return View();
        }

        //Время работы (создание)
        [HttpPost]
        public void CreateTimeOfWork(int hours, int mins)
        {
            APIStoreKeeper.PostRequest("api/work/createorupdatetimeofwork", new TimeOfWorkBindingModel
            {
                Hours = hours,
                Mins = mins
            });
            Response.Redirect("TimeOfWorks");
        }

        //Время работы (изменение)
        [HttpGet]
        public IActionResult EditTimeOfWork(int timeofworkId)
        {
            return View(APIStoreKeeper.GetRequest<TimeOfWorkViewModel>($"api/work/gettimeofwork?timeofworkId={timeofworkId}"));
        }

        //Время работы (создание)
        [HttpPost]
        public void EditTimeOfWork(int timeofworkId, int hours, int mins)
        {
            APIStoreKeeper.PostRequest("api/work/createorupdatetimeofwork", new TimeOfWorkBindingModel
            {
                Id = timeofworkId,
                Hours = hours,
                Mins = mins
            });
            Response.Redirect("TimeOfWorks");
        }

        //Запчасти (вывод)
        [HttpGet]
        public IActionResult SpareParts()
        {
            return View(APIStoreKeeper.GetRequest<List<SparePartViewModel>>("api/sparepart/getsparepartlist"));
        }

        //Запчасти (создание)
        [HttpGet]
        public IActionResult CreateSparePart()
        {
            ViewBag.Types = Enum.GetValues(typeof(SparePartStatus))
                                    .Cast<SparePartStatus>()
                                    .ToList();
            ViewBag.UMs = Enum.GetValues(typeof(UnitMeasurement))
                                    .Cast<UnitMeasurement>()
                                    .ToList();
            return View();
        }

        //Запчасти (создание)
        [HttpPost]
        public void CreateSparePart(string sparepartname, string factorynumber, decimal price, string type, string um)
        {
            if (!string.IsNullOrEmpty(sparepartname) && !string.IsNullOrEmpty(factorynumber) && price >= 0
                && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(um))
            {
                APIStoreKeeper.PostRequest("api/sparepart/createorupdatesparepart", new SparePartBindingModel
                {
                    Name = sparepartname,
                    FactoryNumber = factorynumber,
                    Price = price,
                    Type = (SparePartStatus)Enum.Parse(typeof(SparePartStatus), type),
                    UMeasurement = (UnitMeasurement)Enum.Parse(typeof(UnitMeasurement), um)
                });
                Response.Redirect("SpareParts");
            }
            else
                throw new Exception("Введите данные");
        }

        //Запчасти (изменение)
        [HttpGet]
        public IActionResult EditSparePart(int sparepartId)
        {
            ViewBag.Types = Enum.GetValues(typeof(SparePartStatus))
                                    .Cast<SparePartStatus>()
                                    .ToList();
            ViewBag.UMs = Enum.GetValues(typeof(UnitMeasurement))
                                    .Cast<UnitMeasurement>()
                                    .ToList();
            return View(APIStoreKeeper.GetRequest<SparePartViewModel>($"api/sparepart/getsparepart?sparepartId={sparepartId}"));
        }

        //Запчасти (изменение)
        [HttpPost]
        public void EditSparePart(int sparepartId, string sparepartname, string factorynumber, decimal price, string type, string um)
        {
            if (!string.IsNullOrEmpty(sparepartname) && !string.IsNullOrEmpty(factorynumber) && price >= 0
                && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(um))
            {
                APIStoreKeeper.PostRequest("api/sparepart/createorupdatesparepart", new SparePartBindingModel
                {
                    Id = sparepartId,
                    Name = sparepartname,
                    FactoryNumber = factorynumber,
                    Price = price,
                    Type = (SparePartStatus)Enum.Parse(typeof(SparePartStatus), type),
                    UMeasurement = (UnitMeasurement)Enum.Parse(typeof(UnitMeasurement), um)
                });
                Response.Redirect("SpareParts");
            }
            else
                throw new Exception("Введите данные");
        }
    }
}