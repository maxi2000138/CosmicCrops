using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;
  
namespace _Project.Scripts.Game.Features.Weapon.Componets
{
  public class WeaponMediatorComponent : MonoComponent<WeaponMediatorComponent>
  {
    [SerializeField] private Transform _container;
    
    public Transform Container => _container;
    public WeaponComponent Weapon { get; private set; }

    public void SetWeapon(WeaponComponent weapon)
    {
      if (Weapon != null)
      {
        Weapon.Remove();
      }

      Weapon = weapon;
    }
  }
}