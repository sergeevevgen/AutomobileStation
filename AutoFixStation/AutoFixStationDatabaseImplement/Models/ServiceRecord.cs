using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutoFixStationDatabaseImplement.Models
{
    public class ServiceRecord
    {
        public int Id { get; set; }
        
        public int CarId { get; set; }

        [Required]
        public DateTime DateBegin { get; set; }
        
        [Required]
        public DateTime DateEnd { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual Car Car { get; set; }
    }
}
