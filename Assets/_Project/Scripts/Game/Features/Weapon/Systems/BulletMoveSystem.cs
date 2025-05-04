using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Infrastructure.Time;
using _Project.Scripts.Utils.Extensions;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public class BulletMoveSystem : SystemComponent<BulletComponent>
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
    
    
    private void Move(BulletComponent bullet) => bullet.transform.position += bullet.Direction * _time.DeltaTime;
  }
}