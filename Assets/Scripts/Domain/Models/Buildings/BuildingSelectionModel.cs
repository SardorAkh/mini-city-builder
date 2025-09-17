using R3;

namespace Domain.Models.Buildings
{
    public class BuildingSelectionModel
    {
        public ReactiveProperty<int> SelectedBuildingTypeId { get; } = new(1);
        public ReactiveProperty<bool> IsBuildingModeActive { get; } = new(false);

        public bool HasSelection => SelectedBuildingTypeId.Value != -1;
    }
}