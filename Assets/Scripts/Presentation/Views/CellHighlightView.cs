using UnityEngine;

namespace Presentation.Views
{
    public class CellHighlightView : MonoBehaviour
    {
        [SerializeField] private Material successMaterial;
        [SerializeField] private Material errorMaterial;
    
        [SerializeField] private Renderer rndr;

        private void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ShowSuccess()
        {
            Show();
            rndr.material = successMaterial;
        }

        public void ShowError()
        {
            Show();
            rndr.material = errorMaterial;
        }
    
    }
}