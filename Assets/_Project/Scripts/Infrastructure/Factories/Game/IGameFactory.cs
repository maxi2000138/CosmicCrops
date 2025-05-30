using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Enemy.Components;
using _Project.Scripts.Game.Features.Abilities.Components;
using _Project.Scripts.Game.Features.Level.Interface;
using _Project.Scripts.Game.Features.Loot._Configs.Data;
using _Project.Scripts.Game.Features.Loot.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factories.Game
{
  public interface IGameFactory
  {
    UniTask<ILevel> CreateLevel();
    UniTask<CharacterComponent> CreateCharacter(Vector3 position, Transform parent);
    UniTask<LootComponent> CreateLoot(LootType lootType, Vector3 position, Transform parent);
    UniTask<EnemyComponent> CreateUnit(string unitName, Vector3 position, Transform parent);
    AbilityComponent CreateAbility(string abilityName, IUnit unit);
  }
}