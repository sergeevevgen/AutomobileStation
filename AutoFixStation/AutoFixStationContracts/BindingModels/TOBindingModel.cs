using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.Enums;

namespace STOContracts.BindingModels
{
    /// <summary>
    /// Доступные ТО
    /// </summary>
    public class TOBindingModel
    {
        public int? Id { get; set; }
        
        /// <summary>
        /// Авто
        /// </summary>
        public int CarId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Sum { get; set; }
        public TOStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public DateTime? DateOver { get; set; }

        /// <summary>
        /// Работы ТО
        /// </summary>
        public Dictionary<int, (string, int)> TOWorks { get; set; }
    }
}
