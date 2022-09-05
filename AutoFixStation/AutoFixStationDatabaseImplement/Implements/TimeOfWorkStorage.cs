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
    public class TimeOfWorkStorage : ITimeOfWorkStorage
    {
        public void Delete(TimeOfWorkBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            TimeOfWork element = context.TimeOfWorks
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.TimeOfWorks.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public TimeOfWorkViewModel GetElement(TimeOfWorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            var element = context.TimeOfWorks
                .Include(rec => rec.WorkTypes)
                .FirstOrDefault(rec => (rec.Hours == model.Hours
                && rec.Mins == model.Mins) || rec.Id == model.Id);
            return element != null ? CreateModel(element) : null;
        }

        public List<TimeOfWorkViewModel> GetFilteredList(TimeOfWorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            return context.TimeOfWorks
                .Include(rec => rec.WorkTypes)
                .Where(rec => (rec.Hours == model.Hours
                    && rec.Mins == model.Mins))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<TimeOfWorkViewModel> GetFullList()
        {
            using var context = new AutoFixStationDatabase();
            return context.TimeOfWorks
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(TimeOfWorkBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.TimeOfWorks.Add(CreateModel(model, new TimeOfWork()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(TimeOfWorkBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.TimeOfWorks
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

        private static TimeOfWork CreateModel(TimeOfWorkBindingModel model, TimeOfWork timeOfWork)
        {
            timeOfWork.Hours = model.Hours;
            timeOfWork.Mins = model.Mins;
            return timeOfWork;
        }

        private static TimeOfWorkViewModel CreateModel(TimeOfWork timeOfWork)
        {
            return new TimeOfWorkViewModel
            {
                Id = timeOfWork.Id,
                Hours = timeOfWork.Hours,
                Mins = timeOfWork.Mins
            };
        }
    }
}
