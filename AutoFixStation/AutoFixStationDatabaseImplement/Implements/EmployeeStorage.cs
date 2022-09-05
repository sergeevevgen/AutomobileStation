using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;
using AutoFixStationDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationDatabaseImplement.Implements
{
    public class EmployeeStorage : IEmployeeStorage
    {
        public void Delete(EmployeeBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            Employee employee = context.Employees
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public EmployeeViewModel GetElement(EmployeeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            var employee = context.Employees
                .Include(rec => rec.TOs)
                .FirstOrDefault(rec => rec.Login == model.Login
                || rec.Id == model.Id);
            return employee != null ? CreateModel(employee) : null;
        }

        public List<EmployeeViewModel> GetFilteredList(EmployeeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            return context.Employees
                .Include(rec => rec.TOs)
                .Where(rec => rec.Login.Equals(model.Login))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<EmployeeViewModel> GetFullList()
        {
            using var context = new AutoFixStationDatabase();
            return context.Employees
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(EmployeeBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Employees.Add(CreateModel(model, new Employee()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(EmployeeBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Employees
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private static Employee CreateModel(EmployeeBindingModel model, Employee employee)
        {
            employee.FIO = model.FIO;
            employee.Login = model.Login;
            employee.Password = model.Password;
            return employee;
        }

        private static EmployeeViewModel CreateModel(Employee employee)
        {
            return new EmployeeViewModel
            {
                Id = employee.Id,
                FIO = employee.FIO,
                Login = employee.Login,
                Password = employee.Password
            };
        }
    }
}
