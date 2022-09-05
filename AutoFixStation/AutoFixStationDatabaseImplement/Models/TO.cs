using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AutoFixStationContracts.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFixStationDatabaseImplement.Models
{
    public class TO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int EmployeeId { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public TOStatus Status { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public DateTime? DateOver { get; set; }

        public virtual Car Car { get; set; }

        public virtual Employee Employee { get; set; }

        [ForeignKey("TOId")]
        public virtual List<TO_Work> TO_Works { get; set; }
    }
}