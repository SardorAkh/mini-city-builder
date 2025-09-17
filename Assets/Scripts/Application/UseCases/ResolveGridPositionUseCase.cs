using System;
using Application.Interfaces;
using Domain.MessagesDTO;
using MessagePipe;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases
{
    public class ResolveGridPositionUseCase : IMessageHandler<MouseClickDTO>, IInitializable, IDisposable
    {
        [Inject] private readonly IGridService _gridService;
        [Inject] private readonly ICameraProvider _cameraProvider;
        [Inject] private readonly IPhysicsLayerProvider _physicsLayerProvider;
        [Inject] private readonly ILogger _logger;

        [Inject] private readonly IPublisher<PlaceBuildingDTO> _placeBuildingDtoPublisher;

        [Inject] private readonly ISubscriber<MouseClickDTO> _mouseClickDtoSubscriber;

        private CompositeDisposable _disposable = new();
        
        public void Handle(MouseClickDTO message)
        {
            
            var ray = _cameraProvider.MainCamera.ScreenPointToRay(message.ScreenPosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _physicsLayerProvider.GroundLayerMask))
            {
                var worldPosition = hit.point;
                var gridPosition = _gridService.WorldToGridPosition(worldPosition);

                _placeBuildingDtoPublisher.Publish(new PlaceBuildingDTO
                {
                    Position = gridPosition
                });
            }
        }

        public void Initialize()
        {
            _disposable.Add(_mouseClickDtoSubscriber.Subscribe(this));
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}