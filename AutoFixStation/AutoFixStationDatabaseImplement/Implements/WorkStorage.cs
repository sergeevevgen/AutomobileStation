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
    public class WorkStorage : IWorkStorage
    {
        public void Delete(WorkBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            Work element = context.Works
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Works.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public WorkViewModel GetElement(WorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            var element = context.Works
                .Include(rec => rec.WorkType)
                .Include(rec => rec.StoreKeeper)
                .FirstOrDefault(rec => rec.Id == model.Id);
            return element != null ? CreateModel(element) : null;
        }

        public List<WorkViewModel> GetFilteredList(WorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            return context.Works
                .Include(rec => rec.WorkType)
                .Include(rec => rec.StoreKeeper)
                .Where(rec => rec.WorkName.Contains(model.WorkName)
                || (model.WorkStatus == rec.WorkStatus) 
                || (rec.StoreKeeperId == model.StoreKeeperId)
                || (rec.WorkTypeId == model.WorkTypeId)
                || (rec.TOId == model.TOId))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<WorkViewModel> GetFullList()
        {
            using var context = new AutoFixStationDatabase();
            return context.Works
                .Include(rec => rec.WorkType)
                .Include(rec => rec.StoreKeeper)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(WorkBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Works.Add(CreateModel(model, new Work()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(WorkBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Works
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

        private static Work CreateModel(WorkBindingModel model, Work work)
        {
            work.WorkTypeId = model.WorkTypeId;
            work.StoreKeeperId = model.StoreKeeperId;
            work.TOId = model.TOId;
            work.WorkName = model.WorkName;
            work.WorkStatus = model.WorkStatus;
            work.Price = model.Price;
            work.NetPrice = model.NetPrice;
            work.WorkBegin = model.WorkBegin;
            work.Count = model.Count;
            return work;
        }

        private static WorkViewModel CreateModel(Work work)
        {
            return new WorkViewModel
            {
                Id = work.Id,
                WorkTypeId = work.WorkTypeId,
                StoreKeeperId = work.StoreKeeperId,
                TOId = work.TOId,
                WorkName = work.WorkName,
                WorkStatus = work.WorkStatus.ToString(),
                Price = work.Price,
                NetPrice = work.NetPrice,
                WorkBegin = work.WorkBegin,
                Count = work.Count,
            };
        }
    }
}