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
    public class WorkTypeStorage : IWorkTypeStorage
    {
        public void Delete(WorkTypeBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            WorkType element = context.WorkTypes
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.WorkTypes.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public WorkTypeViewModel GetElement(WorkTypeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            var element = context.WorkTypes
                .Include(rec => rec.Work_SpareParts)
                .ThenInclude(rec => rec.SparePart)
                .Include(rec => rec.TimeOfWork)
                .FirstOrDefault(rec => rec.WorkName == model.WorkName
                || rec.Id == model.Id);
            return element != null ? CreateModel(element) : null;
        }

        public List<WorkTypeViewModel> GetFilteredList(WorkTypeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            return context.WorkTypes
                .Include(rec => rec.Work_SpareParts)
                .ThenInclude(rec => rec.SparePart)
                .Include(rec => rec.TimeOfWork)
                .Where(rec => (rec.Id.Equals(model.Id))
                || (rec.WorkName.Equals(model.WorkName)))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<WorkTypeViewModel> GetFullList()
        {
            using var context = new AutoFixStationDatabase();
            return context.WorkTypes
                .Include(rec => rec.Work_SpareParts)
                .ThenInclude(rec => rec.SparePart)
                .Include(rec => rec.TimeOfWork)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(WorkTypeBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                WorkType workType = new WorkType()
                {
                    TimeOfWorkId = model.TimeOfWorkId,
                    WorkName = model.WorkName,
                    Price = model.Price,
                    NetPrice = model.NetPrice
                };
                context.WorkTypes.Add(workType);
                context.SaveChanges();
                CreateModel(model, workType, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(WorkTypeBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.WorkTypes
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

        private static WorkType CreateModel(WorkTypeBindingModel model, WorkType workType,
            AutoFixStationDatabase context)
        {
            workType.TimeOfWorkId = model.TimeOfWorkId;
            workType.WorkName = model.WorkName;
            workType.Price = model.Price;
            workType.NetPrice = model.NetPrice;
            if (model.Id.HasValue)
            {
                var work_parts = context.WorkType_SpareParts
                    .Where(rec => rec.WorkTypeId == model.Id.Value)
                    .ToList();
                // удалили те, которых нет в модели
                context.WorkType_SpareParts
                    .RemoveRange(work_parts
                    .Where(rec => !model.WorkSpareParts
                    .ContainsKey(rec.SparePartId))
                    .ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateSparePart in work_parts)
                {
                    updateSparePart.Count =
                    model.WorkSpareParts[updateSparePart.SparePartId].Item2;
                    model.WorkSpareParts.Remove(updateSparePart.SparePartId);
                }
                context.SaveChanges();
            }

            // добавили новые значения в таблицу 
            foreach (var element in model.WorkSpareParts)
            {
                context.WorkType_SpareParts.Add(new WorkType_SparePart
                {
                    WorkTypeId = workType.Id,
                    SparePartId = element.Key,
                    Count = element.Value.Item2
                });
                context.SaveChanges();
            }
            return workType;
        }

        private static WorkTypeViewModel CreateModel(WorkType workType)
        {
            return new WorkTypeViewModel
            {
                Id = workType.Id,
                TimeOfWorkId = workType.TimeOfWorkId,
                WorkName = workType.WorkName,
                Price = workType.Price,
                NetPrice = workType.NetPrice,
                ExecutionTime = workType.TimeOfWork.Hours + workType.TimeOfWork.Mins / 60,
                WorkSpareParts = workType.Work_SpareParts
                    .ToDictionary(recPC => recPC.SparePartId,
                    recPC => (recPC.SparePart.Name, recPC.Count, recPC.SparePart.Price))
            };
        }
    }
}
