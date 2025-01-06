using System;

namespace _Project.Scripts._Infrastructure.ComponentSystemsCore.Systems
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