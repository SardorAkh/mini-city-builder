using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Domain.Models.Buildings;
using Presentation.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Presentation.Views
{
    public class BuildingSelectionView : MonoBehaviour, IBuildingSelectionView
    {
        [Header("UI Settings")] [SerializeField]
        private VisualTreeAsset panelTemplate;

        [SerializeField] private StyleSheet styleSheet;

        [SerializeField]
        private UIDocument _uiDocument;
        private VisualElement _root;
        private Button _activateBuildButton;
        private VisualElement _panel;
        private VisualElement _buttonsContainer;
        private Button _cancelButton;
        private Button[] _buildingButtons;
        private int _selectedBuildingTypeId = -1;

        public event Action OnActivateBuildingMode;
        public event Action<int> OnBuildingTypeSelected;
        public event Action OnCancelSelection;

        public void InitializePanel()
        {
            _root = _uiDocument.rootVisualElement;

            _activateBuildButton = _root.Q<Button>("activate-build-button");
            _panel = _root.Q<VisualElement>("building-selection-panel");
            _buttonsContainer = _root.Q<VisualElement>("building-buttons-container");
            _cancelButton = _root.Q<Button>("cancel-button");
            _activateBuildButton?.RegisterCallback<ClickEvent>(OnActivateBuildButtonClicked);
            _cancelButton?.RegisterCallback<ClickEvent>(OnCancelButtonClicked);

            _panel.style.display = DisplayStyle.None;
        }


        public async UniTask ShowAsync()
        {
            _panel.style.display = DisplayStyle.Flex;

            _panel.style.opacity = 0;
            _panel.style.scale = new Scale(Vector3.zero);

            var startTime = Time.time;
            var duration = 0.3f;

            while (Time.time - startTime < duration)
            {
                var progress = (Time.time - startTime) / duration;
                var easedProgress = EaseOutBack(progress);

                _panel.style.opacity = easedProgress;
                _panel.style.scale = new Scale(Vector3.one * easedProgress);

                await UniTask.Yield();
            }

            _panel.style.opacity = 1;
            _panel.style.scale = new Scale(Vector3.one);
        }

        public async UniTask HideAsync()
        {
            var startTime = Time.time;
            var duration = 0.2f;

            while (Time.time - startTime < duration)
            {
                var progress = (Time.time - startTime) / duration;
                var easedProgress = 1f - EaseInQuart(progress);

                _panel.style.opacity = easedProgress;
                _panel.style.scale = new Scale(Vector3.one * easedProgress);

                await UniTask.Yield();
            }

            _panel.style.display = DisplayStyle.None;
        }

        public void SetAvailableBuildingTypes(IEnumerable<BuildingInfo> buildingInfos)
        {
           
            _buttonsContainer.Clear();
            var infos = buildingInfos.ToList();
            _buildingButtons = new Button[infos.Count];
            for (int i = 0; i < infos.Count; i++)
            {
                var buildingTypeId = infos[i].id;
                var buildingName = infos[i].buildingName;

                var button = new Button(() => OnBuildingButtonClicked(buildingTypeId));
                button.text = buildingName;
                button.name = $"building-button-{buildingTypeId}";
                button.AddToClassList("building-button");

                _buildingButtons[i] = button;
                _buttonsContainer.Add(button);
            }
        }

        public void HighlightSelectedBuilding(int buildingTypeId)
        {
            _selectedBuildingTypeId = buildingTypeId;

            foreach (var button in _buildingButtons)
            {
                if (button != null)
                    button.RemoveFromClassList("building-button--selected");
            }

            if (buildingTypeId != -1)
            {
                var selectedButton = _buttonsContainer.Q<Button>($"building-button-{buildingTypeId}");
                selectedButton?.AddToClassList("building-button--selected");
            }
        }

        private void OnBuildingButtonClicked(int buildingTypeId)
        {
            OnBuildingTypeSelected?.Invoke(buildingTypeId);
        }

        public void UpdateActivateButtonState(bool isActive)
        {
            if (_activateBuildButton == null) return;

            if (isActive)
            {
                _activateBuildButton.text = "🔨 Building...";
                _activateBuildButton.AddToClassList("activate-build-button--active");
            }
            else
            {
                _activateBuildButton.text = "🔨 Build";
                _activateBuildButton.RemoveFromClassList("activate-build-button--active");
            }
        }

        private void OnActivateBuildButtonClicked(ClickEvent evt)
        {
            OnActivateBuildingMode?.Invoke();
        }

        private void OnCancelButtonClicked(ClickEvent evt)
        {
            OnCancelSelection?.Invoke();
        }

        private static float EaseOutBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
        }

        private static float EaseInQuart(float t)
        {
            return t * t * t * t;
        }

        private void OnDestroy()
        {
            _activateBuildButton?.UnregisterCallback<ClickEvent>(OnActivateBuildButtonClicked);
            _cancelButton?.UnregisterCallback<ClickEvent>(OnCancelButtonClicked);
        }
    }
}