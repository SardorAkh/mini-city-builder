using Domain.Enums;
using UnityEngine;

namespace Domain.Models.Economy
{
    [System.Serializable]
    public struct Currency
    {
        [SerializeField] private CurrencyType _type;
        [SerializeField] private int _amount;

        public CurrencyType Type => _type;
        public int Amount => _amount;

        public Currency(CurrencyType type, int amount)
        {
            _type = type;
            _amount = amount;
        }
    }
}