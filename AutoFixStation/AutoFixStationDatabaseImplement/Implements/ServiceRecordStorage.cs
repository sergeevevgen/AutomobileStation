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
    public class ServiceRecordStorage : IServiceRecordStorage
    {
        public void Delete(ServiceRecordBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            ServiceRecord element = context.ServiceRecords
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.ServiceRecords.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ServiceRecordViewModel GetElement(ServiceRecordBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            var element = context.ServiceRecords
                .Include(rec => rec.Car)
                .FirstOrDefault(rec => (rec.CarId == model.CarId
                && rec.DateBegin.Date == model.DateBegin.Date)
                || rec.Id == model.Id);
            return element != null ? CreateModel(element) : null;
        }

        public List<ServiceRecordViewModel> GetFilteredList(ServiceRecordBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            return context.ServiceRecords
                .Include(rec => rec.Car)
                .Where(rec => rec.Id.Equals(model.Id) 
                || (rec.DateBegin.Date >= model.DateBegin.Date && rec.DateEnd.Date <= model.DateEnd.Date)
                || (rec.CarId.Equals(model.CarId)))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<ServiceRecordViewModel> GetFullList()
        {
            using var context = new AutoFixStationDatabase();
            return context.ServiceRecords
                .Include(rec => rec.Car)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(ServiceRecordBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.ServiceRecords.Add(CreateModel(model, new ServiceRecord()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(ServiceRecordBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.ServiceRecords
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

        private static ServiceRecord CreateModel(ServiceRecordBindingModel model, ServiceRecord serviceRecord)
        {
            serviceRecord.DateBegin = model.DateBegin;
            serviceRecord.DateEnd = model.DateEnd;
            serviceRecord.Description = model.Description;
            serviceRecord.CarId = model.CarId;
            return serviceRecord;
        }

        private static ServiceRecordViewModel CreateModel(ServiceRecord serviceRecord)
        {
            return new ServiceRecordViewModel
            {
                Id = serviceRecord.Id,
                CarId = serviceRecord.CarId,
                DateBegin = serviceRecord.DateBegin,
                DateEnd = serviceRecord.DateEnd,
                Description = serviceRecord.Description
            };
        }
    }
}
