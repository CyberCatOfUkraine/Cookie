using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Область
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Район
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Населений пункт
        /// </summary>
        public string Settlement { get; set; }

        /// <summary>
        /// Вулиця
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Будинок
        /// </summary>
        public string House { get; set; }

        /// <summary>
        /// Квартира, Nullable оскільки може бути приватний будинок
        /// </summary>
        public string? Apartment { get; set; }
    }
}
