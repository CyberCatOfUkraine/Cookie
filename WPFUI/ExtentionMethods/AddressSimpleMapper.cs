using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ExtentionMethods
{
    internal static class AddressSimpleMapper
    {

        internal static WPFUI.Models.Address ConvertToWPFAddress(this DatabaseBroker.Models.Address address)
        {
            return new WPFUI.Models.Address(address.Region,address.District,address.Settlement,address.Street,address.House,address.Apartment) { Id = address.Id };
        }

        internal static DatabaseBroker.Models.Address ConvertToDatabaseAddress(this WPFUI.Models.Address address)
        {
            return new DatabaseBroker.Models.Address {
                Region = address.Region,
                District = address.District,
                Settlement = address.Settlement, 
                Street = address.Street, 
                House = address.House, 
                Apartment = address.Apartment };
        }
        internal static List<WPFUI.Models.Address> ConvertToWPFAddress(this List<DatabaseBroker.Models.Address> addresses)
        {

            if (addresses == null)
            {
                return new List<WPFUI.Models.Address>();
            }
            return (from address in addresses select new WPFUI.Models.Address(address.Region,address.District,address.Settlement,address.Street,address.House,address.Apartment) { Id = address.Id }).ToList();
        }

        internal static List<DatabaseBroker.Models.Address> ConvertToDatabaseAddress(this List<WPFUI.Models.Address> addresses)
        {
            if (addresses==null)
            {
                return new List<DatabaseBroker.Models.Address>();
            }
            return (from address in addresses
                select
                new DatabaseBroker.Models.Address {
                Region = address.Region,
                District = address.District,
                Settlement = address.Settlement, 
                Street = address.Street, 
                House = address.House, 
                Apartment = address.Apartment }).ToList();
        }
    }
}
