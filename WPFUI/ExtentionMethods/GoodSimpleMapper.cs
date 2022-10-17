using System;
using System.Collections.Generic;
using System.Linq;

namespace WPFUI.ExtentionMethods
{
    internal static class GoodSimpleMapper
    {
        public static List<DataTransferWrapper.Models.Goods> Convert(this List<WPFUI.Models.Goods> goodsList)
        {
            return (from goods in goodsList
                select new DataTransferWrapper.Models.Goods(goods.Name, goods.Count, goods.MinCount, goods.Price)).ToList();
        }
        public static List<WPFUI.Models.Goods> Convert(this List<DataTransferWrapper.Models.Goods> goodsList)
        {
            return (from goods in goodsList
                select new WPFUI.Models.Goods(goods.Name, goods.Count, goods.MinCount, goods.Price)).ToList();
        }
    }
}
