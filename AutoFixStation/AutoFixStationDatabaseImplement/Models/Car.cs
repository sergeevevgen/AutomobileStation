using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFixStationDatabaseImplement.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string VIN { get; set; }

        [Required]
        public string OwnerPhoneNumber { get; set; }

        [ForeignKey("CarId")]
        public virtual List<ServiceRecord> ServiceRecords { get; set; }

        [ForeignKey("CarId")]
        public virtual List<TO> TOs { get; set; }
    }
}