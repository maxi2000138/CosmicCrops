using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon.Componets;

namespace _Project.Scripts.Game.Features.Weapon
{
  public class BaseMeleeWeapon : BaseWeapon
  {
    private readonly IAbilityApplier _abilityApplier;
    
    private ITarget _target;

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

    public override void Attack(ITarget target = null)
    {
      base.Attack(target);
      
      _target = target;
    }
    
    private void Hit()
    {
      if (_target != null)
      {
        _abilityApplier.Apply(WeaponCharacteristic.Ability, _target);
      }
    }
  }
}