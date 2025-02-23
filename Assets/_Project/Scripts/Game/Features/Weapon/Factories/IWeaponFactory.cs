using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Factories
{
  public interface IWeaponFactory
  {
    UniTask<WeaponComponent> CreateCharacterWeapon(WeaponComponent weapon, WeaponType type, Transform parent);
  }
}