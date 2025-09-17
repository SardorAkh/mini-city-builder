using Domain.Enums;
using Domain.Models.Economy;
using UnityEngine;

namespace Domain.Models.Buildings
{
    [System.Serializable]
    public class BuildingInfo
    {
        [Header("Basic Info")]
        public int id;
        public string buildingName;
        public BuildingType buildingType;
    
        [Header("Costs")]
        public Cost baseCost;                   
        public Cost[] upgradeCosts;           
    
        [Header("Income")]
        public Income[] incomePerLevel;            
    
        [Header("Visual")]
        public GameObject prefab;
        public Sprite icon;
    
        public int MaxLevel => incomePerLevel.Length;
    
        public Cost GetUpgradeCost(int currentLevel)
        {
            if (currentLevel <= 0 || currentLevel >= MaxLevel) return Cost.Empty;
            return upgradeCosts[currentLevel - 1]; 
        }
    
        public Income GetIncomeForLevel(int level)
        {
            if (level <= 0 || level > incomePerLevel.Length) return Income.Empty;
            return incomePerLevel[level - 1]; 
        }
    }
}