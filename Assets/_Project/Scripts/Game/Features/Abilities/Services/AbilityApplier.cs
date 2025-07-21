using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Logger;

namespace _Project.Scripts.Game.Features.Abilities.Services
{
  public class AbilityApplier : IAbilityApplier
  {
    private readonly IGameFactory _gameFactory;
    
    public AbilityApplier(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }
    
    public void Apply(string abilityId, IUnit unit)
    {
      _gameFactory.CreateAbility(abilityId, unit);

      DebugLogger.Log("Apply ability: " + abilityId, LogsType.Ability);
    }
  }
}