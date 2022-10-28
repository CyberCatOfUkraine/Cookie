using System.Data.Entity;

namespace DataBroker
{
    public class TestContext : DbContext
    {
        public DbSet<String> test { get; set; }
    }
    public class DatabaseBroker<T> where T:class //TODO:CRUD
    {
        private static List<T> _itemsList = new();
        
        public void Create(T item)
        {
            _itemsList.Add(item);
        }

        public void Remove(T item)
        {
            _itemsList.Remove(item);
        }

        public void RemoveBy(Predicate<T> predicate)
        {
            Remove(Get(predicate));
        }
        public T? Get(Predicate<T> predicate)
        {
            return _itemsList.Exists(predicate) ? _itemsList.Find(predicate) : null;
        }

        public List<T> GetAll()
        {
            return _itemsList;
        }

        public void Update(Predicate<T> predicate, T newItem)
        {
            if (_itemsList.Exists(predicate))
            {
                _itemsList.Remove(Get(predicate));
                _itemsList.Add(newItem);
            }
            else
            {
                throw new InvalidOperationException("This "+newItem.GetType()+" not exist. Updating aborted");
            }
        }

        public void Clear()
        {

        }
    }
}