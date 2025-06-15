using System;

namespace _Project.Scripts.Infrastructure.Systems
{
    public interface ISystem : IDisposable
    {
        void EnableSystems();
        void DisableSystems();
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}