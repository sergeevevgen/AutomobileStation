using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationBusinessLogic.OfficePackage.HelperEnums;
using AutoFixStationBusinessLogic.OfficePackage.HelperModels;

namespace AutoFixStationBusinessLogic
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateReportTOsByDate(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph
            {
                Text = $"С {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}",
                Style = "Normal"
            });

            foreach (var to in info.TOs)
            {
                CreateParagraph(new PdfParagraph
                {
                    Text = $"ТО #{to.TOId}",
                    Style = "Normal"
                });
                CreateParagraph(new PdfParagraph
                {
                    Text = $"Автомобиль: \"{to.CarId}\"",
                    Style = "Normal"
                });
                CreateParagraph(new PdfParagraph
                {
                    Text = $"Дата начала ТО: {to.DateBegin}",
                    Style = "Normal"
                });
                CreateParagraph(new PdfParagraph
                {
                    Text = $"Дата окончания ТО: {to.DateEnd}",
                    Style = "Normal"
                });
                InsertTOInfo(to.SpareParts/*, to.ServiceRecords*/);
            }

            SavePdf(info);
        }

        private void InsertTOInfo(Dictionary<int, (string, decimal, decimal)> spareParts/*, List<string> serviceRecords*/)
        {
            CreateParagraph(new PdfParagraph
            {
                Text = "Запчасти",
                Style = "NormalTitle"
            });

            CreateTable(new List<string> { "5cm", "5cm", "3cm", "3cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string>
                {
                    "Наименование", "Количество",
                    "Цена за ед.", "Стоимость"
                },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var part in spareParts)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string>
                    {
                        part.Value.Item1,
                        part.Value.Item2.ToString(),
                        part.Value.Item3.ToString(),
                        (part.Value.Item2 * part.Value.Item3).ToString()
                    },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }

            /*CreateParagraph(new PdfParagraph
            {
                Text = "Записи сервисов",
                Style = "NormalTitle"
            });
            uint num = 1;*/
           /* foreach (var sr in serviceRecords)
            {
                CreateParagraph(new PdfParagraph
                {
                    Text = $"Запись #{num}",
                    Style = "Normal"
                });
                CreateParagraph(new PdfParagraph
                {
                    Text = sr,
                    Style = "Normal"
                });
                CreateParagraph(new PdfParagraph
                {
                    Text = "",
                    Style = "Normal"
                });
                num++;
            }*/
        }

        /// <summary>
        /// Cоздание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreatePdf(PdfInfo info);

        /// <summary>
        /// Создание параграфа с текстом
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateParagraph(PdfParagraph paragraph);

        /// <summary>
        /// Создание таблицы
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateTable(List<string> columns);

        /// <summary>
        /// Создание и заполнение строки
        /// </summary>
        /// <param name="rowParameters"></param>
        protected abstract void CreateRow(PdfRowParameters rowParameters);

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SavePdf(PdfInfo info);
    }
}
