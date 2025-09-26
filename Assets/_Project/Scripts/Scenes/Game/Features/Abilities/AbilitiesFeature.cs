using _Project.Scripts.Game.Features.Abilities.Systems;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Features.Abilities
{
  public class AbilitiesFeature : Feature
  {
    public AbilitiesFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new ProcessAbilitySystem());
    }
  }
}