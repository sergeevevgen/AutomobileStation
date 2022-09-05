using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutoFixStationContracts.ViewModels
{
    public class SparePartViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Заводской номер")]
        public string FactoryNumber { get; set; }

        [DisplayName("Стоимость ед.")]
        public decimal Price { get; set; }

        [DisplayName("Тип детали")]
        public string Type { get; set; }

        [DisplayName("Ед. измерения")]
        public string UMeasurement { get; set; }
    }
}