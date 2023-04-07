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
        public void CreateDoc(PdfInfo info)
        {
            //CreatePdf(info);
            /*CreateParagraph(new PdfParagraph { Text = info.Title, Style = "NormalTitle" });
            CreateParagraph(new PdfParagraph { Text = $"с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}", Style = "Normal" });

            CreateTable(new List<string> { "3cm", "6cm", "3cm", "2cm", "3cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата создания", "Название работы", "Количество", "Сумма", "Статус" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var order in info.Works)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { order.DateCreate.ToShortDateString(), order.WorkName, order.Count.ToString(), order.Sum.ToString(), order.Status.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            decimal sum = info.Works.Sum(rec => rec.Sum);
            CreateParagraph(new PdfParagraph
            {
                Text = $"Итого: {sum}",
                Style = "NormalTitle",
            });

            SavePdf(info);*/

            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph
            {
                Text = $"Период с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}",
                Style = "Normal"
            });

            foreach (var to in info.Works)
            {
                CreateParagraph(new PdfParagraph
                {
                    Text = $"ТО #{to.TOId}",
                    Style = "Normal"
                });
                CreateParagraph(new PdfParagraph
                {
                    Text = $"Сумма: \"{to.Sum}\"",
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
                //InsertTOInfo(to.SpareParts, to.ServiceRecords);
            }

            SavePdf(info);
        }

        /// <summary> 
        /// Создание doc-файла
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