using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ExtentionMethods
{
    internal static class AddressSimpleMapper
    {

        internal static WPFUI.Models.Address Convert(this DatabaseBroker.Models.Address address)
        {
            return new WPFUI.Models.Address(address.Region,address.District,address.Settlement,address.Street,address.House,address.Apartment) { Id = address.Id };
        }

        internal static DatabaseBroker.Models.Address Convert(this WPFUI.Models.Address address)
        {
            return new DatabaseBroker.Models.Address {
                Region = address.Region,
                District = address.District,
                Settlement = address.Settlement, 
                Street = address.Street, 
                House = address.House, 
                Apartment = address.Apartment };
        }
    }
}
