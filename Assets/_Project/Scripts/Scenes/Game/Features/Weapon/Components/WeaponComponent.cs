using System;
using _Project.Scripts.Game.Features.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.Animation;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Utils.Constants;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Components
{
  public class WeaponComponent : MonoComponent<WeaponComponent>, IAnimationStateReader
  {
    [SerializeField] private Transform _spawnPoint;

    public Transform SpawnPoint => _spawnPoint;
    public IWeapon Weapon { get; private set; }
    
    public event Action OnHit;

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