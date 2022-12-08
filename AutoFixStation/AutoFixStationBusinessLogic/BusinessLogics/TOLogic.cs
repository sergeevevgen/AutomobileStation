using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.Enums;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;

namespace AutoFixStationBusinessLogic.BusinessLogics
{
    public class TOLogic : ITOLogic
    {
        private readonly ITOStorage _tOStorage;
        private readonly IWorkStorage _workStorage;

        public TOLogic(ITOStorage tOStorage,
            IWorkStorage workStorage)
        {
            _tOStorage = tOStorage;
            _workStorage = workStorage;
        }

        public void CreateTO(CreateTOBindingModel model)
        {
            _tOStorage.Insert(new TOBindingModel
            {
                CarId = model.CarId,
                EmployeeId = model.EmployeeId,
                Sum = model.Sum.HasValue ? model.Sum.Value : 0,
                Status = TOStatus.Принят,
                DateCreate = DateTime.Now,
                Works = model.Works
            });
        }

        public void FinishTO(ChangeTOStatusBindingModel model)
        {
            var tO = _tOStorage.GetElement(new TOBindingModel
            {
                Id = model.TOId
            });

            if (tO == null)
            {
                throw new Exception("ТО не найдено");
            }

            if (tO.Status != Enum.GetName(typeof(TOStatus), 1))
            {
                throw new Exception("ТО не в статусе \"Выполняется\"");
            }
            bool flag = true;
            foreach (var stat in _workStorage.GetFilteredList(new WorkBindingModel
                {
                    TOId = tO.Id
                }))
            {
                if (stat.WorkStatus != Enum.GetName(typeof(WorkStatus), 2))
                {
                    flag = false;
                }
            }

            if (!flag)
            {
                throw new Exception("Не все работы в статусе \"Готов\".");
            }

            _tOStorage.Update(new TOBindingModel
            {
                Id = tO.Id,
                CarId = tO.CarId,
                EmployeeId = tO.EmployeeId,
                Sum = tO.Sum.HasValue ? tO.Sum.Value : 0,
                Status = TOStatus.Готов,
                DateCreate = tO.DateCreate,
                DateImplement = tO.DateImplement,
                DateOver = tO.DateOver,
                Works = tO.Works
            });
        }

        public void IssueTO(ChangeTOStatusBindingModel model)
        {
            var tO = _tOStorage.GetElement(new TOBindingModel
            {
                Id = model.TOId
            });

            if (tO == null)
            {
                throw new Exception("ТО не найдено");
            }

            if (tO.Status != Enum.GetName(typeof(TOStatus), 2))
            {
                throw new Exception("ТО не в статусе \"Готов\"");
            }

            _tOStorage.Update(new TOBindingModel
            {
                Id = tO.Id,
                CarId = tO.CarId,
                EmployeeId = tO.EmployeeId,
                Sum = tO.Sum.HasValue ? tO.Sum.Value : 0,
                Status = TOStatus.Выдан,
                DateCreate = tO.DateCreate,
                DateImplement = tO.DateImplement,
                DateOver = tO.DateOver,
                Works = tO.Works
            });
        }

        public List<TOViewModel> Read(TOBindingModel model)
        {
            if (model == null)
            {
                return _tOStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<TOViewModel> 
                { 
                    _tOStorage.GetElement(model)
                };
            }

            return _tOStorage.GetFilteredList(model);
        }

        public void TakeTOInWork(ChangeTOStatusBindingModel model)
        {
            var tO = _tOStorage.GetElement(new TOBindingModel
            {
                Id = model.TOId
            });

            if (tO == null)
            {
                throw new Exception("ТО не найдено");
            }

            if (tO.Status != Enum.GetName(typeof(TOStatus), 0))
            {
                throw new Exception("ТО не в статусе \"Принят\"");
            }
            
            _tOStorage.Update(new TOBindingModel
            {
                Id = tO.Id,
                CarId = tO.CarId,
                EmployeeId = tO.EmployeeId,
                Sum = tO.Sum.HasValue ? tO.Sum.Value : 0,
                Status = TOStatus.Выполняется,
                DateCreate = tO.DateCreate,
                DateImplement = tO.DateImplement,
                Works = tO.Works
            });
        }
    }
}
