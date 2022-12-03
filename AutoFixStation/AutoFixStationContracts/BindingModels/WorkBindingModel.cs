using AutoFixStationContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BindingModels
{
    public class WorkBindingModel
    {
        public int? Id { get; set; }
        public int StoreKeeperId { get; set; }
        public int WorkTypeId { get; set; }
        public int TOId { get; set; }
        public string WorkName { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public WorkStatus WorkStatus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? WorkBegin { get; set; }
        public int Count { get; set; }
    }
}
