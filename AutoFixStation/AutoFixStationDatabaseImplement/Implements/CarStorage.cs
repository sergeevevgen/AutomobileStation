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
    public class CarStorage : ICarStorage
    {
        public void Delete(CarBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            Car element = context.Cars
                .FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                context.Cars.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public CarViewModel GetElement(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new AutoFixStationDatabase();
            var car = context.Cars
                .Include(rec => rec.ServiceRecords)
                .Include(rec => rec.TOs)
                .FirstOrDefault(rec => rec.VIN == model.VIN 
                || rec.Id == model.Id);
            return car != null ? CreateModel(car) : null;
        }

        public List<CarViewModel> GetFilteredList(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new AutoFixStationDatabase();
            return context.Cars
                .Include(rec => rec.ServiceRecords)
                .Include(rec => rec.TOs)
                .Where(rec => rec.VIN.Equals(model.VIN))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<CarViewModel> GetFullList()
        {
            using var context = new AutoFixStationDatabase();
            return context.Cars
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(CarBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Cars.Add(CreateModel(model, new Car()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(CarBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Cars
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

        private static Car CreateModel(CarBindingModel model, Car car)
        {
            car.Brand = model.Brand;
            car.Model = model.Model;
            car.OwnerPhoneNumber = model.OwnerPhoneNumber;
            car.VIN = model.VIN;
            return car;
        }

        private static CarViewModel CreateModel(Car car)
        {
            return new CarViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                OwnerPhoneNumber = car.OwnerPhoneNumber,
                VIN = car.VIN,
                Records = car.ServiceRecords
                    .ToDictionary(
                    rec => rec.Id, rec => 
                    ((rec.DateBegin, rec.DateEnd), rec.Description))
            };
        }
    }
}