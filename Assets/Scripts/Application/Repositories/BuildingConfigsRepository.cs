using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Models.Buildings;
using UnityEngine;

namespace Application.Repositories
{
    [CreateAssetMenu(fileName = "BuildingConfigs", menuName = "CityBuilder/Building Configs")]
    public class BuildingConfigsRepository : ScriptableObject
    {
        [Header("Building Configurations")] [SerializeField]
        private BuildingInfo[] buildings;
    
        private Dictionary<BuildingType, BuildingInfo> _buildingLookup;
    
        [System.NonSerialized] private bool _initialized = false;
    
        public void Initialize()
        {
            if (_initialized) return;
        
            _buildingLookup = buildings.ToDictionary(b => b.buildingType);
            _initialized = true;
        }
    
        public BuildingInfo GetBuildingInfo(BuildingType type)
        {
            if (!_initialized) Initialize();
            return _buildingLookup.GetValueOrDefault(type);
        }
    
        public IEnumerable<BuildingInfo> GetAllBuildings()
        {
            if (!_initialized) Initialize();
            return buildings;
        }
    }
}