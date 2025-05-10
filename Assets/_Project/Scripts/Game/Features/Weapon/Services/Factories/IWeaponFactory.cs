using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Factories
{
  public interface IWeaponFactory
  {
    UniTask<WeaponComponent> CreateWeaponComponent(string armament, Transform parent);
    UniTask<IProjectile> CreateBullet(string armament, Transform spawnPoint, string ability, float speed, Vector3 direction);
    UniTask<IProjectile> CreateThrowable(string armament, Transform spawnPoint, string ability, float speed, ITarget target);
  }
}