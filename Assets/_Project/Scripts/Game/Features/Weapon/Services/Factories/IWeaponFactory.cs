using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Services.Factories
{
  public interface IWeaponFactory
  {
    UniTask<WeaponComponent> CreateWeaponComponent(int weaponId, Transform parent);
    UniTask<IProjectile> CreateBullet(string armament, Transform spawnPoint, string ability, float speed, Vector3 direction);
    UniTask<IProjectile> CreateThrowable(string armament, Transform spawnPoint, string ability, float speed, IUnit unit);
  }
}