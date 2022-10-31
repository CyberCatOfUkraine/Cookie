using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseBroker.Models;
using DatabaseBroker.Repository;

namespace DatabaseBroker
{
    public class UnitOfCookie:IDisposable
    {
        private static CookieContext _context = new();
        public IRepository<Address> AddressRepository =>  new AddressRepository(_context);

        public IRepository<ClientMessage> ClientMessageRepository => new ClientMessageRepository(_context);
        public IRepository<Access> AccessRepository => new AccessRepository(_context);
        public IRepository<Employee> EmployeeRepository => new EmployeeRepository(_context);
        public IRepository<Pause> PauseRepository => new PauseRepository(_context);
        public IRepository<WorkTask> WorkTaskRepository => new WorkTaskRepository(_context);

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
