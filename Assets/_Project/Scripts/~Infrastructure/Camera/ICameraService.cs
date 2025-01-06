using _Project.Scripts._Infrastructure.UI.Screens;
using UnityEngine;

namespace _Project.Scripts._Infrastructure.Camera
{
    public interface ICameraService
    {
        UnityEngine.Camera Camera { get; }
        void Init();
        void SetTarget(Transform target);
        void ActivateCamera(ScreenType type);
        void Shake();
        bool IsOnScreen(Vector3 viewportPoint);
        void CleanUp();
    }
}