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
    public class TOStorage : ITOStorage
    {
        public void Delete(TOBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            TO element = context.TOs
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.TOs.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public TOViewModel GetElement(TOBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            var to = context.TOs
                .Include(rec => rec.Works)
                .Include(rec => rec.Car)
                .Include(rec => rec.Employee)
                .FirstOrDefault(rec => rec.Id == model.Id);
            return to != null ? CreateModel(to) : null;
        }

        public List<TOViewModel> GetFilteredList(TOBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            return context.TOs
                .Include(rec => rec.Works)
                .Include(rec => rec.Car)
                .Include(rec => rec.Employee)
                .Where(rec => rec.Id.Equals(model.Id) 
                || rec.CarId.Equals(model.CarId) 
                || rec.Status.Equals(model.Status) 
                || rec.EmployeeId.Equals(model.EmployeeId)
                || (rec.DateCreate.Date >= model.DateCreate
                && rec.DateOver <= model.DateOver))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<TOViewModel> GetFullList()
        {
            using var context = new AutoFixStationDatabase();
            return context.TOs
                .Include(rec => rec.Car)
                .Include(rec => rec.Employee)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(TOBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                //TO to = new TO()
                //{
                //    CarId = model.CarId,
                //    EmployeeId = model.EmployeeId,
                //    Sum = model.Sum,
                //    Status = model.Status,
                //    DateCreate = model.DateCreate,
                //    DateOver = model.DateOver,
                //    DateImplement = model.DateImplement
                //};
                context.TOs.Add(CreateModel(model, new TO()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(TOBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.TOs
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

        private static TO CreateModel(TOBindingModel model, TO tO)
        {
            tO.CarId = model.CarId;
            tO.EmployeeId = model.EmployeeId;
            tO.Sum = model.Sum;
            tO.Status = model.Status;
            tO.DateCreate = model.DateCreate;
            tO.DateImplement = model.DateImplement;
            tO.DateOver = model.DateOver;
            return tO;
        }

        private static TOViewModel CreateModel(TO tO)
        {
            return new TOViewModel
            {
                Id = tO.Id,
                CarId = tO.CarId,
                EmployeeId = tO.EmployeeId,
                Sum = tO.Sum,
                Status = tO.Status.ToString(),
                DateCreate = tO.DateCreate,
                DateImplement = tO.DateImplement,
                DateOver = tO.DateOver,
                Works = tO.Works
                    .ToDictionary(recPC => recPC.Id,
                    recPC => (recPC.WorkName, (recPC.Count, recPC.NetPrice)))
            };
        }
    }
}
