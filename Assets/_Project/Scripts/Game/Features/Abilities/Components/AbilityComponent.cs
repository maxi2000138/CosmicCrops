using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Features.Abilities.Components
{
  public class AbilityComponent : Component<AbilityComponent>
  {
    public string AbilityName;
    public ITarget Target;
    
    public void Setup(string abilityName, ITarget target)
    {
      AbilityName = abilityName;
      Target = target;
    }
  }
}