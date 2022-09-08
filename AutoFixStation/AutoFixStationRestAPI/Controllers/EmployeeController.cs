using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoFixStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeLogic _logic;
        public EmployeeController(IEmployeeLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public EmployeeViewModel Login(string login, string password)
        {
            var list = _logic.Read(new EmployeeBindingModel
            {
                Login = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(EmployeeBindingModel model) =>
            _logic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(EmployeeBindingModel model) =>
            _logic.CreateOrUpdate(model);

        [HttpPost]
        public void Delete(EmployeeBindingModel model) =>
            _logic.Delete(model);
    }
}