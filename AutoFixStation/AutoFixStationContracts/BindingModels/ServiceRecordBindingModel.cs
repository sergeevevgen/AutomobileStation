using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BindingModels
{
    public class ServiceRecordBindingModel
    {
        public int? Id { get; set; }
        public int CarId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateBegin { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
        public string Description { get; set; }
    }
}
