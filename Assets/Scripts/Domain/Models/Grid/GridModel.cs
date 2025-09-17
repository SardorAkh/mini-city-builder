using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Domain.Models.Grid
{
    public class GridModel
    {
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;
        private readonly Vector3 _origin;
        private readonly HashSet<Vector2Int> _occupiedCells = new();
    
        public GridModel(int width, int height, float cellSize, Vector3 origin)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _origin = origin;
        }
    
        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;
        public Vector3 Origin => _origin;
    
        public bool IsValidPosition(Vector2Int position)
        {
            return position.x >= 0 && position.x < _width &&
                   position.y >= 0 && position.y < _height;
        }
    
        public bool IsCellOccupied(Vector2Int position) => 
            _occupiedCells.Contains(position);
    
        public void OccupyCell(Vector2Int position)
        {
            if (IsValidPosition(position))
            {
                _occupiedCells.Add(position);
            }
        }
    
        public void FreeCell(Vector2Int position)
        {
            _occupiedCells.Remove(position);
        }
    
        public IEnumerable<Vector2Int> GetOccupiedCells() => _occupiedCells;
    
        public Vector3 GridToWorldPosition(Vector2Int gridPos)
        {
            return _origin + new Vector3(gridPos.x * _cellSize, 0, gridPos.y * _cellSize);
        }
    
        public Vector2Int WorldToGridPosition(Vector3 worldPos)
        {
            var localPos = worldPos - _origin;
            return new Vector2Int(
                Mathf.FloorToInt(localPos.x / _cellSize),
                Mathf.FloorToInt(localPos.z / _cellSize)
            );
        }
    }
}