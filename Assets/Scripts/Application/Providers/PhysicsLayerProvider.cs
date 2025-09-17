using Application.Interfaces;
using UnityEngine;

namespace Application.Providers
{
    public class PhysicsLayerProvider : MonoBehaviour, IPhysicsLayerProvider
    {
        [SerializeField] private LayerMask groundLayerMask;

        public LayerMask GroundLayerMask => groundLayerMask;
    }
}