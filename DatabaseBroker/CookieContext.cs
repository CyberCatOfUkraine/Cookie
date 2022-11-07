using System.Text;
using DatabaseBroker.Models;
using Newtonsoft.Json;

namespace DatabaseBroker
{
    internal class CookieContext:IDisposable
    {
        private FileStream _accessFileStream;
        private FileStream _adddressFileStream;
        private FileStream _clientMessagesFileStream;
        private FileStream _employeesFileStream;
        private FileStream _PausesFileStream;
        private FileStream _WorkTasksFileStream;
        private List<FileStream> _closableFileStreams;
        private string prorgamFolderName = "Cookie";
        public CookieContext()
        {
            //var generalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            //    prorgamFolderName);
            //_accessFileStream = File.Open(Path.Combine(generalPath,typeof(Access).ToString()), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            //_adddressFileStream = File.Open(Path.Combine(generalPath,typeof(Access).ToString()), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            //_clientMessagesFileStream = File.Open(Path.Combine(generalPath,typeof(Access).ToString()), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            //_employeesFileStream = File.Open(Path.Combine(generalPath,typeof(Access).ToString()), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            //_PausesFileStream = File.Open(Path.Combine(generalPath,typeof(Access).ToString()), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            //_WorkTasksFileStream = File.Open(Path.Combine(generalPath,typeof(Access).ToString()), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            //LoadDataFromFiles();
            /*
             * _Accesses = null;
               _Addresses = null;
               _ClientMessages = null;
               _Employees = null;
               _Pauses = null;
               _WorkTasks = null;
             */

        }
        public CustomDbList<Address> Addresses => _Addresses;
        public CustomDbList<ClientMessage> ClientMessages => _ClientMessages;
        public CustomDbList<Access> Accesses => _Accesses;
        public CustomDbList<Employee> Employees => _Employees;
        public CustomDbList<Pause> Pauses => _Pauses;
        public CustomDbList<WorkTask> WorkTasks => _WorkTasks;
        private static CustomDbList<Address> _Addresses = new();
        private static CustomDbList<ClientMessage> _ClientMessages = new();
        private static CustomDbList<Access> _Accesses = new();
        private static CustomDbList<Employee> _Employees = new();
        private static CustomDbList<Pause> _Pauses = new();
        private static CustomDbList<WorkTask> _WorkTasks = new();

        public void Dispose()
        {
            _Accesses.Clear();
            _Addresses.Clear();
            _ClientMessages.Clear();
            _Employees.Clear();
            _Pauses.Clear();
            _WorkTasks.Clear();

            foreach (var fileStream in _closableFileStreams)
            {
                fileStream.Close();
            }
        }
        public void SaveChanges()
        {
            //SaveDataToFiles();
        }

        private async void LoadDataFromFiles()
        {
            
        }
        private void SaveDataToFiles()
        {
            _accessFileStream.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_Accesses)));  
            
        }

        ~CookieContext()
        {
            /*foreach (var fileStream in _closableFileStreams)
            {
                fileStream.Close();
            }*/
        }
    }

    internal class CustomDbList<T>:List<T> where T:class
    {
        internal void Update(Predicate<T> predicate, T item)
        {
            if (Exists(predicate))
            {
                T listItem = Find(predicate);
                var index = IndexOf(listItem);
                this.Insert(index, item);

                this.Remove(listItem);

            }
            else
            {
                throw new NullReferenceException(typeof(T)+" element not found,update canceled!");
            }
            
        }

        internal void RemoveRange(IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
            {
                Remove(item);
            }
        }
    }
}
