using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Level.Interface;
using _Project.Scripts.Game.Loot.Components;
using _Project.Scripts.Game.Loot.Data;
using _Project.Scripts.Game.Units.Character.Components;
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