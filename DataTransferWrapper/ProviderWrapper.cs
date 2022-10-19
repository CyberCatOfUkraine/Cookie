using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferWrapper.Models;

namespace DataTransferWrapper
{
    public class ProviderWrapper:IWrapper<Provider,string>
    {
        private static List<Provider> providersList = new();
        public int Length => providersList.Count;

        public void Add(Provider value)
        {
            if (providersList.Exists(x => x.Name == value.Name))
            {
                throw new InvalidOperationException("Знайдено дублікат, додавання постачальника неможливе !");
            }
            providersList.Add(value);
        }

        public void Sort()
        {
            var thread = new Thread(() =>
            {
                providersList = (from provider in providersList orderby provider.Name select provider).ToList();
            });
            thread.Start();
            thread.Join();
        }
        public void Remove(string key)
        {
            Provider provider;

            try
            {
                provider = GetBy(key);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Постачальника з даним іменем не знайдено, видалення постачальника неможливе !");
            }

            providersList.Remove(provider);
        }

        public Provider GetBy(string key)
        {

            if (!providersList.Exists(x => x.Name == key))
            {
                throw new InvalidOperationException("Постачальника з даним іменем не знайдено, отримання постачальника неможливе !");
            }

            return providersList.First(x => x.Name == key);
        }

        public List<Provider> GetAll()
        {
            return providersList;
        }

        public void Update(string key, Provider value)
        {
            Provider provider;

            try
            {
                provider = GetBy(key);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Постачальника з даним іменем не знайдено, редагування даних про постачальника неможливе !");
            }

            var providerId = providersList.IndexOf(provider);
            providersList[providerId] = value;
        }
    }
}
