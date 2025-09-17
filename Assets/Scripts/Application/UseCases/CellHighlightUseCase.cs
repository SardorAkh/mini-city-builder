using System;
using Application.Interfaces;
using Domain.Enums;
using Domain.MessagesDTO;
using MessagePipe;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases
{
    public class CellHighlightUseCase : IMessageHandler<MousePositionUpdateDTO>, IInitializable, IDisposable
    {
        [Inject] private readonly IGridService _gridService;
        [Inject] private readonly ICameraProvider _cameraProvider;
        [Inject] private readonly IPhysicsLayerProvider _physicsLayerProvider;

        [Inject] private readonly ISubscriber<MousePositionUpdateDTO> _mousePositionUpdateDtoSubscriber;

        [Inject] private readonly IPublisher<CellToHighlightDTO> _cellToHighlightDtoPublisher;

        private CompositeDisposable _disposable = new();

        public void Handle(MousePositionUpdateDTO message)
        {
            var ray = _cameraProvider.MainCamera.ScreenPointToRay(message.MousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _physicsLayerProvider.GroundLayerMask))
            {
                var gridPosition = _gridService.WorldToGridPosition(hit.point);
                var isCellValid = _gridService.IsValidPosition(gridPosition);
                var worldPosition = new Vector3(gridPosition.x, 0, gridPosition.y);
                if (!isCellValid)
                {
                    _cellToHighlightDtoPublisher.Publish(new CellToHighlightDTO()
                    {
                        IsValid = false,
                        Position = worldPosition
                    });
                }

                var canPlace = _gridService.CanPlaceBuilding(gridPosition);

                if (canPlace)
                {
                    _cellToHighlightDtoPublisher.Publish(new CellToHighlightDTO()
                    {
                        IsValid = true,
                        Position = worldPosition,
                        HighlightType = HighlightType.Valid
                    });
                }
                else
                {
                    _cellToHighlightDtoPublisher.Publish(new CellToHighlightDTO()
                    {
                        IsValid = true,
                        Position = worldPosition,
                        HighlightType = HighlightType.Invalid
                    });
                }
            }
        }

        public void Initialize()
        {
            _disposable.Add(_mousePositionUpdateDtoSubscriber.Subscribe(this));
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}