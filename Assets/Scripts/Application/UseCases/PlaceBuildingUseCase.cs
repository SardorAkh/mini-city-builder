using System;
using Application.Factories;
using Application.Interfaces;
using Application.Services;
using Domain.MessagesDTO;
using Domain.Models.Buildings;
using Domain.Models.Economy;
using MessagePipe;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases
{
    public class PlaceBuildingUseCase : IMessageHandler<PlaceBuildingDTO>, IInitializable, IDisposable
    {
        [Inject] private IGridService _gridService;
        [Inject] private CityModel _cityModel;
        [Inject] private CurrencyModel _currencyModel;
        [Inject] private BuildingModelFactory _buildingFactory;
        [Inject] private BuildingSelectionModel _buildingSelectionModel;
        [Inject] private BuildingService _buildingService;

        [Inject] private readonly IPublisher<CellOccupiedDTO> _cellOccupiedDtoPublisher;
        [Inject] private readonly IPublisher<NotEnoughCurrencyDTO> _notEnoughCurrencyDtoPublisher;
        [Inject] private readonly IPublisher<BuildingCreatedDTO> _buildingCreatedDtoPublisher;

        [Inject] private readonly ISubscriber<PlaceBuildingDTO> _placeBuildingDtoSubscriber;

        private CompositeDisposable _disposable = new();
        public void Handle(PlaceBuildingDTO dto)
        {
            if (!_gridService.CanPlaceBuilding(dto.Position))
            {
                _cellOccupiedDtoPublisher.Publish(new CellOccupiedDTO { Position = dto.Position });
                return;
            }
            var selectedBuildingId = _buildingSelectionModel.SelectedBuildingTypeId.Value;

            var cost = _buildingService.GetBuildingCost(selectedBuildingId);
            // if (!_currencyModel.CanAfford(cost))
            // {
            //     _notEnoughCurrencyDtoPublisher.Publish(new NotEnoughCurrencyDTO { TargetCost = cost });
            //     return;
            // }

            var building = _buildingFactory.Create(selectedBuildingId, _buildingService.GetBuildingTypeById(selectedBuildingId), dto.Position);

            _cityModel.AddBuilding(building);
            _gridService.OccupyCell(dto.Position);
            _currencyModel.Spend(cost);
            _buildingCreatedDtoPublisher.Publish(new BuildingCreatedDTO
            {
                BuildingId = building.Id,
                Position = dto.Position
            });
        }

        public void Initialize()
        {
            _disposable.Add(_placeBuildingDtoSubscriber.Subscribe(this));
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}