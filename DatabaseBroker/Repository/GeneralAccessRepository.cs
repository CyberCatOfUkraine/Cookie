using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseBroker.Models;

namespace DatabaseBroker.Repository
{
    public class GeneralAccessRepository:IRepository<GeneralAccess>
    {
        private readonly CookieContext _context;

        internal GeneralAccessRepository(CookieContext context)
        {
            _context = context;
        }

        public void Create(GeneralAccess item)
        {
            _context.GeneralAccesses.Add(item);
        }

        public void Remove(GeneralAccess item)
        {
            _context.GeneralAccesses.Remove(item);
        }

        public void RemoveBy(Predicate<GeneralAccess> predicate)
        {
            _context.GeneralAccesses.Remove(Get(predicate));
        }

        public GeneralAccess Get(Predicate<GeneralAccess> predicate)
        {
            return GetAll().Find(predicate);
        }
        public List<GeneralAccess> GetAll()
        {
            return _context.GeneralAccesses.ToList();
        }

        public void Update(Predicate<GeneralAccess> predicate, GeneralAccess item)
        {
            var access = Get(predicate);

            _context.Entry(access).Entity.Name = item.Name;
            _context.Entry(access).State = EntityState.Modified;
        }

        public void Clear()
        {
            _context.GeneralAccesses.RemoveRange(_context.GeneralAccesses);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
           return _context.GeneralAccesses.Count();
        }
    }
}
