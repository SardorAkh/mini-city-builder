using System;
using Application.Interfaces;
using Application.Repositories;
using Domain.Models.Grid;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.Services
{
    public class GridService : IInitializable, IGridService
    {
        [Inject] private readonly GridConfigRepository _gridConfig;

        private GridModel _gridModel;

        public GridModel GridModel => _gridModel;

        public void Initialize()
        {
            _gridModel = new GridModel(
                _gridConfig.GridWidth,
                _gridConfig.GridHeight,
                _gridConfig.CellSize,
                _gridConfig.GridOrigin
            );
        }


        public Vector3 GridToWorldPosition(Vector2Int gridPos) =>
            _gridModel.GridToWorldPosition(gridPos);

        public Vector2Int WorldToGridPosition(Vector3 worldPos) =>
            _gridModel.WorldToGridPosition(worldPos);

        public bool CanPlaceBuilding(Vector2Int position) =>
            _gridModel.IsValidPosition(position) && !_gridModel.IsCellOccupied(position);

        public bool IsValidPosition(Vector2Int position) => _gridModel.IsValidPosition(position);
        
        public void OccupyCell(Vector2Int position)
        {
            _gridModel.OccupyCell(position);
        }
    }
}