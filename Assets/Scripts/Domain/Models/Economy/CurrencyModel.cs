using System.Collections.Generic;
using Domain.Enums;
using R3;

namespace Domain.Models.Economy
{
    public class CurrencyModel
    {
        private readonly Dictionary<CurrencyType, int> _currencies;
        public ReactiveProperty<Dictionary<CurrencyType, int>> CurrentCurrencies { get; }

        public CurrencyModel()
        {
            _currencies = new Dictionary<CurrencyType, int>();
            CurrentCurrencies = new(_currencies);

            _currencies[CurrencyType.Gold] = 1000;
        }

        public int GetAmount(CurrencyType type) => _currencies.GetValueOrDefault(type, 0);

        public bool CanAfford(Cost cost)
        {
            foreach (var currency in cost.Currencies)
            {
                if (GetAmount(currency.Type) < currency.Amount)
                    return false;
            }

            return true;
        }

        public void Spend(Cost cost)
        {
            foreach (var currency in cost.Currencies)
            {
                _currencies[currency.Type] = GetAmount(currency.Type) - currency.Amount;
            }

            NotifyChanged();
        }

        public void Add(Currency currency)
        {
            _currencies[currency.Type] = GetAmount(currency.Type) + currency.Amount;
            NotifyChanged();
        }

        public void Add(Income income)
        {
            foreach (var currency in income.currencies)
            {
                Add(currency);
            }
        }

        private void NotifyChanged()
        {
            CurrentCurrencies.OnNext(new Dictionary<CurrencyType, int>(_currencies));
        }
    }
}