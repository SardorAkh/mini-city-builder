using System;
using Application.Factories;
using Application.Services;
using Domain.MessagesDTO;
using Domain.Models.Buildings;
using Domain.Models.Economy;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases
{
    public class PlaceBuildingUseCase : IMessageHandler<PlaceBuildingDTO>
    {
        [Inject] private GridService _gridService;
        [Inject] private CityModel _cityModel;
        [Inject] private CurrencyModel _currencyModel;
        [Inject] private BuildingModelFactory _buildingFactory;
        [Inject] private BuildingEconomicsService _economicsService;

        [Inject] private readonly IPublisher<CellOccupiedDTO> _cellOccupiedDtoPublisher;
        [Inject] private readonly IPublisher<NotEnoughCurrencyDTO> _notEnoughCurrencyDtoPublisher;
        [Inject] private readonly IPublisher<BuildingCreatedDTO> _buildingCreatedDtoPublisher;

        [Inject] private readonly ISubscriber<PlaceBuildingDTO> _placeBuildingDtoSubscriber;


        public void Handle(PlaceBuildingDTO dto)
        {
            if (!_gridService.CanPlaceBuilding(dto.Position))
            {
                _cellOccupiedDtoPublisher.Publish(new CellOccupiedDTO { Position = dto.Position });
                return;
            }

            var cost = _economicsService.GetBuildingCost(dto.BuildingType);
            if (!_currencyModel.CanAfford(cost))
            {
                _notEnoughCurrencyDtoPublisher.Publish(new NotEnoughCurrencyDTO { TargetCost = cost });
                return;
            }

            var building = _buildingFactory.Create(dto.BuildingType, dto.Position);

            _cityModel.AddBuilding(building);
            _gridService.GridModel.OccupyCell(dto.Position);
            _currencyModel.Spend(cost);

            _buildingCreatedDtoPublisher.Publish(new BuildingCreatedDTO
            {
                BuildingId = building.UniqueId,
                Position = dto.Position
            });
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}