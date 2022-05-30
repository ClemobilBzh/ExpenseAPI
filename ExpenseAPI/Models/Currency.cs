using System;

namespace ExpenseApi.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public string GetCurrencyInfos()
        {
            return String.Format("{0}, {1}", Name, Symbol);
        }
    }
}
