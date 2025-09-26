using _Project.Scripts.Game.Features.Level.Systems;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Features.Level
{
  public class LevelFeature : Feature
  {
    public LevelFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new BuildGroundNavMeshSystem());
    }
  }
}