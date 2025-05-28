using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Infrastructure.Time;
using _Project.Scripts.Utils.Extensions;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public class HomingArmamentMoveSystem : SystemComponent<HomingArmamentComponent>
  {
    private ITimeService _time;

    [Inject]
    private void Construct(ITimeService time)
    {
      _time = time;
    }
    
    protected override void OnUpdate()
    {
      base.OnUpdate();
      
      Components.Foreach(Move);
    }
    
    
    private void Move(HomingArmamentComponent homingArmament) => 
      homingArmament.transform.position += Direction(homingArmament) * homingArmament.Armament.Speed * _time.DeltaTime;
    
    private Vector3 Direction(HomingArmamentComponent armament) => (armament.Unit.Position - armament.Armament.Position).normalized;
  }


}