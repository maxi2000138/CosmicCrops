using _Project.Scripts.Game.Features.AI.Systems;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Features.AI
{
  public class AIFeature : Feature
  {
    public AIFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new EnemyAISystem());
    }
  }
}