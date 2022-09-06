using AutoFixStationContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BindingModels
{
    /// <summary>
    /// Доступные ТО
    /// </summary>
    public class TOBindingModel
    {
        public int? Id { get; set; }
        public int CarId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Sum { get; set; }
        public TOStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public DateTime? DateOver { get; set; }

        /// <summary>
        /// Работы ТО (номер, (название, (кол-во, стоимость))) 
        /// </summary>
        public Dictionary<int, (string, (int, decimal))> Works { get; set; }
    }
}
