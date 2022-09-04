﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.BindingModels
{
    /// <summary>
    /// Машина клиента сервиса
    /// </summary>
    public class CarBindingModel
    {
        public int? Id { get; set; }

        /// <summary>
        /// Бренд авто (VW, Ford)
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Модель авто (Polo, Fusion)
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// VIN-номер авто (длина 17 символов)
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// Номер телефона владельца
        /// </summary>
        public string OwnerPhoneNumber { get; set; }

        /// <summary>
        /// Записи сервисов
        /// </summary>
        /// <summary>
        /// Записи сервисов
        /// </summary>
        public Dictionary<int, ((DateTime, DateTime), string)>? Records { get; set; }
    }
}