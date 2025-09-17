using System;
using Application.Interfaces;
using Domain.Enums;
using Domain.MessagesDTO;
using MessagePipe;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Input
{
    public class InputService : IInitializable, IDisposable, IInputService
    {
        [Inject] private readonly ICameraProvider _cameraProvider;
        [Inject] private readonly ILogger _logger;
        private GameInputActions _inputActions;

        [Inject] private readonly IPublisher<MouseClickDTO> _mouseClickDtoPublisher;
        [Inject] private readonly IPublisher<MousePositionUpdateDTO> _mousePositionUpdateDtoPublisher;

        private CompositeDisposable _disposable = new();

        public void Initialize()
        {
            _inputActions = new GameInputActions();
            _inputActions.Enable();
            _inputActions.Gameplay.Mouse0.performed += Mouse0Performed;

            Observable.Interval(TimeSpan.FromSeconds(0.05f), UnityTimeProvider.Update)
                .Subscribe(_ =>
                {
                    _mousePositionUpdateDtoPublisher.Publish(new MousePositionUpdateDTO()
                    {
                        MousePosition = _inputActions.Gameplay.MousePosition.ReadValue<Vector2>()
                    });
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _inputActions?.Disable();
            _inputActions?.Dispose();
            _disposable?.Dispose();
        }

        private void Mouse0Performed(InputAction.CallbackContext obj)
        {
            var mouseScreenPosition = _inputActions.Gameplay.MousePosition.ReadValue<Vector2>();
            _mouseClickDtoPublisher.Publish(new MouseClickDTO { ScreenPosition = mouseScreenPosition });
        }
    }

    public interface IInputService
    {
    }
}