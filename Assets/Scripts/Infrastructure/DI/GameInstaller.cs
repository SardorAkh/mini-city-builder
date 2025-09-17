using Application.Interfaces;
using Application.Providers;
using Infrastructure.Input;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private CameraProvider cameraProvider;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe(options =>
            {
                options.InstanceLifetime = InstanceLifetime.Singleton;
                options.EnableCaptureStackTrace = true;
            });

            builder.RegisterInstance<ILogger>(Debug.unityLogger);
            
            builder.RegisterComponent<ICameraProvider>(cameraProvider);
            
            builder.Register<IInputService, InputService>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}