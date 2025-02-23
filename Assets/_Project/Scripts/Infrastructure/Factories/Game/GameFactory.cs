using _Project.Scripts.Game.Entities.Character._Configs;
using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Unit._Configs;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Level._Configs;
using _Project.Scripts.Game.Features.Level.Components;
using _Project.Scripts.Game.Features.Level.Interface;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Loot._Configs;
using _Project.Scripts.Game.Features.Loot.Components;
using _Project.Scripts.Game.Features.Loot.Data;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.StaticData;
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
    private readonly LevelConfig _levelConfig;
    private readonly CharacterConfig _characterConfig;
    private readonly LootConfig _lootConfig;
    private readonly UnitsConfig _unitsConfig;

    public GameFactory(IStaticDataService staticDataService, IAssetService assetService, 
      LevelModel levelModel, LevelConfig levelConfig, CharacterConfig characterConfig, LootConfig lootConfig,
      UnitsConfig unitsConfig)
    {
      _staticDataService = staticDataService;
      _assetService = assetService;
      _levelModel = levelModel;
      _levelConfig = levelConfig;
      _characterConfig = characterConfig;
      _lootConfig = lootConfig;
      _unitsConfig = unitsConfig;
    }
    
    async UniTask<ILevel> IGameFactory.CreateLevel()
    {
      int levelNumber = 1;
      int index = levelNumber > _levelConfig.Data.Count ? levelNumber % _levelConfig.Data.Count : levelNumber ;
      var data = _levelConfig.Data[index];
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.Prefab.Name);
      LevelComponent level = Object.Instantiate(prefab).GetComponent<LevelComponent>();
      _levelModel.SetLevel(level);
      return level;
    }
    
    async UniTask<CharacterComponent> IGameFactory.CreateCharacter(Vector3 position, Transform parent)
    {
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(_characterConfig.Prefab.Name);
      CharacterComponent character = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<CharacterComponent>();
      _levelModel.SetCharacter(character);
      
      character.CharacterController.SetBaseSpeed(_characterConfig.Speed);
      
      return character;
    }
    
    async UniTask<UnitComponent> IGameFactory.CreateUnit(Vector3 position, Transform parent)
    {
      // TODO: set correct unit name
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(_unitsConfig.Data["Base"].PrefabName);
      UnitComponent unit = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<UnitComponent>();
      _levelModel.AddEnemy(unit);
      return unit;
    }

    
    public async UniTask<LootComponent> CreateLoot(LootType lootType, Vector3 position, Transform parent)
    {
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(_lootConfig.Data[lootType].PrefabName);
      LootComponent loot = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<LootComponent>();
      _levelModel.AddLoot(loot);
      return loot;
    }
  }
}