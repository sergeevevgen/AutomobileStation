using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Date)]
        public DateTime DateCreate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата начала")]
        public DateTime? DateImplement { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата окончания")]
        public DateTime? DateOver { get; set; }

        /// <summary>
        /// Работы ТО (номер, (название, (кол-во, стоимость 
        /// </summary>
        public Dictionary<int, (string, (int, decimal))> Works { get; set; }
    }
}