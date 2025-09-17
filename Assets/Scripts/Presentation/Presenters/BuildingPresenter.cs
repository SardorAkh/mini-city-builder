using System;
using System.Collections.Generic;
using Application.Repositories;
using Domain.MessagesDTO;
using MessagePipe;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Presentation.Presenters
{
    public class BuildingPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly ISubscriber<BuildingCreatedDTO> _buildingCreatedDtoSubscriber;
        [Inject] private readonly BuildingConfigsRepository _buildingConfigsRepository;
        private CompositeDisposable _disposable = new();

        private Dictionary<Vector2Int, GameObject> _buildings = new();
        
        public void Initialize()
        {
            _disposable.Add(
                _buildingCreatedDtoSubscriber.Subscribe(CreateBuilding)
                );
        }

        public void CreateBuilding(BuildingCreatedDTO message)
        {
            Debug.Log("I'm here");
            
            var buildingInfo = _buildingConfigsRepository.GetBuildingInfo(message.BuildingId);
            var createdBuilding = Object.Instantiate(buildingInfo.prefab);
            createdBuilding.transform.position = new Vector3(message.Position.x, 0, message.Position.y);

            _buildings.Add(message.Position, createdBuilding);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}