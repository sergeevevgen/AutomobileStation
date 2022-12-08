using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoFixStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreKeeperController : ControllerBase
    {
        private readonly IStoreKeeperLogic _logic;
        public StoreKeeperController(IStoreKeeperLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public StoreKeeperViewModel Login(string login, string password)
        {
            var list = _logic.Read(new StoreKeeperBindingModel
            {
                Login = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpGet]
        public List<StoreKeeperViewModel> GetStoreKeeperList() =>
            _logic.Read(null);

        [HttpPost]
        public void Register(StoreKeeperBindingModel model) =>
            _logic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(StoreKeeperBindingModel model) =>
            _logic.CreateOrUpdate(model);

        [HttpPost]
        public void Delete(StoreKeeperBindingModel model) =>
            _logic.Delete(model);
    }
}