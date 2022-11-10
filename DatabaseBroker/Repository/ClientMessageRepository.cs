using DatabaseBroker.Models;
using System.Data.Entity;

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
            var clientMessage = Get(predicate);

            _context.Entry(clientMessage).Entity.Text = item.Text;
            _context.Entry(clientMessage).Entity.RecivedTime = item.RecivedTime;
            _context.Entry(clientMessage).Entity.Address = item.Address;
            _context.Entry(clientMessage).State = EntityState.Modified;
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
