using _Project.Scripts.Game.Features.Level.Components;
using _Project.Scripts.Infrastructure.Systems;

namespace _Project.Scripts.Game.Features.Level.Systems
{
  public class BuildGroundNavMeshSystem : SystemComponent<GroundComponent>
  {
    protected override void OnEnableComponent(GroundComponent component)
    {
      base.OnEnableComponent(component);
      
      component.BuildNavMesh();
    }
  }
}