namespace _Project.Scripts.Infrastructure.Pool.Item
{
  public interface ISpawnableItem
  {
    void OnCreated(IObjectPool objectPool);
    void OnSpawned();
    void OnDespawned();
    void OnRemoved();
  }
}