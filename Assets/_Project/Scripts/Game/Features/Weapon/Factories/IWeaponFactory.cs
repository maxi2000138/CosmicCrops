using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Factories
{
  public interface IWeaponFactory
  {
    UniTask<WeaponComponent> CreateCharacterWeapon(WeaponType type, Transform parent);
    UniTask<IProjectile> CreateProjectile(ProjectileType type, Transform spawnPoint, string ability, Vector3 direction);
  }
}