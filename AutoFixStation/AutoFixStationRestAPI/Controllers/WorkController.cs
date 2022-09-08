using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoFixStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkLogic _workLogic;
        private readonly IWorkTypeLogic _workTypeLogic;
        private readonly ISparePartLogic _sparePartLogic;
        private readonly ITimeOfWorkLogic _timeOfWorkLogic;

        public WorkController(IWorkLogic workLogic,
            IWorkTypeLogic workTypeLogic,
            ISparePartLogic sparePartLogic,
            ITimeOfWorkLogic timeOfWorkLogic)
        {
            _workLogic = workLogic;
            _workTypeLogic = workTypeLogic;
            _sparePartLogic = sparePartLogic;
            _timeOfWorkLogic = timeOfWorkLogic;
        }

        /// <summary>
        /// Получение списка типов работ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<WorkTypeViewModel> GetWorkTypeList()
            => _workTypeLogic.Read(null)?.ToList();

        /// <summary>
        /// Получение определенного типа работы по Id 
        /// (передался из селектед лист с помощью выбора
        /// из всех типов работ)
        /// </summary>
        /// <param name="workTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        public WorkTypeViewModel GetWorkType(int workTypeId) 
            => _workTypeLogic
            .Read(new WorkTypeBindingModel { Id = workTypeId })?[0];

        /// <summary>
        /// Получение всех текущих работ кладовщика по его номеру (Id)
        /// </summary>
        /// <param name="storeKeeperId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<WorkViewModel> GetWorks(int storeKeeperId) 
            => _workLogic
            .Read(new WorkBindingModel { StoreKeeperId = storeKeeperId });

        /// <summary>
        /// Создание работы
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public void CreateWork(CreateWorkBindingModel model)
            => _workLogic.CreateWork(model);

        /// <summary>
        /// Изменение статуса работы на "Выполняется"
        /// </summary>
        /// <param name="model"></param>
        [HttpPatch]
        public void TakeWorkInWork(ChangeWorkStatusBindingModel model)
            => _workLogic.TakeWorkInWork(model);

        /// <summary>
        /// Изменение статуса работы на "Готово"
        /// </summary>
        /// <param name="model"></param>
        [HttpPatch]
        public void FinishWork(ChangeWorkStatusBindingModel model)
            => _workLogic.FinishWork(model);
    }
}