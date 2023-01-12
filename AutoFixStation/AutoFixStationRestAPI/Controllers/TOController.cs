using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoFixStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TOController : ControllerBase
    {
        private readonly ITOLogic _tOLogic;
        private readonly IServiceRecordLogic _serviceRecordLogic;
        public TOController(ITOLogic tOLogic,
            IServiceRecordLogic serviceRecordLogic)
        {
            _tOLogic = tOLogic;
            _serviceRecordLogic = serviceRecordLogic;
        }

        /// <summary>
        /// Получение списка ТО для работника по его номеру (Id)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<TOViewModel> GetTOList(int employeeId)
            => _tOLogic
            .Read(new TOBindingModel { EmployeeId = employeeId });

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
        [HttpPost]
        public void TakeTOInWork(ChangeTOStatusBindingModel model)
            => _tOLogic.TakeTOInWork(model);

        /// <summary>
        /// Изменение статуса ТО на "Готово"
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public void FinishTO(ChangeTOStatusBindingModel model)
            => _tOLogic.FinishTO(model);

        /// <summary>
        /// Изменение статуса ТО на "Выдано"
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public void IssueTO(ChangeTOStatusBindingModel model)
            => _tOLogic.IssueTO(model);

        [HttpGet]
        //Получение записи
        public ServiceRecordViewModel GetServiceRecord(int servicerecordId)
        {
            return _serviceRecordLogic.Read(new ServiceRecordBindingModel
            {
                Id = servicerecordId
            })?[0];
        }

        [HttpPost]
        //Изменение записи
        public void UpdateServiceRecord(ServiceRecordBindingModel model)
        {
            _serviceRecordLogic.Update(model);
        }
    }
}
