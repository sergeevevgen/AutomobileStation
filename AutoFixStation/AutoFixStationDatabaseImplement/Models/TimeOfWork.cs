using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFixStationDatabaseImplement.Models
{
    public class TimeOfWork
    {
        public int Id { get; set; }

        [Required]
        public int Hours { get; set; }
        
        [Required]
        public int Mins { get; set; }

        [ForeignKey("TimeOfWorkId")]
        public virtual List<WorkType> WorkTypes { get; set; }
    }
}