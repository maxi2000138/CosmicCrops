using System;

namespace _Project.Scripts.Infrastructure.Systems.Components
{
    public static class ComponentsContainer<T> where T : IComponent
    {
        public static event Action<T> OnRegistered;
        public static event Action<T> OnUnregistered;

        public static void Registered(IComponent component)
        {
            if (component is T typedComponent)
            {
                OnRegistered?.Invoke(typedComponent);
            }
        }

        public static void Unregistered(IComponent component)
        {
            if (component is T typedComponent)
            {
                OnUnregistered?.Invoke(typedComponent);
            }
        }
    }
}