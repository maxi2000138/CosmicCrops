using System;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.LifeTime.EntryPoints.Core
{
    public abstract class EntryPointSystemBase : IEntryPoint, ITickable, IFixedTickable, ILateTickable
    {
        protected ISystem[] Systems = Array.Empty<ISystem>();

        public virtual void Initialize() { }
        public virtual void Entry() { }
        
        void IInitializable.Initialize()
        {
            Initialize();
            
            Systems.Foreach(Enable);
        }
        
        void IStartable.Start()
        {
            Entry();
        }

        void ITickable.Tick()
        {
            for (int i = 0; i < Systems.Length; i++)
            {
                Systems[i].Update();
            }
        }
        void IFixedTickable.FixedTick()
        {
            for (int i = 0; i < Systems.Length; i++)
            {
                Systems[i].FixedUpdate();
            }
        }
        void ILateTickable.LateTick()
        {
            for (int i = 0; i < Systems.Length; i++)
            {
                Systems[i].LateUpdate();
            }
        }
        
        void IDisposable.Dispose()
        {
            Systems.Foreach(Disable);
            Systems = Array.Empty<ISystem>();
        }

        private void Enable(ISystem system) => system.EnableSystem();
        
        private void Disable(ISystem system)
        {
            system.DisableSystem();
            system.Dispose();
        }
    }
}