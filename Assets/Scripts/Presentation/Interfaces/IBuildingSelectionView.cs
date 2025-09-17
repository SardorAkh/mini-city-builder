using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Domain.Models.Buildings;

namespace Presentation.Interfaces
{
    public interface IBuildingSelectionView
    {
        void InitializePanel();
        event Action OnActivateBuildingMode;
        event Action<int> OnBuildingTypeSelected;
        
        event Action OnCancelSelection;
        
        UniTask ShowAsync();
        
        UniTask HideAsync();
        
        void SetAvailableBuildingTypes(IEnumerable<BuildingInfo> buildingInfos);
        
        void HighlightSelectedBuilding(int buildingTypeId);
        void UpdateActivateButtonState(bool isActive);
    }
}