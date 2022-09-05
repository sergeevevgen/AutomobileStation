using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutoFixStationDatabaseImplement.Models
{
    public class WorkType_SparePart
    {
        public int Id { get; set; }

        public int SparePartId { get; set; }

        public int WorkTypeId { get; set; }

        [Required]
        public decimal Count { get; set; }
        public virtual SparePart SparePart { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
