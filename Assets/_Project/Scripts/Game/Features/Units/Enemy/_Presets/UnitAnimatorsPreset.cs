using System.Collections.Generic;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData.Presets;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Units.Enemy._Presets
{
  [CreateAssetMenu(fileName = nameof(UnitAnimatorsPreset), order = -1, menuName = "Presets/" + nameof(UnitAnimatorsPreset))]
  public class UnitAnimatorsPreset : BasePreset
  {
    public Dictionary<WeaponType, RuntimeAnimatorController> Controllers;
  }
}