using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutoFixStationContracts.ViewModels
{
    public class TimeOfWorkViewModel
    {
        public int Id { get; set; }

        [DisplayName("Часы")]
        public int Hours { get; set; }

        [DisplayName("Минуты")]
        public int Mins { get; set; }
    }
}