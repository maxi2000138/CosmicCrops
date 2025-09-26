using _Project.Scripts.Game.Features.Units._Interfaces;

namespace _Project.Scripts.Game.Features.Abilities.Services
{
  public interface IAbilityApplier
  {
    void Apply(string abilityId, IUnit unit);
  }
}