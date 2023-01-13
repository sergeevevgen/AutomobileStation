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
        public void CreateDoc(WordInfo info)
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
            foreach (var dish in info.Dishes)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)>
                    {
                        (dish.DishName + ": ", new WordTextProperties
                        {
                            Size = "24",
                            Bold = true
                        }),
                        (dish.Price.ToString(), new WordTextProperties
                        {
                            Size = "24",
                        })
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);
        }

        public void CreateDocWareHouses(WordInfo info)
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
            CreateTable(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {
                    ("Название:", new WordTextProperties
                    {
                        Size = "24",
                        Bold = true
                    }),
                    ("ФИО кладовщика:", new WordTextProperties
                    {
                        Size = "24",
                        Bold = true,
                    }),
                    ("Дата создания:", new WordTextProperties
                    {
                        Size = "24",
                        Bold = true
                    })
                },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Both
                }
            });
            foreach (var warehouse in info.WareHouses)
            {
                CreateRowInTable(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)>
                    {
                        (warehouse.WareHouseName, new WordTextProperties
                        {
                            Size = "24"
                        }),
                        (warehouse.StorekeeperFIO, new WordTextProperties
                        {
                            Size = "24",
                        }),
                        (warehouse.DateCreate.ToShortDateString(), new WordTextProperties
                        {
                            Size = "24",
                        })
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
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
