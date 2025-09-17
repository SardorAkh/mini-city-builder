using Application.Repositories;
using Domain.Enums;
using Domain.Models.Buildings;
using Domain.Models.Economy;
using VContainer;

namespace Application.Services
{
    public class BuildingEconomicsService
    {
        [Inject] private BuildingConfigsRepository _buildingConfigs;
    
        public Cost GetBuildingCost(BuildingType type)
        {
            var info = _buildingConfigs.GetBuildingInfo(type);
            return info?.baseCost ?? Cost.Empty;
        }
    
        public Cost GetUpgradeCost(BuildingModel building)
        {
            var info = _buildingConfigs.GetBuildingInfo(building.Type);
            return info?.GetUpgradeCost(building.Level.Value) ?? Cost.Empty;
        }
    
        public Income GetBuildingIncome(BuildingModel building)
        {
            var info = _buildingConfigs.GetBuildingInfo(building.Type);
            return info?.GetIncomeForLevel(building.Level.Value) ?? Income.Empty;
        }
    
        public bool CanUpgradeBuilding(BuildingModel building)
        {
            var info = _buildingConfigs.GetBuildingInfo(building.Type);
            return building.CanUpgrade(info?.MaxLevel ?? 1);
        }
    }
}