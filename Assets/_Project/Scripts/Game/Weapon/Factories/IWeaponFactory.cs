using _Project.Scripts.Game.Weapon.Componets;
using _Project.Scripts.Game.Weapon.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
namespace _Project.Scripts.Game.Weapon.Factories
{
  public interface IWeaponFactory
  {
    UniTask<WeaponComponent> CreateCharacterWeapon(WeaponComponent weapon, WeaponType type, Transform parent);
  }
}