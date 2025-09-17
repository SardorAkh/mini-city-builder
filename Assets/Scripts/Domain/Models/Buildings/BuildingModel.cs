using Domain.Enums;
using R3;
using UnityEngine;

namespace Domain.Models.Buildings
{
    public class BuildingModel
    {
        public int UniqueId { get; private set; }
        public BuildingType Type { get; private set; }
        public Vector2Int Position { get; set; }
        public ReactiveProperty<int> Level { get; private set; }
    
        public BuildingModel(int uniqueId, BuildingType type, Vector2Int position)
        {
            UniqueId = uniqueId;
            Type = type;
            Position = position;
            Level = new ReactiveProperty<int>(1); 
        }
    
        public void UpgradeLevel()
        {
            Level.Value++;
        }
    
        public bool CanUpgrade(int maxLevel)
        {
            return Level.Value < maxLevel;
        }
    }
}