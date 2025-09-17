using UnityEngine;

namespace Application.Repositories
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "CityBuilder/Grid Config")]
    public class GridConfigRepository : ScriptableObject
    {
        [Header("Grid Dimensions")]
        [SerializeField] private int gridWidth = 32;
        [SerializeField] private int gridHeight = 32;
        [SerializeField] private float cellSize = 1f;
    
        [Header("World Position")]
        [SerializeField] private Vector3 gridOrigin = Vector3.zero;
        [SerializeField] private bool centerGrid = true;
    
        [Header("Editor Visualization")]
        [SerializeField] private bool showGizmos = true;
        [SerializeField] private Color gridColor = Color.white;
        [SerializeField] private Color originColor = Color.red;
    
        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;
        public float CellSize => cellSize;
        public Vector3 GridOrigin => centerGrid ? CalculateCenteredOrigin() : gridOrigin;
    
        public Vector3 GridWorldSize => new Vector3(GridWidth * CellSize, 0, GridHeight * CellSize);
        public Vector3 GridCenter => GridOrigin + GridWorldSize * 0.5f;
        public Bounds GridBounds => new Bounds(GridCenter, GridWorldSize);
    
        private Vector3 CalculateCenteredOrigin()
        {
            var worldSize = GridWorldSize;
            return gridOrigin - new Vector3(worldSize.x * 0.5f, 0, worldSize.z * 0.5f);
        }
    
        public bool IsValidGridPosition(Vector2Int gridPos)
        {
            return gridPos.x >= 0 && gridPos.x < GridWidth &&
                   gridPos.y >= 0 && gridPos.y < GridHeight;
        }
    
        public Vector3 GridToWorldPosition(Vector2Int gridPos)
        {
            return GridOrigin + new Vector3(gridPos.x * CellSize, 0, gridPos.y * CellSize);
        }
    
        public Vector2Int WorldToGridPosition(Vector3 worldPos)
        {
            var localPos = worldPos - GridOrigin;
            return new Vector2Int(
                Mathf.FloorToInt(localPos.x / CellSize),
                Mathf.FloorToInt(localPos.z / CellSize)
            );
        }
    
        public Vector3 SnapToGrid(Vector3 worldPos)
        {
            var gridPos = WorldToGridPosition(worldPos);
            return GridToWorldPosition(gridPos);
        }
    }
}