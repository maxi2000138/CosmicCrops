using System;

namespace _Project.Scripts._Infrastructure.ComponentSystemsCore.Components
{
    public static class ComponentsContainer<T> where T : Component
    {
        public static event Action<T> OnRegistered;
        public static event Action<T> OnUnregistered;

        public static void Registered(Component component)
        {
            if (component is T typedComponent)
            {
                OnRegistered?.Invoke(typedComponent);
            }
        }

        public static void Unregistered(Component component)
        {
            if (component is T typedComponent)
            {
                OnUnregistered?.Invoke(typedComponent);
            }
        }
    }
}