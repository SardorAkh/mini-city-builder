using System;
using Application.Interfaces;
using Domain.Enums;
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

        public void Initialize()
        {
            _inputActions = new GameInputActions();
            _inputActions.Enable();
            _inputActions.Gameplay.Mouse0.performed += Mouse0Performed;
        }

        public void Dispose()
        {
            _inputActions?.Disable();
            _inputActions?.Dispose();
        }

        private void Mouse0Performed(InputAction.CallbackContext obj)
        {
            var mouseScreenPosition = _inputActions.Gameplay.MousePosition.ReadValue<Vector2>();
           
        }
    }

    public interface IInputService
    {
    }
}