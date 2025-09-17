using Domain.Enums;

namespace Domain.Models.Economy
{
    public struct Currency
    {
        private readonly CurrencyType _type;
        private readonly int _amount;

        public CurrencyType Type => _type;
        public int Amount => _amount;

        public Currency(CurrencyType type, int amount)
        {
            _type = type;
            _amount = amount;
        }
    }
}