using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutoFixStationContracts.ViewModels
{
    public class TOViewModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int EmployeeId { get; set; }

        [DisplayName("Стоимость")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата начала")]
        public DateTime? DateImplement { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime? DateOver { get; set; }

        /// <summary>
        /// Работы ТО (номер, (название, (кол-во, стоимость 
        /// </summary>
        public Dictionary<int, (string, (int, decimal))> Works { get; set; }
    }
}