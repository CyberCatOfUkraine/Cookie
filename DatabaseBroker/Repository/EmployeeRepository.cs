using DatabaseBroker.Models;

namespace DatabaseBroker.Repository
{
    internal class EmployeeRepository:IRepository<Employee>
    {
        private readonly CookieContext _context ;

        public EmployeeRepository(CookieContext context)
        {
            _context=context;
        }

        public void Create(Employee item)
        {
            _context.Employees.Add(item);
        }

        public void Remove(Employee item)
        {
            _context.Employees.Remove(item);
        }

        public void RemoveBy(Predicate<Employee> predicate)
        {
            Remove(Get(predicate));
        }

        public Employee Get(Predicate<Employee> predicate)
        {
            return GetAll().Find(predicate);
        }

        public List<Employee> GetAll()
        {
            return  _context.Employees.ToList();
        }

        public void Update(Predicate<Employee> predicate, Employee item)
        {
            _context.Employees.Update(predicate, item);
        }

        public void Clear()
        {
            _context.Employees.RemoveRange(_context.Employees);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Employees.Count();
        }
    }
}
