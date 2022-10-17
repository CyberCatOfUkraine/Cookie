using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferWrapper
{
    internal interface IWrapper<T,KeyType> where T : class
    {
        void Add(T value);
        void Remove(KeyType key);
        T GetBy(KeyType key);
        List<T> GetAll();
        void Update(KeyType key,T value);
    }
}
