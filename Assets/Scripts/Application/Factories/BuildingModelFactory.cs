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
        
        public BuildingModel Create(int id, BuildingType buildingType,Vector2Int position)
        {
            return new BuildingModel(id, buildingType, position);
        }
    }
}