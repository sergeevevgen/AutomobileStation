using AutoFixStationBusinessLogic.OfficePackage.HelperEnums;
using AutoFixStationBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateReportTOSpareParts(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {
                    (info.Title, new WordTextProperties
                    {
                        Bold = true, Size = "24",
                    })
                },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var element in info.TOSpareParts)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)>
                    {
                        ($"ТО #{element.TOId} по автомобилю \"{element.CarName}\"", new WordTextProperties
                        {
                            Size = "18",
                            Bold = true
                        })
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "18",
                        JustificationType = WordJustificationType.Both
                    }
                });
                InsertTOInfo(element.SpareParts);
            }
            SaveWord(info);
        }

        private void InsertTOInfo(Dictionary<int, (string, decimal, decimal)> spareParts)
        {
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                    {
                        ("Список запчастей:\n", new WordTextProperties
                        {
                            Size = "16",
                            Bold = false
                        })
                    },
                TextProperties = new WordTextProperties
                {
                    Size = "16",
                    JustificationType = WordJustificationType.Both
                }
            });

            uint num = 1;
            foreach(var part in spareParts)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)>
                    {
                        ($"{num}) {part.Value.Item1} ({part.Value.Item2}) - Цена: {part.Value.Item3}р. - Стоимость: {part.Value.Item2 * part.Value.Item3}р.", new WordTextProperties
                        {
                            Size = "14",
                            Bold = false
                        })
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "14",
                        JustificationType = WordJustificationType.Both
                    }
                });
                num++;
            }
        }

        public void CreateReportWorkTypesWord(WordInfo info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            foreach (var wtsp in info.WorkTypeSpareParts)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {(wtsp.WorkTypeName + "\n", new WordTextProperties{Bold = true, Size = "24", }),
                        },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> 
                    {
                        ("Количество различных запчастей: " + wtsp.TotalCount.ToString(),
                        new WordTextProperties { Bold = false, Size = "24" }),
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });

                uint num = 1;
                foreach (var part in wtsp.SpareParts)
                {
                    CreateParagraph(new WordParagraph
                    {

                        Texts = new List<(string, WordTextProperties)>
                    {
                        ($"{num}) {part.Item1} - Количество: {part.Item2} ед.", new WordTextProperties
                        {
                            Size = "18",
                            Bold = false
                        })
                    },
                        TextProperties = new WordTextProperties
                        {
                            Size = "18",
                            JustificationType = WordJustificationType.Both
                        }
                    });
                    num++;
                }
            }
            SaveWord(info);
        }

        /// <summary>
        /// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateWord(WordInfo info);

        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        protected abstract void CreateParagraph(WordParagraph paragraph);

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveWord(WordInfo info);

        /// <summary>
        /// Создание таблицы с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        protected abstract void CreateTable(WordParagraph paragraph);

        /// <summary>
        /// Создание строки в таблице
        /// </summary>
        /// <param name="paragraph"></param>
        protected abstract void CreateRowInTable(WordParagraph paragraph);
    }
}
