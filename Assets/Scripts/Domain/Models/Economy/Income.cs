using System.Collections.Generic;
using System.Linq;
using Domain.Enums;

namespace Domain.Models.Economy
{
    [System.Serializable]
    public struct Income
    {
        public Currency[] currencies;
        public int intervalSeconds;

        public Income(int intervalSeconds, params Currency[] currencies)
        {
            this.intervalSeconds = intervalSeconds;
            this.currencies = currencies ?? new Currency[0];
        }

        public Income(CurrencyType type, int amount, int intervalSeconds)
        {
            this.intervalSeconds = intervalSeconds;
            this.currencies = new Currency[] { new Currency(type, amount) };
        }

        public int GetAmountPerInterval(CurrencyType type)
        {
            foreach (var currency in currencies)
            {
                if (currency.Type == type)
                    return currency.Amount;
            }
            return 0;
        }

        public static Income Empty => new Income(0);
    }
}