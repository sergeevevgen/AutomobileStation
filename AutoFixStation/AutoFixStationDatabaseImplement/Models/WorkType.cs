using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFixStationDatabaseImplement.Models
{
    public class WorkType
    {
        public int Id { get; set; }

        public int TimeOfWorkId { get; set; }

        [Required]
        public string WorkName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal NetPrice { get; set; }//with spareparts

        public virtual TimeOfWork TimeOfWork { get; set; }

        [ForeignKey("WorkTypeId")]
        public virtual List<WorkType_SparePart> Work_SpareParts { get; set; }

        [ForeignKey("WorkTypeId")]
        public virtual List<Work> Works { get; set; }
    }
}
