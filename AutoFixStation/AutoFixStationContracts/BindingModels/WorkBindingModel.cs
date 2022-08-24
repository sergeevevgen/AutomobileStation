using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.Enums;

namespace STOContracts.BindingModels
{
    public class WorkBindingModel
    {
        public int? Id { get; set; }
        public string WorkName { get; set; }
        public int? StoreKeeperId { get; set; }

        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public WorkStatus WorkStatus { get; set; }

        /// <summary>
        /// Время выполнения работы
        /// </summary>
        public int TimeOfWorkId { get; set; }

        /// <summary>
        /// Необходимые детали и расходники (int - id, string - название, decimal, потому что может быть не целое (например, 0.8 л масла))
        /// </summary>
        public Dictionary<int, (string, decimal)> WorkSpareParts { get; set; }
    }
}
