using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BindingModels
{
    public class WorkTypeBindingModel
    {
        public int? Id { get; set; }
        public int TimeOfWorkId { get; set; }
        public string WorkName { get; set; }

        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }//with spareparts

        /// <summary>
        /// Необходимые детали и расходники (int - id, string - название, decimal, потому что может быть не целое (например, 0.8 л масла))
        /// </summary>
        public Dictionary<int, (string, decimal, decimal)> WorkSpareParts { get; set; }
    }
}
