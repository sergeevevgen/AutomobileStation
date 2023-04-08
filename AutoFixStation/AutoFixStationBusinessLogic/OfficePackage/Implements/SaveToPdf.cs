using AutoFixStationBusinessLogic.OfficePackage.HelperEnums;
using AutoFixStationBusinessLogic.OfficePackage.HelperModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Collections.Generic;

namespace AutoFixStationBusinessLogic.OfficePackage.Implements
{
    public class SaveToPdf : AbstractSaveToPdf
    {
        private Document _document;

        private Section _section;

        private Table _table;
        private static ParagraphAlignment GetParagraphAlignment(PdfParagraphAlignmentType
        type)
        {
            return type switch
            {
                PdfParagraphAlignmentType.Center => ParagraphAlignment.Center,
                PdfParagraphAlignmentType.Left => ParagraphAlignment.Left,
                _ => ParagraphAlignment.Justify,
            };
        }

        /// <summary>
        /// Создание стилей для документа
        /// </summary>
        /// <param name="document"></param>
        private static void DefineStyles(Document document)
        {
            var style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }

        /// <summary>
        /// Создание файла-пдф
        /// </summary>
        /// <param name="info"></param>
        protected override void CreatePdf(PdfInfo info)
        {
            _document = new Document();
            DefineStyles(_document);
            _section = _document.AddSection();
        }

        /// <summary>
        /// Создание абзаца
        /// </summary>
        /// <param name="pdfParagraph"></param>
        protected override void CreateParagraph(PdfParagraph pdfParagraph)
        {
            var paragraph = _section.AddParagraph(pdfParagraph.Text);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = pdfParagraph.Style;
            paragraph.Format.SpaceBefore = "0.5cm";
        }

        /// <summary>
        /// Создание столбца
        /// </summary>
        /// <param name="columns"></param>
        protected override void CreateTable(List<string> columns)
        {
            _table = _document.LastSection.AddTable();
            foreach (var elem in columns)
            {
                _table.AddColumn(elem);
            }
        }

        /// <summary>
        /// Создание строки
        /// </summary>
        /// <param name="rowParameters"></param>
        protected override void CreateRow(PdfRowParameters rowParameters)
        {
            var row = _table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                row.Cells[i].AddParagraph(rowParameters.Texts[i]);
                if (!string.IsNullOrEmpty(rowParameters.Style))
                {
                    row.Cells[i].Style = rowParameters.Style;
                }
                Unit borderWidth = 0.5;
                row.Cells[i].Borders.Left.Width = borderWidth;
                row.Cells[i].Borders.Right.Width = borderWidth;
                row.Cells[i].Borders.Top.Width = borderWidth;
                row.Cells[i].Borders.Bottom.Width = borderWidth;
                row.Cells[i].Format.Alignment =
                GetParagraphAlignment(rowParameters.ParagraphAlignment);
                row.Cells[i].VerticalAlignment = VerticalAlignment.Center;
            }
        }

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected override void SavePdf(PdfInfo info)
        {
            var renderer = new PdfDocumentRenderer(true)
            {
                Document = _document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
    }
}
