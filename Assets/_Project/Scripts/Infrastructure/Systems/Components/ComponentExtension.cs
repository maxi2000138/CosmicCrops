namespace _Project.Scripts.Infrastructure.Systems.Components
{
    public static class ComponentExtension
    {
        public static void SetActive(this Component component, bool value) => component.gameObject.SetActive(value);

        public static void CleanSubscribe(this Component component) => component.LifetimeDisposable.Clear();
    }
}