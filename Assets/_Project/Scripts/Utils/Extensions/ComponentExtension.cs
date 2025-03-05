namespace _Project.Scripts.Infrastructure.Systems.Components
{
    public static class ComponentExtension
    {
        public static void CleanSubscribe(this IComponent component) => component.LifetimeDisposable.Clear();
    }
}