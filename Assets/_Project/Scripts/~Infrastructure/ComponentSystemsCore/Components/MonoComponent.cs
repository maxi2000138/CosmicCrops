namespace _Project.Scripts._Infrastructure.ComponentSystemsCore.Components
{
    public abstract class MonoComponent<T> : Component where T : Component
    {
        private void Awake()
        {
            OnComponentCreate();
        }

        private void OnEnable()
        {
            OnComponentEnable();
            
            ComponentsContainer<T>.Registered(this);
        }

        private void OnDisable()
        {
            OnComponentDisable();

            ComponentsContainer<T>.Unregistered(this);
        }

        private void OnDestroy()
        {
            OnComponentDestroy();
        }
    }
}