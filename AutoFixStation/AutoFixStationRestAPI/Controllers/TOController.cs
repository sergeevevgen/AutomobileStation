using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoFixStationRestAPI.Controllers
{
    public class TOController : ControllerBase
    {
        private readonly ITOLogic _tOLogic;
        private readonly IWorkLogic _workLogic;
        private readonly IWorkTypeLogic _workTypeLogic;
        private readonly ISparePartLogic _sparePartLogic;
        private readonly ITimeOfWorkLogic _timeOfWorkLogic;

        public TOController(ITOLogic tOLogic, 
            IWorkLogic workLogic,
            IWorkTypeLogic workTypeLogic,
            ISparePartLogic sparePartLogic,
            ITimeOfWorkLogic timeOfWorkLogic)
        {
            _tOLogic = tOLogic;
            _workLogic = workLogic;
            _workTypeLogic = workTypeLogic;
            _sparePartLogic = sparePartLogic;
            _timeOfWorkLogic = timeOfWorkLogic;
        }

        /// <summary>
        /// Получение списка ТО для работника по его номеру (Id)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<TOViewModel> GetTOListByEmployee(int employeeId)
            => _tOLogic
            .Read(new TOBindingModel { EmployeeId = employeeId });

        /// <summary>
        /// Получение списка типов работ для выбора нужных при создании ТО 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<WorkTypeViewModel> GetWorkTypeList()
            => _workTypeLogic.Read(null);

        /// <summary>
        /// Получение определенного ТО по его номеру 
        /// (передается с помощью выбора из компонента)
        /// </summary>
        /// <param name="tOId"></param>
        /// <returns></returns>
        [HttpGet]
        public TOViewModel GetTO(int tOId)
            => _tOLogic.Read(new TOBindingModel { Id = tOId })?[0];

        /// <summary>
        /// Создание ТО
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public void CreateTO(CreateTOBindingModel model)
            => _tOLogic.CreateTO(model);

        /// <summary>
        /// Изменение статуса ТО на "Выполняется"
        /// </summary>
        /// <param name="model"></param>
        [HttpPatch]
        public void TakeTOInWork(ChangeTOStatusBindingModel model)
            => _tOLogic.TakeTOInWork(model);

        /// <summary>
        /// Изменение статуса ТО на "Готово"
        /// </summary>
        /// <param name="model"></param>
        [HttpPatch]
        public void FinishTO(ChangeTOStatusBindingModel model)
            => _tOLogic.FinishTO(model);

        /// <summary>
        /// Изменение статуса ТО на "Выдано"
        /// </summary>
        /// <param name="model"></param>
        [HttpPatch]
        public void IssueTO(ChangeTOStatusBindingModel model)
            => _tOLogic.IssueTO(model);
    }
}
