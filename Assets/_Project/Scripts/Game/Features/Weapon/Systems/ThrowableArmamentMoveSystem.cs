using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public class ThrowableArmamentMoveSystem : SystemComponent<ThrowableArmamentComponent>
  {
    protected override void OnUpdate()
    {
      base.OnUpdate();
      
      Components.Foreach(Move);
    }
    
    private void Move(ThrowableArmamentComponent throwableArmament) => 
      throwableArmament.transform.position = throwableArmament.transform.position.SetY(throwableArmament.InitialHeight + Height(throwableArmament));
    
    private float Height(ThrowableArmamentComponent armament)
    {
      float percentDistance = CurrentDistance(armament) / armament.InitialDistance;
      return armament.Trajectory.Evaluate(percentDistance) * armament.ThrowHeight;
    }
    
    private float CurrentDistance(ThrowableArmamentComponent armament) =>
      armament.transform.position.HorizontalProjectedSqrDistance(armament.Armament.HomingComponent.Unit.Position);
  }
}