using _Project.Scripts.Game.Features.Units._Systems;
using _Project.Scripts.Game.Features.Units._Systems.UI;
using _Project.Scripts.Game.Features.Units.Character.Systems;
using _Project.Scripts.Game.Features.Units.Enemy.Systems;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Features.Units
{
  public class UnitsFeature : Feature
  {
    public UnitsFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new UnitStateMachineUpdateSystem());
      Add(new CharacterSpawnerSystem());
      Add(new EnemySpawnerSystem());
      
      Add(new UnitAnimatorSystem());
      Add(new CharacterHealthViewUpdateSystem());
      Add(new EnemyHealthProviderSystem());
      Add(new EnemyHealthViewUpdateSystem());
    }
  }
}