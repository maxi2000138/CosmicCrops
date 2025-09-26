using System;
using _Project.Scripts.Game._Editor;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Infrastructure.Systems.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Menu.Features.CharacterPreview.Components
{
  public class CharacterPreviewModelComponent : MonoComponent<CharacterPreviewModelComponent>
  {
    [SerializeField] private WeaponData[] _weapons;
    [SerializeField] private SkinData[] _skins;
    [SerializeField] private Material[] _equipmentMaterials;
    [SerializeField] private Material[] _weaponMaterials;

    public SkinData[] Skins => _skins;
    public WeaponData[] Weapons => _weapons;
    public Material[] EquipmentMaterials => _equipmentMaterials;
    public Material[] WeaponMaterials => _weaponMaterials;
  }

  [Serializable]
  public struct WeaponData
  {
    [ValueDropdown(ConfigEnum.Weapons)]
    public int WeaponId;
    public WeaponComponent Weapon;
  }

  [Serializable]
  public struct SkinData
  {
    [ValueDropdown(ConfigEnum.Skins)]
    public int SkinId;
    public GameObject Visual;
  }
}