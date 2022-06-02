using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseAPI.Models
{
    public class Currency
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Symbol { get; set; }

        public string GetCurrencyInfos()
        {
            return String.Format("{0}, {1}", Name, Symbol);
        }
    }
}
