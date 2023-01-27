using AutoFixStationBusinessLogic.OfficePackage.HelperEnums;
using AutoFixStationBusinessLogic.OfficePackage.HelperModels;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        /// <summary>
        /// Создание отчeта
        /// </summary>
        /// <param name="info"></param>
        public void CreateReportTOSpareParts(ExcelInfo info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "D1"
            });
            uint rowIndex = 2;
            foreach(var element in info.TOSpareParts)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = $"ТО #{element.TOId} по автомобилю \"{element.CarName}\"",
                    StyleInfo = ExcelStyleInfoType.Text
                });
                MergeCells(new ExcelMergeParameters
                {
                    CellFromName = $"A{rowIndex}",
                    CellToName = $"D{rowIndex}"
                });
                rowIndex = InsertTOInfo(element.SpareParts, rowIndex) + 1;
            }
            
            SaveExcel(info);
        }

        private uint InsertTOInfo(Dictionary<int, (string, decimal, decimal)> spareParts, uint rowIndex)
        {
            rowIndex++;
            //Заголовок
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "B",
                RowIndex = rowIndex,
                Text = "Список запчастей",
                StyleInfo = ExcelStyleInfoType.Text
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = $"B{rowIndex}",
                CellToName = $"C{rowIndex}"
            });
            rowIndex++;

            //Вставка мини-заголовков
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = rowIndex,
                Text = "Наименование",
                StyleInfo = ExcelStyleInfoType.TextWithBorder
            });
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "B",
                RowIndex = rowIndex,
                Text = "Количество",
                StyleInfo = ExcelStyleInfoType.TextWithBorder
            });
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "C",
                RowIndex = rowIndex,
                Text = "Цена за шт.",
                StyleInfo = ExcelStyleInfoType.TextWithBorder
            });
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "D",
                RowIndex = rowIndex,
                Text = "Стоимость",
                StyleInfo = ExcelStyleInfoType.TextWithBorder
            });

            //Вставка запчастей
            rowIndex++;
            foreach (var sp in spareParts)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = sp.Value.Item1,
                    StyleInfo = ExcelStyleInfoType.TextWithBorder
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = sp.Value.Item2.ToString(),
                    StyleInfo = ExcelStyleInfoType.TextWithBorder
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = sp.Value.Item3.ToString(),
                    StyleInfo = ExcelStyleInfoType.TextWithBorder
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "D",
                    RowIndex = rowIndex,
                    Text = (sp.Value.Item2 * sp.Value.Item3).ToString(),
                    StyleInfo = ExcelStyleInfoType.TextWithBorder
                });
                rowIndex++;
            }
            return rowIndex;
        }

        /// <summary>
        /// Создание excel-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateExcel(ExcelInfo info);

        /// <summary>
        /// Добавляем новую ячейку в лист
        /// </summary>
        /// <param name="excelCellParams"></param>
        protected abstract void InsertCellInWorksheet(ExcelCellParameters cellParams);

        /// <summary>
        /// Объединение ячеек
        /// </summary>
        /// <param name="mergeParams"></param>
        protected abstract void MergeCells(ExcelMergeParameters mergeParams);

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveExcel(ExcelInfo info);
    }
}
