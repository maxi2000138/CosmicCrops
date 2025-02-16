using _Project.Scripts.Game.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Weapon.Componets
{
  public class WeaponComponent : MonoComponent<WeaponComponent>
  {
    public IWeapon Weapon { get; private set; }

    public void SetWeapon(IWeapon weapon)
    {
      Weapon?.Dispose();
      Weapon = weapon;
    }

    protected override void OnComponentDisable()
    {
      base.OnComponentDisable();

      Weapon?.Dispose();
    }
  }
}