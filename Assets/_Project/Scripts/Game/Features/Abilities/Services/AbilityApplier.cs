using _Project.Scripts.Game.Entities._Interfaces;
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
    
    public void Apply(string abilityId, ITarget target)
    {
      _gameFactory.CreateAbility(abilityId, target);

      DebugLogger.Log("Apply ability: " + abilityId, LogsType.Ability);
    }
  }
}