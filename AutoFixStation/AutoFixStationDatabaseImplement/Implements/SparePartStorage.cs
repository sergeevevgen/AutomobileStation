using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;
using AutoFixStationDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationDatabaseImplement.Implements
{
    public class SparePartStorage : ISparePartStorage
    {
        public void Delete(SparePartBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            SparePart element = context.SpareParts
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.SpareParts.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public SparePartViewModel GetElement(SparePartBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            var element = context.SpareParts
                .FirstOrDefault(rec => rec.Name == model.Name
                || rec.Id == model.Id);
            return element != null ? CreateModel(element) : null;
        }

        public List<SparePartViewModel> GetFilteredList(SparePartBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutoFixStationDatabase();
            return context.SpareParts
                .Where(rec => rec.Name.Contains(model.Name))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<SparePartViewModel> GetFullList()
        {
            using var context = new AutoFixStationDatabase();
            return context.SpareParts
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(SparePartBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            context.SpareParts.Add(CreateModel(model, new SparePart()));
            context.SaveChanges();
        }

        public void Update(SparePartBindingModel model)
        {
            using var context = new AutoFixStationDatabase();
            var element = context.SpareParts
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        private static SparePart CreateModel(SparePartBindingModel model, SparePart
        sparePart)
        {
            sparePart.Name = model.Name;
            sparePart.FactoryNumber = model.FactoryNumber;
            sparePart.Price = model.Price;
            sparePart.Type = model.Type;
            sparePart.UMeasurement = model.UMeasurement;
            return sparePart;
        }

        private static SparePartViewModel CreateModel(SparePart sparePart)
        {
            return new SparePartViewModel
            {
                Id = sparePart.Id,
                Name = sparePart.Name,
                FactoryNumber = sparePart.FactoryNumber,
                Price = sparePart.Price,
                Type = sparePart.Type.ToString(),
                UMeasurement = sparePart.UMeasurement.ToString()
            };
        }
    }
}
