using AutoFixStationBusinessLogic.OfficePackage;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToPdf _saveToPdf;
        private readonly ISparePartStorage _sparePartStorage;
        private readonly IServiceRecordStorage _serviceRecordStorage;
        private readonly ITOStorage _tOStorage;
        private readonly IWorkStorage _workStorage;
        private readonly IWorkTypeStorage _workTypeStorage;
        private readonly ICarStorage _carStorage;

        public ReportLogic(AbstractSaveToWord saveToWord, 
            AbstractSaveToExcel saveToExcel, 
            AbstractSaveToPdf saveToPdf, 
            ISparePartStorage sparePartStorage, 
            IServiceRecordStorage serviceRecordStorage, 
            ITOStorage tOStorage,
            IWorkStorage workStorage,
            IWorkTypeStorage workTypeStorage,
            ICarStorage carStorage)
        {
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
            _sparePartStorage = sparePartStorage;
            _serviceRecordStorage = serviceRecordStorage;
            _tOStorage = tOStorage;
            _workStorage = workStorage;
            _workTypeStorage = workTypeStorage;
            _carStorage = carStorage;
        }

        public List<ReportTOsViewModel> GetTOs(ReportBindingModel model)
        {
            var list = new List<ReportTOsViewModel>();
            var tos = _tOStorage.GetFilteredList(new TOBindingModel
            {
                DateCreate = model.DateFrom.Value,
                DateOver = model.DateTo
            });

            foreach (var to in tos)
            {
                var record = new ReportTOsViewModel
                {
                    DateBegin = to.DateCreate,
                    DateEnd = to.DateOver.Value,
                    CarName = to.CarName,
                    TOId = to.Id
                };

                //Получаем записи
                var car = _carStorage.GetElement(new CarBindingModel { Id = to.CarId });
                var list_records = new List<string>();
                foreach(var sr in car.Records)
                {
                    list_records.Add(sr.Value.Item2);
                }
                record.ServiceRecords = list_records;

                //Получаем запчасти
                var list_parts = new Dictionary<int, (string, decimal, decimal)>();
                foreach (var workId in to.Works.Keys)
                {
                    var work = _workStorage.GetElement(new WorkBindingModel
                    {
                        Id = workId
                    });
                    var worktype = _workTypeStorage.GetElement(new WorkTypeBindingModel
                    {
                        Id = work.WorkTypeId
                    });

                    foreach (var part in worktype.WorkSpareParts)
                    {
                        if (list_parts.ContainsKey(part.Key))
                        {
                            list_parts[part.Key] = (list_parts[part.Key].Item1, list_parts[part.Key].Item2 + part.Value.Item2, list_parts[part.Key].Item3);
                        }
                        else
                        {
                            list_parts.Add(part.Key, part.Value);
                        }
                    }
                }
                record.SpareParts = list_parts;
                list.Add(record);
            }
            return list;
        }

        public List<ReportTOSparePartViewModel> GetTOSparePart(ReportBindingModel model)
        {
            var tos = model.TOs;
            var list = new List<ReportTOSparePartViewModel>();
            foreach (var tO in tos)
            {
                var record = new ReportTOSparePartViewModel
                {
                    CarName = tO.CarName,
                    TOId = tO.Id
                };

                var list_parts = new Dictionary<int, (string, decimal, decimal)>();
                foreach(var workId in tO.Works.Keys)
                {
                    var work = _workStorage.GetElement(new WorkBindingModel
                    {
                        Id = workId
                    });
                    var worktype = _workTypeStorage.GetElement(new WorkTypeBindingModel
                    {
                        Id = work.WorkTypeId
                    });

                    foreach(var part in worktype.WorkSpareParts)
                    {
                        if (list_parts.ContainsKey(part.Key))
                        {
                            list_parts[part.Key] = (list_parts[part.Key].Item1, list_parts[part.Key].Item2 + part.Value.Item2, list_parts[part.Key].Item3);
                        }
                        else
                        {
                            list_parts.Add(part.Key, part.Value);
                        }
                    }
                }
                record.SpareParts = list_parts;
                list.Add(record);
            }
            return list;
        }

        public void SaveTOsByDateToPdfFile(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void SaveTOSparePartToExcelFile(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void SaveTOSparePartToWordFile(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
