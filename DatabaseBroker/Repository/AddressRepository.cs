using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseBroker.Models;

namespace DatabaseBroker.Repository
{
    internal class AddressRepository:IRepository<Address>
    {
        private readonly CookieContext _context;

        public AddressRepository(CookieContext context)
        {
            _context=context;
        }

        public void Create(Address item)
        {
            _context.Addresses.Add(item);
        }

        public void Remove(Address item)
        {
            _context.Addresses.Remove(item);
        }

        public void RemoveBy(Predicate<Address> predicate)
        {
            Remove(Get(predicate));
        }

        public Address Get(Predicate<Address> predicate)
        {
            return GetAll().Find(predicate);
        }

        public List<Address> GetAll()
        {
            return _context.Addresses.ToList();
        }

        public void Update(Predicate<Address> predicate, Address item)
        {
            var address = Get(predicate);

            _context.Entry(address).Entity.Oblast = item.Oblast;
            _context.Entry(address).Entity.Rajon = item.Rajon;
            _context.Entry(address).Entity.NaseleniyPunkt = item.NaseleniyPunkt;
            _context.Entry(address).Entity.Vulicya = item.Vulicya;
            _context.Entry(address).Entity.Kvartira = item.Kvartira;
            _context.Entry(address).Entity.Budinok = item.Budinok;
            _context.Entry(address).State = EntityState.Modified; ;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
