using _Project.Scripts.Game.Features.Weapon.Systems;
using _Project.Scripts.Infrastructure.Systems;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon
{
  public class WeaponFeature : Feature
  {
    public WeaponFeature(IObjectResolver objectResolver) : base(objectResolver)
    {
      Add(new DestroyArmamentsByLifetimeSystem());
      Add(new DestroyHomingArmamentWithoutTargetSystem());
      Add(new RemoveArmamentOnDestroySystem());
     
      Add(new ExecuteWeaponSystem());
      Add(new ArmamentCollisionSystem());
      
      Add(new DirectionArmamentMoveSystem());
      Add(new HomingArmamentMoveSystem());
      Add(new ThrowableArmamentMoveSystem());
    }
  }
}