using Application.Interfaces;
using UnityEngine;

namespace Application.Providers
{
    public class CameraProvider : MonoBehaviour, ICameraProvider
    {
        [SerializeField] private Camera mainCamera;
        public Camera MainCamera => mainCamera;
    }
}