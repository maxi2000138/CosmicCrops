using _Project.Scripts.Game.Features.Weapon.Interfaces;
using _Project.Scripts.Infrastructure.Systems.Components;
using R3;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Weapon.Componets
{
  public class ArmamentComponent : MonoComponent<ArmamentComponent>, IProjectile
  {
    [field: SerializeField] public DirectionArmamentComponent DirectionComponent { get; private set; }
    [field: SerializeField] public HomingArmamentComponent HomingComponent { get; private set; }
    [field: SerializeField] public ThrowableArmamentComponent ThrowableComponent { get; private set; }
    
    public Vector3 Position => transform.position;
    public float CollisionSqrDistance { get; private set; }
    public string Ability { get; private set; }
    public float LifeTime { get; set; } = int.MaxValue;
    public int CollisionMask { get; set; }
    public float Speed { get; private set; }

    public void SetSpeed(float speed) => Speed = speed;
    public void SetCollisionMask(int mask) => CollisionMask = mask;
    public void SetCollisionSqrDistance(float collisionDistance) => CollisionSqrDistance = collisionDistance;
    public void SetAbility(string ability) => Ability = ability;
        
    public ReactiveCommand<Unit> OnDestroy { get; } = new ReactiveCommand();
  }
}