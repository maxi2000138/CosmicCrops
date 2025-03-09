using _Project.Scripts.Game.Features.Abilities.Components;

namespace _Project.Scripts.Infrastructure.Pool.Service
{
  public class PoolProvider : IPoolProvider
  {
    public ObjectPoolComponent<AbilityComponent> PoolAbility { get; private set; }

    public void Init()
    {
      PoolAbility = new ObjectPoolComponent<AbilityComponent>(() => new AbilityComponent(), 10);
    }
  }
}