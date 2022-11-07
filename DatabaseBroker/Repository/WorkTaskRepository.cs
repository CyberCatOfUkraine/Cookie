using DatabaseBroker.Models;

namespace DatabaseBroker.Repository
{
    internal class WorkTaskRepository:IRepository<WorkTask>
    {
        private readonly CookieContext _context;

        public WorkTaskRepository(CookieContext context)
        {
            _context=context;
        }

        public void Create(WorkTask item)
        {
            _context.WorkTasks.Add(item);
        }

        public void Remove(WorkTask item)
        {
            _context.WorkTasks.Remove(item);
        }

        public void RemoveBy(Predicate<WorkTask> predicate)
        {
            Remove(Get(predicate));
        }

        public WorkTask Get(Predicate<WorkTask> predicate)
        {
            return GetAll().Find(predicate);
        }

        public List<WorkTask> GetAll()
        {
            return _context.WorkTasks.ToList();
        }

        public void Update(Predicate<WorkTask> predicate, WorkTask item)
        {
            _context.WorkTasks.Update(predicate,item);
        }

        public void Clear()
        {
            _context.WorkTasks.RemoveRange(_context.WorkTasks);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.WorkTasks.Count();
        }
    }
}
