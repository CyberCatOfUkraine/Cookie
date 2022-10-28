using DatabaseBroker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Repository
{
    internal class PauseRepository : IRepository<Pause>
    {
        private readonly CookieContext _context = new();

        public PauseRepository(CookieContext context)
        {
            _context=context;
        }

        public void Create(Pause item)
        {
            _context.Pauses.Add(item);
        }

        public void Remove(Pause item)
        {
            _context.Pauses.Remove(item);
        }

        public void RemoveBy(Predicate<Pause> predicate)
        {
            Remove(Get(predicate));
        }

        public Pause Get(Predicate<Pause> predicate)
        {
            return GetAll().Find(predicate);
        }

        public List<Pause> GetAll()
        {
            return _context.Pauses.ToList();
        }

        public void Update(Predicate<Pause> predicate, Pause item)
        {
            var pause = Get(predicate);

            _context.Entry(pause).Entity.StartedTime=item.StartedTime;
            _context.Entry(pause).Entity.FinishedTime=item.FinishedTime;
            _context.Entry(pause).State = EntityState.Modified;
        }

        public void Clear()
        {
            _context.Employees.RemoveRange(_context.Employees);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
