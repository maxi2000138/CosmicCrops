using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Loot.Components;
using _Project.Scripts.Game.Entities.Loot.Data;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Level.Components;
using _Project.Scripts.Game.Level.Interface;
using _Project.Scripts.Game.Level.Model;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factories.Game
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
      LevelConfig levelConfig = _staticDataService.LevelConfig();
      int index = levelNumber > levelConfig.Levels.Length ? (levelNumber - 1) % levelConfig.Levels.Length : levelNumber - 1;
      var data = levelConfig.Levels[index];
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
      LevelComponent level = Object.Instantiate(prefab).GetComponent<LevelComponent>();
      _levelModel.SetLevel(level);
      return level;
    }
    
    async UniTask<CharacterComponent> IGameFactory.CreateCharacter(Vector3 position, Transform parent)
    {
      CharacterConfig characterConfig = _staticDataService.CharacterConfig();
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(characterConfig.PrefabReference);
      CharacterComponent character = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<CharacterComponent>();
      _levelModel.SetCharacter(character);
      
      character.CharacterController.SetBaseSpeed(characterConfig.Speed);
      
      return character;
    }
    
    async UniTask<UnitComponent> IGameFactory.CreateUnit(Vector3 position, Transform parent)
    {
      UnitConfig config = _staticDataService.UnitConfig();
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(config.PrefabReference);
      UnitComponent unit = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<UnitComponent>();
      _levelModel.AddEnemy(unit);
      return unit;
    }

    
    public async UniTask<LootComponent> CreateLoot(LootType lootType, Vector3 position, Transform parent)
    {
      LootConfig lootConfig = _staticDataService.LootConfig();
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(lootConfig.Loot[lootType].PrefabReference);
      LootComponent loot = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<LootComponent>();
      _levelModel.AddLoot(loot);
      return loot;
    }
  }
}