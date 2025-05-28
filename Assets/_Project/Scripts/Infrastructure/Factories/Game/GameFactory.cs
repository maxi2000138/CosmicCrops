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
using _Project.Scripts.Game.Features.Loot._Configs.Data;
using _Project.Scripts.Game.Features.Loot.Components;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.Pool;
using _Project.Scripts.Infrastructure.Progress;
using _Project.Scripts.Infrastructure.Systems.Components;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factories.Game
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly LevelModel _levelModel;
    private readonly LevelConfig _levelConfig;
    private readonly CharacterConfig _characterConfig;
    private readonly IProgressService _progressService;
    private readonly LootConfig _lootConfig;
    private readonly EnemiesConfig _enemiesConfig;

    public GameFactory(IAssetProvider assetProvider, LevelModel levelModel, LevelConfig levelConfig, 
      CharacterConfig characterConfig, IProgressService progressService, LootConfig lootConfig, EnemiesConfig enemiesConfig)
    {
      _assetProvider = assetProvider;
      _levelModel = levelModel;
      _levelConfig = levelConfig;
      _characterConfig = characterConfig;
      _progressService = progressService;
      _lootConfig = lootConfig;
      _enemiesConfig = enemiesConfig;
    }
    
    async UniTask<ILevel> IGameFactory.CreateLevel()
    {
      int levelNumber = _progressService.LevelData.Data.CurrentValue;
      int index = levelNumber > _levelConfig.Data.Count ? levelNumber % _levelConfig.Data.Count : levelNumber ;
      var data = _levelConfig.Data[index];
      var prefab = await _assetProvider.LoadFromAddressable<GameObject>(data.Prefab.Name);
      LevelComponent level = UnityObjectFactory.Instantiate(prefab).GetComponent<LevelComponent>();
      _levelModel.SetLevel(level);
      return level;
    }
    
    async UniTask<CharacterComponent> IGameFactory.CreateCharacter(Vector3 position, Transform parent)
    {
      var prefab = await _assetProvider.LoadFromAddressable<GameObject>(_characterConfig.Prefab.Name);
      CharacterComponent character = UnityObjectFactory.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<CharacterComponent>();
      _levelModel.SetCharacter(character);
      
      character.CharacterController.SetBaseSpeed(_characterConfig.Speed);
      
      return character;
    }
    
    async UniTask<EnemyComponent> IGameFactory.CreateUnit(string unitName, Vector3 position, Transform parent)
    {
      GameObject prefab = await _assetProvider.LoadFromAddressable<GameObject>(_enemiesConfig.Data[unitName].PrefabName);
      EnemyComponent enemy = UnityObjectFactory.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<EnemyComponent>();
      _levelModel.AddEnemy(enemy);
      return enemy;
    }


    async UniTask<LootComponent> IGameFactory.CreateLoot(LootType lootType, Vector3 position, Transform parent)
    {
      GameObject prefab = await _assetProvider.LoadFromAddressable<GameObject>(_lootConfig.Data[lootType].PrefabName);
      LootComponent loot = UnityObjectFactory.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<LootComponent>();
      _levelModel.AddLoot(loot);
      return loot;
    }
    
    AbilityComponent IGameFactory.CreateAbility(string abilityName, IUnit unit)
    {
      AbilityComponent abilityComponent = new AbilityComponent();
      abilityComponent.Setup(abilityName, unit);
      abilityComponent.Create();

      return abilityComponent;
    }
  }
}