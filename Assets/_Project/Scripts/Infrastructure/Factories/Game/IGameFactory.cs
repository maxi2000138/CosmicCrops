using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Loot.Components;
using _Project.Scripts.Game.Entities.Loot.Data;
using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Level.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factories.Game
{
  public interface IGameFactory
  {
    UniTask<ILevel> CreateLevel();
    UniTask<CharacterComponent> CreateCharacter(Vector3 position, Transform parent);
    UniTask<LootComponent> CreateLoot(LootType lootType, Vector3 position, Transform parent); 
  }
}