using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutoFixStationContracts.ViewModels
{
    public class WorkViewModel
    {
        public int Id { get; set; }
        public int StoreKeeperId { get; set; }
        public int WorkTypeId { get; set; }

        [DisplayName("Название")]
        public string WorkName { get; set; }

        //[DisplayName("Количество")]
        //public int Count { get; set; }

        [DisplayName("Стоимость работы")]
        public decimal Price { get; set; }

        [DisplayName("Стоимость с учётом запчастей")]
        public decimal NetPrice { get; set; }

       [DisplayName("Статус")] 
        public string WorkStatus { get; set; }
    }
}