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
    public class StoreKeeperStorage : IStoreKeeperStorage
    {
        public void Delete(StoreKeeperBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            StoreKeeper storeKeeper = context.StoreKeepers
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (storeKeeper != null)
            {
                context.StoreKeepers.Remove(storeKeeper);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public StoreKeeperViewModel GetElement(StoreKeeperBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            var storeKeeper = context.StoreKeepers
                .Include(rec => rec.Works)
                .FirstOrDefault(rec => rec.Login == model.Login 
                || rec.Id == model.Id);
            return storeKeeper != null ? CreateModel(storeKeeper) : null;
        }

        public List<StoreKeeperViewModel> GetFilteredList(StoreKeeperBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            return context.StoreKeepers
                .Include(rec => rec.Works)
                .Where(rec => rec.Login.Equals(model.Login))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<StoreKeeperViewModel> GetFullList()
        {
            using var context = new AutoFixStationDatabase();
            return context.StoreKeepers
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(StoreKeeperBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.StoreKeepers.Add(CreateModel(model, new StoreKeeper()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(StoreKeeperBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.StoreKeepers
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

        private static StoreKeeper CreateModel(StoreKeeperBindingModel model, StoreKeeper storeKeeper)
        {
            storeKeeper.FIO = model.FIO;
            storeKeeper.Login = model.Login;
            storeKeeper.Password = model.Password;
            return storeKeeper;
        }

        private static StoreKeeperViewModel CreateModel(StoreKeeper storeKeeper)
        {
            return new StoreKeeperViewModel
            {
                Id = storeKeeper.Id,
                FIO = storeKeeper.FIO,
                Login = storeKeeper.Login,
                Password = storeKeeper.Password
            };
        }
    }
}
