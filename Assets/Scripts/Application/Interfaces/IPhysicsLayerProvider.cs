using UnityEngine;

namespace Application.Interfaces
{
    public interface IPhysicsLayerProvider
    {
        LayerMask GroundLayerMask { get; }
    }
}