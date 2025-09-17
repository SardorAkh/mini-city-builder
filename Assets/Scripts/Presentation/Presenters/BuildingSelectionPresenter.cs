using System;
using Application.Repositories;
using Cysharp.Threading.Tasks;
using Domain.Models.Buildings;
using Presentation.Interfaces;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Presenters
{
    public class BuildingSelectionPresenter : IBuildingSelectionPresenter, IInitializable, IDisposable
    {
        [Inject] private readonly IBuildingSelectionView _view;
        [Inject] private readonly BuildingSelectionModel _selectionModel;

        [Inject]
        private readonly BuildingConfigsRepository _buildingConfigsRepository;

        private readonly CompositeDisposable _disposables = new();
        private bool _isInitialized = false;

        public async UniTask ActivateAsync()
        {
            await _view.ShowAsync();
        }

        public void Initialize()
        {
            _view.InitializePanel();
            InitializeBuildingTypes();
            SetupViewEvents();
            SetupModelSubscriptions();

            _isInitialized = true;
        }

        private void SetupViewEvents()
        {
            _view.OnActivateBuildingMode += OnActivateBuildingMode;
            _view.OnBuildingTypeSelected += OnBuildingTypeSelected;
            _view.OnCancelSelection += OnCancelSelection;
        }

        private void SetupModelSubscriptions()
        {
          
            _selectionModel.SelectedBuildingTypeId
                .Subscribe(OnSelectedBuildingTypeChanged)
                .AddTo(_disposables);

            _selectionModel.IsBuildingModeActive
                .Subscribe(OnBuildingModeChanged)
                .AddTo(_disposables);
        }

        private void InitializeBuildingTypes()
        {
            var buildingsInfos = _buildingConfigsRepository.GetAllBuildings();

            _view.SetAvailableBuildingTypes(buildingsInfos);
        }

        private void OnBuildingTypeSelected(int buildingTypeId)
        {

            _selectionModel.SelectedBuildingTypeId.Value = buildingTypeId;
            _selectionModel.IsBuildingModeActive.Value = true;
        }

        private void OnCancelSelection()
        {

            _selectionModel.SelectedBuildingTypeId.Value = -1;
            _selectionModel.IsBuildingModeActive.Value = false;

            _view.HideAsync().Forget();
        }

        private void OnSelectedBuildingTypeChanged(int buildingTypeId)
        {
            _view.HighlightSelectedBuilding(buildingTypeId);
        }

        private void OnBuildingModeChanged(bool isActive)
        {
        }

        public void ActivateBuildingMode()
        {

            _selectionModel.IsBuildingModeActive.Value = true;
            ActivateAsync().Forget();
        }

        public void DeactivateBuildingMode()
        {

            _selectionModel.SelectedBuildingTypeId.Value = -1;
            _selectionModel.IsBuildingModeActive.Value = false;
            _view.HideAsync().Forget();
        }

        private void OnActivateBuildingMode()
        {

            if (_selectionModel.IsBuildingModeActive.Value)
            {
                DeactivateBuildingMode();
            }
            else
            {
                ActivateBuildingMode();
            }
        }

        public void Dispose()
        {
            if (!_isInitialized)
                return;
            
            if (_view != null)
            {
                _view.OnBuildingTypeSelected -= OnBuildingTypeSelected;
                _view.OnCancelSelection -= OnCancelSelection;
            }
            
            _disposables?.Dispose();

            _isInitialized = false;
        }
    }
}