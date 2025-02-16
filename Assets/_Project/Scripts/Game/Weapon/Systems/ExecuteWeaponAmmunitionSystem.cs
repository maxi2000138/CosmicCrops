using _Project.Scripts.Game.Weapon.Componets;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Weapon.Systems
{
  public sealed class ExecuteWeaponAmmunitionSystem : SystemComponent<WeaponComponent>
  {
    protected override void OnUpdate()
    {
      base.OnUpdate();
      
      Components.Foreach(Execute);
    }

    private void Execute(WeaponComponent component) => component.Weapon.Execute();
  }
}