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
            _context.Addresses.Update(predicate,item);
        }

        public void Clear()
        {
            _context.Addresses.RemoveRange(_context.Addresses);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
           return _context.Addresses.Count();
        }
    }
}
