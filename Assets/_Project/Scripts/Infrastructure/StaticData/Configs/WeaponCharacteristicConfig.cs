using System.Collections.Generic;
using _Project.Scripts.Game.Weapon;
using _Project.Scripts.Game.Weapon.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.StaticData.Data
{
  [CreateAssetMenu(fileName = nameof(WeaponCharacteristicConfig), menuName = "Config/" + nameof(WeaponCharacteristicConfig))]
  public class WeaponCharacteristicConfig : SerializedScriptableObject
  {
    public Dictionary<WeaponType, WeaponCharacteristicData> Data;
  }
  
  public class WeaponCharacteristicData
  {
    [Space] public WeaponType WeaponType;
    public WeaponCharacteristic WeaponCharacteristic;
  }
}