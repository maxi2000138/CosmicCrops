using _Project.Scripts.Game.Features.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;
using R3;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Componets
{
  public class BulletComponent : MonoComponent<BulletComponent>, IProjectile
  {
    public Vector3 Position => transform.position;
    public Vector3 Direction { get; private set; }
    public float CollisionDistance { get; private set; }
    public string Ability { get; private set; }
    public float LifeTime { get; set; } = int.MaxValue;
    public int CollisionMask { get; set; }
        
    public void SetCollisionMask(int mask) => CollisionMask = mask;
    public void SetDirection(Vector3 direction) => Direction = direction;
    public void SetCollisionDistance(float collisionDistance) => CollisionDistance = collisionDistance;
    public void SetAbility(string ability) => Ability = ability;
        
    public ReactiveCommand<Unit> OnDestroy { get; } = new ReactiveCommand();
  }
}