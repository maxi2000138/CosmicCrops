using _Project.Scripts.UI.Screens;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Camera
{
    public interface ICameraService
    {
        UnityEngine.Camera Camera { get; }
        void Init();
        void SetTarget(Transform target);
        void ActivateCamera(ScreenType type);
        void Shake();
        bool IsOnScreen(Vector3 viewportPoint);
        void Cleanup();
    }
}