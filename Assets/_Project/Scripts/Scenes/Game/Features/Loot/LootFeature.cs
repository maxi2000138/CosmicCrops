using _Project.Scripts.Game.Features.Loot.Systems;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Features.Loot
{
  public class LootFeature : Feature
  {
    public LootFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new LootSpawnerSystem());
    }
  }
}