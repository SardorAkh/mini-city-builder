using Application.Repositories;
using UnityEngine;

namespace Utilities
{
    public class GridVisualizer : MonoBehaviour
    {
        [SerializeField] private GridConfigRepository gridConfig;
        [SerializeField] private GridVisualizationSettings visualSettings;
    
        public GridConfigRepository GridConfig 
        { 
            get => gridConfig; 
            set => gridConfig = value; 
        }
    
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (gridConfig == null) return;
            if (visualSettings.showOnlyInSceneView && !UnityEditor.SceneView.currentDrawingSceneView) return;
        
            DrawGrid();
            DrawOrigin();
            DrawBounds();
        }
    
        private void DrawGrid()
        {
            if (!visualSettings.showMainLines && !visualSettings.showSubLines) return;
        
            var origin = gridConfig.GridOrigin;
            var cellSize = gridConfig.CellSize;
            var width = gridConfig.GridWidth;
            var height = gridConfig.GridHeight;
        
            if (visualSettings.showMainLines)
            {
                Gizmos.color = GetColorWithOpacity(visualSettings.mainLineColor, visualSettings.gridOpacity);
                DrawGridLines(origin, cellSize, width, height, 5, visualSettings.mainLineWidth);
            }
        
            if (visualSettings.showSubLines)
            {
                Gizmos.color = GetColorWithOpacity(visualSettings.subLineColor, visualSettings.gridOpacity * 0.5f);
                DrawGridLines(origin, cellSize, width, height, 1, visualSettings.subLineWidth);
            }
        }
    
        private void DrawGridLines(Vector3 origin, float cellSize, int width, int height, int step, float lineWidth)
        {
            for (int y = 0; y <= height; y += step)
            {
                var start = origin + new Vector3(0, 0, y * cellSize);
                var end = origin + new Vector3(width * cellSize, 0, y * cellSize);
                DrawLine(start, end, lineWidth);
            }
        
            for (int x = 0; x <= width; x += step)
            {
                var start = origin + new Vector3(x * cellSize, 0, 0);
                var end = origin + new Vector3(x * cellSize, 0, height * cellSize);
                DrawLine(start, end, lineWidth);
            }
        }
    
        private void DrawOrigin()
        {
            Gizmos.color = visualSettings.originColor;
            Gizmos.DrawWireCube(gridConfig.GridOrigin, Vector3.one * 0.5f);
        
            var origin = gridConfig.GridOrigin;
            Gizmos.DrawRay(origin, Vector3.right * gridConfig.CellSize);
            Gizmos.DrawRay(origin, Vector3.forward * gridConfig.CellSize);
        }
    
        private void DrawBounds()
        {
            Gizmos.color = GetColorWithOpacity(visualSettings.boundsColor, 0.3f);
            Gizmos.DrawWireCube(gridConfig.GridCenter, gridConfig.GridWorldSize);
        }
    
        private void DrawLine(Vector3 start, Vector3 end, float width)
        {
            Gizmos.DrawLine(start, end);
        }
    
        private Color GetColorWithOpacity(Color color, float opacity)
        {
            return new Color(color.r, color.g, color.b, color.a * opacity);
        }
#endif
    }
}