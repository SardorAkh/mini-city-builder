using System.Collections.Generic;
using System.Linq;
using Domain.Enums;

namespace Domain.Models.Economy
{
    [System.Serializable]
    public struct Income
    {
        private readonly Dictionary<CurrencyType, int> _currenciesPerInterval;
        private readonly int _intervalSeconds;

        public IReadOnlyDictionary<CurrencyType, int> CurrenciesPerInterval => _currenciesPerInterval;
        public int IntervalSeconds => _intervalSeconds;

        public Income(int intervalSeconds, params Currency[] currencies)
        {
            _intervalSeconds = intervalSeconds;
            _currenciesPerInterval = currencies.ToDictionary(r => r.Type, r => r.Amount);
        }

        public Income(CurrencyType type, int amount, int intervalSeconds)
        {
            _intervalSeconds = intervalSeconds;
            _currenciesPerInterval = new Dictionary<CurrencyType, int> { { type, amount } };
        }

        public int GetAmountPerInterval(CurrencyType type) => _currenciesPerInterval.GetValueOrDefault(type, 0);
        public static Income Empty => new();
    }
}