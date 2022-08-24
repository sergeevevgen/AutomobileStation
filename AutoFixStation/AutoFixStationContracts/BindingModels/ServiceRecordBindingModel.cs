using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.BindingModels
{
    public class ServiceRecordBindingModel
    {
        public int? Id { get; set; }
        public int CarId { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public string Description { get; set; }
    }
}
