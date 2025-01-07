using _Project.Scripts.Game.Interfaces;
using _Project.Scripts.Game.Level.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts._Infrastructure.Factories.Game
{
  public interface IGameFactory
  {
    UniTask<ILevel> CreateLevel();
    UniTask<CharacterComponent> CreateCharacter(Vector3 position, Transform parent);
  }
}