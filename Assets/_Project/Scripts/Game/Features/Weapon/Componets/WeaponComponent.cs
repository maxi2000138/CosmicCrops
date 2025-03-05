using System;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.Animation;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Utils;
using _Project.Scripts.Utils.Constants;

namespace _Project.Scripts.Game.Features.Weapon.Componets
{
  public class WeaponComponent : MonoComponent<WeaponComponent>, IAnimationStateReader
  {
    public event Action OnHit;
    public IWeapon Weapon { get; private set; }

    public void SetWeapon(IWeapon weapon)
    {
      Weapon?.Dispose();
      Weapon = weapon;
    }

    public override void OnComponentDisable()
    {
      base.OnComponentDisable();
      
      Weapon?.Dispose();
    }
    public void EnteredState(int stateHash) { }
    public void ExitedState(int stateHash) { }
    public void UpdateState(int stateHash)
    {
      if (stateHash == Animations.Attack)
      {
        OnHit?.Invoke();
      }
    }
  }
}