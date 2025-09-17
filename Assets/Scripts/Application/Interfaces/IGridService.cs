using UnityEngine;

namespace Application.Interfaces
{
    public interface IGridService
    {
        public Vector3 GridToWorldPosition(Vector2Int gridPos);
        public Vector2Int WorldToGridPosition(Vector3 worldPos);
        public bool CanPlaceBuilding(Vector2Int position);
        public bool IsValidPosition(Vector2Int position);
        public void OccupyCell(Vector2Int position);
    }
}