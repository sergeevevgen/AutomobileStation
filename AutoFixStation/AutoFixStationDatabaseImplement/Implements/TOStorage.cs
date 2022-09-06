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
                .Include(rec => rec.TO_Works)
                .ThenInclude(rec => rec.Work)
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
                .Include(rec => rec.TO_Works)
                .ThenInclude(rec => rec.Work)
                .Include(rec => rec.Car)
                .Include(rec => rec.Employee)
                .Where(rec => (rec.Id.Equals(model.Id)) 
                || (rec.CarId.Equals(model.CarId)) 
                || (rec.Status.Equals(model.Status)) 
                || (rec.EmployeeId.Equals(model.EmployeeId))
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
                .Include(rec => rec.TO_Works)
                .ThenInclude(rec => rec.Work)
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
                TO to = new TO()
                {
                    CarId = model.CarId,
                    EmployeeId = model.EmployeeId,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateOver = model.DateOver,
                    DateImplement = model.DateImplement
                };
                context.TOs.Add(to);
                context.SaveChanges();
                CreateModel(model, to, context);
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
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private static TO CreateModel(TOBindingModel model, TO tO,
            AutoFixStationDatabase context)
        {
            tO.CarId = model.CarId;
            tO.EmployeeId = model.EmployeeId;
            tO.Sum = model.Sum;
            tO.Status = model.Status;
            tO.DateCreate = model.DateCreate;
            tO.DateImplement = model.DateImplement;
            tO.DateOver = model.DateOver;
            if (model.Id.HasValue)
            {
                var to_works = context.TO_Works
                    .Where(rec => rec.TOId == model.Id.Value)
                    .ToList();
                // удалили те, которых нет в модели
                context.TO_Works
                    .RemoveRange(to_works
                    .Where(rec => !model.Works
                    .ContainsKey(rec.WorkId))
                    .ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateWork in to_works)
                {
                    updateWork.Count =
                    model.Works[updateWork.WorkId].Item2.Item1;
                    model.Works.Remove(updateWork.WorkId);
                }
                context.SaveChanges();
            }

            // добавили новые значения в таблицу 
            foreach (var element in model.Works)
            {
                context.TO_Works.Add(new TO_Work
                {
                    TOId = tO.Id,
                    WorkId = element.Key,
                    Count = element.Value.Item2.Item1
                });
                context.SaveChanges();
            }
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
                Works = tO.TO_Works
                    .ToDictionary(recPC => recPC.WorkId,
                    recPC => (recPC.Work.WorkName, (recPC.Count, recPC.Work.NetPrice)))
            };
        }
    }
}
