using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoFixStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SparePartController : ControllerBase
    {
        private readonly ISparePartLogic _logic;
        public SparePartController(ISparePartLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public List<SparePartViewModel> GetSparePartList() => _logic.Read(null)?.ToList();

        [HttpGet]
        public SparePartViewModel GetSparePart(int sparepartId) => _logic
            .Read(new SparePartBindingModel { Id = sparepartId })?[0];

        [HttpPost]
        public void CreateOrUpdateSparePart(SparePartBindingModel model) => _logic.CreateOrUpdate(model);
    }
}
