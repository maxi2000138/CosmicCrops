using System.Collections.Generic;
using _Project.Scripts.Game.Loot.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Infrastructure.StaticData.Data
{
  [CreateAssetMenu(fileName = nameof(LootConfig), menuName = "Config/" + nameof(LootConfig))]
  public class LootConfig : SerializedScriptableObject
  {
    public Dictionary<LootType, LootData> Loot;
  }

  public class LootData
  {
    public AssetReference PrefabReference;
  }
}