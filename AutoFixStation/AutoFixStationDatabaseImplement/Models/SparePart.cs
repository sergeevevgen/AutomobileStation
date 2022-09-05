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
    public class SparePart
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FactoryNumber { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public SparePartStatus Type { get; set; }

        [Required]
        public UnitMeasurement UMeasurement { get; set; }

        [ForeignKey("SparePartId")]
        public virtual List<WorkType_SparePart> Work_SpareParts { get; set; }
    }
}