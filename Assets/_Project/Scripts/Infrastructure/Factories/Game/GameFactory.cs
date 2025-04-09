using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Character._Configs;
using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Entities.Unit._Configs;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.Abilities.Components;
using _Project.Scripts.Game.Features.Level._Configs;
using _Project.Scripts.Game.Features.Level.Components;
using _Project.Scripts.Game.Features.Level.Interface;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Loot._Configs;
using _Project.Scripts.Game.Features.Loot.Components;
using _Project.Scripts.Game.Features.Loot.Data;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Systems.Components;
using CodeBase.Infrastructure.Pool;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factories.Game
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class GameFactory : IGameFactory
  {
    private readonly IAssetService _assetService;
    private readonly LevelModel _levelModel;
    private readonly LevelConfig _levelConfig;
    private readonly CharacterConfig _characterConfig;
    private readonly IObjectPoolService _objectPoolService;
    private readonly LootConfig _lootConfig;
    private readonly UnitsConfig _unitsConfig;

    public GameFactory(IAssetService assetService, 
      LevelModel levelModel, LevelConfig levelConfig, CharacterConfig characterConfig, IObjectPoolService objectPoolService,
      LootConfig lootConfig, UnitsConfig unitsConfig)
    {
      _assetService = assetService;
      _levelModel = levelModel;
      _levelConfig = levelConfig;
      _characterConfig = characterConfig;
      _objectPoolService = objectPoolService;
      _lootConfig = lootConfig;
      _unitsConfig = unitsConfig;
    }
    
    async UniTask<ILevel> IGameFactory.CreateLevel()
    {
      int levelNumber = 1;
      int index = levelNumber > _levelConfig.Data.Count ? levelNumber % _levelConfig.Data.Count : levelNumber ;
      var data = _levelConfig.Data[index];
      var prefab = await _assetService.LoadFromAddressable<GameObject>(data.Prefab.Name);
      LevelComponent level = Object.Instantiate(prefab).GetComponent<LevelComponent>();
      _levelModel.SetLevel(level);
      return level;
    }
    
    async UniTask<CharacterComponent> IGameFactory.CreateCharacter(Vector3 position, Transform parent)
    {
      var prefab = await _assetService.LoadFromAddressable<GameObject>(_characterConfig.Prefab.Name);
      CharacterComponent character = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<CharacterComponent>();
      _levelModel.SetCharacter(character);
      
      character.CharacterController.SetBaseSpeed(_characterConfig.Speed);
      
      return character;
    }
    
    async UniTask<UnitComponent> IGameFactory.CreateUnit(string unitName, Vector3 position, Transform parent)
    {
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(_unitsConfig.Data[unitName].PrefabName);
      UnitComponent unit = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<UnitComponent>();
      _levelModel.AddEnemy(unit);
      return unit;
    }


    async UniTask<LootComponent> IGameFactory.CreateLoot(LootType lootType, Vector3 position, Transform parent)
    {
      GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(_lootConfig.Data[lootType].PrefabName);
      LootComponent loot = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<LootComponent>();
      _levelModel.AddLoot(loot);
      return loot;
    }
    
    AbilityComponent IGameFactory.CreateAbility(string abilityName, ITarget target)
    {
      AbilityComponent abilityComponent = new AbilityComponent();
      abilityComponent.Setup(abilityName, target);
      abilityComponent.Create();

      return abilityComponent;
    }
  }
}