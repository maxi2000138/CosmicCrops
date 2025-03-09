using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Utils.Extensions
{
    public static class ComponentExtension
    {
        public static void CleanSubscribe(this IComponent component) => component.LifetimeDisposable.Clear();
    }
}