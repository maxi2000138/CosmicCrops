using System;

namespace _Project.Scripts.Infrastructure.Systems
{
    public interface ISystem : IDisposable
    {
        void EnableSystem();
        void DisableSystem();
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}