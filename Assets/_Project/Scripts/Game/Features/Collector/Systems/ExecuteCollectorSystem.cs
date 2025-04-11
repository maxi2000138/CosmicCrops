using _Project.Scripts.Game.Features.Collector.Components;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Features.Collector.Systems
{
  public class ExecuteCollectorSystem : SystemComponent<CollectorComponent>
  {
    protected override void OnUpdate()
    {
      base.OnUpdate();
      
      Components.Foreach(Execute);
    }
    
    private void Execute(CollectorComponent component) => component.Collector?.Execute();
  }
}