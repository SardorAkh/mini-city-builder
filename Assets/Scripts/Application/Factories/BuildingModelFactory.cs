using Application.Repositories;
using Domain.Enums;
using Domain.Models.Buildings;
using UnityEngine;
using VContainer;

namespace Application.Factories
{
    public class BuildingModelFactory
    {
        [Inject] private BuildingConfigsRepository _buildingConfigs;
    
        private int _nextId = 1;
    
        public BuildingModel Create(BuildingType type, Vector2Int position)
        {
           
            return new BuildingModel(_nextId++, type, position);
        }
    }
}