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
    public class Work
    {
        public int Id { get; set; }
        public int StoreKeeperId { get; set; }
        public int WorkTypeId { get; set; }

        [Required]
        public string WorkName { get; set; }

        //[Required]
        //public int Count { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal NetPrice { get; set; }

        [Required]
        public WorkStatus WorkStatus { get; set; }

        public virtual StoreKeeper StoreKeeper { get; set; }

        public virtual WorkType WorkType { get; set; }

        [ForeignKey("WorkId")]
        public virtual List<TO_Work> TO_Works { get; set; }
    }
}