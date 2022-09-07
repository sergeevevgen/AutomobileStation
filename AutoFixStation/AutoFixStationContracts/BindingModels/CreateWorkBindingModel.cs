using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BindingModels
{
    public class CreateWorkBindingModel
    {
        public int StoreKeeperId { get; set; }
        public int WorkTypeId { get; set; }
        public int TOId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
    }
}
