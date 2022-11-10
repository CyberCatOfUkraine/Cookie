using DatabaseBroker.Models;
using System.Data.Entity;

namespace DatabaseBroker.Repository
{
    internal class AccessRepository:IRepository<Access>
    {
        private readonly CookieContext _context;

        public AccessRepository(CookieContext context)
        {
            _context = context;
        }

        public void Create(Access item)
        {
            _context.Accesses.Add(item);
        }

        public void Remove(Access item)
        {
            _context.Accesses.Remove(item);
        }

        public void RemoveBy(Predicate<Access> predicate)
        {
            Remove(Get(predicate));
        }

        public Access Get(Predicate<Access> predicate)
        {
            return GetAll().Find(predicate);
        }

        public List<Access> GetAll()
        {
            return _context.Accesses.ToList();
        }

        public void Update(Predicate<Access> predicate, Access item)
        {
            var access = Get(predicate);

            _context.Entry(access).Entity.Name = item.Name;
            _context.Entry(access).State = EntityState.Modified;
        }

        public void Clear()
        {
            _context.Accesses.RemoveRange(_context.Accesses);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
           return _context.Accesses.Count();
        }
    }
}
