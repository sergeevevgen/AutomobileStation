using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.BindingModels
{
    public class CreateTOBindingModel
    {
        public int CarId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Sum { get; set; }
        public Dictionary<int, (string, (int, decimal))> Works { get; set; }
    }
}
