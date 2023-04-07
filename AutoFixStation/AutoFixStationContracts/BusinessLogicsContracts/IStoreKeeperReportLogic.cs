using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.ViewModels;

namespace AutoFixStationContracts.BusinessLogicsContracts
{
    public interface IStoreKeeperReportLogic
    {
        /// </summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        List<ReportWorkTypeSPViewModel> GetWorkTypeSpareParts(List<WorkTypeViewModel> worktypes);
        List<ReportTOViewModel> GetTOs(ReportBindingModel model);
        void SaveWorkTypesToWordFile(ReportSparePartBindingModel model);
        void SaveWorkTypesToExcelFile(ReportSparePartBindingModel model);
        void SaveWorksToPdfFile(ReportBindingModel model);
    }
}
