using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferWrapper;
using WPFUI.Models;

namespace WPFUI.ExtentionMethods
{
    internal static class ProviderSimpleMapper
    {
        public static List<DataTransferWrapper.Models.Provider> Convert(this List<WPFUI.Models.Provider> providersList)
        {
            return (from provider in providersList
                select new DataTransferWrapper.Models.Provider(provider.Name,provider.GoodsList.Convert(),provider.TelegramId)).ToList();
        }
        public static List<WPFUI.Models.Provider> Convert(this List<DataTransferWrapper.Models.Provider> providersList)
        {
            return (from provider in providersList
                select new WPFUI.Models.Provider(provider.Name,provider.GoodsList.Convert(),provider.TelegramId)).ToList();
        }
        public static List<WPFUI.Models.ProviderUI> ConvertUI(this List<DataTransferWrapper.Models.Provider> providersList)
        {
            
            return (from provider in providersList
                select new WPFUI.Models.ProviderUI(provider.Name, (from goods in provider.GoodsList select goods.Name).EnumerableToCustomString(), provider.TelegramId)).ToList();
        }
        
        public static WPFUI.Models.Provider Convert(this WPFUI.Models.ProviderUI providerUI)
        {

            DataTransferWrapper.Models.Provider provider;
            try
            {
                provider = new ProviderWrapper().GetBy(providerUI.Name);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Помилка перетворення типу. "+e.Message);
            }

            return new Provider(provider.Name,provider.GoodsList.Convert(),provider.TelegramId) ;
        }

        private static string EnumerableToCustomString(this IEnumerable enumerable)
        {
            var sb = new StringBuilder();
            foreach (var value in enumerable)
            {

                sb.AppendLine(value as string);
            }

            return sb.ToString();
        }
    }
}
