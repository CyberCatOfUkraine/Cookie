using System.Data.Entity;
using System.Text;
using DatabaseBroker.Models;
using Newtonsoft.Json;

namespace DatabaseBroker
{
    internal class CookieContext : DbContext
    {
        public CookieContext()
        {
            //Database.Delete();
            Database.CreateIfNotExists();
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ClientMessage> ClientMessages { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Pause> Pauses { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }
    }
}
