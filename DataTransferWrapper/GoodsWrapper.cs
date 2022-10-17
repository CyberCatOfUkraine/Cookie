using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferWrapper.Models;

namespace DataTransferWrapper
{
    public class GoodsWrapper:IWrapper<Goods,string>
    {
        private static List<Goods> goodsList=new();
        public int Length =>goodsList.Count;

        public void Add(Goods value)
        {
            if (goodsList.Exists(x => x.Name == value.Name))
            {
                throw new InvalidOperationException("Знайдено дублікат, додавання товару неможливе !");
            }
            goodsList.Add(value);
        }

        public void Sort()
        {
            var thread = new Thread(() =>
            {
                goodsList = (from goods in goodsList orderby goods.Name select goods).ToList();
            });
            thread.Start();
            thread.Join();
        }
        public void Remove(string key)
        {
            Goods goods;

            try
            {
                goods = GetBy(key);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Товару з даним іменем не знайдено, видалення товару неможливе !");
            }

            goodsList.Remove(goods);
        }

        public Goods GetBy(string key)
        {
            
            if (!goodsList.Exists(x => x.Name == key))
            {
                throw new InvalidOperationException("Товару з даним іменем не знайдено, отримання товару неможливе !");
            }

            return goodsList.First(x => x.Name == key);
        }

        public List<Goods> GetAll()
        {
            return goodsList;
        }

        public void Update(string key, Goods value)
        {
            Goods goods;

            try
            {
                goods = GetBy(key);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Товару з даним іменем не знайдено, оновлення товару неможливе !");
            }

            var goodsId = goodsList.IndexOf(goods);
            goodsList[goodsId] = value;
        }
    }
    
}
