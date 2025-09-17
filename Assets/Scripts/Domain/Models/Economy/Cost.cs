using System.Collections.Generic;
using System.Linq;
using Domain.Enums;

namespace Domain.Models.Economy
{
    [System.Serializable]
    public struct Cost 
    {
        private readonly Dictionary<CurrencyType, int> _currencies;
    
        public IReadOnlyDictionary<CurrencyType, int> Currencies => _currencies;
    
        public Cost(params Currency[] resources)
        {
            _currencies = resources.ToDictionary(r => r.Type, r => r.Amount);
        }
        
        public Cost(CurrencyType type, int amount)
        {
            _currencies = new Dictionary<CurrencyType, int> { { type, amount } };
        }
    
        public int GetAmount(CurrencyType type) => _currencies.GetValueOrDefault(type, 0);
        public bool IsEmpty => !_currencies.Any();
        public static Cost Empty => new Cost();
    }
}