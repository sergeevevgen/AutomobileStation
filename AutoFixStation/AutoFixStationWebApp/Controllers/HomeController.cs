using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using AutoFixStationWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoFixStationWebApp.Controllers
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

            return
            View(APIEmployee.GetRequest<List<TOViewModel>>($"api/to/gettolist?employeeId={Program.Employee.Id}"));
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
    }
}