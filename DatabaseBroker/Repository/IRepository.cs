using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Repository
{
    public interface IRepository<T> where T:class
    {
        void Create(T item);
        void Remove(T item);
        void RemoveBy(Predicate<T> predicate);
        public T Get(Predicate<T> predicate);
        public List<T> GetAll();
        void Update(Predicate<T> predicate, T item);
        void Clear();
        void SaveChanges();
    }
}
