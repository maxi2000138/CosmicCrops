using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Features.Abilities.Components
{
  public class AbilityComponent : Component<AbilityComponent>
  {
    public string AbilityName;
    public IUnit Unit;
    
    public void Setup(string abilityName, IUnit unit)
    {
      AbilityName = abilityName;
      Unit = unit;
    }
  }
}