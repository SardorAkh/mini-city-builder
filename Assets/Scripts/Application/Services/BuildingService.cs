using System.Linq;
using Application.Repositories;
using Domain.Enums;
using Domain.Models.Buildings;
using Domain.Models.Economy;
using VContainer;

namespace Application.Services
{
    public class BuildingService
    {
        [Inject] private BuildingConfigsRepository _buildingConfigs;

        public BuildingType GetBuildingTypeById(int id)
        {
            return _buildingConfigs.GetAllBuildings().FirstOrDefault(b => b.id == id).buildingType;
        }
        public Cost GetBuildingCost(int id)
        {
            var info = _buildingConfigs.GetBuildingInfo(id);
            return info?.baseCost ?? Cost.Empty;
        }
    
        public Cost GetUpgradeCost(BuildingModel building)
        {
            var info = _buildingConfigs.GetBuildingInfo(building.Id);
            return info?.GetUpgradeCost(building.Level.Value) ?? Cost.Empty;
        }
    
        public Income GetBuildingIncome(BuildingModel building)
        {
            var info = _buildingConfigs.GetBuildingInfo(building.Id);
            return info?.GetIncomeForLevel(building.Level.Value) ?? Income.Empty;
        }
    
        public bool CanUpgradeBuilding(BuildingModel building)
        {
            var info = _buildingConfigs.GetBuildingInfo(building.Id);
            return building.CanUpgrade(info?.MaxLevel ?? 1);
        }
    }
}