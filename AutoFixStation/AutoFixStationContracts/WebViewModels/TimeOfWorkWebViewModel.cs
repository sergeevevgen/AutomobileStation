using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationContracts.WebViewModels
{
    public class TimeOfWorkWebViewModel
    {
        public int Id { get; set; }

        [DisplayName("Время")]
        public string Time { get; set; }
    }
}
