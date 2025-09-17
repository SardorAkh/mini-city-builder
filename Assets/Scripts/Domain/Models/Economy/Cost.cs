using System.Collections.Generic;
using System.Linq;
using Domain.Enums;

namespace Domain.Models.Economy
{
    [System.Serializable]
    public struct Cost 
    {
        public Currency[] Currencies;
    
        public Cost(params Currency[] currencies)
        {
            Currencies = currencies ?? new Currency[0];
        }
        public int GetAmount(CurrencyType type)
        {
            foreach (var currency in Currencies)
            {
                if (currency.Type == type)
                    return currency.Amount;
            }
            return 0;
        }
        public bool IsEmpty => !Currencies.Any();
        public static Cost Empty => new Cost();
    }
}