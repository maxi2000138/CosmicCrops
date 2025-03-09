using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Abilities.Components;
using _Project.Scripts.Game.Features.Level.Interface;
using _Project.Scripts.Game.Features.Loot.Components;
using _Project.Scripts.Game.Features.Loot.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factories.Game
{
  public interface IGameFactory
  {
    UniTask<ILevel> CreateLevel();
    UniTask<CharacterComponent> CreateCharacter(Vector3 position, Transform parent);
    UniTask<LootComponent> CreateLoot(LootType lootType, Vector3 position, Transform parent);
    UniTask<UnitComponent> CreateUnit(string unitName, Vector3 position, Transform parent);
    AbilityComponent CreateAbility(string abilityName, ITarget target);
  }
}