using DatabaseBroker.Models;

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
            _context.Pauses.Update(predicate, item);
        }

        public void Clear()
        {
            _context.Pauses.RemoveRange(_context.Pauses);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Pauses.Count();
        }
    }
}
