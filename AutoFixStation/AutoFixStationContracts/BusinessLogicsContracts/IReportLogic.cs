using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        /// <summary>
        /// Получение списка запчастей по выбранным ТО
        /// </summary>
        /// <returns></returns>
        List<ReportTOSparePartViewModel> GetTOSparePart(ReportBindingModel model);

        /// <summary>
        /// Сохранение списка запчастей по выбранным ТО в файл-Word
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void SaveTOSparePartToWordFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение списка запчастей по выбранным ТО в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void SaveTOSparePartToExcelFile(ReportBindingModel model);

        /// <summary>
        /// Получение списка ТО за период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ReportTOsViewModel> GetTOs(ReportBindingModel model);

        /// <summary>
        /// Сохранение ТО в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        void SaveTOsByDateToPdfFile(ReportBindingModel model);
    }
}
