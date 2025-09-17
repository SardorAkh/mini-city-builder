using UnityEngine;

namespace Utilities
{
    [System.Serializable]
    public class GridVisualizationSettings
    {
        [Header("Line Settings")]
        public bool showMainLines = true;
        public bool showSubLines = true;
        public float mainLineWidth = 0.02f;
        public float subLineWidth = 0.01f;
    
        [Header("Colors")]
        public Color mainLineColor = Color.white;
        public Color subLineColor = Color.gray;
        public Color originColor = Color.red;
        public Color boundsColor = Color.blue;
    
        [Header("Visibility")]
        [Range(0f, 1f)] public float gridOpacity = 0.5f;
        public bool showOnlyInSceneView = true;
    }
}