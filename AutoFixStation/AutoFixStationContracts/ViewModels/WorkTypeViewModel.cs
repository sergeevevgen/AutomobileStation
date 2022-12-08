using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutoFixStationContracts.ViewModels
{
    public class WorkTypeViewModel
    {
        public int Id { get; set; }
        public int TimeOfWorkId { get; set; }
        public decimal ExecutionTime { get; set; }

        [DisplayName("Название работы")]
        public string WorkName { get; set; }

        [DisplayName("Стоимость работы")]
        public decimal Price { get; set; }

        [DisplayName("Стоимость с учётом деталей")]
        public decimal NetPrice { get; set; }//with spareparts

        /// <summary>
        /// Необходимые детали и расходники (int - id, string - название, decimal, потому что может быть не целое (например, 0.8 л масла))
        /// </summary>
        public Dictionary<int, (string, decimal)> WorkSpareParts { get; set; }
    }
}