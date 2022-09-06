using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationBusinessLogic.BusinessLogics
{
    public class EmployeeLogic : IEmployeeLogic
    {

        private readonly IEmployeeStorage _employeeStorage;

        public EmployeeLogic(IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void CreateOrUpdate(EmployeeBindingModel model)
        {
            var element = _employeeStorage.GetElement(new EmployeeBindingModel
            {
                Login = model.Login,
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть работник с таким логином");
            }

            if (model.Id.HasValue)
            {
                _employeeStorage.Update(model);
            }
            else
            {
                _employeeStorage.Insert(model);
            }
        }

        public void Delete(EmployeeBindingModel model)
        {
            var element = _employeeStorage.GetElement(new EmployeeBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _employeeStorage.Delete(model);
        }

        public List<EmployeeViewModel> Read(EmployeeBindingModel model)
        {
            if (model == null)
            {
                return _employeeStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<EmployeeViewModel>() 
                { 
                    _employeeStorage.GetElement(model)
                };
            }

            return _employeeStorage.GetFilteredList(model);
        }
    }
}