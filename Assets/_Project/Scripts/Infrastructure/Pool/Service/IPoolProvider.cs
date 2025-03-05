using _Project.Scripts.Game.Features.Abilities.Components;

namespace _Project.Scripts.Infrastructure.Pool.Service
{
  public interface IPoolProvider
  {
    ObjectPoolComponent<AbilityComponent> PoolAbility { get; }
    void Init();
  }
}