using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutoFixStationDatabaseImplement.Models
{
    public class TO_Work
    {
        public int Id { get; set; }

        public int TOId { get; set; }

        public int WorkId { get; set; }

        [Required]
        public int Count { get; set; }
        public virtual TO TO { get; set; }
        public virtual Work Work { get; set; }
    }
}