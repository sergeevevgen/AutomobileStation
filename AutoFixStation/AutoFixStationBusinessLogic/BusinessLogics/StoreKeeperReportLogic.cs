using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationBusinessLogic.OfficePackage;
using AutoFixStationBusinessLogic.OfficePackage.HelperModels;

namespace AutoFixStationBusinessLogic.BusinessLogics
{
    public class StoreKeeperReportLogic : IStoreKeeperReportLogic
    {
        private readonly ISparePartStorage _sparePartStorage;
        private readonly IWorkTypeStorage _workTypeStorage;
        private readonly ITOStorage _tOStorage;
        private readonly IWorkStorage _workStorage;
        private readonly ICarStorage _carStorage;
        private readonly IServiceRecordStorage _serviceRecordStorage;

        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;

        public StoreKeeperReportLogic(IWorkTypeStorage pizzaStorage, ISparePartStorage ingredientStorage, IWorkStorage workStorage, IServiceRecordStorage serviceRecordStorage,
                            ICarStorage carStorage, ITOStorage tOStorage, AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _workTypeStorage = pizzaStorage;
            _sparePartStorage = ingredientStorage;
            _workStorage = workStorage;
            _tOStorage = tOStorage;
            _carStorage = carStorage;
            _serviceRecordStorage = serviceRecordStorage;

            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        /// <summary>
        /// Получение списка компонент с указанием цены
        /// </summary> 
        /// <returns></returns>
        public List<ReportWorkTypeSPViewModel> GetWorkTypeSpareParts(List<WorkTypeViewModel> worktypes)
        {
            var ingredients = _sparePartStorage.GetFullList();

            //var pizzas = _workTypeStorage.GetFullList();

            var list = new List<ReportWorkTypeSPViewModel>();

            foreach (var pizza in worktypes)
            {
                var record = new ReportWorkTypeSPViewModel
                {
                    WorkTypeName = pizza.WorkName,
                    SpareParts = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var ingredient in ingredients)
                {
                    if (pizza.WorkSpareParts.ContainsKey(ingredient.Id))
                    {
                        record.SpareParts.Add(new Tuple<string, int>(ingredient.Name, (int)pizza.WorkSpareParts[ingredient.Id].Item2));
                        record.TotalCount += (int)pizza.WorkSpareParts[ingredient.Id].Item2;
                    }
                }

                list.Add(record);
            }

            return list;
        }

        public List<ReportTOViewModel> GetTOs(ReportBindingModel model)
        {
            var list = new List<ReportTOViewModel>();
            var tos = _tOStorage.GetFilteredList(new TOBindingModel
            {
                DateCreate = model.DateFrom.Value,
                DateOver = model.DateTo
            }).Where(x => x.Status.Equals("Выдан"));

            foreach (var to in tos)
            {
                var record = new ReportTOViewModel
                {
                    DateBegin = to.DateCreate,
                    DateEnd = to.DateOver.Value,
                    CarId = to.CarId.ToString(),
                    TOId = to.Id
                };

                //Получаем записи
                var car = _carStorage.GetElement(new CarBindingModel { Id = to.CarId });
                var list_records = new List<string>();
                foreach (var sr in car.Records)
                {
                    list_records.Add(sr.Value.Item2);
                }
                record.ServiceRecords = list_records;
                string sparts = "";
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
                            sparts += list_parts[part.Key].Item1;

                        }
                        else
                        {
                            list_parts.Add(part.Key, part.Value);
                            sparts += part.Value;
                        }
                    }
                }
                record.SpareParts = list_parts;
                record.sParts = sparts;
                list.Add(record);
            }
            return list;
        }

        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveWorkTypesToWordFile(ReportSparePartBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список запчастей",
                WorkTypeSP = GetWorkTypeSpareParts(model.WorkTypes)
            });
        }

        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveWorkTypesToExcelFile(ReportSparePartBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список запчастей",
                WorkTypeSP = GetWorkTypeSpareParts(model.WorkTypes)
            });
        }

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveTOsByDateToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateReportTOsByDate(new PdfInfo
            {
                FileName = model.FileName,
                Title = $"Сведения по ТО за период с {model.DateFrom} по {model.DateTo}",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                TOs = GetTOs(model)
            });
        }
    }
}
