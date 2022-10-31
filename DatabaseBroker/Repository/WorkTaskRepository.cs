using DatabaseBroker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var workTask = Get(predicate);
            _context.Entry(workTask).Entity.Name=item.Name;
            _context.Entry(workTask).Entity.Started=item.Started;
            _context.Entry(workTask).Entity.Finished=item.Finished;
            _context.Entry(workTask).Entity.PausesList=item.PausesList;
            _context.Entry(workTask).Entity.AssignedEmployees=item.AssignedEmployees;
            _context.Entry(workTask).Entity.AssignedEmployeesAccesses=item.AssignedEmployeesAccesses;
            _context.Entry(workTask).Entity.Addresses=item.Addresses;
            _context.Entry(workTask).Entity.CurrentState=item.CurrentState;
            _context.Entry(workTask).State = EntityState.Modified;
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
