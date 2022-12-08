using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoFixStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarLogic _logic;
        public CarController(ICarLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public List<CarViewModel> CarList() => _logic.Read(null)?.ToList();

        [HttpGet]
        public CarViewModel GetCar(int carId) => _logic
            .Read(new CarBindingModel { Id = carId })?[0];

        [HttpPost]
        public void CreateOrUpdateCar(CarBindingModel car) => _logic.CreateOrUpdate(car);
    }
}
