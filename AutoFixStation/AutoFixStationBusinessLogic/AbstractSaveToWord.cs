using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationBusinessLogic.OfficePackage.HelperEnums;
using AutoFixStationBusinessLogic.OfficePackage.HelperModels;

namespace AutoFixStationBusinessLogic
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
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

            foreach (var pizza in info.WorkTypeSP)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {(pizza.WorkTypeName, new WordTextProperties{Bold = true, Size = "24", }),
                        (" Общая цена: " + pizza.TotalCount.ToString(), new WordTextProperties {Bold = false, Size = "24"})},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
                uint num = 1;
                foreach (var part in pizza.SpareParts)
                {
                    CreateParagraph(new WordParagraph
                    {
      
                        Texts = new List<(string, WordTextProperties)>
                    {
                        ($"{num}) {part.Item1} - Цена:({part.Item2}) р.", new WordTextProperties
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
    }
}
