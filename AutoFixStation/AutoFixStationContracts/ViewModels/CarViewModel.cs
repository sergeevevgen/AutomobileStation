using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutoFixStationContracts.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [DisplayName("Марка")]
        public string Brand { get; set; }

        [DisplayName("Модель")]
        public string Model { get; set; }

        [DisplayName("VIN")]
        public string VIN { get; set; }

        [DisplayName("Номер телефона владельца")]
        public string OwnerPhoneNumber { get; set; }

        /// <summary>
        /// Записи сервисов (номер, ((Дата начала, Дата конца), описание)
        /// </summary>
        public Dictionary<int, ((DateTime, DateTime), string)>? Records { get; set; }
    }
}