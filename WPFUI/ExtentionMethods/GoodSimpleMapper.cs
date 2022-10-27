using System;
using System.Collections.Generic;
using System.Linq;

namespace WPFUI.ExtentionMethods
{
    internal static class GoodSimpleMapper
    {
        /*public static List<DataTransferWrapper.Models.Goods> Convert(this List<Models.Goods> goodsList)
        {
            return (from goods in goodsList
                select new DataTransferWrapper.Models.Goods(goods.Name, goods.Count, goods.MinCount, goods.Price)).ToList();
        }
        public static List<Models.Goods> Convert(this List<DataTransferWrapper.Models.Goods> goodsList)
        {
            return (from goods in goodsList
                select new Models.Goods(goods.Name, goods.Count, goods.MinCount, goods.Price)).ToList();
        }
        public static DataTransferWrapper.Models.Goods Convert(this Models.Goods goods)
        {
            return new DataTransferWrapper.Models.Goods(goods.Name, goods.Count, goods.MinCount, goods.Price);
        }
        public static Models.Goods Convert(this DataTransferWrapper.Models.Goods goods)
        {
            return new Models.Goods(goods.Name,goods.Count,goods.MinCount,goods.Price);
        }*/
    }
}
