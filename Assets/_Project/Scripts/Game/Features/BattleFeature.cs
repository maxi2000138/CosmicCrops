using _Project.Scripts.Game.Features.Abilities;
using _Project.Scripts.Game.Features.AI;
using _Project.Scripts.Game.Features.Collector;
using _Project.Scripts.Game.Features.Input;
using _Project.Scripts.Game.Features.Level;
using _Project.Scripts.Game.Features.Loot;
using _Project.Scripts.Game.Features.Units;
using _Project.Scripts.Game.Features.Weapon;
using _Project.Scripts.Game.UI;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Features
{
  public class BattleFeature : Feature
  {
    public BattleFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new UnitsFeature(objectResolver));
      Add(new AbilitiesFeature(objectResolver));
      Add(new AIFeature(objectResolver));
      Add(new CollectorFeature(objectResolver));
      Add(new InputFeature(objectResolver));
      Add(new LevelFeature(objectResolver));
      Add(new LootFeature(objectResolver));
      Add(new WeaponFeature(objectResolver));
      
      Add(new UIFeature(objectResolver));
    }
  }
}