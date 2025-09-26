using _Project.Scripts.Game.Features.Weapon.Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public sealed class ExecuteWeaponSystem : SystemComponent<WeaponComponent>
  {
    protected override void OnUpdate()
    {
      base.OnUpdate();
      
      Components.Foreach(Execute);
    }

    private void Execute(WeaponComponent component) => component.Weapon.Execute();
  }
}