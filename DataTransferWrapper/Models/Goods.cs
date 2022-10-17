namespace DataTransferWrapper.Models
{
    public class Goods
    {
        public Goods(string name, int count, int minCount, decimal price)
        {
            Name = name;
            Count = count;
            MinCount = minCount;
            Price = price;
        }

        public string Name { get; set; }
        public int Count { get; set; }
        public int MinCount { get; set; }
        public decimal Price { get; set; }
    }
}