using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Data;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Factories
{
  public interface IWeaponFactory
  {
    UniTask<WeaponComponent> CreateCharacterWeapon(WeaponComponent weapon, WeaponType type, Transform parent);
    UniTask<IProjectile> CreateProjectile(ProjectileType type, Transform spawnPoint, int damage, Vector3 direction);
  }
}