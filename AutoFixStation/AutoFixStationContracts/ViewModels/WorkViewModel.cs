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
        public int TOId { get; set; }

        [DisplayName("Название")]
        public string WorkName { get; set; }

        [DisplayName("Стоимость работы")]
        public decimal Price { get; set; }

        [DisplayName("Стоимость с учётом запчастей")]
        public decimal NetPrice { get; set; }

       [DisplayName("Статус")] 
        public string WorkStatus { get; set; }

        [DisplayName("Дата начала")]
        public DateTime? WorkBegin { get; set; }

        [DisplayName("Кол-во")]
        public int Count { get; set; }
    }
}