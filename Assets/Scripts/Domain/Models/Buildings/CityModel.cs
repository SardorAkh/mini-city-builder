using System.Collections.Generic;
using UnityEngine;

namespace Domain.Models.Buildings
{
    public class CityModel
    {
        private readonly Dictionary<int, BuildingModel> _buildings = new();
        private readonly Dictionary<Vector2Int, BuildingModel> _buildingsByPosition = new();
    
        public IReadOnlyCollection<BuildingModel> AllBuildings => _buildings.Values;
    
        public void AddBuilding(BuildingModel building)
        {
            _buildings[building.UniqueId] = building;
            _buildingsByPosition[building.Position] = building;
        }
    
        public void RemoveBuilding(int buildingId)
        {
            if (_buildings.TryGetValue(buildingId, out var building))
            {
                _buildings.Remove(buildingId);
                _buildingsByPosition.Remove(building.Position);
            }
        }
    
        public BuildingModel GetBuilding(int id) => 
            _buildings.GetValueOrDefault(id);
    
        public BuildingModel GetBuildingAtPosition(Vector2Int position) => 
            _buildingsByPosition.GetValueOrDefault(position);
    
        public bool HasBuildingAt(Vector2Int position) => 
            _buildingsByPosition.ContainsKey(position);
    }
}