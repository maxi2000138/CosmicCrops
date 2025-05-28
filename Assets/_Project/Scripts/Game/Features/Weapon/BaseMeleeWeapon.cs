using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Game.Features.Weapon.Componets;

namespace _Project.Scripts.Game.Features.Weapon
{
  public class BaseMeleeWeapon : BaseWeapon
  {
    private readonly IAbilityApplier _abilityApplier;
    
    private IUnit _unit;

    protected BaseMeleeWeapon(WeaponComponent weapon, WeaponCharacteristicData weaponCharacteristic, IAbilityApplier abilityApplier) 
      : base(weapon, weaponCharacteristic)
    {
        _abilityApplier = abilityApplier;
    }

    public override void Initialize()
    {
      base.Initialize();

      Weapon.OnHit += Hit;
    }

    public override void Dispose()
    {
      base.Dispose();
      
      Weapon.OnHit -= Hit;
    }

    public override void Attack(IUnit unit = null)
    {
      base.Attack(unit);
      
      _unit = unit;
    }
    
    private void Hit()
    {
      if (_unit != null)
      {
        _abilityApplier.Apply(WeaponCharacteristic.Ability, _unit);
      }
    }
  }

}