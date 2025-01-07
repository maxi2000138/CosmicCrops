using _Project.Scripts._Infrastructure.AssetData;
using _Project.Scripts._Infrastructure.StaticData;
using _Project.Scripts._Infrastructure.StaticData.Data;
using _Project.Scripts.Game.Interfaces;
using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Models;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts._Infrastructure.Factories.Game
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class GameFactory : IGameFactory
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IAssetService _assetService;
    private readonly LevelModel _levelModel;

    public GameFactory(IStaticDataService staticDataService, 
      IAssetService assetService, LevelModel levelModel)
    {
      _staticDataService = staticDataService;
      _assetService = assetService;
      _levelModel = levelModel;
    }
    
    async UniTask<ILevel> IGameFactory.CreateLevel()
    {
      int levelNumber = 1;
      LevelData levelData = _staticDataService.LevelData();
      int index = levelNumber > levelData.Levels.Length ? (levelNumber - 1) % levelData.Levels.Length : levelNumber - 1;
      var data = levelData.Levels[index];
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
      LevelComponent level = Object.Instantiate(prefab).GetComponent<LevelComponent>();
      _levelModel.SetLevel(level);
      return level;
    }
    
    async UniTask<CharacterComponent> IGameFactory.CreateCharacter(Vector3 position, Transform parent)
    {
      CharacterData characterData = _staticDataService.CharacterData();
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(characterData.PrefabReference);
      CharacterComponent character = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<CharacterComponent>();
      _levelModel.SetCharacter(character);
      return character;
    }
  }
}