using UnityEngine;

namespace Application.Interfaces
{
    public interface ICameraProvider
    {
        Camera MainCamera { get; }
    }
}