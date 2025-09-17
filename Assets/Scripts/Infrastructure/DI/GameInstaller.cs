using Application.Factories;
using Application.Interfaces;
using Application.Providers;
using Application.Repositories;
using Application.Services;
using Application.UseCases;
using Domain.Models.Buildings;
using Domain.Models.Economy;
using Infrastructure.Input;
using MessagePipe;
using Presentation.Interfaces;
using Presentation.Presenters;
using Presentation.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private CameraProvider cameraProvider;
        [SerializeField] private PhysicsLayerProvider physicsLayerProvider;
        [SerializeField] private BuildingConfigsRepository buildingConfigsRepository;
        [SerializeField] private GridConfigRepository gridConfigRepository;
        [SerializeField] private CellHighlightView cellHighlightView;
        [SerializeField] private BuildingSelectionView buildingSelectionView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe(options =>
            {
                options.InstanceLifetime = InstanceLifetime.Singleton;
                options.EnableCaptureStackTrace = true;
            });

            builder.RegisterComponent<ICameraProvider>(cameraProvider);
            builder.RegisterComponent<IPhysicsLayerProvider>(physicsLayerProvider);
            builder.RegisterComponent<IBuildingSelectionView>(buildingSelectionView);

            builder.RegisterInstance<ILogger>(Debug.unityLogger);

            builder.RegisterInstance(buildingConfigsRepository);
            builder.RegisterInstance(gridConfigRepository);

            builder.Register<CurrencyModel>(Lifetime.Scoped);
            builder.Register<CityModel>(Lifetime.Scoped);
            builder.Register<BuildingModel>(Lifetime.Scoped);
            builder.Register<BuildingModelFactory>(Lifetime.Scoped);
            builder.Register<BuildingSelectionModel>(Lifetime.Scoped);
            builder.Register<BuildingService>(Lifetime.Scoped);

            builder.Register<CellHighlightPresenter>(Lifetime.Scoped)
                .WithParameter(cellHighlightView).AsImplementedInterfaces();

            builder.Register<GridService>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<IInputService, InputService>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ResolveGridPositionUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlaceBuildingUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<CellHighlightUseCase>(Lifetime.Scoped).AsImplementedInterfaces();

            builder.Register<IBuildingSelectionPresenter, BuildingSelectionPresenter>(Lifetime.Scoped)
                .AsImplementedInterfaces();

            builder.Register<BuildingPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}