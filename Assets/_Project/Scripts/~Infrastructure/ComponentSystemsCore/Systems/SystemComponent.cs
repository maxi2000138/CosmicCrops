using System.Collections.Generic;
using _Project.Scripts._Infrastructure.ComponentSystemsCore.Components;

namespace _Project.Scripts._Infrastructure.ComponentSystemsCore.Systems
{
    public abstract class SystemComponent<T> : SystemBase where T : Component
    {
        private readonly List<T> _entities;
        
        protected IReadOnlyList<T> Entities => _entities;

        protected SystemComponent()
        {
            _entities = new List<T>();
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            ComponentsContainer<T>.OnRegistered += OnEnableComponent;
            ComponentsContainer<T>.OnUnregistered += OnDisableComponent;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            ComponentsContainer<T>.OnRegistered -= OnEnableComponent;
            ComponentsContainer<T>.OnUnregistered -= OnDisableComponent;
        }

        protected virtual void OnEnableComponent(T component)
        {
            if (_entities.Contains(component))
            {
                return;
            }
            
            _entities.Add(component);
        }

        protected virtual void OnDisableComponent(T component)
        {
            _entities.Remove(component);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _entities.Clear();
        }
    }
}