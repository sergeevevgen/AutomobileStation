using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutoFixStationContracts.ViewModels
{
    public class ServiceRecordViewModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }

        [DisplayName("Дата начала")]
        public DateTime DateBegin { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime DateEnd { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}