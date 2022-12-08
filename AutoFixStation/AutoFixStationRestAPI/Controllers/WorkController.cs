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
        private readonly ITimeOfWorkLogic _timeOfWorkLogic;
        private readonly ITOLogic _tOLogic;
        public WorkController(IWorkLogic workLogic,
            IWorkTypeLogic workTypeLogic,
            ITimeOfWorkLogic timeOfWorkLogic,
            ITOLogic tOLogic)
        {
            _workLogic = workLogic;
            _workTypeLogic = workTypeLogic;
            _timeOfWorkLogic = timeOfWorkLogic;
            _tOLogic = tOLogic;
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
        public List<WorkViewModel> GetWorkList(int storeKeeperId) 
            => _workLogic
            .Read(new WorkBindingModel { StoreKeeperId = storeKeeperId });

        /// <summary>
        /// Получение всех текущих работ кладовщика по его номеру (Id)
        /// </summary>
        /// <param name="storeKeeperId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<WorkViewModel> GetWorkListByEmployee(int employeeId)
        {
            var tolist = _tOLogic.Read(new TOBindingModel { EmployeeId = employeeId })?.ToList();
            var listworks = new List<WorkViewModel>();
            foreach (var i in tolist)
            {
                var elem = _workLogic.Read(new WorkBindingModel { TOId = i.Id });
                foreach(var item in elem)
                {
                    listworks.Add(item);
                }
            }
            return listworks;
        }
            

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

        /// <summary>
        /// Получение работы по номеру
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        [HttpGet]
        public WorkViewModel GetWork(int workId) => _workLogic
            .Read(new WorkBindingModel { Id = workId })?[0];


        //Получение листа времени выполнений
        [HttpGet]
        public List<TimeOfWorkViewModel> GetTimeOfWorkList() => 
            _timeOfWorkLogic.Read(null)?.ToList();

        //Получение времени выполнения по номеру
        [HttpGet]
        public TimeOfWorkViewModel GetTimeOfWork(int timeofworkId) =>
            _timeOfWorkLogic.Read(new TimeOfWorkBindingModel
            {
                Id = timeofworkId
            })?[0];

        //Получение времени выполнения по номеру
        [HttpPost]
        public void CreateOrUpdateTimeOfWork(TimeOfWorkBindingModel model) =>
            _timeOfWorkLogic.CreateOrUpdate(model);
    }
}