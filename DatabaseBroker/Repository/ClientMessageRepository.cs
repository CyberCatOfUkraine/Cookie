using DatabaseBroker.Models;

namespace DatabaseBroker.Repository
{
    internal class ClientMessageRepository:IRepository<ClientMessage>
    {
        private readonly CookieContext _context;

        public ClientMessageRepository(CookieContext context)
        {
            _context=context;
        }

        public void Create(ClientMessage item)
        {
            _context.ClientMessages.Add(item);
        }

        public void Remove(ClientMessage item)
        {
            _context.ClientMessages.Remove(item);
        }

        public void RemoveBy(Predicate<ClientMessage> predicate)
        {
            Remove(Get(predicate));
        }

        public ClientMessage Get(Predicate<ClientMessage> predicate)
        {
           return GetAll().Find(predicate);
        }

        public List<ClientMessage> GetAll()
        {
            return _context.ClientMessages.ToList();
        }

        public void Update(Predicate<ClientMessage> predicate, ClientMessage item)
        {
            _context.ClientMessages.Update(predicate, item);
        }

        public void Clear()
        {
            _context.ClientMessages.RemoveRange(_context.ClientMessages);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.ClientMessages.Count();
        }
    }
}
