using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{
    public class Address
    {
        /// <summary>
        /// Конструктор що задає адресу
        /// </summary>
        /// <param name="region">Область</param>
        /// <param name="district">Район</param>
        /// <param name="settlement">Населений пункт</param>
        /// <param name="street">Вулиця</param>
        /// <param name="house">Будинок</param>
        /// <param name="apartment">Квартира</param>
        public Address(string region, string district, string settlement, string street, string house, string? apartment)
        {
            Region = region;
            District = district;
            Settlement = settlement;
            Street = street;
            House = house;
            Apartment = apartment;
        }

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
        /// Квартира
        /// </summary>
        public string? Apartment { get; set; }
    }
}
